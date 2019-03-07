using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RHFramework
{
    public class BuildAssetBundle : EditorWindow
    {
        [MenuItem("图片批处理/3.集体打包工具", false,  4)]
        static void Init()
        {
            Rect rect = new Rect(0, 0, 500, 300);
            BuildAssetBundle myWindow = (BuildAssetBundle)EditorWindow.GetWindowWithRect(typeof(BuildAssetBundle), rect, false, "BuildAssetBundle");//创建窗口
            myWindow.Show();//展示
        }

        private List<string> selectionPathes = new List<string>();

        private string assetBundlePath;

        private string targetExtension = "prefab";

        void OnGUI()
        {
            Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.TopLevel);
            selectionPathes = new List<string>();
            foreach (var obj in objs)
            {
                string objPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(obj);
                if (!selectionPathes.Contains(objPath))
                {
                    selectionPathes.Add(objPath);
                }
            }

            string allPath = string.Join(";", selectionPathes.ToArray());
            //Debug.Log(allPath);

            EditorGUILayout.TextField("选择目录和/或文件", allPath);

            assetBundlePath = EditorGUILayout.TextField("导出路径", assetBundlePath);

            //GUILayout.Label(string.Format("{0}{1}", "导出路径:", assetBundlePath));

            targetExtension = EditorGUILayout.TextField("目标扩展名", targetExtension);

#if UNITY_STANDALONE_WIN
            if (GUILayout.Button("打包Win64"))
            {
                Build();
            }

            if (GUILayout.Button("生成文件夹"))
            {
                CreateDir();
            }
#endif

#if UNITY_IOS
            if (GUILayout.Button("打包iOS"))
            {
                Build();
            }
#endif
        }

        private void Build()
        {
            if (!Directory.Exists(assetBundlePath))
            {
                Directory.CreateDirectory(assetBundlePath);
            }

            var allPrefab = new List<string>();

            foreach (var path in selectionPathes)
            {
                if (File.Exists(path) && path.ToLower().EndsWith("." + targetExtension))
                {
                    if (!allPrefab.Contains(path))
                    {
                        allPrefab.Add(path);
                    }
                }
                else if (Directory.Exists(path))
                {
                    var prefabs = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Where(s => s.EndsWith("." + targetExtension));
                    foreach (var prefab in prefabs)
                    {
                        string tempPath = prefab.Replace(@"\", "/");
                        if (!allPrefab.Contains(tempPath))
                        {
                            allPrefab.Add(tempPath);
                        }
                    }
                }
                else
                {
                    //todo : 是否需要抛出某些异常？
                }
            }

            //List<AssetBundleBuild> buildMaps = new List<AssetBundleBuild>();
            foreach (string item in allPrefab)
            {
                string strTemp = item.Replace(@"\", "/");
                strTemp = strTemp.Substring(strTemp.IndexOf("Assets"));
                Debug.Log(strTemp);

                string abName = strTemp.Substring(strTemp.LastIndexOf('/') + 1, strTemp.IndexOf("." + targetExtension) - strTemp.LastIndexOf('/') - 1) + ".res";
                Debug.Log(abName);

                AssetBundleBuild tempABBuild = new AssetBundleBuild();
                tempABBuild.assetBundleName = abName;
                tempABBuild.assetNames = new string[] { strTemp };

                AssetBundleBuild[] buildMaps = new AssetBundleBuild[] { tempABBuild };
#if UNITY_STANDALONE_WIN
                BuildPipeline.BuildAssetBundles(assetBundlePath + "/" + abName.Remove(abName.IndexOf('.')), buildMaps.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
#endif

#if UNITY_IOS
            BuildPipeline.BuildAssetBundles(assetBundlePath + "/" + abName.Remove(abName.IndexOf('.')), buildMaps.ToArray(), buildMaps.ToArray(), BuildAssetBundleOptions.None, BuildTarget.iOS);
#endif
            }

            Debug.Log(string.Format("生成成功，共处理{0}个预制体", allPrefab.Count));
            AssetDatabase.Refresh();
        }

        private void CreateDir()
        {
            string pp = @"E:\STClone\MyFramework\MyFrameWork\AssetBundles";

            DirectoryInfo parentDir = new DirectoryInfo(pp);

            FileInfo[] fileSub = parentDir.GetFiles("*.res");

            foreach (var file in fileSub)
            {
                DirectoryInfo di = Directory.CreateDirectory(pp + "/" + file.Name.Substring(0, file.Name.IndexOf(".")));
                File.Copy(file.FullName, di.FullName + "/" + file.Name);
            }
        }
    }
}