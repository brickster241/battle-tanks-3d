using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMVC {
    public class EnemyAttackState : EnemyBaseState
    {
        
        public EnemyAttackState(EnemyStateMachine _enemySM) : base(_enemySM) {}

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            // Debug.Log("ATTACK STATE ENTER.");
            enemySM.StartCoroutine(AttackPlayer());
        }

        public override void OnStateUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE)
        {
            base.OnStateUpdate(distance, CHASE_RANGE, ATTACK_RANGE);
            // Debug.Log("ATTACK STATE UPDATE.");
            if (distance > ATTACK_RANGE) {
                enemySM.SwitchState(EnemyState.CHASE);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            // Debug.Log("ATTACK STATE EXIT.");
        }

        IEnumerator AttackPlayer() {
            EnemyController _ec = enemySM.GetEnemyController();
            NavMeshAgent navAgent = _ec.GetEnemyView().GetNavMeshAgent();
            Transform playerTransform = _ec.GetPlayerTransform();
            Transform enemyTransform = _ec.GetEnemyView().transform;
            navAgent.SetDestination(playerTransform.position);
            while (playerTransform.gameObject.activeInHierarchy && enemyTransform.gameObject.activeInHierarchy && enemySM.GetEnemyStateEnum(enemySM.currentEnemyState) == EnemyState.ATTACK) {
                EnemyService.Instance.FireBullet(enemyTransform.position, enemyTransform.forward, _ec.GetEnemyModel().TANK_TYPE);
                yield return new WaitForSeconds(2f);
            }
        }
    }

}
