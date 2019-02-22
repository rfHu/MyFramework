using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CreateMaterial : Editor
{
    [MenuItem("MyTools/BuildFloorMaterial")]
    private static void CreateMat()
    {
        Material tempMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Editor/CreateMaterial/TempMat.mat");



        CreateMat(tempMat, "Assets/Editor/CreateMaterial", "test", 2000, 2000, 2, 2, "Assets/Editor/CreateMaterial/testT.png");



        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
    }
    
    [MenuItem("MyTools/BuildAllMaterial")]
    private static void BuildAllMaterial()
    {
        Material tempMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Knowledges/Xudawang Lab/CreateMaterial/TempMat.mat");

        string filesParentPath = @"E:\STClone\MyFramework\MyFrameWork\Assets\Knowledges\Xudawang Lab\finalpic";

        DirectoryInfo parentDir = new DirectoryInfo(filesParentPath);

        FileInfo[] fileSub = parentDir.GetFiles("*.tga");

        foreach (var file in fileSub)
        {
            var arr = file.Name.Split(new char[] { '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
            CreateMat(tempMat, "Assets/Knowledges/Xudawang Lab/Materials", arr[0], float.Parse(arr[1]), float.Parse(arr[2]), float.Parse(arr[3]), float.Parse(arr[4]), "Assets/Knowledges/Xudawang Lab/finalpic/" + file.Name);
        }
        
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();

        Debug.Log("finish");
    }

    private static void CreateMat(Material tempMat, string materialPath, string matName, float l, float w, float textureL, float textureW, string mainTexturePath)
    {
        Material newMat = new Material(tempMat);
        newMat.SetFloat("_Lmm", l);
        newMat.SetFloat("_Wmm", w);
        newMat.SetFloat("_Texture_L", textureL);
        newMat.SetFloat("_Texture_W", textureW);

        TextureImporter textureImporter = AssetImporter.GetAtPath(mainTexturePath) as TextureImporter;
        if (textureImporter != null)
        {
            //textureImporter.textureType = TextureImporterType.Default;
            textureImporter.isReadable = true;
            //textureImporter.wrapMode = TextureWrapMode.Clamp;
            textureImporter.maxTextureSize = 8192;
        }

        newMat.SetTexture("_Albedo", AssetDatabase.LoadAssetAtPath<Texture>(mainTexturePath));
        AssetDatabase.CreateAsset(newMat, string.Format("{0}/{1}.mat", materialPath, matName));
    }
}