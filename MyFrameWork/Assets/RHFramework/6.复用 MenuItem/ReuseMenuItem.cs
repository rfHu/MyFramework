using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{
    public class ReuseMenuItem
    {
#if UNITY_EDITOR
        [MenuItem("RHFramework/6.MenuItem复用")]
        private static void MenuClicked() {
            EditorApplication.ExecuteMenuItem("RHFramework/4.导出 UnityPackage");
            Application.OpenURL("file://" + Path.Combine(Application.dataPath, "../"));
        }
#endif
    }
}