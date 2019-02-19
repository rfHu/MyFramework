using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class LevelManagerExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/15.LevelManager", false, 15)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject().AddComponent<LevelManagerExample>();
        }
#endif

        private void Start()
        {
            DontDestroyOnLoad(this);

            LevelManager.Init(new List<string> { "Home", "Level" });

            LevelManager.LoadCurrent();

            Delay(10f, LevelManager.LoadNext);
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}