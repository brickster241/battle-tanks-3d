using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private EnemyController enemyController;
    [SerializeField] MeshRenderer[] COLOR_MATERIALS;
    
    public void SetEnemyController(EnemyController _enemyController) {
        enemyController = _enemyController;
    }

    public MeshRenderer[] GetMaterialMeshes() {
        return COLOR_MATERIALS;
    }

    void Update() {
        enemyController.Move();
    }
}
