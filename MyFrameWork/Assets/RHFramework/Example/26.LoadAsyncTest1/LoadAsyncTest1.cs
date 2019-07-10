using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework {
    public class LoadAsyncTest1 : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/26.LoadAsyncTest1", false, 26)]
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("LoadAsyncTest1").AddComponent<LoadAsyncTest1>();
        }
#endif

        ResLoader mResLoader = new ResLoader();

        private void Start()
        {
            mResLoader.LoadAsync<AssetBundle>("/square", 
                squareBundle => { Debug.Log(squareBundle.name);  });

            mResLoader.LoadSync<AssetBundle>("/square");
        }
    }
}