using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankMVC {
    // LOGIC
    public class TankController
    {
        private TankModel tankModel;
        private TankView tankView;
        private Transform tankTransform;
        private Rigidbody tankRB;

        public TankController(TankModel _tankModel, TankView _tankView) {
            tankModel = _tankModel;
            tankView = _tankView;
            tankTransform = tankView.transform;
            tankRB = tankView.gameObject.GetComponent<Rigidbody>();
        }

        public void MoveTank(float horizontal, float vertical) {
            SetTankVelocity(vertical);
            SetTankRotation(horizontal, vertical);
        }

        public void FireBullet() {
            TankService.Instance.FireBullet(tankTransform.position, tankTransform.forward);
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
    }

}
