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

        public BulletModel(float _BULLET_DAMAGE, float _BULLET_SPEED, float _BULLET_DISTANCE) {
            BULLET_DAMAGE = _BULLET_DAMAGE;
            BULLET_SPEED = _BULLET_SPEED;
            BULLET_DISTANCE = _BULLET_DISTANCE;
        }

        public void SetParticleSystem(ParticleSystem _explosionPS) {
            explosionPS = _explosionPS;
        }

        public void SetBulletController(BulletController _bulletController) {
            bulletController = _bulletController;
        }

    }

}
