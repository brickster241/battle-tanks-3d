using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using Events;

namespace ParticleEffects {

    public enum ParticleEffectType {
        BULLET_EXPLOSION,
        TANK_EXPLOSION
    }

    public class ParticleEffectService : GenericMonoSingleton<ParticleEffectService>
    {
        [SerializeField] ParticleSystem TankExplosionPrefab;
        [SerializeField] Transform bulletPETransform;
        [SerializeField] Transform tankPETransform;
        [SerializeField] ParticleSystem BulletExplosionPrefab;

        GenericObjectPool<ParticleSystem> tankExplosionPEPool;
        GenericObjectPool<ParticleSystem> bulletExplosionPEPool;
        private EventService eventService;

        protected override void Awake() {
            base.Awake();
            eventService = new EventService();
            tankExplosionPEPool = new GenericObjectPool<ParticleSystem>();
            bulletExplosionPEPool = new GenericObjectPool<ParticleSystem>();
            tankExplosionPEPool.GeneratePool(TankExplosionPrefab.gameObject, 30, tankPETransform);
            bulletExplosionPEPool.GeneratePool(BulletExplosionPrefab.gameObject, 50, bulletPETransform);
        }

        private void OnEnable() {
            EventService.Instance.onGameObjectDestroyed += DisplayParticleEffect;
        }

        public void DisplayParticleEffect(ParticleEffectType particleEffectType, Vector3 position) {
            if (particleEffectType == ParticleEffectType.BULLET_EXPLOSION) {
                ParticleSystem bulletPE = bulletExplosionPEPool.GetItem();
                bulletPE.gameObject.transform.position = position;
                bulletPE.gameObject.SetActive(true);
                bulletPE.Play();
                bulletExplosionPEPool.ReturnItem(bulletPE);

            } else if (particleEffectType == ParticleEffectType.TANK_EXPLOSION) {
                ParticleSystem tankPE = tankExplosionPEPool.GetItem();
                tankPE.gameObject.transform.position = position;
                tankPE.gameObject.SetActive(true);
                tankPE.Play();
                tankExplosionPEPool.ReturnItem(tankPE);
            }
        }

        private void OnDisable() {
            EventService.Instance.onGameObjectDestroyed -= DisplayParticleEffect;
        }
    }

}

