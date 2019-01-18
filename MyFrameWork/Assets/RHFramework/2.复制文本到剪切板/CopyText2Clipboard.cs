using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{

    public class CopyText2Clipboard
    {
#if UNITY_EDITOR
        [MenuItem("RHFramework/2.复制文本到剪切板")]
#endif
        private static void MenuClicked()
        {
            GUIUtility.systemCopyBuffer = "复制文本";
        }

    }
}