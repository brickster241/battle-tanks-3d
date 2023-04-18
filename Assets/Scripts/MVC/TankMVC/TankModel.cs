using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankMVC {
    public class TankModel
    {
        private TankController tankController;
        public float TANK_SPEED {get; }
        public float ROTATION_SPEED {get; }
        public int TANK_HEALTH {get; set;}

        public TankModel(float _TANK_SPEED, float _ROTATION_SPEED, int _TANK_HEALTH) {
            TANK_SPEED = _TANK_SPEED;
            ROTATION_SPEED = _ROTATION_SPEED;
            TANK_HEALTH = _TANK_HEALTH;
        }

        public void SetTankHealth(int health) {
            TANK_HEALTH = health;
        }

        public void SetTankController(TankController _tankController) {
            tankController = _tankController;
        }

        
    }
}