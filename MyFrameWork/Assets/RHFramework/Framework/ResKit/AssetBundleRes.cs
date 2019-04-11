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
                    var mainBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/StreamingAssets");

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

        private string mAssetPath;

        public AssetBundleRes(string assetPath)
        {
            mAssetPath = assetPath;

            Name = assetPath;

            State = ResState.Waiting;
        }

        private ResLoader mResLoader = new ResLoader();

        public override bool LoadSync()
        {
            State = ResState.Loading;

            var dependencyBundleNames = Manifest.GetDirectDependencies(mAssetPath.Substring(Application.streamingAssetsPath.Length + 1));

            foreach (var dependencyBundleName in dependencyBundleNames)
            {
                mResLoader.LoadSync<AssetBundle>(Application.streamingAssetsPath + "/" + dependencyBundleName);
            }

            AssetBundle = AssetBundle.LoadFromFile(mAssetPath);

            State = ResState.Loaded;

            return AssetBundle;
        }

        public override void LoadAsync()
        {
            State = ResState.Loading;

            var resRequest = AssetBundle.LoadFromFileAsync(mAssetPath);

            resRequest.completed += operation =>
            {
                AssetBundle = resRequest.assetBundle;

                State = ResState.Loaded;
            };
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