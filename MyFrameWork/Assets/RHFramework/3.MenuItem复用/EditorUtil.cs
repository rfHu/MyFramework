using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace RHFramework
{
    public partial class EditorUtil
    {

        public static void OpenInForder(string folderPath)
        {
            Application.OpenURL("file://" + folderPath);
        }

#if UNITY_EDITOR
        public static void ExportPackage(string assetPathName, string fileName, ExportPackageOptions option = ExportPackageOptions.Recurse)
        {
            AssetDatabase.ExportPackage(assetPathName, fileName, option);
        }

        public static void CallMenuItem(string menuName)
        {
            EditorApplication.ExecuteMenuItem(menuName);
        }



        [MenuItem("RHFramework/3.复用 MenuItem", false , 3)]
        private static void MenuClicked6()
        {
            EditorUtil.CallMenuItem("2.复制文字到剪切板");
        }
#endif
    }
}