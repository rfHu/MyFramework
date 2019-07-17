using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class HideExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/7.Hide 脚本", false, 7)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var gameObj = new GameObject("Hide");
            gameObj.AddComponent<Hide>();
        }
#endif
    }
}