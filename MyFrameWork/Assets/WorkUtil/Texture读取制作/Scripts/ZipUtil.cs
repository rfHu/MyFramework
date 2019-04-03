using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class ZipUtil
{
    #region 压缩
    /// <summary>
    /// 压缩文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="outFolder"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string CompressZip(string filePath, string outFolder, string password = null)
    {
        if (!File.Exists(filePath))
        {
            UnityEngine.Debug.LogError("需要压缩的文件不存在");
            return null;
        }

        string zipFileName = outFolder + Path.GetFileNameWithoutExtension(filePath) + ".zip";

        using (FileStream fs = File.Create(zipFileName))
        {
            using (ZipOutputStream zipStream = new ZipOutputStream(fs))
            {
                zipStream.Password = password;
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    string fileName = Path.GetFileName(filePath);
                    ZipEntry zipEntry = new ZipEntry(fileName);
                    zipStream.PutNextEntry(zipEntry);
                    byte[] buffer = new byte[1024];
                    int sizeRead = 0;
                    try
                    {
                        do
                        {
                            sizeRead = stream.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, sizeRead);
                        } while (sizeRead > 0);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                    stream.Close();
                }
                zipStream.Finish();
                zipStream.Close();
            }
            fs.Close();
            return zipFileName;
        }
    }

    /// <summary>
    /// 压缩多层目录 压缩文件夹
    /// </summary>
    /// <param name="dir">The directory.</param>
    /// <param name="zipFilePath">The ziped file.</param>
    public static void ZipDir(string dir, string zipFilePath)
    {
        using (System.IO.FileStream ZipFile = System.IO.File.Create(zipFilePath))
        {
            using (ZipOutputStream s = new ZipOutputStream(ZipFile))
            {
                ZipDirDetail(dir, s, "");
            }
        }
    }

    /// <summary>
    /// 递归遍历目录
    /// </summary>
    /// <param name="dir">The directory.</param>
    /// <param name="outStream">The ZipOutputStream Object.</param>
    /// <param name="parentPath">The parent path.</param>
    private static void ZipDirDetail(string dir, ZipOutputStream outStream, string parentPath)
    {
        if (dir[dir.Length - 1] != Path.DirectorySeparatorChar)
        {
            dir += Path.DirectorySeparatorChar;
        }
        Crc32 crc = new Crc32();
        //            outStream.SetLevel(6); // 0 - store only to 9 - means best compression  
        Encoding gbk = Encoding.GetEncoding("gbk"); // 防止中文名乱码  
        ICSharpCode.SharpZipLib.Zip.ZipConstants.DefaultCodePage = gbk.CodePage;


        string[] filenames = Directory.GetFileSystemEntries(dir);

        foreach (string file in filenames) // 遍历所有的文件和目录
        {
            if (Directory.Exists(file)) // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
            {
                string pPath = parentPath;
                pPath += file.Substring(file.LastIndexOf("\\") + 1);
                pPath += "\\";
                ZipDirDetail(file, outStream, pPath);
            }

            else // 否则直接压缩文件
            {
                //打开压缩文件
                using (FileStream fs = File.OpenRead(file))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);

                    string fileName = parentPath + file.Substring(file.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(fileName);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;

                    fs.Close();

                    crc.Reset();
                    crc.Update(buffer);

                    entry.Crc = crc.Value;
                    outStream.PutNextEntry(entry);

                    outStream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
    #endregion

    #region 解压
    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="outFolder"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static bool DecompressZip(string filePath, string outFolder, string password = null)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            Debug.LogError("请检查路径:" + filePath);
            return false;
        }
        return DecompressZip(File.OpenRead(filePath), outFolder, password);
    }

    /// <summary>
    /// 解压下载的byte[] 
    /// </summary>
    /// <param name="ZipByte"></param>
    /// <param name="outFolder"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static bool DecompressZip(byte[] ZipByte, string outFolder, string password)
    {
        return DecompressZip(new MemoryStream(ZipByte), outFolder, password);
    }

    /// <summary>
    /// 详细解压
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="outFolder"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private static bool DecompressZip(Stream stream, string outFolder, string password)
    {
        bool result = false;
        //string outPath = null;
        FileStream fs = null;
        ZipInputStream zipStream = null;
        ZipEntry ent = null;
        string fileName = null;

        if (!Directory.Exists(outFolder))
        {
            Directory.CreateDirectory(outFolder);
        }
        // 防止中文名乱码  !!!
        Encoding gbk = Encoding.GetEncoding("gbk");
        ICSharpCode.SharpZipLib.Zip.ZipConstants.DefaultCodePage = gbk.CodePage;
        try
        {
            zipStream = new ZipInputStream(stream);

            if (!string.IsNullOrEmpty(password))
            {
                Debug.Log("use password");
                zipStream.Password = password;
            }

            while ((ent = zipStream.GetNextEntry()) != null)
            {
                if (!string.IsNullOrEmpty(ent.Name))
                {
                    fileName = Path.Combine(outFolder, ent.Name);

                    #region Android

                    fileName = fileName.Replace('\\', '/');

                    if (fileName.EndsWith("/"))
                    {
                        Directory.CreateDirectory(fileName);
                        continue;
                    }

                    #endregion

                    if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                    }
                    fs = File.Create(fileName);
                    int size = 2048;
                    byte[] data = new byte[size];


                    while (true)
                    {
                        size = zipStream.Read(data, 0, data.Length);

                        if (size > 0)
                        {
                            fs.Write(data, 0, data.Length);
                        }
                        else
                            break;
                    }
                }
            }
            result = true;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
            }
            if (zipStream != null)
            {
                zipStream.Close();
                zipStream.Dispose();
            }
            if (ent != null)
            {
                ent = null;
            }
            GC.Collect();
            GC.Collect(1);
        }
        return result;
    }
    #endregion

    #region 解压到内存
    public static Dictionary<string, byte[]> GetDecZipByteList(byte[] ZipByte)
    {
        Dictionary<string, byte[]> abData = new Dictionary<string, byte[]>();
        Stream stream = new MemoryStream(ZipByte);

        ZipInputStream zipStream = null;
        ZipEntry ent = null;
        string fileName = null;

        try
        {
            zipStream = new ZipInputStream(stream);
            while ((ent = zipStream.GetNextEntry()) != null)
            {
                if (!string.IsNullOrEmpty(ent.Name))
                {
                    fileName = ent.Name;
                }

                byte[] data = new byte[zipStream.Length];
                int size = zipStream.Read(data, 0, data.Length);
                abData.Add(fileName, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        finally
        {
            if (zipStream != null)
            {
                zipStream.Close();
                zipStream.Dispose();
            }
            if (ent != null)
            {
                ent = null;
            }
            GC.Collect();
            GC.Collect(1);
        }
        return abData;
    }

    public static AB_Data GetDecZipAB_Data(byte[] ZipByte)
    {
        AB_Data abData = new AB_Data();
        Stream stream = new MemoryStream(ZipByte);

        ZipInputStream zipStream = null;
        ZipEntry ent = null;
        string fileName = null;

        try
        {
            zipStream = new ZipInputStream(stream);
            while ((ent = zipStream.GetNextEntry()) != null)
            {
                if (!string.IsNullOrEmpty(ent.Name))
                {
                    fileName = ent.Name;
                }
                if (!fileName.Contains(".manifest"))
                {
                    byte[] data = new byte[zipStream.Length];
                    int size = zipStream.Read(data, 0, data.Length);

                    abData.name = fileName;
                    abData.binary = data;
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        finally
        {
            if (zipStream != null)
            {
                zipStream.Close();
                zipStream.Dispose();
            }
            if (ent != null)
            {
                ent = null;
            }
            GC.Collect();
            GC.Collect(1);
        }
        return abData;
    }
    #endregion

    public struct AB_Data
    {
        public string name;
        public byte[] binary;
    }
}
