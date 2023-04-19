using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMVC {
    public class EnemyView : MonoBehaviour
    {
        private EnemyController enemyController;
        private NavMeshAgent navMeshAgent;
        [SerializeField] MeshRenderer[] COLOR_MATERIALS;
        
        public void SetEnemyController(EnemyController _enemyController) {
            enemyController = _enemyController;
        }

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();    
        }

        public MeshRenderer[] GetMaterialMeshes() {
            return COLOR_MATERIALS;
        }

        void Update() {
            enemyController.Move();
        }

        private void OnCollisionEnter(Collision other) {
            enemyController.CollisionHandler(other);
        }

        public NavMeshAgent GetNavMeshAgent() {
            return this.navMeshAgent;
        }

        public Transform GetEnemyTransform() {
            return this.transform;
        }

        public EnemyController GetEnemyController() {
            return enemyController;
        }
    }

}
