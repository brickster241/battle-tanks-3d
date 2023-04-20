using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyMVC {
    public enum EnemyState {
        ATTACK,
        PATROL,
        CHASE,
        NONE
    }

    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyController enemyController = null;
        private EnemyAttackState attackState;
        private EnemyChaseState chaseState;
        private EnemyPatrolState patrolState;
        public EnemyBaseState currentEnemyState = null;

        private void Start() {
            attackState = new EnemyAttackState(this);
            chaseState = new EnemyChaseState(this);
            patrolState = new EnemyPatrolState(this);
        }

        public void SetEnemyController(EnemyController _enemyController) {
            enemyController = _enemyController;
        }

        public EnemyController GetEnemyController() {
            return enemyController;
        }

        private void SetState(EnemyState enemyState) {
            if (GetEnemyStateEnum(currentEnemyState) == enemyState)
                return;
            if (currentEnemyState != null)
                currentEnemyState.OnStateExit();
            currentEnemyState = GetEnemyBaseState(enemyState);
            currentEnemyState.OnStateEnter();
        }

        public EnemyState GetEnemyStateEnum(EnemyBaseState enemyBaseState) {
            if (enemyBaseState == attackState) {
                return EnemyState.ATTACK;
            } else if (enemyBaseState == chaseState) {
                return EnemyState.CHASE;
            } else if (enemyBaseState == patrolState) {
                return EnemyState.PATROL;
            } else {
                return EnemyState.NONE;
            }
        }

        public void SetEnemyState(float distance, float CHASE_RANGE, float ATTACK_RANGE) {
            if (distance > CHASE_RANGE) {
                SetState(EnemyState.PATROL);
            } else if (distance > ATTACK_RANGE) {
                SetState(EnemyState.CHASE);
            } else {
                SetState(EnemyState.ATTACK);
            }
        }   

        private EnemyBaseState GetEnemyBaseState(EnemyState enemyState) {
            if (enemyState == EnemyState.ATTACK) {
                return attackState;
            } else if (enemyState == EnemyState.CHASE) {
                return chaseState;
            } else if (enemyState == EnemyState.PATROL) {
                return patrolState;
            } else {
                return null;
            }
        }

        private void Update() {
            if (currentEnemyState != null)
                currentEnemyState.OnStateUpdate();
        }
    }

}
