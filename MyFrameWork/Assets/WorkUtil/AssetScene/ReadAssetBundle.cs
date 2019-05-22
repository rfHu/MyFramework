using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RHFramework
{
    public class ReadAssetBundle : MonoBehaviour
    {
        public string StreamingAssets_BundlePath;
        public string SceneName;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(Load(Application.streamingAssetsPath + StreamingAssets_BundlePath, SceneName));
            }
        }
        
        public IEnumerator Load(string path, string sceneName)
        {
            var abRequest = AssetBundle.LoadFromFileAsync(path);

            yield return abRequest;

            var ab = abRequest.assetBundle;

            if (ab == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }
            else
            {
                string oldSceneName = SceneManager.GetActiveScene().name;

                AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                yield return ao;

                Scene newScene2 = SceneManager.GetSceneByName(sceneName);
                SceneManager.SetActiveScene(newScene2);
                SceneManager.UnloadSceneAsync(oldSceneName);

                ab.Unload(false);
            }
        }
    }
}