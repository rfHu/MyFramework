using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 填入对应的StreamingAssets之后的路径
/// 运行程序
/// 也可以打包后保持路径不变看效果
/// </summary>

namespace RHFramework
{
    public class ReadAssetBundle : MonoBehaviour
    {
        public string StreamingAssets_BundlePath;
        public string SceneName;


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(LoadSceneFromAB(StreamingAssets_BundlePath));
        }
        
        /// <summary>
        /// 从AB包加载场景
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IEnumerator LoadSceneFromAB(string path)
        {
            var abRequest = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + path);

            yield return abRequest;

            var ab = abRequest.assetBundle;

            if (ab == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                //todo LoadingUI 显示加载失败，并撤销
                yield break;
            }
            else
            {
                //从AB包中获得场景文件名
                string[] scenePaths = ab.GetAllScenePaths();
                var sceneName = Path.GetFileNameWithoutExtension(scenePaths[0]);

                AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                yield return ao;

                ab.Unload(false);

                Debug.Log("finish");
            }
        }
    }
}