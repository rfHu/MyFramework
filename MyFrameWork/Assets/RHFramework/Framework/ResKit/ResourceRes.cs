using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class ResourceRes : Res  
    {
        private string mAssetPath;

        public ResourceRes(string assetPath)
        {
            mAssetPath = assetPath.Substring("resources://".Length);//确认开头截掉内容的情况下这是一种好写法

            Name = assetPath;
        }

        public override bool LoadSync()
        {
            return Asset = Resources.Load(mAssetPath);
        }

        public override void LoadAsync(Action<Res> OnLoaded)
        {
            var resRequest = Resources.LoadAsync(mAssetPath);

            resRequest.completed += operation =>
            {
                Asset = resRequest.asset;
                OnLoaded(this);
            };
        }

        protected override void OnReleaseRes()
        {
            if (Asset is GameObject)
            {
                Asset = null;
                Resources.UnloadUnusedAssets();
            }
            else
            {
                Resources.UnloadAsset(Asset);
            }

            ResMgr.Instance.SharedLoadedReses.Remove(this);

            Asset = null;
        }
    }
}