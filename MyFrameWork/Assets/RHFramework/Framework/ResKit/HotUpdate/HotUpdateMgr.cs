using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RHFramework
{
    public class HotUpdateMgr : MonoSingleton<HotUpdateMgr>
    {
        public int GetLocalResVersion() 
        {
            var path = Application.streamingAssetsPath + "/AssetBundles/Windows/Resversion.json";
            var jsonString = File.ReadAllText(path);
            var localResVersion = JsonUtility.FromJson<ResVersion>(jsonString);
            return localResVersion.version;
        }

        public void HasNewVersionRes(Action<bool> onResult)
        {
            FakeResServer.Instance.GetRemoteResVersion(remoteResVersion => 
            {
                var result = remoteResVersion > GetLocalResVersion();
                onResult(result);
            });
        }

        public void UpdateRes(Action onUpdateDone)
        {
            Debug.Log("开始更新");
            Debug.Log("1.下载资源");
            FakeResServer.Instance.DownloadRes(remoteResVersion =>
            {
                ReplaceLocalRes(remoteResVersion);
                Debug.Log("结束更新");
            });
        }

        private void ReplaceLocalRes(ResVersion remoteVerVersion)
        {
            Debug.Log("2.替换掉本地资源");
            var path = Application.streamingAssetsPath + "/AssetBundles/Windows/Resversion.json";
            var jsonString = JsonUtility.ToJson(remoteVerVersion);
            File.WriteAllText(path, jsonString);
        }
    }
}