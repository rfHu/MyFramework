using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class UIXXXPanel : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/9.UIXXXPanel", false, 9)]
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var panel = new GameObject("UIXXXPanel").AddComponent<UIXXXPanel>();
            panel.gameObject.AddComponent<UIYYYPanel>();
        }
#endif
        
        ResLoader mResLoader = new ResLoader();
        
        void Start()
        {
            var coinClip = mResLoader.LoadSync<AudioClip>("resources://getcoin");
        }

        private void OtherFunc()
        {
            var otherClip = mResLoader.LoadSync<AudioClip>("resources://getcoin");
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
            var coinClip = mResLoader.LoadSync<AudioClip>("resources://getcoin");

            //var homeClip = mResLoader.LoadAsset<AudioClip>("home");

            //var bgClip = mResLoader.LoadAsset<AudioClip>("bg");
        }

        private void OtherFunc()
        {
            var otherClip = mResLoader.LoadSync<AudioClip>("resources://getcoin");
        }

        void OnDestroy()
        {
            mResLoader.ReleaseAll();
            mResLoader = null;
        }
    }
}