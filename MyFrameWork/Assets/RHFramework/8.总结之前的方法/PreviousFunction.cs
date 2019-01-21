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
    public class PreviousFunction
    {

        public static string GeneratePackageName()
        {
            return "RHFramework_" + DateTime.Now.ToString("yyyyMMdd_HH");
        }

        public static void CopyText(string text)
        {
            GUIUtility.systemCopyBuffer = text;
        }

        public static void ExportPackage(string assetPathName, string fileName, ExportPackageOptions option = ExportPackageOptions.Recurse)
        {
            AssetDatabase.ExportPackage(assetPathName, fileName, option);
        }

        private static void OpenInForder(string folderPath)
        {
            Application.OpenURL("file://" + folderPath);
        }

        private static void ReuseItem(string menuName)
        {
            EditorApplication.ExecuteMenuItem(menuName);
            Application.OpenURL("file://" + Path.Combine(Application.dataPath, "../"));
        }

#if UNITY_EDITOR
        [MenuItem("RHFramework/8.总结之前的所有方法/1.自动生成package名字")]
        private static void MenuClicked()
        {
            Debug.Log(GeneratePackageName());
        }

        [MenuItem("RHFramework/8.总结之前的所有方法/2.复制文字到剪切板")]
        private static void MenuClicked2()
        {
            CopyText("复制文本");
        }

        [MenuItem("RHFramework/8.总结之前的所有方法/3.生成文件名到剪切板")]
        private static void MenuClicked3()
        {
            CopyText(GeneratePackageName());
        }

        [MenuItem("RHFramework/8.总结之前的所有方法/4.导出 Unitypackage")]
        private static void MenuClicked4()
        {
            ExportPackage("Assets/RHFramework", GeneratePackageName() + ".unitypackage");
        }

        [MenuItem("RHFramework/8.总结之前的所有方法/5.打开所在文件夹")]
        private static void MenuClicked5()
        {
            OpenInForder(Application.dataPath);
        }

        [MenuItem("RHFramework/8.总结之前的所有方法/6.复用 MenuItem")]
        private static void MenuClicked6()
        {
            ReuseItem("RHFramework/4.导出 UnityPackage");
        }

        [MenuItem("RHFramework/8.总结之前的所有方法/7.创建快捷键")]
        private static void CustonShortCut()
        {
            Debug.Log("%e 意思是快捷键 cmd/ctrl + e");
        }
#endif
    }
}