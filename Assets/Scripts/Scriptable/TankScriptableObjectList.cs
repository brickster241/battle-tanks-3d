using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables {
    [CreateAssetMenu(fileName = "DataList", menuName = "ScriptableObjects/AddTankScriptableObjectList")]
    public class TankScriptableObjectList : ScriptableObject
    {
        public TankScriptableObject[] tankConfigs;
    }

}
