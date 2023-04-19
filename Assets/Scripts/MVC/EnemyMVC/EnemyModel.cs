using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TankMVC;
using Scriptables;

namespace EnemyMVC {
    public class EnemyModel
    {
        private EnemyController enemyController;
        public float TANK_SPEED {get; }
        public int TANK_HEALTH {get; set;}
        public float ROTATION_SPEED {get; }
        public TankType TANK_TYPE {get; set;}
        public Material TANK_COLOR {get; set;}
        public Vector3 AGENT_TARGET {get; set;}
        public float ATTACK_RANGE {get; }
        public float CHASE_RANGE {get; }
        
        public EnemyModel(EnemyScriptableObject enemyScriptableObject) {
            TANK_SPEED = enemyScriptableObject.TANK_SPEED;
            ROTATION_SPEED = enemyScriptableObject.ROTATION_SPEED;
            TANK_HEALTH = enemyScriptableObject.TANK_HEALTH;
            TANK_TYPE = enemyScriptableObject.TANK_TYPE;
            TANK_COLOR = enemyScriptableObject.TANK_MATERIAL_COLOR;
            AGENT_TARGET = Vector3.zero;
            ATTACK_RANGE = enemyScriptableObject.ATTACK_RANGE;
            CHASE_RANGE = enemyScriptableObject.CHASE_RANGE;
        }

        public void SetEnemyController(EnemyController _enemyController) {
            enemyController = _enemyController;
        }

        public EnemyController GetEnemyController() {
            return enemyController;
        }
    }

}
