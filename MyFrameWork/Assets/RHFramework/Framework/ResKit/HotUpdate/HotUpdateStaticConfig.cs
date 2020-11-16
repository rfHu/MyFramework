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

        //填写的bundle名将不录入json文件，不会被热更新下载
        public static string[] LocalBundleNames = new string[]
        {

        };
    }
}