using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankMVC {
    // KEEPS TRACK OF UI
    public class TankView : MonoBehaviour
    {
        private TankController tankController;

        public void SetTankController(TankController _tankController) {
            tankController = _tankController;
        }

        private void Update() {
            tankController.MoveTank();
        }
    }
}
