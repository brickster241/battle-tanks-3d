using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using BulletMVC;
using Scriptables;
using Events;
using ParticleEffects;

namespace TankMVC {
    public class TankService : GenericMonoSingleton<TankService>
    {
        [SerializeField] TankView tankPrefab;
        public TankScriptableObjectList scriptableConfigs;
        
        protected override void Awake() {
            base.Awake();
            CreatePlayerTank();
        }

        public void CreatePlayerTank() {
            // GENERATE RANDOM PLAYER.
            int randomIndex = Random.Range(0, scriptableConfigs.tankConfigs.Length);
            TankModel tankModel = new TankModel(scriptableConfigs.tankConfigs[randomIndex]);
            TankView tankView = GameObject.Instantiate<TankView>(tankPrefab);
            TankController tankController = new TankController(tankModel, tankView);
            SetTankMVCAttributes(tankController, tankModel, tankView);
        }

        private void SetTankMVCAttributes(TankController tankController, TankModel tankModel, TankView tankView) {
            tankController.SetTankColor(tankModel.TANK_COLOR);
            tankModel.SetTankController(tankController);
            tankView.SetTankController(tankController);
        }

        public void FireBullet(Vector3 spawnPosition, Vector3 spawnDirection, TankType tankType) {
            EventService.Instance.InvokePlayerFiredEvent();
            BulletService.Instance.SpawnBullet(spawnPosition, spawnDirection, tankType);
        }

        public int GetBulletDamage(Collision other) {
            return BulletService.Instance.GetBulletDamage(other);
        }

        public void DestroyTank(TankController tankController) {
            tankController.GetTankView().gameObject.SetActive(false);
            EventService.Instance.InvokeParticleSystemEvent(ParticleEffectType.TANK_EXPLOSION, tankController.GetTankView().transform.position);
        }
    }
}