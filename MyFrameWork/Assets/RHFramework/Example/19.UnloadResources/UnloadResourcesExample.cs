using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class UnloadResourcesExample : MonoBehaviour
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/19.UnloadResourcesExample", false, 19)]
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject().AddComponent<UnloadResourcesExample>();
        }
#endif

        IEnumerator Start()
        {
            var coin = Resources.Load("getcoin");

            yield return new WaitForSeconds(5.0f);

            Resources.UnloadAsset(coin);
            Debug.Log("getcoin Unloaded");
        }
    }
}