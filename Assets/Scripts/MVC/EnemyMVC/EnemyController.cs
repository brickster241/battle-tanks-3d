using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController
{
    private EnemyModel enemyModel;
    private EnemyView enemyView;
    public NavMeshAgent navAgent;
    Transform playerTransform;

    public EnemyController(EnemyModel _enemyModel, EnemyView _enemyView) {
        enemyModel = _enemyModel;
        enemyView = _enemyView;
        navAgent = enemyView.gameObject.GetComponent<NavMeshAgent>();
        SetNavAgentParameters();
    }

    public void SetNavAgentParameters() {
        NavMeshAgent agent = navAgent;
        agent.speed = enemyModel.TANK_SPEED;
        agent.angularSpeed = enemyModel.ROTATION_SPEED;
        navAgent = agent;
    }

    public void SetPlayerTransform(Transform _playerTransform) {
        playerTransform = _playerTransform;
    }

    public void SetTankColor(Material TANK_COLOR) {
        MeshRenderer[] colorMaterials = enemyView.GetMaterialMeshes();
        for (int i = 0; i < colorMaterials.Length; i++) {
            Material[] materials = colorMaterials[i].materials;
            materials[0] = TANK_COLOR;
            colorMaterials[i].materials = materials;
        }
    }

    public void Move() {
        navAgent.SetDestination(playerTransform.position);
    }

    public EnemyModel GetEnemyModel() {
        return enemyModel;
    }

    public EnemyView GetEnemyView() {
        return enemyView;
    }
}
