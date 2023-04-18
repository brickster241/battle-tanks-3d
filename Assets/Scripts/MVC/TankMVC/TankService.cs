using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using BulletMVC;
using Scriptables;

namespace TankMVC {
    public class TankService : GenericMonoSingleton<TankService>
    {
        [SerializeField] TankView tankPrefab;
        public TankScriptableObjectList scriptableConfigs;
        
        private void Start() {
            CreatePlayerTank();
        }

        public void CreatePlayerTank() {
            // GENERATE RANDOM PLAYER.
            int randomIndex = Random.Range(0, scriptableConfigs.tankConfigs.Length);
            TankModel tankModel = new TankModel(scriptableConfigs.tankConfigs[randomIndex]);
            TankView tankView = GameObject.Instantiate<TankView>(tankPrefab);
            TankController tankController = new TankController(tankModel, tankView);
            tankController.SetTankColor(tankModel.TANK_COLOR);
            tankModel.SetTankController(tankController);
            tankView.SetTankController(tankController);

        }

        public void FireBullet(Vector3 spawnPosition, Vector3 spawnDirection, TankType tankType) {
            BulletService.Instance.SpawnBullet(spawnPosition, spawnDirection, tankType);
        }
    }
}