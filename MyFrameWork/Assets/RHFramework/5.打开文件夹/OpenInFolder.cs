using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{
    public class OpenInFolder
    {
#if UNITY_EDITOR
        [MenuItem("RHFramework/5.打开文件夹")]
#endif
        private static void MenuClicked()
        {
            Application.OpenURL("file://" + Application.dataPath);
        }
    }
}