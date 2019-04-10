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
        }

        private ResLoader mResLoader = new ResLoader();

        public override bool LoadSync()
        {
            var dependencyBundleNames = Manifest.GetDirectDependencies(mAssetPath.Substring(Application.streamingAssetsPath.Length + 1));

            foreach (var dependencyBundleName in dependencyBundleNames)
            {
                mResLoader.LoadSync<AssetBundle>(Application.streamingAssetsPath + "/" + dependencyBundleName);
            }

            return AssetBundle = AssetBundle.LoadFromFile(mAssetPath);
        }

        public override void LoadAsync(System.Action<Res> OnLoaded)
        {
            var resRequest = AssetBundle.LoadFromFileAsync(mAssetPath);

            resRequest.completed += operation =>
            {
                AssetBundle = resRequest.assetBundle;
                OnLoaded(this);
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