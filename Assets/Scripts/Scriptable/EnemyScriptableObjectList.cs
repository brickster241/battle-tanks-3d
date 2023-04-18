using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables {

    [CreateAssetMenu(fileName = "DataList", menuName = "ScriptableObjects/AddEnemyScriptableObjectList")]
    public class EnemyScriptableObjectList : ScriptableObject
    {
        public EnemyScriptableObject[] enemyConfigs;
    }

}

