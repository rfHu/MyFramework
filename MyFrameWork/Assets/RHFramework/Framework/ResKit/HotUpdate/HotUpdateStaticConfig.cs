using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public enum HotUpdateType 
    {
        full,
        Increment
    }
    public class HotUpdateStaticConfig
    {
        public static HotUpdateConfig MgrAssetBundlesConfig { get { return new HotUpdateConfig(); } }

        public static readonly HotUpdateType HotUpdateType = HotUpdateType.Increment;

        public static string BuildAssetBundlesFolder(string bundleName) 
        {
            return Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtil.GetPlatformName() + "/" + bundleName;
        }

        public static int RemoteBundleVersion = 0;

        //不记录在ResVersion中的包名
        public static string[] UnrecordBundleName =
        {
        };
    }
}