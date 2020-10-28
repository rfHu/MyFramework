using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RHFramework
{
    public enum HotUpdateState 
    {
        /// <summary>
        /// 从未更新
        /// </summary>
        NeverUpdate,
        /// <summary>
        /// 有更新过
        /// </summary>
        Updated,
        /// <summary>
        /// 有个更新过，覆盖安装
        /// </summary>
        Overrided
    }

    public class HotUpdateMgr : MonoSingleton<HotUpdateMgr>
    {
        private HotUpdateState mState;

        public HotUpdateState State { get => mState; }

        public HotUpdateConfig Config { get; set; }

        private void Awake()
        {
            Config = new HotUpdateConfig();
        }

        public void CheckState(Action done) 
        {
            var persistResVersion = Config.LoadHotUpdateAssetBundlesFolderResVersion();
            if (persistResVersion == null)
            {
                mState = HotUpdateState.NeverUpdate;
                done();
            }
            else 
            {
                StartCoroutine(Config.GetStreamingAssetResVersion(streamingResVersion =>
                {
                    if (persistResVersion.Version > streamingResVersion.Version)
                    {
                        mState = HotUpdateState.Updated;
                    }
                    else
                    {
                        mState = HotUpdateState.Overrided;
                    }
                    done();
                }));

            }
        }

        public void GetLocalResVersion(Action<int> onResult) 
        {
            if (mState == HotUpdateState.NeverUpdate || mState == HotUpdateState.Overrided)
            {
                StartCoroutine(Config.GetStreamingAssetResVersion(resVersion => onResult(resVersion.Version)));
                return;
            }

            var localResVersion = Config.LoadHotUpdateAssetBundlesFolderResVersion();
            onResult(localResVersion.Version);
        }

        public void HasNewVersionRes(Action<bool> onResult)
        {
            FakeResServer.Instance.GetRemoteResVersion(remoteResVersion => 
            {
                GetLocalResVersion(localResVersion =>
                {
                    var result = remoteResVersion > localResVersion;
                    onResult(result);
                });

            });
        }

        public void UpdateRes(Action onUpdateDone)
        {
            Debug.Log("开始更新");
            Debug.Log("1.下载资源");
            FakeResServer.Instance.DownloadRes(() =>
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
                Directory.Delete(assetBundlefolders, true);
            }

            Directory.Move(tempAssetBundleFolders, assetBundlefolders);

            if (Directory.Exists(tempAssetBundleFolders))
            {
                Directory.Delete(tempAssetBundleFolders, true);
            }
        }
    }
}