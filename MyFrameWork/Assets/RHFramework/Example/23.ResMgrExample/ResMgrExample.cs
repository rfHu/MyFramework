using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class ResMgrExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/23.ResMgrExample", false, 23)]
#endif
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var panel = new GameObject("UIXXXPanel").AddComponent<ResMgrExample>();
        }

        ResLoader mResLoader = new ResLoader();

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(5);

            mResLoader.LoadAsset<Texture2D>("pic1");

            yield return new WaitForSeconds(2);

            mResLoader.LoadAsset<AudioClip>("getcoin");

            yield return new WaitForSeconds(2);

            mResLoader.LoadAsset<AudioClip>("home");

            yield return new WaitForSeconds(2);

            mResLoader.LoadAsset<AudioClip>("getcoin");

            yield return new WaitForSeconds(3);

            mResLoader.ReleaseAll();
        }
    }
}