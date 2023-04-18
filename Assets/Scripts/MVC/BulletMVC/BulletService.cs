using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using TankMVC;

namespace BulletMVC {
    public class BulletService : GenericMonoSingleton<BulletService>
    {
        [SerializeField] BulletView BulletPrefab;
        public BulletScriptableObjectList scriptableConfigs;
        [SerializeField] ParticleSystem BulletExplosionPS;
        public void SpawnBullet(Vector3 spawnPosition, Vector3 direction, TankType tankType) {
            BulletScriptableObject bulletConfig = GetBulletConfiguration(tankType);
            BulletModel bulletModel = new BulletModel(bulletConfig);
            BulletView bulletView = GameObject.Instantiate<BulletView>(BulletPrefab);
            BulletController bulletController = new BulletController(bulletModel, bulletView);
            bulletModel.SetBulletController(bulletController);
            bulletModel.SetParticleSystem(BulletExplosionPS);
            bulletView.SetBulletController(bulletController);
            StartCoroutine(bulletController.FireBullet(spawnPosition, direction, bulletModel.BULLET_DISTANCE));
        }

        public void DestroyBullet(BulletController bullet, bool isDistanceComplete) {
            Vector3 finalPos = bullet.GetBulletView().transform.position;
            Destroy(bullet.GetBulletView().gameObject);
            if (!isDistanceComplete)
                Instantiate(BulletExplosionPS, finalPos, Quaternion.identity).Play();
        }

        public BulletScriptableObject GetBulletConfiguration(TankType tankType) {
            return Array.Find(scriptableConfigs.bulletConfigs, config => config.TANK_TYPE == tankType);
        }
    }
}
