using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class AssetBundleRes : Res
    {
        private static AssetBundleManifest mManifest;

        public static AssetBundleManifest Manifest
        {
            get
            {
                if (!mManifest)
                {
                    var mainBundle =
                        AssetBundle.LoadFromFile(ResKitUtil.FullPathForAssetBundle(ResKitUtil.GetPlatformName()));

                    mManifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                }

                return mManifest;
            }
        }


        public AssetBundle AssetBundle
        {
            get { return Asset as AssetBundle; }
            set { Asset = value; }
        }

        private string mPath;

        public AssetBundleRes(string assetName)
        {
            mPath = ResKitUtil.FullPathForAssetBundle(assetName);

            Name = assetName;

            State = ResState.Waiting;
        }

        private ResLoader mResLoader = new ResLoader();

        public override bool LoadSync()
        {
            State = ResState.Loading;

            //var dependencyBundleNames = Manifest.GetDirectDependencies(Name);

            var dependencyBundleNames = ResData.Instance.GetDirectDependencies(Name);

            foreach (var dependencyBundleName in dependencyBundleNames)
            {
                mResLoader.LoadSync<AssetBundle>(dependencyBundleName);
            }

            //AssetBundle = AssetBundle.LoadFromFile(mPath);

            State = ResState.Loaded;

            return AssetBundle;
        }

        private void LoadDependencyBundlesAsync(Action onAllLoaded)
        {
            var dependencyBundleNames = Manifest.GetDirectDependencies(Name);

            var loadedCount = 0;

            if (dependencyBundleNames.Length == 0)
            {
                onAllLoaded();
            }

            foreach (var dependencyBundleName in dependencyBundleNames)
            {
                mResLoader.LoadAsync<AssetBundle>(dependencyBundleName,
                    dependBundle =>
                    {
                        loadedCount++;

                        if (loadedCount == dependencyBundleNames.Length)
                        {
                            onAllLoaded();
                        }
                    });
            }
        }

        public override void LoadAsync()
        {
            State = ResState.Loading;

            LoadDependencyBundlesAsync(() =>
            {
                var resRequest = AssetBundle.LoadFromFileAsync(mPath);

                resRequest.completed += operation =>
                {
                    AssetBundle = resRequest.assetBundle;

                    State = ResState.Loaded;
                };
            });
        }

        protected override void OnReleaseRes()
        {
            if (AssetBundle != null)
            {
                AssetBundle.Unload(true);
                AssetBundle = null;

                mResLoader.ReleaseAll();
                mResLoader = null;
            }

            ResMgr.Instance.SharedLoadedReses.Remove(this);
        }
    }
}