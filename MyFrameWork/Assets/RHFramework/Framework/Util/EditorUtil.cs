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
#endif
    }
}