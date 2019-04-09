using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class ResMgrExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/23.ResMgrExample", false, 23)]
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var panel = new GameObject("UIXXXPanel").AddComponent<ResMgrExample>();
        }
#endif

        ResLoader mResLoader = new ResLoader();

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(5);

            mResLoader.LoadSync<Texture2D>("pic1");

            yield return new WaitForSeconds(2);

            Debug.Log("Start load:" + Time.time);
            mResLoader.LoadAsync<AudioClip>("getcoin", callback => 
            {
                Debug.Log(callback.name);

                Debug.Log("end load:" + Time.time);
            });

            yield return new WaitForSeconds(2);

            mResLoader.LoadSync<AudioClip>("home");

            yield return new WaitForSeconds(2);

            mResLoader.LoadSync<AudioClip>("Audio/getcoin");

            yield return new WaitForSeconds(3);

            var homePanelPrefab = mResLoader.LoadSync<GameObject>("HomePanel");

            yield return new WaitForSeconds(3);

            mResLoader.ReleaseAll();

            Debug.Log(homePanelPrefab == null);
        }
    }
}