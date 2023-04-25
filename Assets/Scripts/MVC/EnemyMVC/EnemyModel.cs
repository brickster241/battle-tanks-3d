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
        public float TANK_SPEED;
        public int TANK_HEALTH;
        public float ROTATION_SPEED;
        public TankType TANK_TYPE;
        public Material TANK_COLOR;
        public Vector3 AGENT_TARGET;
        public float ATTACK_RANGE;
        public float CHASE_RANGE;
    
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

        public void SetModelConfig(EnemyScriptableObject enemyScriptableObject) {
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
