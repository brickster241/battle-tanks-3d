using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using TankMVC;
using Scriptables;
using Events;
using ParticleEffects;

namespace BulletMVC {
    public class BulletService : GenericMonoSingleton<BulletService>
    {
        [SerializeField] BulletView BulletPrefab;
        GenericObjectPool<BulletView> bulletPool;
        [SerializeField] Transform poolParentTransform;
        public BulletScriptableObjectList scriptableConfigs;
        
        protected override void Awake() {
            base.Awake();
            bulletPool = new GenericObjectPool<BulletView>();
            bulletPool.GeneratePool(BulletPrefab.gameObject, 30, poolParentTransform);
        }

        public void SpawnBullet(Vector3 spawnPosition, Vector3 direction, TankType tankType) {
            BulletScriptableObject bulletConfig = GetBulletConfiguration(tankType);
            BulletView bulletView = bulletPool.GetItem();
            if (bulletView.GetBulletController() == null) {
                BulletModel bulletModel = new BulletModel(bulletConfig);
                BulletController bulletController = new BulletController(bulletModel, bulletView);
                SetBulletMVCAttributes(bulletController, bulletModel, bulletView, spawnPosition, direction);
            } else {
                bulletView.GetBulletController().GetBulletModel().SetModelConfig(bulletConfig);
                SetBulletMVCAttributes(bulletView.GetBulletController(), bulletView.GetBulletController().GetBulletModel(), bulletView, spawnPosition, direction);
            }
        }

        private void SetBulletMVCAttributes(BulletController bulletController, BulletModel bulletModel, BulletView bulletView, Vector3 spawnPosition, Vector3 direction) {
            bulletView.gameObject.SetActive(true);
            bulletModel.SetBulletController(bulletController);
            bulletView.SetBulletController(bulletController);
            StartCoroutine(bulletController.FireBullet(spawnPosition, direction, bulletModel.BULLET_DISTANCE));
        }

        public void DestroyBullet(BulletController bullet, bool isDistanceComplete) {
            Vector3 finalPos = bullet.GetBulletView().transform.position;
            bullet.GetBulletView().gameObject.SetActive(false);
            bulletPool.ReturnItem(bullet.GetBulletView());
            if (!isDistanceComplete)
                EventService.Instance.InvokeParticleSystemEvent(ParticleEffectType.BULLET_EXPLOSION, finalPos);
        }

        private BulletScriptableObject GetBulletConfiguration(TankType tankType) {
            return Array.Find(scriptableConfigs.bulletConfigs, config => config.TANK_TYPE == tankType);
        }

        public int GetBulletDamage(Collision other) {
            BulletController bulletController = other.gameObject.GetComponent<BulletView>().GetBulletController();
            return bulletController.GetBulletModel().BULLET_DAMAGE;            
        }
    }
}
