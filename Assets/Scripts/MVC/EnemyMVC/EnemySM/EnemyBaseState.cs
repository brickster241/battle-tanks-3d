using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyMVC {
    public class EnemyBaseState
    {
        protected EnemyStateMachine enemySM;

        public EnemyBaseState(EnemyStateMachine _enemySM) {
            enemySM = _enemySM;
        }

        public virtual void OnStateEnter() {}
        public virtual void OnStateUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE) {}
        public virtual void OnStateExit() {} 
    }

}
