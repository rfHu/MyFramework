using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class ResLoader
    {
        private List<Res> mResRecords = new List<Res>();
        
        public T LoadSync<T>(string assetName) where T : Object
        {
            //查询当前资源记录
            var res = GetResFromRecord(assetName);
            
            if (res != null)
            {
                return res.Asset as T;
            }

            //查询全局资源池
            res = GetResFromResMgr(assetName);

            if (res != null)
            {
                AddRes2Record(res);

                return res as T;
            }

            //真正加载资源
            res = new Res(assetName);

            res.LoadSync();
            
            ResMgr.Instance.SharedLoadedReses.Add(res);
            
            AddRes2Record(res);
            
            return res.Asset as T;
        }

        public void ReleaseAll()
        {
            mResRecords.ForEach(loadedAsset => loadedAsset.Release());

            mResRecords.Clear();
            mResRecords = null;
        }

        #region private
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