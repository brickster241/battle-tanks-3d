using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletMVC {
    public class BulletView : MonoBehaviour
    {
        private BulletController bulletController = null;
        
        public void SetBulletController(BulletController _bulletController) {
            bulletController = _bulletController;
        }

        public BulletController GetBulletController() {
            return bulletController;
        }

        public Transform GetBulletTransform() {
            return this.transform;
        }

        private void OnCollisionEnter(Collision other) {
            bulletController.HandleBulletCollision(other);
        }
    }
}
