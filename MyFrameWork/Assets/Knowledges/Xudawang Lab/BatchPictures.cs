using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using UnityEditor;
using Zgke.MyImage.ImageFile;

public class BatchPictures : EditorWindow
{
    enum BatchImageFormat
    {
        PNG,
        JPG,
        TGA,
    }

    [MenuItem("MyKnowledge/BatchPictures", false, 3)]
    static void Init()
    {
        Rect rect = new Rect(0, 0, 500, 300);
        BatchPictures myWindow = (BatchPictures)EditorWindow.GetWindowWithRect(typeof(BatchPictures), rect, false, "BatchPictures");//创建窗口
        myWindow.Show();//展示
    }

    string parentPath;
    string xlsxPath { get { return string.Format("{0}\\配置表.xlsx", parentPath); } }
    string imagesParentPath { get { return string.Format("{0}\\Pictures", parentPath); } }

    private BatchImageFormat batchImageFormat = 0;
    List<ExcelData> datas;

    private void Awake()
    {

    }

    private void OnGUI()
    {
        GUILayout.Label("请在目录下放置“配置表.xlsx”和“Pictures”文件夹");
        parentPath = EditorGUILayout.TextField("选择目录", parentPath);
        batchImageFormat = (BatchImageFormat)EditorGUILayout.EnumPopup("图片格式：", batchImageFormat);
        
    }


    private Dictionary<int, Vector2> dic_WidthHeight = new Dictionary<int, Vector2>()
    {
        { 1 , new Vector2(1,1)},
        { 2 , new Vector2(2,1)},
        { 3 , new Vector2(3,1)},
        { 4 , new Vector2(2,2)},
        { 6 , new Vector2(3,2)},
        { 8 , new Vector2(4,2)},
        { 9 , new Vector2(3,3)},
        { 12 , new Vector2(4,3)},
        { 16 , new Vector2(4,4)},
    };

    

    private void Start()
    {
        datas = JsonToObject.JsonToObject_ByJsonFile<ExcelData>(xlsxPath);
        Debug.Log(datas.Count);
        List<string> ll = new List<string>();
        foreach (var item in datas)
        {
            if (ll.Contains(item.id))
            {
                Debug.LogError(string.Format("{0}: --this ID Already have", item.id));
                WriteErrorDirectory(string.Format("{0}: --this ID Already have", item.id), parentPath);
            }
        }

        GetAllDirectory(datas, @"E:\STClone\MyFramework\简一瓷砖ID");
        OrganizePictures(@"E:\STClone\MyFramework\简一瓷砖ID");
        MosaicPicturesToOne(@"E:\STClone\MyFramework\简一瓷砖ID", datas);
    }


    /// <summary>
    /// Stap1
    /// </summary>
    /// <param name="datas"></param>
    /// <param name="imagesParentPath"></param>
    private void GetAllDirectory(List<ExcelData> datas, string imagesParentPath)
    {
        foreach (var data in datas)
        {
            if (data.matType == "有")
            {
                string path = string.Format("{0}\\{1}", imagesParentPath, data.id);
                if (Directory.Exists(path))
                {
                    Debug.Log(path);
                }
                else
                {
                    WriteErrorDirectory(string.Format("{0}: --Cant Find ID", data.id), imagesParentPath);

                    Debug.LogError(string.Format("{0}: --Cant Find ID" , data.id));
                }
            }
            else if (data.matType == "复制")
            {
                var copyids = datas.Where(d => d.productNum == data.productNum && d.id != data.id && d.matType == "有").ToArray();
                string copyid = "?";
                if (copyids.Length != 0)
                {
                    copyid = copyids[0].id;
                }

                if (!string.IsNullOrEmpty(copyid))
                {
                    if (Directory.Exists(string.Format("{0}\\{1}", imagesParentPath, copyid)))
                    {
                        CopyFolder(string.Format("{0}\\{1}", imagesParentPath, copyid), string.Format("{0}\\{1}", imagesParentPath, data.id));
                        Debug.Log(imagesParentPath + data.id);
                    }
                    else
                    {
                        WriteErrorDirectory(string.Format("{0}: --Cant Find ID, copy : {1}", data.id, copyid), imagesParentPath);
                        Debug.LogError(string.Format("{0}: --Cant Find ID, copy : {1}", data.id, copyid));
                    }
                }
                else
                {
                    WriteErrorDirectory(string.Format("{0}: --Cant Find ID, copy : {1}", data.id, copyid), imagesParentPath);
                    Debug.LogError(string.Format("{0}: --Cant Find ID, copy : {1}", data.id, copyid));
                }
            }
        }
    }

