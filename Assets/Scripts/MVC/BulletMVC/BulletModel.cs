using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletMVC {
    public class BulletModel
    {
        private BulletController bulletController;
        public float BULLET_DAMAGE {get ;}
        public float BULLET_SPEED {get ;}
        public float BULLET_DISTANCE {get ;}
        public ParticleSystem explosionPS {get; set; }

        public BulletModel(BulletScriptableObject bulletScriptableObject) {
            BULLET_DAMAGE = bulletScriptableObject.BULLET_DAMAGE;
            BULLET_SPEED = bulletScriptableObject.BULLET_SPEED;
            BULLET_DISTANCE = bulletScriptableObject.BULLET_DISTANCE;
        }

        public void SetParticleSystem(ParticleSystem _explosionPS) {
            explosionPS = _explosionPS;
        }

        public void SetBulletController(BulletController _bulletController) {
            bulletController = _bulletController;
        }

    }

}
