using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletMVC {
    public class BulletView : MonoBehaviour
    {
        private BulletController bulletController;
        
        public void SetBulletController(BulletController _bulletController) {
            bulletController = _bulletController;
        }

        private void OnCollisionEnter(Collision other) {
            bulletController.CollisionHandler(other);
        }
    }
}