    /// <summary>
    /// Stap2
    /// </summary>
    /// <param name="imagesParentPath"></param>
    private void OrganizePictures(string imagesParentPath)
    {
        DirectoryInfo parentDir = new DirectoryInfo(imagesParentPath);

        DirectoryInfo[] DirSub = parentDir.GetDirectories();

        foreach (var DI in DirSub)
        {
            DirectoryInfo tempDir = new DirectoryInfo(string.Format("{0}\\{1}", imagesParentPath , DI.Name));

            var minWidth = int.MaxValue;
            var maxWidth = int.MinValue;
            var minHeight = int.MaxValue;
            var maxHeight = int.MinValue;

            var files = tempDir.GetFiles("*.jpg");
            for (int i = 0; i < files.Length; i++)
            {
                Image img = Image.FromFile(files[i].FullName);
                minWidth = minWidth > img.Width ? img.Width : minWidth;
                maxWidth = maxWidth < img.Width ? img.Width : maxWidth;
                minHeight = minHeight > img.Height ? img.Height : minHeight;
                maxHeight = maxHeight < img.Height ? img.Height : maxHeight;
                img.Dispose();
            }

            if ((float)minWidth / (float)maxWidth < 0.8f || (float)minHeight / (float)maxHeight < 0.8f)
            {
                WriteErrorDirectory(string.Format("{0}: --In Directory, Picture size difference is too large", DI.Name), imagesParentPath);

                Debug.LogError(string.Format("{0}: --In Directory, Picture size difference is too large", DI.Name));
            }
            else
            {
                for (int i = 0; i < files.Length; i++)
                {
                    CaptureImage(files[i].FullName, 0, 0, string.Format("{0}\\{1}_{2}.jpg", DI.FullName, DI.Name, i + 1), minWidth, minHeight);
                    File.Delete(files[i].FullName);
                }
            }
        }
        Debug.Log("finish");
    }

    /// <summary>
    /// Stap3
    /// </summary>
    /// <param name="imagesParentPath"></param>
    /// <param name="datas"></param>
    private void MosaicPicturesToOne(string imagesParentPath, List<ExcelData> datas)
    {
        var arr_ErrorDirs = ReadAllErrorDirectory(imagesParentPath);

        DirectoryInfo parentDir = new DirectoryInfo(imagesParentPath);

        DirectoryInfo[] DirSub = parentDir.GetDirectories();

        foreach (var DI in DirSub)
        {
            if (arr_ErrorDirs.Contains(DI.Name))
            {
                continue;
            }

            DirectoryInfo tempDir = new DirectoryInfo(string.Format("{0}\\{1}", imagesParentPath, DI.Name));

            var Images = tempDir.GetFiles("*.jpg");

            if (!dic_WidthHeight.ContainsKey(Images.Length))
            {
                WriteErrorDirectory(string.Format("{0}: --Wrong picture number", DI.Name), imagesParentPath);
                Debug.LogError(string.Format("{0}: --Wrong picture number", DI.Name));
                continue;
            }
            else
            {
                var vactor = dic_WidthHeight[Images.Length];
                var data = datas.Where(d => d.id == DI.Name).First();
                MosaicImage(Images, string.Format("{0}\\finalpic", imagesParentPath ),string.Format("{0}_{1}_{2}_{3}_{4}.png", data.id, data.width, data.depth, vactor.x, vactor.y), (int)vactor.x, (int)vactor.y);

            }
        }

        Debug.Log("finish");
    }

    private string[] ReadAllErrorDirectory(string imagesParentPath)
    {
        string txtPath = imagesParentPath + "\\" + "ErrorDirectory.txt";

        if (!File.Exists(txtPath))
        {
            return new string[0] { };
        }

        StreamReader sr = new StreamReader(txtPath);

        var str = sr.ReadToEnd();
        var arr = str.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var finalArr = new string[arr.Length];

        for (int i = 0; i < arr.Length; i++)
        {
            finalArr[i] = arr[i].Substring(0, arr[i].IndexOf(':'));
        }

        sr.Dispose();
        sr.Close();
        return finalArr;
    }

