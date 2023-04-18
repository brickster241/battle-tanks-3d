using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankMVC;

namespace Scriptables {
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AddTankScriptableObject")]
    public class TankScriptableObject : ScriptableObject
    {
        public float TANK_SPEED;
        public float ROTATION_SPEED;
        public int TANK_HEALTH;
        public TankType TANK_TYPE;
        public Material TANK_MATERIAL_COLOR;
            
    }
}
