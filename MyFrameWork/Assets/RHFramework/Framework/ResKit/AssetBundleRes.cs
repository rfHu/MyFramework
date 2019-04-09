using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class AssetBundleRes : Res
    {
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

        public override bool LoadSync()
        {
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
            }

            ResMgr.Instance.SharedLoadedReses.Remove(this);
        }
    }
}