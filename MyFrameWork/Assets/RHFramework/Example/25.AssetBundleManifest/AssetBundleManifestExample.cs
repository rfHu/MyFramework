using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RHFramework
{
    public class AssetBundleManifestExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/25.AssetBundleManifastExample", false, 25)]
        static void MenuCilcked()
        {
            var mainAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/StreamingAssets");

            var bundleManifest = mainAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            bundleManifest.GetAllDependencies("testgo")
                .ToList()
                .ForEach(dependency => { Debug.LogFormat("testgo dependency : {0}", dependency); });

            bundleManifest.GetAllAssetBundles()
                .ToList()
                .ForEach(assetBundle => { Debug.Log(assetBundle); });

            bundleManifest.GetDirectDependencies("testgo")
                .ToList()
                .ForEach(dependency => { Debug.LogFormat("testgo direct dependency : {0}", dependency); });

            mainAssetBundle.Unload(true);
        }
#endif
    }
}