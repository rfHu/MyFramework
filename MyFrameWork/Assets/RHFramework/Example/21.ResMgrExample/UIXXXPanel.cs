using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class UIXXXPanel : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/21.UIXXXPanel", false, 21)]
#endif
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var panel = new GameObject("UIXXXPanel").AddComponent<UIXXXPanel>();
            panel.gameObject.AddComponent<UIYYYPanel>();
        }
        
        ResLoader mResLoader = new ResLoader();
        
        void Start()
        {
            var coinClip = mResLoader.LoadAsset<AudioClip>("getcoin");

            //var homeClip = mResLoader.LoadAsset<AudioClip>("home");

            //var bgClip = mResLoader.LoadAsset<AudioClip>("bg");
        }

        private void OtherFunc()
        {
            var otherClip = mResLoader.LoadAsset<AudioClip>("getcoin");
        }
        
        void OnDestroy()
        {
            mResLoader.ReleaseAll();
            mResLoader = null;
        }
    }

    public class UIYYYPanel : MonoBehaviour
    {
        ResLoader mResLoader = new ResLoader();

        void Start()
        {
            var coinClip = mResLoader.LoadAsset<AudioClip>("getcoin");

            //var homeClip = mResLoader.LoadAsset<AudioClip>("home");

            //var bgClip = mResLoader.LoadAsset<AudioClip>("bg");
        }

        private void OtherFunc()
        {
            var otherClip = mResLoader.LoadAsset<AudioClip>("getcoin");
        }

        void OnDestroy()
        {
            mResLoader.ReleaseAll();
            mResLoader = null;
        }
    }
}