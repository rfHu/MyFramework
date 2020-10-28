using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace RHFramework
{
    [Serializable]
    public class ResVersion 
    {
        public int Version;

        public List<string> AssetBundleNames = new List<string>();
    }

    public class FakeResServer : MonoSingleton<FakeResServer>
    {
        public static string TempAssetBundlesPath 
        {
            get { return Application.persistentDataPath + "/TempAssetBundles/"; }
        }

        public void GetRemoteResVersion(Action<int> onRemoteResVersionGet) 
        {
            StartCoroutine(HotUpdateMgr.Instance.Config.RequestRemoteResVersion(remoteResversion => 
            {
                onRemoteResVersionGet(remoteResversion.Version);
            }));
        }

        public void DownloadRes(Action downloadDone)
        {
            StartCoroutine(HotUpdateMgr.Instance.Config.RequestRemoteResVersion(remoteResVersion => 
            {
                StartCoroutine(DoDownloadRes(remoteResVersion, downloadDone));
            }));
        }

        private IEnumerator DoDownloadRes(ResVersion remoteResVersion, Action downloadDone) 
        {
            //创建临时目录
            if (!Directory.Exists(TempAssetBundlesPath))
            {
                Directory.CreateDirectory(TempAssetBundlesPath);
            }

            //保存 ResVersion.json
            var tempResVersionFilePath = TempAssetBundlesPath + "ResVersion.json";
            var tempResVersionJson = JsonUtility.ToJson(remoteResVersion);
            File.WriteAllText(tempResVersionFilePath, tempResVersionJson);

            var remoteBasePath = HotUpdateMgr.Instance.Config.RemoteAssetBundleURLBase;

            //补上 AssetBundleMenifest 文件

            remoteResVersion.AssetBundleNames.Add(ResKitUtil.GetPlatformName());

            foreach (var assetBundleName in remoteResVersion.AssetBundleNames)
            {
                var www = new WWW(remoteBasePath + assetBundleName);
                yield return www;

                var bytes = www.bytes;

                var filepath = TempAssetBundlesPath + assetBundleName;
                File.WriteAllBytes(filepath, bytes);
            }

            downloadDone();
        }
    }
}