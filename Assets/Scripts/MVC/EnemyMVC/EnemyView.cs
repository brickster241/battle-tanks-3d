using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMVC {
    public class EnemyView : MonoBehaviour
    {
        private EnemyController enemyController = null;
        private NavMeshAgent navMeshAgent;
        [SerializeField] MeshRenderer[] COLOR_MATERIALS;
        
        public void SetEnemyController(EnemyController _enemyController) {
            enemyController = _enemyController;
        }

        private void Awake() {
            navMeshAgent = GetComponent<NavMeshAgent>();    
        }

        private void Update() {
            enemyController.UpdateEnemyState();
        }

        public MeshRenderer[] GetMaterialMeshes() {
            return COLOR_MATERIALS;
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
