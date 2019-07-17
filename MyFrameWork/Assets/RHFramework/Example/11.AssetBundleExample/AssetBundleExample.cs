using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RHFramework {
    public class AssetBundleExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/11.AssetBundleExample/Build AssetBundle", false, 11)]
        static void MenuCilcked1()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            UnityEditor.BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, UnityEditor.BuildAssetBundleOptions.None, UnityEditor.BuildTarget.StandaloneWindows);
        }
#endif

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/24.AssetBundleExample/Run", false, 24)]
        static void MenuCilcked2()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("AssetBundleExample").AddComponent<AssetBundleExample>();
        }
#endif

        private ResLoader mResLoader = new ResLoader();

        private AssetBundle mBundle;

        private void Start()
        {
            mBundle = mResLoader.LoadSync<AssetBundle>("/testgo");
            var go = mBundle.LoadAsset<GameObject>("GameObject");

            Instantiate(go);
        }

        private void OnDestroy()
        {
            mBundle = null;
            mResLoader.ReleaseAll();
            mResLoader = null;
        }
    }
}