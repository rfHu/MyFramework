using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{

    public class GenerateUnityPackageName
    {

#if UNITY_EDITOR
        [MenuItem("RHFramework/1.自动生成package名字")]
#endif

        private static void MenuClicked()
        {
            Debug.Log("RHFramework_" + DateTime.Now.ToString("yyyyMMdd_HH"));
        }
    }
}