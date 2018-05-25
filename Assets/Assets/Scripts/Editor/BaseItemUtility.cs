using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseItemUtility{
[MenuItem("Assets/Create/BaseStuff/Item")]
 static public void CreateItem()
    {
        ScriptableObjectUtility.CreateAsset<BaseItem>();


    }
}
