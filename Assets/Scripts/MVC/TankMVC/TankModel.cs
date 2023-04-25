using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace TankMVC {
    public enum TankType {
        NONE = -1,
        SLOW_SPEED = 0,
        MED_SPEED = 1,
        FAST_SPEED = 2
    }

    public class TankModel
    {
        private TankController tankController;
        public float TANK_SPEED;
        public float ROTATION_SPEED;
        public int TANK_HEALTH;
        public int TANK_TOTAL_HEALTH;
        public TankType TANK_TYPE;
        public Material TANK_COLOR;

        public TankModel(TankScriptableObject tankScriptableObject) {
            TANK_SPEED = tankScriptableObject.TANK_SPEED;
            ROTATION_SPEED = tankScriptableObject.ROTATION_SPEED;
            TANK_TOTAL_HEALTH = tankScriptableObject.TANK_HEALTH;
            TANK_HEALTH = tankScriptableObject.TANK_HEALTH;
            TANK_TYPE = tankScriptableObject.TANK_TYPE;
            TANK_COLOR = tankScriptableObject.TANK_MATERIAL_COLOR;
        }

        public void SetTankHealth(int health) {
            TANK_HEALTH = health;
        }

        public void SetTankController(TankController _tankController) {
            tankController = _tankController;
        }

        public TankController GetTankController() {
            return tankController;
        }

        
    }
}