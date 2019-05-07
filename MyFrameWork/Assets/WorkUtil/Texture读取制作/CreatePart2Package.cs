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
        private InputField _IF_OriNo;

        private string str_Path;
        private List<string> list_OriNo = new List<string>();

        // Start is called before the first frame update
        void Start()
        {
            _Btn_Path = GetComponentInChildren<Button>();
            _IF_Path = transform.Find("IF_Path").GetComponent<InputField>();
            _Text_Path = transform.Find("PathText").GetComponent<Text>();
            _Btn_Deal = transform.Find("Btn Deal").GetComponent<Button>();
            _IF_OriNo = transform.Find("IF_PartOri").GetComponent<InputField>();

            _IF_OriNo.onValueChanged.AddListener((str)=> 
            {
                list_OriNo = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            });

            _IF_Path.onValueChanged.AddListener((str) =>
            {
                str_Path = str;
            });

            //_Btn_Path.onClick.AddListener(() =>
            //{
            //    DllOpenFileDialog.SelectDir((path =>
            //    {
            //        str_Path = path;
            //        _Text_Path.text = str_Path;
            //    }));

            //});

            _Btn_Deal.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(str_Path)) return;



                string dirName = str_Path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last();

                string path1 = str_Path + "\\icon.png";
                string path2 = string.Format("{0}\\icon_{1}.png", str_Path, dirName);

                //处理icon图命名
                ScaleAndSave(str_Path, "icon.png", string.Format("icon_{0}.png", dirName), 280);
                //处理outline命名
                ScaleAndSave(str_Path, "outline.png", string.Format("outline_{0}.png", dirName), 280);


                //获取所有part图的bitmap
                List<Bitmap> partBitmapList = new List<Bitmap>();
                for (int i = 0; File.Exists(string.Format(@"{0}\\{1}.png", str_Path, i + 1)); i++)
                {
                    partBitmapList.Add(new Bitmap(string.Format(@"{0}\\{1}.png", str_Path, i + 1)));
                }

                //得到RGB图List
                var RGBBitmaps = RGBImageUtil.CreatePart2RGB(partBitmapList);

                //RGB图命名并保存
                for (int i = 0; i < RGBBitmaps.Count; i++)
                {
                    if (i == RGBBitmaps.Count - 1)
                    {
                        string bgname = string.Format("{0}\\0.png", str_Path);
                        RGBBitmaps[i].Save(bgname);
                        continue;
                    }

                    string name = string.Format("{0}\\{1}_{2}", str_Path, "rgb", dirName);
                    for (int j = i * 3; j < (i + 1) * 3; j++)
                    {
                        name += j < list_OriNo.Count ? "_" + list_OriNo[j] : "_null";
                    }
                    name += "_" + i + ".png";

                    RGBBitmaps[i].Save(name);
                }

                //清除IO
                foreach (var item in RGBBitmaps)
                {
                    item.Dispose();
                }
                RGBBitmaps = null;
                foreach (var item in partBitmapList)
                {
                    item.Dispose();
                }
                partBitmapList = null;

                //part图名字处理
                for (int i = 0; File.Exists(string.Format(@"{0}\{1}.png", str_Path, i)); i++)
                {
                    ScaleAndSave(str_Path, string.Format(@"{0}.png", i), string.Format("partimg_{0}_{1}.png", dirName, i + 1), 280);
                }


                //生成JSON
                OutputJson(str_Path);

                //打包ZIP
                //BuildZip(str_Path);

                //icon图片复制
                //File.Copy(path2, @"C:\Users\Administrator\Desktop" + "\\icon_" + dirName + ".png");
            });
        }

        void ScaleAndSave(string path, string oriname, string newname, int length)
        {
            Bitmap bitmap = new Bitmap(string.Format("{0}\\{1}", path, oriname));
            bitmap = RGBImageUtil.UniformScale(bitmap, length);
            bitmap.Save(string.Format("{0}\\{1}", path, newname));

            bitmap.Dispose();
            bitmap = null;
            File.Delete(string.Format("{0}\\{1}", path, oriname));
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