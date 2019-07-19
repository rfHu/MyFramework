using System;
using System.Collections;
using UnityEngine;

namespace RHFramework
{
    public class DelayWithCoroutine : MonoBehaviourSimplify
    {
        private void Start()
        {
            Delay(3.0f, () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            });
        }

        protected override void OnBeforeDestroy()
        {
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/1.定时功能", false, 1)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var gameObj = new GameObject("GO");
            gameObj.AddComponent<DelayWithCoroutine>();
        }
#endif
    }
}