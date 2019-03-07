using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;

namespace RHFramework
{
    public class DecompressTest : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyKnowledge/2.解压测试", false, 2)]
        private static void MenuClicked()
        {
            Stream expectedStream = new FileStream(@"C:\Users\Administrator\Desktop\PH - 副本\OutPut\aaa.zip", FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] expectedBytes = new byte[expectedStream.Length];
            expectedStream.Read(expectedBytes, 0, expectedBytes.Length);
            expectedStream.Dispose();
            expectedStream.Close();

            var dic = ZipUtil.GetDecZipByteList(expectedBytes);

            var t2d = Bytes2Texture2D(dic["partimg_aaa_0.png"]);
            Sprite sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
            GameObject.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = sprite;

        }
#endif

        public static Texture2D Bytes2Texture2D(byte[] streamByte)
        {
            MemoryStream ms = new MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            Texture2D t2d = new Texture2D(img.Width, img.Height);
            img.Dispose();
            t2d.LoadImage(streamByte);
            t2d.Apply();
            return t2d;
        }
    }
}