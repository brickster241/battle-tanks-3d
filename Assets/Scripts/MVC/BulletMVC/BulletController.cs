using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletMVC {
    public class BulletController
    {
        private BulletModel bulletModel;
        private BulletView bulletView;
        private Transform BulletTransform;

        public BulletController(BulletModel _bulletModel, BulletView _bulletView) {
            bulletModel = _bulletModel;
            bulletView = _bulletView;
            BulletTransform = bulletView.transform;
        }

        public IEnumerator FireBullet(Vector3 spawnPosition, Vector3 forwardDirection, float distance) {
            BulletTransform.position = new Vector3(spawnPosition.x, 1f, spawnPosition.z);
            BulletTransform.forward = forwardDirection;
            Vector3 targetPosition = spawnPosition + forwardDirection.normalized * distance;
            targetPosition.y = 1f;
            while (BulletTransform.position != targetPosition) {
                BulletTransform.position = Vector3.MoveTowards(BulletTransform.position, targetPosition, bulletModel.BULLET_SPEED * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            // DESTROY BULLET
            BulletService.Instance.DestroyBullet(this, true);
        }

        public BulletModel GetBulletModel() {
            return bulletModel;
        }

        public BulletView GetBulletView() {
            return bulletView;
        }

        public void CollisionHandler(Collision other) {
            BulletService.Instance.DestroyBullet(this, false);
        }
        
    }

}
