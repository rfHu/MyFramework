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
        
        [UnityEditor.MenuItem("RHFramework/Framework/Editor/导出UnityPackage %e", false, 1)]
        private static void MenuClicked()
        {
            EditorUtil.ExportPackage("Assets/RHFramework", GeneratePackageName() + ".unitypackage");
            EditorUtil.OpenInForder(Path.Combine(Application.dataPath, "../"));
        }
    }
}