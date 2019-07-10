using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            AssetDatabase.Refresh();
        }
    }
}