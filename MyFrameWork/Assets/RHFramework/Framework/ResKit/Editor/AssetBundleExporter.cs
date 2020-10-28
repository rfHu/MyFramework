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
            var outputPath = ResKitUtil.FullPathForAssetBundle(string.Empty);

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

            var versionConfigFilePath = outputPath + "/ResVersion.json";

            var resVersion = new ResVersion()
            {
                Version = 15,
                AssetBundleNames = AssetDatabase.GetAllAssetBundleNames().ToList()
            };

            var resVersionJson = JsonUtility.ToJson(resVersion, true);

            File.WriteAllText(versionConfigFilePath, resVersionJson);

            AssetDatabase.Refresh();
        }
    }
}