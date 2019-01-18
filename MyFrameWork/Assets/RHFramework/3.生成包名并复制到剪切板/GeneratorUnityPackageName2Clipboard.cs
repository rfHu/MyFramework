#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
#endif

namespace RHFramework
{
    public class GeneratorUnityPackageName2Clipboard
    {
#if UNITY_EDITOR
        [MenuItem("RHFramework/3.生成文件名到剪切板")]
#endif
        private static void MenuClicked()
        {
            GUIUtility.systemCopyBuffer = "RHFramework_" + DateTime.Now.ToString("yyyyMMdd_HH");
        }
    }
}