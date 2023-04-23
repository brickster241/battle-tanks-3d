using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMVC {
    public class EnemyChaseState : EnemyBaseState
    {
        
        public EnemyChaseState(EnemyStateMachine _enemySM) : base(_enemySM) {}

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            // Debug.Log("CHASE STATE ENTER");
        }

        public override void OnStateUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE)
        {
            base.OnStateUpdate(distance, CHASE_RANGE, ATTACK_RANGE);
            // Debug.Log("CHASE STATE UPDATE.");
            // CUSTOM IMPLEMENTATION
            if (distance > CHASE_RANGE) {
                enemySM.SwitchState(EnemyState.PATROL);
            } else if (distance > ATTACK_RANGE) {
                ChasePlayer();
            } else {
                enemySM.SwitchState(EnemyState.ATTACK);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            // Debug.Log("CHASE STATE EXIT.");
        }

        private void ChasePlayer() {
            EnemyController _ec = enemySM.GetEnemyController();
            NavMeshAgent navAgent = _ec.GetEnemyView().GetNavMeshAgent();
            Transform playerTransform = _ec.GetPlayerTransform();
            navAgent.SetDestination(playerTransform.position);
        }
    }

}
