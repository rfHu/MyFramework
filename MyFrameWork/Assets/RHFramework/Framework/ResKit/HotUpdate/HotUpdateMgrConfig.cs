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
    public class HotUpdateMgrConfig
    {
        public static readonly HotUpdateType HotUpdateType = HotUpdateType.Increment;

        public static HotUpdateConfig BuildAssetBundlesConfig { get { return new HotUpdateConfig(); } }

        public static HotUpdateConfig MgrAssetBundlesConfig { get { return new HotUpdateConfig(); } }

    }
}