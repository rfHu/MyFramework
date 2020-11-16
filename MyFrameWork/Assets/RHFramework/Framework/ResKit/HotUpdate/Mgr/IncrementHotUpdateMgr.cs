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

        private void Awake()
        {
            Config = HotUpdateStaticConfig.MgrAssetBundlesConfig;
        }

        public void HasNewVersionRes(Action<ResVersion, bool> onResult) 
        {
            FakeResServer.Instance.IncrementGetRemoteResVersion(remoteResVersion =>
            {
                var needDownloadResVersion = GetNeedDownloadResVersion(remoteResVersion);

                onResult(needDownloadResVersion, needDownloadResVersion.AssetBundleNames.Count != 0);
            });
        }

        private ResVersion GetNeedDownloadResVersion(ResVersion remoteResVersion)
        {
            var needDownloadResVersion = new ResVersion();

            for (int i = 0; i < remoteResVersion.AssetBundleNames.Count; i++)
            {
                var tmpBundlePath = string.Format("{0}{1}", IncrementHotUpdateMgr.Instance.Config.HotUpdateAssetBundlesFolder, remoteResVersion.AssetBundleNames[i]);
                
                if (File.Exists(tmpBundlePath))
                {
                    if (!FileMD5Tools.MD5Stream(tmpBundlePath).Equals(remoteResVersion.AssetBundleMD5s[i]))
                    {
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

        public void UpdateRes(ResVersion needDownloadResVersion, Action onUpdateDone)
        {
            Debug.Log("开始更新");
            Debug.Log("1.下载资源");
            FakeResServer.Instance.IncrementDownloadRes(needDownloadResVersion, () =>
            {
                ReplaceLocalRes();
                Debug.Log("结束更新");
                onUpdateDone();
            });
        }

        private void ReplaceLocalRes()
        {
            Debug.Log("2.替换掉本地资源");
            var tempAssetBundleFolders = FakeResServer.TempAssetBundlesPath;
            var assetBundlefolders = Config.HotUpdateAssetBundlesFolder;

            if (Directory.Exists(assetBundlefolders))
            {
                var tmpFiles = Directory.GetFiles(tempAssetBundleFolders);
                for (int i = 0; i < tmpFiles.Length; i++)
                {
                    var oldPath =  tmpFiles[i];
                    var newPath = string.Format("{0}{1}", assetBundlefolders, tmpFiles[i].Substring(tempAssetBundleFolders.Length));
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }
                    File.Move(oldPath, newPath);
                }
            }
            else
            {
                Directory.Move(tempAssetBundleFolders, assetBundlefolders);
            }

            if (Directory.Exists(tempAssetBundleFolders))
            {
                Directory.Delete(tempAssetBundleFolders, true);
            }
        }
    }
}