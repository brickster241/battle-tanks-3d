using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BulletMVC;

namespace EnemyMVC {
    public enum EnemyMovementType {
        PATROL,
        CHASE,
        ATTACK
    }

    public class EnemyController
    {
        private EnemyModel enemyModel;
        private EnemyView enemyView;
        // REFERENCES FROM ENEMY VIEW , MODEL & SERVICE
        private NavMeshAgent navAgent;
        private Transform enemyTransform;
        private Transform playerTransform;

        // COROUTINE VARIABLES FOR PATROLLING & ATTACKING
        bool isPatrolCoroutineRunning = false;
        bool isAttackCoroutineRunning = false;
        
        public EnemyController(EnemyModel _enemyModel, EnemyView _enemyView) {
            enemyModel = _enemyModel;
            enemyView = _enemyView;
        }

        public void SetEnemyControllerAttributes() {
            navAgent = enemyView.gameObject.GetComponent<NavMeshAgent>();
            enemyTransform = enemyView.GetEnemyTransform();
            SetNavAgentParameters();
            Vector3 enemyPos = enemyTransform.position;
            enemyPos = EnemyService.Instance.GetRandomPoint(enemyPos, 60f);
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

        public void SetMovement() {
            float distance = Vector3.Distance(enemyTransform.position, playerTransform.position);
            if (distance >= enemyModel.CHASE_RANGE) {
                enemyModel.MOVEMENT_TYPE = EnemyMovementType.PATROL;
            } else if (distance >= enemyModel.ATTACK_RANGE) {
                navAgent.SetDestination(playerTransform.position);
                enemyModel.MOVEMENT_TYPE = EnemyMovementType.CHASE;
            } else {
                navAgent.SetDestination(playerTransform.position);
                enemyModel.MOVEMENT_TYPE = EnemyMovementType.ATTACK;
            }

            // USE THESE BOOLEANS TO CALL COROUTINES.
            if (enemyModel.MOVEMENT_TYPE == EnemyMovementType.ATTACK && !isAttackCoroutineRunning)
                EnemyService.Instance.StartCoroutine(AttackPlayer());
            if (enemyModel.MOVEMENT_TYPE == EnemyMovementType.PATROL && !isPatrolCoroutineRunning)
                EnemyService.Instance.StartCoroutine(PatrolEnvironment());
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


        IEnumerator AttackPlayer() {
            isAttackCoroutineRunning = true;
            navAgent.SetDestination(playerTransform.position);
            while (playerTransform.gameObject.activeInHierarchy && enemyTransform.gameObject.activeInHierarchy && enemyModel.MOVEMENT_TYPE == EnemyMovementType.ATTACK) {
                EnemyService.Instance.FireBullet(enemyTransform.position, enemyTransform.forward, enemyModel.TANK_TYPE);
                yield return new WaitForSeconds(2f);
            }
            isAttackCoroutineRunning = false;
        }

        IEnumerator PatrolEnvironment() {
            isPatrolCoroutineRunning = true;
            while (playerTransform.gameObject.activeInHierarchy && enemyTransform.gameObject.activeInHierarchy && enemyModel.MOVEMENT_TYPE == EnemyMovementType.PATROL) {
                Vector3 NEXT_TARGET = EnemyService.Instance.GetRandomPoint(enemyTransform.position, 60f);
                navAgent.SetDestination(NEXT_TARGET);
                Debug.Log("NEXT TARGET : " + NEXT_TARGET);
                yield return new WaitForSeconds(10f);
            }
            isPatrolCoroutineRunning = false;
        }
    }

}
