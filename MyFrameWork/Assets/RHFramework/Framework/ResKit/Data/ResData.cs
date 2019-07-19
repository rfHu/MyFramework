using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RHFramework {
    public class ResData : Singleton<ResData>
    {
        private ResData() { Load(); }

        private AssetBundleManifest mManifest;

        private void Load()
        {
            if (ResMgr.IsSimulationModeLogic)
            {
#if UNITY_EDITOR
                var assetBundleNames = UnityEditor.AssetDatabase.GetAllAssetBundleNames();

                foreach (var assetBundleName in assetBundleNames)
                {
                    var assetPaths = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);

                    var assetBundleData = new AssetBundleData()
                    {
                        Name = assetBundleName,
                        DenpendnecyBundleNames = UnityEditor.AssetDatabase.GetAssetBundleDependencies(assetBundleName, false)
                    };

                    foreach (var assetPath in assetPaths)
                    {
                        var assetData = new AssetData()
                        {
                            Name = assetPath.Split('/')
                            .Last()
                            .Split('.')
                            .First(),
                            OwnerBundleName = assetBundleName
                        };

                        assetBundleData.AssetDataList.Add(assetData);
                    }

                    AssetBundleDatas.Add(assetBundleData);
                }

                AssetBundleDatas.ForEach(abData =>
                {
                    Debug.LogFormat("----{0}----", abData.Name);
                    abData.AssetDataList.ForEach(assetData =>
                    {
                        Debug.LogFormat("AB:{0} AssetData:{1}", abData.Name, assetData.Name);
                    });

                    foreach (var dependencyBundleName in abData.DenpendnecyBundleNames)
                    {
                        Debug.LogFormat("AB:{0} Depend:{1}", abData.Name, dependencyBundleName);
                    }
                });
#endif
            }
            else
            {
                var mainBundle = AssetBundle.LoadFromFile(ResKitUtil.FullPathForAssetBundle(ResKitUtil.GetPlatformName()));

                mManifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
        }

        public List<AssetBundleData> AssetBundleDatas = new List<AssetBundleData>();

        public string[] GetDirectDependencies(string bundleName)
        {
            if (ResMgr.IsSimulationModeLogic)
            {
                return AssetBundleDatas
                    .Find(data => data.Name == bundleName)
                    .DenpendnecyBundleNames;
            }

            return mManifest.GetDirectDependencies(bundleName);
        }

    }
}