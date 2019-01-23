using System;
using System.Collections;
using UnityEngine;

namespace RHFramework
{
    public partial class MonoBehaviourSimplify
    {
        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private static IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);

            onFinished();
        }
    }

    public class DelayWithCoroutine : MonoBehaviourSimplify
    {
        private void Start()
        {
            Delay(5.0f, () =>
            {
                UnityEditor.EditorApplication.isPlaying = false;
            });
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/11.定时功能", false, 11)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var gameObj = new GameObject("GO");
            gameObj.AddComponent<DelayWithCoroutine>();
        }

        protected override void OnBeforeDestroy()
        {
        }
#endif
    }
}