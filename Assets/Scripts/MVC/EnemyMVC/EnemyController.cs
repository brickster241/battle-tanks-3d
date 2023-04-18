using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BulletMVC;

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

    public void CollisionHandler(Collision other) {
        if (other.gameObject.CompareTag("Bullet")) {
            BulletController bulletController = other.gameObject.GetComponent<BulletView>().GetBulletController();
            enemyModel.TANK_HEALTH = Mathf.Max(0, enemyModel.TANK_HEALTH - bulletController.GetBulletModel().BULLET_DAMAGE);
            Debug.Log("ENEMY HEALTH : " + enemyModel.TANK_HEALTH);
            if (enemyModel.TANK_HEALTH == 0)
                DestroyEnemy();
        }
    }

    public void DestroyEnemy() {
        GameObject.Instantiate(EnemyService.Instance.enemyExplosionPS, enemyView.transform.position, Quaternion.identity).Play();
        enemyView.gameObject.SetActive(false);
    }
}
