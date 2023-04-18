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

        public TankController(TankModel _tankModel, TankView _tankView) {
            tankModel = _tankModel;
            tankView = _tankView;
            tankTransform = tankView.transform;
        }

        public void MoveTank() {
            if (Input.GetKey(KeyCode.UpArrow)) {
                tankModel.IS_REVERSE = false;
                tankTransform.Translate(tankTransform.forward.normalized * tankModel.TANK_SPEED * Time.deltaTime, Space.World);
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                tankModel.IS_REVERSE = true;
                tankTransform.Translate(-tankTransform.forward.normalized * tankModel.TANK_SPEED * Time.deltaTime, Space.World);
            } else {
                tankModel.IS_REVERSE = false;
                tankTransform.Translate(Vector3.zero, Space.World);
            }

            int factor = (tankModel.IS_REVERSE) ? -1 : 1;

            if (Input.GetKey(KeyCode.LeftArrow)) {
                tankTransform.Rotate(new Vector3(0, -factor * tankModel.ROTATION_SPEED * Time.deltaTime, 0), Space.World);
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                tankTransform.Rotate(new Vector3(0, factor * tankModel.ROTATION_SPEED * Time.deltaTime, 0), Space.World);
            } else {
                tankTransform.Rotate(Vector3.zero, Space.World);
            }
        }
        public TankModel GetTankModel() {
            return tankModel;
        }

        public TankView GetTankView() {
            return tankView;
        }
    }

}
