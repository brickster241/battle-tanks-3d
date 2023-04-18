using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankMVC {
    public class TankModel
    {
        private TankController tankController;
        public float TANK_SPEED {get; }
        public float ROTATION_SPEED {get; }
        public bool IS_REVERSE {get; set;}

        public TankModel(float _TANK_SPEED, float _ROTATION_SPEED) {
            TANK_SPEED = _TANK_SPEED;
            ROTATION_SPEED = _ROTATION_SPEED;
            IS_REVERSE = false;
        }

        public void SetTankController(TankController _tankController) {
            tankController = _tankController;
        }

        
    }
}