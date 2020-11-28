using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RHFramework
{
    public class AssetBundleExporter
    {
        [MenuItem("RHFramework/Framework/ResKit/Build AssetBundles", false)]
        static void BuildAssetBundles()
        {
            var outputPath = ResKitUtil.BuildAssetBundleFullPath(string.Empty);

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

            var versionConfigFilePath = outputPath + "/ResVersion.json";


            List<string> AssetBundleMD5s = GetAssetBundleMD5s(outputPath);

            var resVersion = new ResVersion()
            {
                Version = HotUpdateStaticConfig.RemoteBundleVersion,
                AssetBundleNames = AssetDatabase.GetAllAssetBundleNames().Where(name => File.Exists(string.Format("{0}/{1}", outputPath, name)) && !HotUpdateStaticConfig.UnrecordBundleName.Contains(name)).ToList(),
                AssetBundleMD5s = AssetBundleMD5s
            };

            var resVersionJson = JsonUtility.ToJson(resVersion, true);

            File.WriteAllText(versionConfigFilePath, resVersionJson);

            AssetDatabase.Refresh();
        }

        private static List<string> GetAssetBundleMD5s(string outputPath)
        {

            var AssetBundleNames = AssetDatabase.GetAllAssetBundleNames();
            var AssetBundleMD5s = new List<string>();
            for (int i = 0; i < AssetBundleNames.Length; i++)
            {
                var bundlePath = string.Format("{0}/{1}", outputPath, AssetBundleNames[i]);
                if (File.Exists(bundlePath) && !HotUpdateStaticConfig.UnrecordBundleName.Contains(AssetBundleNames[i]))
                {
                    var bundleMD5Str = FileMD5Tools.MD5Stream(bundlePath);
                    AssetBundleMD5s.Add(bundleMD5Str);
                }
            }

            return AssetBundleMD5s;
        }
    }
}