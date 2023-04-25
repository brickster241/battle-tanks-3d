using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletMVC {
    public class BulletController
    {
        private BulletModel bulletModel;
        private BulletView bulletView;

        public BulletController(BulletModel _bulletModel, BulletView _bulletView) {
            bulletModel = _bulletModel;
            bulletView = _bulletView;
        }

        public IEnumerator FireBullet(Transform tankTransform, float distance) {
            Vector3 spawnPosition = tankTransform.position;
            spawnPosition.y = 1f;
            bulletView.transform.position = spawnPosition;
            bulletView.transform.rotation = tankTransform.rotation;
            bulletView.transform.forward = tankTransform.forward;
            
            Vector3 bulletForward = bulletView.transform.forward;
            Vector3 targetPosition = spawnPosition + bulletForward * distance;
            targetPosition.y = 1f;

            while (bulletView.gameObject.activeInHierarchy && Vector3.Distance(bulletView.transform.position, targetPosition) > 0.5f) {
                bulletView.transform.Translate(bulletForward * Time.deltaTime * bulletModel.BULLET_SPEED, Space.World);
                yield return new WaitForEndOfFrame();
            }
            // DESTROY BULLET
            if (bulletView.gameObject.activeInHierarchy)
                BulletService.Instance.DestroyBullet(this, true);
        }

        public BulletModel GetBulletModel() {
            return bulletModel;
        }

        public BulletView GetBulletView() {
            return bulletView;
        }

        public void HandleBulletCollision(Collision other) {
            BulletService.Instance.DestroyBullet(this, false);
        }
        
    }

}
