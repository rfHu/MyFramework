using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework {
    public class ExportUnityPackage {

#if UNITY_EDITOR
        [MenuItem("RHFramework/4.导出 UnityPackage")]
#endif
        private static void MenuClicked()
        {
            var assetPathName = "Assets/RHFramework";
            var fileName = "RHFramework_" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
            AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
        }
    }
}