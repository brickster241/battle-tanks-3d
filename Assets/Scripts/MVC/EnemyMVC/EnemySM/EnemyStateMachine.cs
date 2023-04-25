using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyMVC {
    public enum EnemyState {
        IDLE, // WHEN GAME IS PAUSED
        ATTACK,
        PATROL,
        CHASE,
        NONE
    }

    public class EnemyStateMachine
    {
        private EnemyController enemyController = null;
        private EnemyAttackState attackState;
        private EnemyChaseState chaseState;
        private EnemyPatrolState patrolState;
        public EnemyBaseState currentEnemyState = null;

        public EnemyStateMachine() {
            attackState = new EnemyAttackState(this);
            chaseState = new EnemyChaseState(this);
            patrolState = new EnemyPatrolState(this);
            // SwitchState(EnemyState.PATROL);
        }

        public void SetEnemyController(EnemyController _enemyController) {
            enemyController = _enemyController;
            SwitchState(EnemyState.PATROL);
        }

        public EnemyController GetEnemyController() {
            return enemyController;
        }

        public void SwitchState(EnemyState enemyState) {
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

        public void ESMUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE) {
            if (currentEnemyState != null)
                currentEnemyState.OnStateUpdate(distance, CHASE_RANGE, ATTACK_RANGE);
        }
    }

}
