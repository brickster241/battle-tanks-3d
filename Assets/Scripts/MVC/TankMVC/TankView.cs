using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthServices;

namespace TankMVC {
    // KEEPS TRACK OF UI
    public class TankView : MonoBehaviour
    {
        private TankController tankController;
        [SerializeField] HealthBar healthBar;
        [SerializeField] MeshRenderer[] COLOR_MATERIALS;

        public void SetTankController(TankController _tankController) {
            tankController = _tankController;
        }

        public HealthBar GetHealthBar() {
            return healthBar;
        }

        public TankController GetTankController() {
            return tankController;
        }

        public MeshRenderer[] GetMaterialMeshes() {
            return COLOR_MATERIALS;
        }

        private void Update() {
            float horizontal = Input.GetAxisRaw("Horizontal1");
            float vertical = Input.GetAxisRaw("Vertical1");
            // Debug.Log(horizontal + " " + vertical);
            tankController.MoveTank(horizontal, vertical);
            if (Input.GetKeyDown(KeyCode.Space))
                tankController.FireBullet();
        }

        private void OnCollisionEnter(Collision other) {
            tankController.HandleTankCollision(other);
        }

        public Transform GetTankTransform() {
            return this.transform;
        }
    }
}