    private void WriteErrorDirectory(string str, string parentPath)
    {
        string txtPath = parentPath + "\\" + "ErrorDirectory.txt";

        StreamWriter sw = new StreamWriter(txtPath, true);

        sw.WriteLine(str);

        sw.Dispose();
        sw.Close();
    }

    #region 从大图中截取一部分图片
    /// <summary>
    /// 从大图中截取一部分图片
    /// </summary>
    /// <param name="fromImagePath">来源图片地址</param>        
    /// <param name="offsetX">从偏移X坐标位置开始截取</param>
    /// <param name="offsetY">从偏移Y坐标位置开始截取</param>
    /// <param name="toImagePath">保存图片地址</param>
    /// <param name="width">保存图片的宽度</param>
    /// <param name="height">保存图片的高度</param>
    /// <returns></returns>
    public void CaptureImage(string fromImagePath, int offsetX, int offsetY, string toImagePath, int width, int height)
    {
        //原图片文件
        Image fromImage = Image.FromFile(fromImagePath);
        //创建新图位图
        Bitmap bitmap = new Bitmap(width, height);
        //创建作图区域
        System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);
        //截取原图相应区域写入作图区
        graphic.DrawImage(fromImage, (bitmap.Width - width)/2, (bitmap.Height - height)/2,  width, height);
        //从作图区生成新图
        Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
        //保存图片
        saveImage.Save(toImagePath, ImageFormat.Jpeg);
        //释放资源   
        fromImage.Dispose();
        saveImage.Dispose();
        graphic.Dispose();
        bitmap.Dispose();
    }
    #endregion
    public void MosaicImage(FileInfo[] Images, string toImagePath, string name, int widthCount, int heightCount)
    {
        Image getWHImg = Image.FromFile(Images[0].FullName);

        var width = getWHImg.Width; 
        var height = getWHImg.Height;

        getWHImg.Dispose();

        //创建新图位图
        Bitmap bitmap = new Bitmap(width * widthCount, height * heightCount);
        //创建作图区域
        System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);
        
        for (int i = 0; i < widthCount; i++)
        {
            for (int j = 0; j < heightCount; j++)
            {
                Image tempImage = Image.FromFile(Images[heightCount * i + j].FullName);
                graphic.DrawImage(tempImage, i * width, j * height, width, height);
                tempImage.Dispose();
            }
        }
        //保存图片
        if (!Directory.Exists(toImagePath))
        {
            Directory.CreateDirectory(toImagePath);
        }

        if (batchImageFormat == BatchImageFormat.JPG)
        {
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            saveImage.Save(string.Format("{0}\\{1}", toImagePath, name), ImageFormat.Png);
            saveImage.Dispose();
        }
        else if (batchImageFormat == BatchImageFormat.PNG)
        {
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            saveImage.Save(string.Format("{0}\\{1}", toImagePath, name), ImageFormat.Jpeg);
            saveImage.Dispose();
        }
        else if (batchImageFormat == BatchImageFormat.TGA)
        {
            ImageTGA _Tga = new ImageTGA();
            _Tga.Image = bitmap;
            //保存图片
            if (!Directory.Exists(toImagePath))
            {
                Directory.CreateDirectory(toImagePath);
            }
            _Tga.SaveImage(string.Format("{0}\\{1}", toImagePath, name));
        }
        
        //释放资源   
        graphic.Dispose();
        bitmap.Dispose();
    }

    private static void CopyFolder(string from, string to)
    {
        if (!Directory.Exists(to))
            Directory.CreateDirectory(to);
        // 子文件夹
        foreach (string sub in Directory.GetDirectories(from))
            CopyFolder(sub + "\\", to + "\\" + Path.GetFileName(sub) + "\\");
        // 文件
        foreach (string file in Directory.GetFiles(from))
            File.Copy(file, to + "\\" + Path.GetFileName(file), true);
    }
}

public class ExcelData
{
    public string productSeries;
    public string color;
    public string productNameCHS;
    public string productNameEn;
    public string id;
    public string productNum;
    public string width;
    public string depth;
    public string price;
    public string matType;
}



