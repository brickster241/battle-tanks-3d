using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankMVC {
    public class TankService : MonoBehaviour
    {
        [SerializeField] TankView tankPrefab;
        
        private void Awake() {
            CreatePlayerTank();
        }

        public void CreatePlayerTank() {
            TankModel tankModel = new TankModel(10, 90);
            TankView tankView = GameObject.Instantiate<TankView>(tankPrefab);
            TankController tankController = new TankController(tankModel, tankView);
            tankModel.SetTankController(tankController);
            tankView.SetTankController(tankController);
        }
    }
}