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

            State = ResState.Waiting;
        }

        public override bool LoadSync<T>()
        {
            State = ResState.Loading;

            Asset = Resources.Load<T>(mAssetPath);

            State = ResState.Loaded;

            return Asset;
        }

        public override void LoadAsync<T>()
        {
            State = ResState.Loading;
            
            var resRequest = Resources.LoadAsync<T>(mAssetPath);

            resRequest.completed += operation =>
            {
                Asset = resRequest.asset;

                State = ResState.Loaded;
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