using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BulletMVC;

namespace EnemyMVC {
    public class EnemyController
    {
        private EnemyModel enemyModel;
        private EnemyView enemyView;
        // REFERENCES FROM ENEMY VIEW , MODEL & SERVICE
        private NavMeshAgent navAgent;
        private Transform enemyTransform;
        private Transform playerTransform;
        

        public EnemyController(EnemyModel _enemyModel, EnemyView _enemyView) {
            enemyModel = _enemyModel;
            enemyView = _enemyView;
        }

        public void SetEnemyControllerAttributes() {
            navAgent = enemyView.gameObject.GetComponent<NavMeshAgent>();
            enemyTransform = enemyView.GetEnemyTransform();
            SetNavAgentParameters();
            enemyModel.AGENT_TARGET = playerTransform.position;
        }

        private void SetNavAgentParameters() {
            NavMeshAgent agent = navAgent;
            agent.speed = enemyModel.TANK_SPEED;
            agent.angularSpeed = enemyModel.ROTATION_SPEED;
            navAgent = agent;
        }

        public void SetPlayerTransform(Transform _playerTransform) {
            playerTransform = _playerTransform;
        }

        public void Move() {
            Debug.Log(enemyModel.AGENT_TARGET);
            enemyModel.AGENT_TARGET = playerTransform.position;
            navAgent.SetDestination(enemyModel.AGENT_TARGET);
        }

        public void SetTankColor(Material TANK_COLOR) {
            MeshRenderer[] colorMaterials = enemyView.GetMaterialMeshes();
            for (int i = 0; i < colorMaterials.Length; i++) {
                Material[] materials = colorMaterials[i].materials;
                materials[0] = TANK_COLOR;
                colorMaterials[i].materials = materials;
            }
        }

        public EnemyModel GetEnemyModel() {
            return enemyModel;
        }

        public EnemyView GetEnemyView() {
            return enemyView;
        }

        public void CollisionHandler(Collision other) {

            if (other.gameObject.CompareTag("Bullet")) {
                int BULLET_DAMAGE = EnemyService.Instance.GetBulletDamage(other);
                enemyModel.TANK_HEALTH = Mathf.Max(0, enemyModel.TANK_HEALTH - BULLET_DAMAGE);
                if (enemyModel.TANK_HEALTH == 0)
                    EnemyService.Instance.DestroyTank(this);
            }
        }
    }

}
