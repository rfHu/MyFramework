using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenDialogDir
{
    public IntPtr hwndOwner = IntPtr.Zero;
    public IntPtr pidlRoot = IntPtr.Zero;
    public String pszDisplayName = null;
    public String lpszTitle = null;
    public UInt32 ulFlags = 0;
    public IntPtr lpfn = IntPtr.Zero;
    public IntPtr lParam = IntPtr.Zero;
    public int iImage = 0;
}

public static class DllOpenFileDialog
{
    //链接指定系统函数       打开文件对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);//执行打开文件的操作
    }

    //链接指定系统函数        另存为对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
    public static bool GetSFN([In, Out] OpenFileName ofn)
    {
        return GetSaveFileName(ofn);//执行保存选中文件的操作
    }

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern IntPtr SHBrowseForFolder([In, Out] OpenDialogDir ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);

    public static void SelectFile(string type, Action action)
    {
        var openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "文件(*." + type + ")\0*." + type + "";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = "选择文件";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (DllOpenFileDialog.GetSaveFileName(openFileName))//点击系统对话框框保存按钮
        {
            action();
        }
    }
    
    public static void SelectDir(Action<string> action)
    {
        OpenDialogDir ofn = new OpenDialogDir();
        ofn.pszDisplayName = new string(new char[2000]);     // 存放目录路径缓冲区  
        ofn.lpszTitle = "选择文件夹";// 标题  
        IntPtr pidlPtr = DllOpenFileDialog.SHBrowseForFolder(ofn);

        char[] charArray = new char[2000];
        for (int i = 0; i < 2000; i++)
            charArray[i] = '\0';

        if (DllOpenFileDialog.SHGetPathFromIDList(pidlPtr, charArray))
        {
            string str = "";
            for (int i = 0; i < 2000; i++)
                if (charArray[i] != '\0')
                {
                    str += charArray[i];
                }

            action(new string(charArray));
        }
        //DllOpenFileDialog.SHGetPathFromIDList(pidlPtr, charArray);
        //string fullDirPath = new String(charArray);
    }
}
