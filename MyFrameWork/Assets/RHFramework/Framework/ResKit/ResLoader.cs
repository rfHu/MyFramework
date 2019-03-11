using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class ResLoader
    {
        public static List<Res> SharedLoadedReses = new List<Res>();

        private List<Res> mResRecords = new List<Res>();

        public T LoadAsset<T>(string assetName) where T : Object
        {
            //查询当前资源记录
            var res = mResRecords.Find(loadedAsset => loadedAsset.Name == assetName);
            
            if (res != null)
            {
                return res.Asset as T;
            }
            
            //查询全局资源池
            res = SharedLoadedReses.Find(loadedAsset => loadedAsset.Name == assetName);

            if (res != null)
            {
                mResRecords.Add(res);

                res.Retain();

                return res as T;
            }

            //真正加载资源
            var asset = Resources.Load<T>(assetName);

            res = new Res(asset);
            
            SharedLoadedReses.Add(res);

            mResRecords.Add(res);

            res.Retain();

            return asset;
        }

        public void ReleaseAll()
        {
            mResRecords.ForEach(loadedAsset => loadedAsset.Release());

            mResRecords.Clear();
            mResRecords = null;
        }
    }
}