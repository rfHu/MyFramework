using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace RHFramework
{
    public class CreatePart2Package : MonoBehaviour
    {
        private Button _Btn_Path;
        private InputField _IF_Path;
        private Text _Text_Path;
        private Button _Btn_Deal;
        private InputField _IF_SavePath;

        private string str_Path;
        private string str_SavePath;
        private string dirName;
        private List<string> list_OriNo;

        public string Str_SavePath
        {
            get
            {
                if (!Directory.Exists(str_SavePath + "\\" + dirName))
                {
                    Directory.CreateDirectory(str_SavePath + "\\" + dirName);
                }
                return str_SavePath + "\\" + dirName;
            }

            set
            {

                str_SavePath = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _Btn_Path = GetComponentInChildren<Button>();
            _IF_Path = transform.Find("IF_Path").GetComponent<InputField>();
            _Text_Path = transform.Find("PathText").GetComponent<Text>();
            _Btn_Deal = transform.Find("Btn Deal").GetComponent<Button>();
            _IF_SavePath = transform.Find("IF_Savepath").GetComponent<InputField>();
            
            Str_SavePath = _IF_SavePath.text = PlayerPrefs.GetString("savepath");
            _IF_SavePath.onValueChanged.AddListener((str)=> 
            {
                Str_SavePath = str;
                PlayerPrefs.SetString("savepath", str);
            });

            _IF_Path.onValueChanged.AddListener((str) =>
            {
                str_Path = str;
            });

            _Btn_Deal.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(str_Path)) return;
                
                //记录文件夹名
                dirName = str_Path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last();

                //处理icon图命名
                ScaleAndSave(str_Path, Str_SavePath, "icon.png", string.Format("icon_{0}.png", dirName), 280);
                //处理outline命名
                ScaleAndSave(str_Path, Str_SavePath, "outline.png", string.Format("outline_{0}.png", dirName), 280);


                //获取所有part图的bitmap
                DirectoryInfo di = new DirectoryInfo(str_Path);
                var files = di.GetFiles();
                var partImgFiles =  files.Where(file => file.Name.Contains('_')).ToList();
                List<Bitmap> partBitmapList = new List<Bitmap>();
                list_OriNo = new List<string>();
                for (int i = 0; i < partImgFiles.Count; i++)
                {
                    partBitmapList.Add(null);
                    list_OriNo.Add(null);
                }

                foreach (var file in partImgFiles)
                {
                    var msg = file.Name.Split(new char[] { '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
                    var num = int.Parse(msg[0]) - 1;
                    partBitmapList[num] = new Bitmap(file.FullName);
                    list_OriNo[num] = msg[1];
                }

                //得到RGB图List
                var RGBBitmaps = RGBImageUtil.CreatePart2RGB(partBitmapList);

                //RGB图命名并保存
                for (int i = 0; i < RGBBitmaps.Count; i++)
                {
                    if (i == RGBBitmaps.Count - 1)
                    {
                        partBitmapList.Insert(0, RGBBitmaps[i]);
                        continue;
                    }

                    string name = string.Format("{0}\\{1}_{2}", Str_SavePath, "rgb", dirName);
                    for (int j = i * 3; j < (i + 1) * 3; j++)
                    {
                        name += j < list_OriNo.Count ? "_" + list_OriNo[j] : "_null";
                    }
                    name += "_" + i + ".png";

                    RGBBitmaps[i].Save(name);
                }

                //part图名字处理
                for (int i = 0; i < partBitmapList.Count; i++)
                {
                    partBitmapList[i].Black2Trans();
                    partBitmapList[i] = RGBImageUtil.UniformScale(partBitmapList[i], 280);
                    partBitmapList[i].Save(Str_SavePath + "\\" + string.Format("partimg_{0}_{1}.png", dirName, i));
                }

                //清除RGB Bitmap
                foreach (var item in RGBBitmaps)
                {
                    item.Dispose();
                }
                RGBBitmaps = null;

                //清除part Bitmap
                foreach (var item in partBitmapList)
                {
                    item.Dispose();
                }
                partBitmapList = null;

                //生成JSON
                OutputJson(Str_SavePath);

                //打包ZIP
                //BuildZip(str_SavePath + "\\" + string.Format("{0}.zip", dirName));
            });
        }


        void ScaleAndSave(string path, string savepath, string oriname, string newname, int length)
        {
            Bitmap bitmap = new Bitmap(string.Format("{0}\\{1}", path, oriname));
            bitmap = RGBImageUtil.UniformScale(bitmap, length);
            bitmap.Save(string.Format("{0}\\{1}", savepath, newname));

            bitmap.Dispose();
            bitmap = null;
        }

        void BuildZip(string path)
        {
            DirectoryInfo DI = new DirectoryInfo(path);

            var files = DI.GetFiles();
            var savePath = @"C:\Users\Administrator\Desktop";

            ZipUtil.ZipDir(DI.FullName, string.Format("{0}\\{1}.zip", savePath, DI.Name));
        }


        public void OutputJson(string path)
        {
            DirectoryInfo parentDir = new DirectoryInfo(path);
            FileInfo[] files = parentDir.GetFiles();

            var jsonData = new BDXPHJsonData();
            jsonData.type = 1;

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
            }

            var partImgFiles = files.Where(f => f.Name.Contains("partimg")).OrderBy(f => f.Name[f.Name.LastIndexOf('.') - 1]).ToArray();
            foreach (var file in partImgFiles)
            {
                jsonData.part_img_files.Add(file.Name.Substring(0, file.Name.LastIndexOf('.')));
            }

            var rgbFiles = files.Where(f => f.Name.Substring(0, 3) == "rgb").OrderBy(f => f.Name[f.Name.LastIndexOf('.') - 1]).ToArray();


            foreach (var rgbfile in rgbFiles)
            {
                jsonData.rgb_imgs.Add(rgbfile.Name.Substring(0, rgbfile.Name.LastIndexOf('.')));

                var arr = rgbfile.FullName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 2; i < arr.Length - 1; i++)
                {
                    jsonData.part_imgs.Add(arr[i]);
                }
            }

            jsonData.total_part_num = jsonData.part_img_files.Count - 1;

            string data = JsonUtility.ToJson(jsonData);

            string datapath = path + "\\data.json";
            StreamWriter sw = new StreamWriter(datapath, true);
            sw.WriteLine(data);
            sw.Dispose();
            sw.Close();
        }
    }


    [System.Serializable]
    public class BDXPHJsonData
    {
        public int type;
        public string outline_img;
        public string icon_img;
        public List<string> rgb_imgs = new List<string>();
        public int total_part_num;
        public List<string> part_img_files = new List<string>();
        public List<string> part_imgs = new List<string>();
    }
}