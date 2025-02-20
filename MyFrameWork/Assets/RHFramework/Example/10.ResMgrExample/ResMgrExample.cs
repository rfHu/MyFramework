﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class ResMgrExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/10.ResMgrExample", false, 10)]
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

            mResLoader.LoadSync<Texture2D>("resources://pic1");

            yield return new WaitForSeconds(2);

            Debug.Log("Start load:" + Time.time);
            mResLoader.LoadAsync<AudioClip>("resources://getcoin", callback => 
            {
                Debug.Log(callback.name);

                Debug.Log("end load:" + Time.time);
            });

            yield return new WaitForSeconds(2);

            mResLoader.LoadSync<AudioClip>("resources://home");

            yield return new WaitForSeconds(2);

            mResLoader.LoadSync<AudioClip>("resources://Audio/getcoin");

            yield return new WaitForSeconds(3);

            var homePanelPrefab = mResLoader.LoadSync<GameObject>("resources://HomePanel");

            yield return new WaitForSeconds(3);

            mResLoader.ReleaseAll();

            Debug.Log(homePanelPrefab == null);
        }
    }
}