using System.IO;
using UnityEngine;

namespace RHFramework
{
    public class ResKitUtil
    {
        public static string FullPathForAssetBundle(string assetBundleName)
        {
            if (HotUpdateStaticConfig.HotUpdateType == HotUpdateType.full)
            {
                return FullBundleFullPath(assetBundleName);
            }
            else
            {
                return IncrementBundleFullPath(assetBundleName);
            }
        }

        private static string IncrementBundleFullPath(string assetBundleName)
        {
            if (File.Exists(IncrementHotUpdateMgr.Instance.Config.HotUpdateAssetBundlesFolder + assetBundleName))
            {
                return IncrementHotUpdateMgr.Instance.Config.HotUpdateAssetBundlesFolder + assetBundleName;
            }
            else
            {
                return IncrementHotUpdateMgr.Instance.Config.LocalAssetBundlesFolder + assetBundleName;
            }
        }

        private static string FullBundleFullPath(string assetBundleName)
        {
            var hotUpdateState = FullHotUpdateMgr.Instance.State;

            if (hotUpdateState == FullHotUpdateState.Updated)
            {
                return FullHotUpdateMgr.Instance.Config.HotUpdateAssetBundlesFolder + assetBundleName;
            }

            else
            {
                return FullHotUpdateMgr.Instance.Config.LocalAssetBundlesFolder + assetBundleName;
            }
        }

        public static string BuildAssetBundleFullPath(string assetBundleName)
        {
            return HotUpdateStaticConfig.BuildAssetBundlesFolder(assetBundleName);
        }

        public static string GetPlatformName()
        {

#if UNITY_EDITOR
            return GetPlatformName(UnityEditor.EditorUserBuildSettings.activeBuildTarget);
#else
            return GetPlatformName(Application.platform);
#endif
        }

#if UNITY_EDITOR
        private static string GetPlatformName(UnityEditor.BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case UnityEditor.BuildTarget.StandaloneWindows:
                case UnityEditor.BuildTarget.StandaloneWindows64:
                    return "Windows";
                case UnityEditor.BuildTarget.iOS:
                    return "iOS";
                case UnityEditor.BuildTarget.Android:
                    return "Android";
                case UnityEditor.BuildTarget.StandaloneLinux:
                case UnityEditor.BuildTarget.StandaloneLinux64:
                case UnityEditor.BuildTarget.StandaloneLinuxUniversal:
                    return "Linux";
                case UnityEditor.BuildTarget.StandaloneOSX:
                    return "OSX";
                case UnityEditor.BuildTarget.WebGL:
                    return "WebGL";
                default:
                    return null;
            }
        }
#endif

        private static string GetPlatformName(RuntimePlatform runtimePlatform)
        {
            switch (runtimePlatform)
            {
                case RuntimePlatform.WebGLPlayer:
                    return "WebgGL";
                case RuntimePlatform.OSXPlayer:
                    return "OSX";
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.Android:
                    return "Android";
                default:
                    return null;
            }
        }
    }
}