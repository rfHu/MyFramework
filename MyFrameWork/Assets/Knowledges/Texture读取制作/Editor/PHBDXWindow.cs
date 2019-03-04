using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Drawing;
using System;

namespace RHFramework
{
    enum imgType
    {
        BDX,
        PH
    }

    public class PHBDXWindow : EditorWindow
    {
        [MenuItem("拼花波导线/1.处理工具", false, 1)]
        static void Init()
        {
            Rect rect = new Rect(0, 0, 500, 300);
            PHBDXWindow myWindow = (PHBDXWindow)EditorWindow.GetWindowWithRect(typeof(PHBDXWindow), rect, false, "拼花波导线图片处理");//创建窗口
            myWindow.Show();//展示
        }

        string path;

        imgType type;

        private void OnGUI()
        {
            path = EditorGUILayout.TextField("选择目录：", path);

            type = (imgType)EditorGUILayout.EnumPopup("图片格式：", type);

            if (GUILayout.Button("产生Part图，处理图片大小"))
            {
                OutputPartImages();
            }
            if (GUILayout.Button("产生Json"))
            {
                OutputJson();
            }
            if (GUILayout.Button("生成ZIP"))
            {
                BuildZip();
            }

        }

        void OutputPartImages()
        {
            DirectoryInfo parentDir = new DirectoryInfo(path);
            DirectoryInfo[] DirSub = parentDir.GetDirectories();

            foreach (var DI in DirSub)
            {
                var files = DI.GetFiles();
                var rgbFiles = files.Where(f => f.Name.Substring(0, 3) == "rgb").OrderBy(f => f.Name[f.Name.LastIndexOf('.') - 1]).ToArray();
                var images = new Bitmap[rgbFiles.Length];
                for (int i = 0; i < rgbFiles.Length; i++)
                {
                    images[i] = new Bitmap(rgbFiles[i].FullName);
                }
                RGBImageUtil.DrawPartImg(images, DI.FullName, DI.Name);

                DirectoryInfo di = new DirectoryInfo(DI.FullName);
                if (type == imgType.PH)
                {
                    PHImgsScale(di.GetFiles());
                }
                else if (type == imgType.BDX)
                {
                    BDXImgsScale(di.GetFiles());
                }
            }
        }

        void PHImgsScale(FileInfo[] files)
        {
            foreach (var file in files)
            {
                if (!file.Name.Contains("rgb"))
                {
                    var filepath = file.FullName;
                    var img = new Bitmap(file.FullName);
                    var scaledImg = RGBImageUtil.ScaleToSize(img, 280, 280);
                    img.Dispose();
                    File.Delete(file.FullName);
                    scaledImg.Save(filepath);
                    scaledImg.Dispose();
                }
            }
        }

        void BDXImgsScale(FileInfo[] files)
        {
            foreach (var file in files)
            {
                if (file.Name.Contains("part") && file.Name[file.Name.LastIndexOf(".") - 1] == '0' )
                {
                    file.Delete();
                    continue;
                }

                if (file.Name.Contains("part"))
                {
                    var filepath = file.FullName;
                    var img = Image.FromFile(file.FullName);

                    var img2 = new Bitmap(1160, 290);
                    System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(img2);
                    graphic.DrawImage(img, 0, 0, new Rectangle(0, 0, 1160, 290), GraphicsUnit.Pixel);
                    img.Dispose();
                    File.Delete(file.FullName);

                    //img2.Save(filepath);
                    var scaledImg = RGBImageUtil.ScaleToSize(img2, 300, 75);
                    graphic.Dispose();
                    img2.Dispose();

                    scaledImg.Save(filepath);
                    scaledImg.Dispose();
                }
            }
        }

        void OutputJson()
        {
            DirectoryInfo parentDir = new DirectoryInfo(path);
            DirectoryInfo[] DirSub = parentDir.GetDirectories();

            foreach (var DI in DirSub)
            {
                var jsonData = new BDXPHJsonData();
                jsonData.type = type == imgType.BDX ? 0 : 1;

                var files = DI.GetFiles();
                foreach (var file in files)
                {
                    var name = file.Name;
                    if (name.Contains("outline"))
                    {
                        jsonData.outline_img = file.Name.Substring(0, file.Name.LastIndexOf('.'));
                    }
                    else if (name.Contains("icon"))
                    {
                        jsonData.icon_img = file.Name.Substring(0, file.Name.LastIndexOf('.'));
                    }
                    //else if (name.Contains("rgb"))
                    //{
                    //    jsonData.rgb_imgs.Add(file.Name.Substring(0, file.Name.LastIndexOf('.')));
                    //}
                }

                var rgbFiles = files.Where(f => f.Name.Substring(0, 3) == "rgb").OrderBy(f => f.Name[f.Name.LastIndexOf('.') - 1]).ToArray();

                
                foreach (var rgbfile in rgbFiles)
                {
                    jsonData.rgb_imgs.Add(rgbfile.Name.Substring(0, rgbfile.Name.LastIndexOf('.')));

                    var arr=  rgbfile.FullName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 2; i < arr.Length - 1; i++)
                    {
                        jsonData.part_imgs.Add(arr[i]);
                    }
                }

                jsonData.total_part_num = jsonData.part_imgs.Count;

                string data = JsonUtility.ToJson(jsonData);

                string datapath = DI.FullName + "\\data.json";
                StreamWriter sw = new StreamWriter(datapath, true);
                sw.WriteLine(data);
                sw.Dispose();
                sw.Close();
            }
        }

        void BuildZip()
        {
            DirectoryInfo parentDir = new DirectoryInfo(path);
            DirectoryInfo[] DirSub = parentDir.GetDirectories();

            if (!Directory.Exists(path + "\\OutPut"))
            {
                Directory.CreateDirectory(path + "\\OutPut");
            }

            foreach (var DI in DirSub)
            {
                var files = DI.GetFiles();
                foreach (var file in files)
                {
                    if (file.Name.Contains("icon"))
                    {
                        file.MoveTo(path + "\\OutPut\\" + file.Name);
                        break;
                    }
                }

                if (DI.Name == "OutPut")
                {
                    continue;
                }

                ZipUtil.ZipFile(DI.FullName, string.Format("{0}\\OutPut\\{1}.zip", path, DI.Name));
            }
        }
    }

    public class BDXPHJsonData
    {
        public int type;
        public string outline_img;
        public string icon_img;
        public List<string> rgb_imgs = new List<string>();
        public int total_part_num;
        public List<string> part_imgs = new List<string>();
    }
}

