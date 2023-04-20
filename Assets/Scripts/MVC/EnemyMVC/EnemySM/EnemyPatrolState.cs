using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMVC {
    public class EnemyPatrolState : EnemyBaseState
    {
        
        public EnemyPatrolState(EnemyStateMachine _enemySM) : base(_enemySM) {}

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("PATROL STATE ENTER");
            enemySM.StartCoroutine(PatrolEnvironment());
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            Debug.Log("PATROL STATE UPDATE.");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("PATROL STATE EXIT.");
        }

        IEnumerator PatrolEnvironment() {
            EnemyController _ec = enemySM.GetEnemyController();
            NavMeshAgent navAgent = _ec.GetEnemyView().GetNavMeshAgent();
            Transform playerTransform = _ec.GetPlayerTransform();
            Transform enemyTransform = _ec.GetEnemyView().transform;
            while (playerTransform.gameObject.activeInHierarchy && enemyTransform.gameObject.activeInHierarchy && enemySM.GetEnemyStateEnum(enemySM.currentEnemyState) == EnemyState.PATROL) {
                Vector3 NEXT_TARGET = EnemyService.Instance.GetRandomPoint(enemyTransform.position, 60f, playerTransform.position);
                navAgent.SetDestination(NEXT_TARGET);
                Debug.Log("NEXT TARGET : " + NEXT_TARGET);
                yield return new WaitForSeconds(10f);
            }
        }
    }

}
