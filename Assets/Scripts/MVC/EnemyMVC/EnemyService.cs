using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using TankMVC;
using BulletMVC;
using Scriptables;

public class EnemyService : GenericMonoSingleton<EnemyService>
{
    [SerializeField] EnemyView enemyPrefab;
    public EnemyScriptableObjectList scriptableConfigs;
    Transform playerTank;
    [SerializeField] int EnemyCount;

    private void Start() {
        playerTank = GameObject.FindGameObjectWithTag("Player").transform;
        CreateEnemies();
    }

    void CreateEnemies() {
        int randomIndex = Random.Range(0, scriptableConfigs.enemyConfigs.Length);
        EnemyModel enemyModel = new EnemyModel(scriptableConfigs.enemyConfigs[randomIndex]);
        EnemyView enemyView = GameObject.Instantiate<EnemyView>(enemyPrefab);
        EnemyController enemyController = new EnemyController(enemyModel, enemyView);
        enemyController.SetPlayerTransform(playerTank);
        enemyController.SetTankColor(enemyModel.TANK_COLOR);
        enemyModel.SetEnemyController(enemyController);
        enemyView.SetEnemyController(enemyController);
    }

    public void FireBullet(Vector3 spawnPosition, Vector3 spawnDirection, TankType tankType) {
        BulletService.Instance.SpawnBullet(spawnPosition, spawnDirection, tankType);
    }
}
