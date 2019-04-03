using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RHFramework
{
    public class ReadAssetBundle : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Load());
        }

        IEnumerator Load()
        {
            var abRequest = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + @"/LifeCycleExample/lifecycleexample.res");

            yield return abRequest;

            var ab = abRequest.assetBundle;

            if (ab == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }
            else
            {
                AsyncOperation ao = SceneManager.LoadSceneAsync("LifeCycleExample", LoadSceneMode.Additive);
                yield return ao;
                Scene newScene2 = SceneManager.GetSceneByName("LifeCycleExample");
                SceneManager.SetActiveScene(newScene2);

                ab.Unload(false);
            }
        }
    }
}