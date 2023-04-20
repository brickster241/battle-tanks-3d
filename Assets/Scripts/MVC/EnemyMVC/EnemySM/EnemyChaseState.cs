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
            Debug.Log("CHASE STATE ENTER");
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            Debug.Log("CHASE STATE UPDATE.");
            // CUSTOM IMPLEMENTATION
            ChasePlayer();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("CHASE STATE EXIT.");
        }

        private void ChasePlayer() {
            EnemyController _ec = enemySM.GetEnemyController();
            NavMeshAgent navAgent = _ec.GetEnemyView().GetNavMeshAgent();
            Transform playerTransform = _ec.GetPlayerTransform();
            navAgent.SetDestination(playerTransform.position);
        }
    }

}
