using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataList", menuName = "ScriptableObjects/AddBulletScriptableObjectList")]
public class BulletScriptableObjectList : ScriptableObject
{
    public BulletScriptableObject[] bulletConfigs;
}


