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
        private EnemyStateMachine enemySM;

        public EnemyController(EnemyModel _enemyModel, EnemyView _enemyView) {
            enemyModel = _enemyModel;
            enemyView = _enemyView;
        }

        public void SetEnemySM(EnemyStateMachine _enemySM) {
            enemySM = _enemySM;
        }

        public void SetEnemyControllerAttributes() {
            navAgent = enemyView.gameObject.GetComponent<NavMeshAgent>();
            enemyTransform = enemyView.GetEnemyTransform();
            SetNavAgentParameters();
            Vector3 enemyPos = enemyTransform.position;
            enemyPos = EnemyService.Instance.GetRandomPoint(enemyPos, 60f, playerTransform.position);
            enemyTransform.position = enemyPos;
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

        public void SetTankColor(Material TANK_COLOR) {
            MeshRenderer[] colorMaterials = enemyView.GetMaterialMeshes();
            for (int i = 0; i < colorMaterials.Length; i++) {
                Material[] materials = colorMaterials[i].materials;
                materials[0] = TANK_COLOR;
                colorMaterials[i].materials = materials;
            }
        }

        public (float, float, float) GetEnemySMUpdateParameters() {
            float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);
            float CHASE_RANGE = enemyModel.CHASE_RANGE;
            float ATTACK_RANGE = enemyModel.ATTACK_RANGE;
            return (distance, CHASE_RANGE, ATTACK_RANGE);
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

        public Transform GetPlayerTransform() {
            return playerTransform;
        }
    }

}
