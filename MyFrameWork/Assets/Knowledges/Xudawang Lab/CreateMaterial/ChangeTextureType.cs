//using UnityEngine;
//using System.Collections;
//using UnityEditor;

//public class ChangeTextureType : Editor
//{
//    [MenuItem("XQY/ChangeToSprite")]
//    private static void ChangeToSprite()
//    {
//        object[] textures = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);
//        Selection.objects = new Object[0];

//        for (int i = 0; i < textures.Length; i++)
//        {
//            EditorUtility.DisplayProgressBar("处理进度", string.Format("正在处理第{0}/{1}个图片", i, textures.Length), i / (float)textures.Length);
//            Texture texture = textures[i] as Texture;
//            string path = AssetDatabase.GetAssetPath(texture);
//            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
//            textureImporter.textureType = TextureImporterType.Sprite;

//            textureImporter.isReadable = true;
//            AssetDatabase.ImportAsset(path);
//            AssetDatabase.Refresh();
//        }
//        EditorUtility.ClearProgressBar();
//    }

//    [MenuItem("XQY/ChangeToTexture")]
//    private static void ChangeToTexture()
//    {
//        EditorUtility.ClearProgressBar();
//        object[] textures = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);
//        Selection.objects = new Object[0];
//        //Debug.Log(textures.Length);
//        for (int i = 0; i < textures.Length; i++)
//        {
//            EditorUtility.DisplayProgressBar("处理进度", string.Format("正在处理第{0}/{1}个图片", i, textures.Length), (float)i / textures.Length);
//            Texture texture = textures[i] as Texture;
//            string path = AssetDatabase.GetAssetPath(texture);
//            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
//            if (textureImporter != null)
//            {
//                //textureImporter.textureType = TextureImporterType.Default;
//                textureImporter.isReadable = true;
//                //textureImporter.wrapMode = TextureWrapMode.Clamp;
//                textureImporter.maxTextureSize = 8192;
//                //TextureImporterPlatformSettings ss = new TextureImporterPlatformSettings();
//                //ss.format = TextureImporterFormat.ETC_RGB4;
//                //textureImporter.SetPlatformTextureSettings(ss);
//                //textureImporter.maxTextureSize = 512;
//                AssetDatabase.ImportAsset(path);
//                AssetDatabase.Refresh();
//            }
//        }
//        EditorUtility.ClearProgressBar();
//    }
//}