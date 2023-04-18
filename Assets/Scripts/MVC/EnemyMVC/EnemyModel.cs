using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TankMVC;
using Scriptables;

public class EnemyModel
{
    private EnemyController enemyController;
    public float TANK_SPEED {get; }
    public int TANK_HEALTH {get; set;}
    public float ROTATION_SPEED {get; }
    public TankType TANK_TYPE {get; set;}
    public Material TANK_COLOR {get; set;}

    public EnemyModel(EnemyScriptableObject enemyScriptableObject) {
        TANK_SPEED = enemyScriptableObject.TANK_SPEED;
        ROTATION_SPEED = enemyScriptableObject.ROTATION_SPEED;
        TANK_HEALTH = enemyScriptableObject.TANK_HEALTH;
        TANK_TYPE = enemyScriptableObject.TANK_TYPE;
        TANK_COLOR = enemyScriptableObject.TANK_MATERIAL_COLOR;
    }

    public void SetEnemyController(EnemyController _enemyController) {
        enemyController = _enemyController;
    }

    public EnemyController GetEnemyController() {
        return enemyController;
    }
}
