using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using HealthServices;

namespace EnemyMVC {
    public class EnemyView : MonoBehaviour
    {
        private EnemyController enemyController = null;
        private NavMeshAgent navMeshAgent;
        [SerializeField] HealthBar healthBar;
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

        public HealthBar GetHealthBar() {
            return healthBar;
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
