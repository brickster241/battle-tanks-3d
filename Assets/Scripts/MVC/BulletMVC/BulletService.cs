using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using TankMVC;
using Scriptables;

namespace BulletMVC {
    public class BulletService : GenericMonoSingleton<BulletService>
    {
        [SerializeField] BulletView BulletPrefab;
        GenericObjectPool<BulletView> bulletPool;
        [SerializeField] Transform poolParentTransform;
        public BulletScriptableObjectList scriptableConfigs;
        [SerializeField] ParticleSystem BulletExplosionPS;

        private void Start() {
            bulletPool = new GenericObjectPool<BulletView>();
            bulletPool.GeneratePool(BulletPrefab.gameObject, 30, poolParentTransform);
        }

        public void SpawnBullet(Vector3 spawnPosition, Vector3 direction, TankType tankType) {
            BulletScriptableObject bulletConfig = GetBulletConfiguration(tankType);
            BulletModel bulletModel = new BulletModel(bulletConfig);
            BulletView bulletView = bulletPool.GetItem();
            BulletController bulletController = new BulletController(bulletModel, bulletView);
            SetBulletMVCAttributes(bulletController, bulletModel, bulletView, spawnPosition, direction);
        }

        private void SetBulletMVCAttributes(BulletController bulletController, BulletModel bulletModel, BulletView bulletView, Vector3 spawnPosition, Vector3 direction) {
            bulletView.gameObject.SetActive(true);
            bulletModel.SetBulletController(bulletController);
            bulletModel.SetParticleSystem(BulletExplosionPS);
            bulletView.SetBulletController(bulletController);
            StartCoroutine(bulletController.FireBullet(spawnPosition, direction, bulletModel.BULLET_DISTANCE));
        }

        public void DestroyBullet(BulletController bullet, bool isDistanceComplete) {
            Vector3 finalPos = bullet.GetBulletView().transform.position;
            if (!isDistanceComplete)
                Instantiate(BulletExplosionPS, finalPos, Quaternion.identity).Play();
            bullet.GetBulletView().gameObject.SetActive(false);
            bulletPool.ReturnItem(bullet.GetBulletView());
        }

        private BulletScriptableObject GetBulletConfiguration(TankType tankType) {
            return Array.Find(scriptableConfigs.bulletConfigs, config => config.TANK_TYPE == tankType);
        }

        public int GetBulletDamage(Collision other) {
            BulletController bulletController = other.gameObject.GetComponent<BulletView>().GetBulletController();
            // Debug.Log("BULLET DAMAGE : " + bulletController.GetBulletModel().BULLET_DAMAGE);
            return bulletController.GetBulletModel().BULLET_DAMAGE;            
        }
    }
}
