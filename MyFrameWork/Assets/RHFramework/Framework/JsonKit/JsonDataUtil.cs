using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RHFramework
{
    public static class JsonDataUtil
    {
        /// <summary>
        /// 保存类为json文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        /// <param name="fileName">不添加 .json 后缀</param>
        public static void SaveDataToPath(object data, string path, string fileName)
        {
            var jsonString = JsonUtility.ToJson(data);
            fileName = fileName + ".json";
            var fullPath = string.Format("{0}/{1}", path, fileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(fullPath, jsonString);
        }

        public static T ReadDataFromPath<T>(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Can't find file");
            }

            var jsonString = File.ReadAllText(fullPath);

            var dataObj = JsonUtility.FromJson<T>(jsonString);

            return dataObj;
        }
    }
}