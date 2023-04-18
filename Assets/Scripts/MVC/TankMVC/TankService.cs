using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using BulletMVC;

namespace TankMVC {
    public class TankService : GenericMonoSingleton<TankService>
    {
        [SerializeField] TankView tankPrefab;
        
        private void Start() {
            CreatePlayerTank();
        }

        public void CreatePlayerTank() {
            TankModel tankModel = new TankModel(10, 90, 100);
            TankView tankView = GameObject.Instantiate<TankView>(tankPrefab);
            TankController tankController = new TankController(tankModel, tankView);
            tankModel.SetTankController(tankController);
            tankView.SetTankController(tankController);
        }

        public void FireBullet(Vector3 spawnPosition, Vector3 spawnDirection) {
            BulletService.Instance.SpawnBullet(spawnPosition, spawnDirection);
        }
    }
}