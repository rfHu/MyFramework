using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework {
    public class LoadAsyncTest3 : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/28.LoadAsyncTest3", false, 28)]
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("LoadAsyncTest3").AddComponent<LoadAsyncTest3>();
        }
#endif

        ResLoader mResLoader = new ResLoader();

        private void Start()
        {
            mResLoader.LoadAsync<Texture2D>("resources://BigTexture", bigTexture => { Debug.Log(bigTexture.name); });
            mResLoader.LoadAsync<Texture2D>("resources://BigTexture", bigTexture => { Debug.Log(bigTexture.name); });
        }
    }
}