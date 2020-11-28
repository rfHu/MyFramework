using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RHFramework
{
    public class IncrementHotUpdateMgr : MonoSingleton<IncrementHotUpdateMgr>
    {
        public HotUpdateConfig Config { get; set; }

        public ResVersion UpdatedResVersion { get; set; }

        private void Awake()
        {
            Config = HotUpdateStaticConfig.MgrAssetBundlesConfig;
            UpdatedResVersion = new ResVersion() { Version = 0, AssetBundleMD5s = new List<string>(), AssetBundleNames = new List<string>() };
        }

        public void HasNewVersionRes(Action<ResVersion> onResult)
        {
            FakeResServer.Instance.IncrementGetRemoteResVersion(remoteResVersion =>
            {
                onResult(remoteResVersion);
            });
        }

        public void RecordClearAndGetDownLoadNames(ResVersion remoteResVersion, Action<List<string>> onResult)
        {
            //Record
            UpdatedResVersion = new ResVersion()
            {
                Version = remoteResVersion.Version,
                AssetBundleNames = new List<string>(),
                AssetBundleMD5s = new List<string>()
            };
            remoteResVersion.AssetBundleNames.ForEach(name => UpdatedResVersion.AssetBundleNames.Add(name));
            remoteResVersion.AssetBundleMD5s.ForEach(name => UpdatedResVersion.AssetBundleMD5s.Add(name));

            //Delete
            DeleteUnusedBundle(remoteResVersion);

            //Get and return needDownload Names
            var needDownloadResVersion = GetNeedDownloadResVersion(remoteResVersion);

            onResult(needDownloadResVersion.AssetBundleNames);
        }

        private void DeleteUnusedBundle(ResVersion remoteResVersion)
        {
            if (!Directory.Exists(Config.HotUpdateAssetBundlesFolder))
            {
                return;
            }

            var files = Directory.GetFiles(Config.HotUpdateAssetBundlesFolder);

            for (int i = 0; i < files.Length; i++)
            {
                var fileName = files[i].Substring(Config.HotUpdateAssetBundlesFolder.Length);

                if (!remoteResVersion.AssetBundleNames.Contains(fileName))
                {
                    File.Delete(files[i]);
                }
            }
        }

        private ResVersion GetNeedDownloadResVersion(ResVersion remoteResVersion)
        {
            var needDownloadResVersion = new ResVersion();

            for (int i = 0; i < remoteResVersion.AssetBundleNames.Count; i++)
            {
                var tmpBundlePath = string.Format("{0}{1}", Config.HotUpdateAssetBundlesFolder, remoteResVersion.AssetBundleNames[i]);

                if (File.Exists(tmpBundlePath))
                {
                    if (!FileMD5Tools.MD5Stream(tmpBundlePath).Equals(remoteResVersion.AssetBundleMD5s[i]))
                    {
                        File.Delete(tmpBundlePath);

                        needDownloadResVersion.AssetBundleNames.Add(remoteResVersion.AssetBundleNames[i]);
                    }
                    else
                    {
                        Debug.Log("MD5相同,下一个");
                    }
                }
                else
                {
                    needDownloadResVersion.AssetBundleNames.Add(remoteResVersion.AssetBundleNames[i]);
                }
            }

            return needDownloadResVersion;
        }

        public void UpdateRes(List<string> AssetBundleNames, Action onUpdateDone, Action<string> onDownLoadError)
        {
            Debug.Log("开始更新");
            Debug.Log("1.下载资源");
            FakeResServer.Instance.IncrementDownloadRes(AssetBundleNames, () =>
            {
                Debug.Log("结束更新");
                onUpdateDone();
            }, onDownLoadError);
        }
    }
}