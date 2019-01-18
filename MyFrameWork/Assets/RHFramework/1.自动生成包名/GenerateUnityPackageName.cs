using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenerateUnityPackageName {

    [MenuItem("RHFramework/1.自动生成package名字")]
    public static void MenuClicked()
    {
        Debug.Log("RHFramework_" + DateTime.Now.ToString("yyyyMMdd_HH"));
    }
}
