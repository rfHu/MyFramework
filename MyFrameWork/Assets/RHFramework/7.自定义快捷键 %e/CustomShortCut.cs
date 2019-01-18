using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{
    public class CustomShortCut
    {
#if UNITY_EDITOR
        [MenuItem("RHFramework/7.创建快捷键 %e")]
        private static void MenuClicked()
        {
            EditorApplication.ExecuteMenuItem("RHFramework/6.MenuItem复用");
        }
#endif
    }
}