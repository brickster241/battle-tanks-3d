using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Generics;
using TankMVC;
using BulletMVC;
using Scriptables;
using Events;
using ParticleEffects;

namespace EnemyMVC {
    public class EnemyService : GenericMonoSingleton<EnemyService>
    {
        [SerializeField] EnemyView enemyPrefab;
        [SerializeField] Transform enemyPoolParentTransform;
        GenericObjectPool<EnemyView> enemyPool;
        
        public EnemyScriptableObjectList scriptableConfigs;
        public ParticleSystem enemyExplosionPS;
        Transform playerTank;
        
        protected override void Awake() {
            base.Awake();
            playerTank = GameObject.FindGameObjectWithTag("Player").transform;
            enemyPool = new GenericObjectPool<EnemyView>();
            enemyPool.GeneratePool(enemyPrefab.gameObject, 10, enemyPoolParentTransform);
            // MAKE A COROUTINE WHICH WILL KEEP ON SPAWNING NEW ENEMIES
            StartCoroutine(SpawnEnemiesAtInterval());
        }

        IEnumerator SpawnEnemiesAtInterval() {
            for (int i = 0; i < 5; i++)
                SpawnEnemy();
            while (playerTank.gameObject.activeInHierarchy) {
                yield return new WaitForSeconds(15f);
                SpawnEnemy();
            }
        }

        private void SpawnEnemy() {
            // Debug.Log(scriptableConfigs.enemyConfigs);
            int randomIndex = UnityEngine.Random.Range(0, scriptableConfigs.enemyConfigs.Length);
            EnemyView enemyView = enemyPool.GetItem();
            if (enemyView.GetEnemyController() == null) {
                EnemyModel enemyModel = new EnemyModel(scriptableConfigs.enemyConfigs[randomIndex]);
                EnemyController enemyController = new EnemyController(enemyModel, enemyView);
                EnemyStateMachine enemySM = enemyController.GetEnemySM();
                SetEnemyMVCAttributes(enemyController, enemyModel, enemyView, enemySM);
            } else {
                enemyView.GetEnemyController().GetEnemyModel().SetModelConfig(scriptableConfigs.enemyConfigs[randomIndex]);
                SetEnemyMVCAttributes(enemyView.GetEnemyController(), enemyView.GetEnemyController().GetEnemyModel(), enemyView, enemyView.GetEnemyController().GetEnemySM());
            }
            
        }

        private void SetEnemyMVCAttributes(EnemyController enemyController, EnemyModel enemyModel, EnemyView enemyView, EnemyStateMachine enemySM) {
            enemyView.gameObject.SetActive(true);
            enemyController.SetPlayerTransform(playerTank);
            enemyController.SetTankColor(enemyModel.TANK_COLOR);
            enemyModel.SetEnemyController(enemyController);
            enemyView.SetEnemyController(enemyController);
            enemySM.SetEnemyController(enemyController);
            enemyController.SetEnemySM(enemySM);
            enemyController.SetEnemyControllerAttributes();
        }

        public void FireBullet(Vector3 spawnPosition, Vector3 spawnDirection, TankType tankType) {
            BulletService.Instance.SpawnBullet(spawnPosition, spawnDirection, tankType);
        }

        public int GetBulletDamage(Collision other) {
            return BulletService.Instance.GetBulletDamage(other);
        }

        public void DestroyTank(EnemyController enemyController) {
            enemyController.GetEnemyView().gameObject.SetActive(false);
            enemyPool.ReturnItem(enemyController.GetEnemyView());
            EventService.Instance.InvokeEnemyDeathEvent();
            EventService.Instance.InvokeParticleSystemEvent(ParticleEffectType.TANK_EXPLOSION, enemyController.GetEnemyView().transform.position);
        }

        public Vector3 GetRandomPoint(Vector3 center, float range, Vector3 playerPosition) {
            Vector3 result = Vector3.zero;
            while (result == Vector3.zero || Vector3.Distance(result, playerPosition) < 35f) {
                Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                }
            }
            return result;
        }
    }

}
