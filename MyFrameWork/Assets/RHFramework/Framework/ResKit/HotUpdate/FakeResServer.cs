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

        public List<string> AssetBundleMD5s = new List<string>();
    }

    public class FakeResServer : MonoSingleton<FakeResServer>
    {
        public static string TempAssetBundlesPath 
        {
            get { return Application.persistentDataPath + "/TempAssetBundles/"; }
        }

        public void FullGetRemoteResVersion(Action<int> onRemoteResVersionGet) 
        {
            StartCoroutine(FullHotUpdateMgr.Instance.Config.RequestRemoteResVersion(remoteResversion => 
            {
                onRemoteResVersionGet(remoteResversion.Version);
            }));
        }

        public void IncrementGetRemoteResVersion(Action<ResVersion> onRemoteResVersionGet)
        {
            StartCoroutine(IncrementHotUpdateMgr.Instance.Config.RequestRemoteResVersion(remoteResversion =>
            {
                onRemoteResVersionGet(remoteResversion);
            }));
        }

        public void FullDownloadRes(Action downloadDone)
        {
            StartCoroutine(FullHotUpdateMgr.Instance.Config.RequestRemoteResVersion(remoteResVersion => 
            {
                StartCoroutine(DoDownloadRes(remoteResVersion, downloadDone));
            }));
        }

        public void IncrementDownloadRes(ResVersion needBundleResVersion, Action downloadDone) 
        {
            StartCoroutine(DoDownloadRes(needBundleResVersion, downloadDone));
        }

        private IEnumerator DoDownloadRes(ResVersion remoteResVersion, Action downloadDone) 
        {
            //创建临时目录
            if (!Directory.Exists(TempAssetBundlesPath))
            {
                Directory.CreateDirectory(TempAssetBundlesPath);
            }

            var remoteBasePath = GetRemoteAssetBundleURLBase();

            //补上 AssetBundleMenifest 文件
            remoteResVersion.AssetBundleNames.Add(ResKitUtil.GetPlatformName());

            foreach (var assetBundleName in remoteResVersion.AssetBundleNames)
            {
                var www = new WWW(remoteBasePath + assetBundleName);
                yield return www;
                if (www.error != null)
                {
                    Debug.LogError("download bundle error: " + www.error);
                }
                else
                {
                    var bytes = www.bytes;

                    var filepath = TempAssetBundlesPath + assetBundleName;
                    File.WriteAllBytes(filepath, bytes);
                }
            }

            downloadDone();
        }

        private string GetRemoteAssetBundleURLBase()
        {
            if (HotUpdateMgrConfig.HotUpdateType == HotUpdateType.full)
            {
                return FullHotUpdateMgr.Instance.Config.RemoteAssetBundleURLBase;
            }
            else
            {
                return IncrementHotUpdateMgr.Instance.Config.RemoteAssetBundleURLBase;
            }
        }
    }
}