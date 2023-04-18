using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletMVC;

namespace TankMVC {
    // LOGIC
    public class TankController
    {
        private TankModel tankModel;
        private TankView tankView;
        private Transform tankTransform;
        
        public TankController(TankModel _tankModel, TankView _tankView) {
            tankModel = _tankModel;
            tankView = _tankView;
            tankTransform = tankView.transform;
        }

        public void SetTankColor(Material TANK_COLOR) {
            MeshRenderer[] colorMaterials = tankView.GetMaterialMeshes();
            for (int i = 0; i < colorMaterials.Length; i++) {
                Material[] materials = colorMaterials[i].materials;
                materials[0] = TANK_COLOR;
                colorMaterials[i].materials = materials;
            }
        }

        public void MoveTank(float horizontal, float vertical) {
            SetTankVelocity(vertical);
            SetTankRotation(horizontal, vertical);
        }

        public void FireBullet() {
            TankService.Instance.FireBullet(tankTransform.position, tankTransform.forward, tankModel.TANK_TYPE);
        }

        private void SetTankRotation(float horizontal, float vertical)
        {
            if (horizontal != 0 && vertical != 0) {
                tankTransform.Rotate(new Vector3(0, horizontal * vertical * tankModel.ROTATION_SPEED * Time.deltaTime, 0), Space.World);    
            } else if (vertical == 0) {
                tankTransform.Rotate(new Vector3(0, horizontal * tankModel.ROTATION_SPEED * Time.deltaTime, 0), Space.World);    
            }
        }

        private void SetTankVelocity(float vertical)
        {
            tankTransform.Translate(vertical * tankTransform.forward.normalized * tankModel.TANK_SPEED * Time.deltaTime, Space.World);
        }

        public TankModel GetTankModel() {
            return tankModel;
        }

        public TankView GetTankView() {
            return tankView;
        }

        public void CollisionHandler(Collision other) {
            if (other.gameObject.CompareTag("Bullet")) {
                BulletController bulletController = other.gameObject.GetComponent<BulletView>().GetBulletController();
                tankModel.TANK_HEALTH = Mathf.Max(0, tankModel.TANK_HEALTH - bulletController.GetBulletModel().BULLET_DAMAGE);
                if (tankModel.TANK_HEALTH == 0)
                    DestroyPlayerTank();
            }
        }


        public void DestroyPlayerTank() {
            GameObject.Instantiate(TankService.Instance.tankExplosionPS, tankView.transform.position, Quaternion.identity).Play();
            tankView.gameObject.SetActive(false);
        }
    }

}
