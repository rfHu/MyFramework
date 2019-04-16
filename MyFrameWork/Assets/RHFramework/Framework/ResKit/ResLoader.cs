using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class ResLoader
    {
        public T LoadSync<T>(string assetName) where T : Object
        {
            var res = GetOrCreateRes(assetName);

            if (res != null)
            {
                switch (res.State)
                {
                    case ResState.Loading:
                        throw new System.Exception(string.Format("请不要在异步加载 {0} 时，进行 {0} 同步加载", res.Name));
                    case ResState.Loaded:
                        return res.Asset as T;
                }
            }

            //真正加载资源
            res = CreateRes(assetName);

            res.LoadSync();
            
            return res.Asset as T;
        }

        public void LoadAsync<T>(string assetName, System.Action<T> onLoaded) where T : Object
        {
            //查询当前资源记录
            var res = GetOrCreateRes(assetName);

            System.Action<Res> onResLoaded = null;

            onResLoaded = loadedRes =>
            {
                onLoaded(loadedRes.Asset as T);

                res.UnregisterOnLoadedEvent(onResLoaded);
            };

            if (res != null)
            {
                if (res.State == ResState.Loading)
                {
                    res.RegisterOnLoadedEvent(onResLoaded);
                }
                else if (res.State == ResState.Loaded)
                {
                    onLoaded(res.Asset as T);
                }
                return;
            }

            //真正加载资源
            res = CreateRes(assetName);

            res.RegisterOnLoadedEvent(onResLoaded);

            res.LoadAsync();
            
        }

        public void ReleaseAll()
        {
            mResRecords.ForEach(loadedAsset => loadedAsset.Release());

            mResRecords.Clear();
            mResRecords = null;
        }

        #region private
        private List<Res> mResRecords = new List<Res>();

        private Res GetOrCreateRes(string assetName)
        {
            //查询当前资源记录
            var res = GetResFromRecord(assetName);

            if (res != null)
            {
                return res;
            }

            //查询全局资源池
            res = GetResFromResMgr(assetName);

            if (res != null)
            {
                AddRes2Record(res);

                return res;
            }

            return res;
        }

        private Res CreateRes(string assetName)
        {
            Res res = null;

            if (assetName.StartsWith("resources://"))
            {
                res = new ResourceRes(assetName);
            }
            else
            {
                res = new AssetBundleRes(assetName);
            }

            ResMgr.Instance.SharedLoadedReses.Add(res);

            AddRes2Record(res);

            return res;
        }

        private Res GetResFromRecord(string assetName)
        {
            return mResRecords.Find(loadedAsset => loadedAsset.Name == assetName);
        }

        private Res GetResFromResMgr(string assetName)
        {
            return ResMgr.Instance.SharedLoadedReses.Find(loadedAsset => loadedAsset.Name == assetName);
        }

        private void AddRes2Record(Res resFromResMgr)
        {
            mResRecords.Add(resFromResMgr);

            resFromResMgr.Retain();
        }
        #endregion
    }
}