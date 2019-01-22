using System;
using System.IO;
using UnityEngine;

namespace RHFramework
{
    public class Exporter
    {
        private static string GeneratePackageName()
        {
            return "RHFramework_" + DateTime.Now.ToString("yyyyMMdd_HH");
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/1.导出UnityPackage %e", false, 1)]
        private static void MenuClicked()
        {
            EditorUtil.ExportPackage("Assets/RHFramework", GeneratePackageName() + ".unitypackage");
            EditorUtil.OpenInForder(Path.Combine(Application.dataPath, "../"));
        }
#endif
    }
}