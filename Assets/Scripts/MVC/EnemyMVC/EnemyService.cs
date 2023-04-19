using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using TankMVC;
using BulletMVC;
using Scriptables;

namespace EnemyMVC {
    public class EnemyService : GenericMonoSingleton<EnemyService>
    {
        [SerializeField] EnemyView enemyPrefab;
        public EnemyScriptableObjectList scriptableConfigs;
        public ParticleSystem enemyExplosionPS;
        Transform playerTank;
        [SerializeField] int EnemyCount;

        private void Start() {
            playerTank = GameObject.FindGameObjectWithTag("Player").transform;
            for (int i = 0; i < EnemyCount; i++)
                SpawnEnemy();
        }

        private void SpawnEnemy() {
            Debug.Log(scriptableConfigs.enemyConfigs);
            int randomIndex = Random.Range(0, scriptableConfigs.enemyConfigs.Length);
            EnemyModel enemyModel = new EnemyModel(scriptableConfigs.enemyConfigs[randomIndex]);
            EnemyView enemyView = GameObject.Instantiate<EnemyView>(enemyPrefab);
            EnemyController enemyController = new EnemyController(enemyModel, enemyView);
            SetEnemyMVCAttributes(enemyController, enemyModel, enemyView);
        }

        private void SetEnemyMVCAttributes(EnemyController enemyController, EnemyModel enemyModel, EnemyView enemyView) {
            enemyController.SetPlayerTransform(playerTank);
            enemyController.SetTankColor(enemyModel.TANK_COLOR);
            enemyModel.SetEnemyController(enemyController);
            enemyView.SetEnemyController(enemyController);
            enemyController.SetEnemyControllerAttributes();
        }

        public void FireBullet(Vector3 spawnPosition, Vector3 spawnDirection, TankType tankType) {
            BulletService.Instance.SpawnBullet(spawnPosition, spawnDirection, tankType);
        }

        public int GetBulletDamage(Collision other) {
            return BulletService.Instance.GetBulletDamage(other);
        }

        public void DestroyTank(EnemyController enemyController) {
            GameObject.Instantiate(enemyExplosionPS, enemyController.GetEnemyView().transform.position, Quaternion.identity).Play();
            enemyController.GetEnemyView().gameObject.SetActive(false);
        }
    }

}
