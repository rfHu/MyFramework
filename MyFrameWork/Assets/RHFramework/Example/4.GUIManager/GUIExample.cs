using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class GUIExample : MonoBehaviourSimplify
    {
        void Start()
        {
            GUIManager.SetResolution(1280, 720, 0);

            GUIManager.LoadPanel("HomePanel", UILayer.Common);

            Delay(3.0f, () => { GUIManager.UnloadPanel("HomePanel"); });
        }

        protected override void OnBeforeDestroy()
        {
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/4.GUIManager", false, 4)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("GUIExample").AddComponent<GUIExample>();
        }
#endif
    }
}