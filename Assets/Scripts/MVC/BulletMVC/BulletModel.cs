using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace BulletMVC {
    public class BulletModel
    {
        private BulletController bulletController;
        public int BULLET_DAMAGE;
        public float BULLET_SPEED;
        public float BULLET_DISTANCE;
        
        public BulletModel(BulletScriptableObject bulletScriptableObject) {
            BULLET_DAMAGE = bulletScriptableObject.BULLET_DAMAGE;
            BULLET_SPEED = bulletScriptableObject.BULLET_SPEED;
            BULLET_DISTANCE = bulletScriptableObject.BULLET_DISTANCE;
        }

        public void SetModelConfig(BulletScriptableObject bulletScriptableObject) {
            BULLET_DAMAGE = bulletScriptableObject.BULLET_DAMAGE;
            BULLET_SPEED = bulletScriptableObject.BULLET_SPEED;
            BULLET_DISTANCE = bulletScriptableObject.BULLET_DISTANCE;
        }

        public BulletController GetBulletController() {
            return bulletController;
        }

        public void SetBulletController(BulletController _bulletController) {
            bulletController = _bulletController;
        }

    }

}
