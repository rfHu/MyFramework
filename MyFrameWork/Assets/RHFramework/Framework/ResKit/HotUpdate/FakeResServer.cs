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
                StartCoroutine(DoFullDownloadRes(remoteResVersion, downloadDone));
            }));
        }

        private IEnumerator DoFullDownloadRes(ResVersion remoteResVersion, Action downloadDone)
        {
            //创建临时目录
            if (!Directory.Exists(TempAssetBundlesPath))
            {
                Directory.CreateDirectory(TempAssetBundlesPath);
            }

            var remoteBasePath = FullHotUpdateMgr.Instance.Config.RemoteAssetBundleURLBase;

            //补上 AssetBundleMenifest 文件

            remoteResVersion.AssetBundleNames.Add(ResKitUtil.GetPlatformName());

            for (int i = 0; i < remoteResVersion.AssetBundleNames.Count; i++)
            {
                string assetBundleName = remoteResVersion.AssetBundleNames[i];

                UnityWebRequest uwr = UnityWebRequest.Get(string.Format("{0}/{1}", remoteBasePath, assetBundleName));
                uwr.timeout = 5;
                uwr.SendWebRequest();
                if (uwr.isHttpError || uwr.isNetworkError)
                {
                    Debug.Log(uwr.error);
                    //todo error处理
                }
                else
                {
                    while (!uwr.isDone)
                    {
                        var progress = uwr.downloadProgress;
                        yield return 0;
                    }

                    if (uwr.isDone) //如果下载完成了
                    {
                        Debug.Log("完成");
                    }

                    var bytes = uwr.downloadHandler.data;

                    var filepath = TempAssetBundlesPath + assetBundleName;
                    File.WriteAllBytes(filepath, bytes);
                }

            }

            downloadDone();
        }

        public void IncrementDownloadRes(List<string> AssetBundleNames, Action downloadDone, Action<string> downloadError)
        {
            StartCoroutine(DoIncrementDownloadRes(AssetBundleNames, downloadDone, downloadError));
        }

        private IEnumerator DoIncrementDownloadRes(List<string> AssetBundleNames, Action downloadDone, Action<string> downloadError)
        {
            if (!Directory.Exists(IncrementHotUpdateMgr.Instance.Config.HotUpdateAssetBundlesFolder))
            {
                Directory.CreateDirectory(IncrementHotUpdateMgr.Instance.Config.HotUpdateAssetBundlesFolder);
            }

            var remoteBasePath = IncrementHotUpdateMgr.Instance.Config.RemoteAssetBundleURLBase;

            //补上 AssetBundleMenifest 文件

            AssetBundleNames.Add(ResKitUtil.GetPlatformName());

            for (int i = 0; i < AssetBundleNames.Count; i++)
            {
                string assetBundleName = AssetBundleNames[i];

                UnityWebRequest uwr = UnityWebRequest.Get(string.Format("{0}/{1}", remoteBasePath, assetBundleName));
                uwr.timeout = 5;
                uwr.SendWebRequest();

                if (uwr.isHttpError || uwr.isNetworkError)
                {
                    downloadError(uwr.error);
                }
                else
                {
                    while (!uwr.isDone)
                    {
                        var progress = uwr.downloadProgress;
                        yield return 0;
                    }

                    if (uwr.isDone) //如果下载完成了
                    {
                        Debug.Log("完成");
                    }

                    var bytes = uwr.downloadHandler.data;

                    var filepath = TempAssetBundlesPath + assetBundleName;
                    File.WriteAllBytes(filepath, bytes);
                }
            }

            downloadDone();
        }
    }
}