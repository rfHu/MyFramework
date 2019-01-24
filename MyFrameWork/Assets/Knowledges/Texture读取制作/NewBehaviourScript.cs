using System.IO;
using UnityEngine;

namespace RHFramework {
    public class NewBehaviourScript : MonoBehaviour
    {
        public static void GetAllTexturePixels(Texture2D texture2D)
        {

            var pic1 = Resources.Load<Texture2D>("pic1");
            var pic2 = Resources.Load<Texture2D>("pic2");
            var pic3 = Resources.Load<Texture2D>("pic3");


            var colorArr = texture2D.GetPixels();


            for (int y = 0; y < texture2D.height; y++)
            {
                for (int x = 0; x < texture2D.width; x++)
                {
                    var tempColor = colorArr[x + y * texture2D.width];

                    if (tempColor.b != 0)
                    {
                        if (tempColor.g != 0)
                        {
                            texture2D.SetPixel(x, y, pic1.GetPixel(x, y));
                        }
                        else if (tempColor.r != 0)
                        {
                            texture2D.SetPixel(x, y, pic2.GetPixel(x, y));
                        }
                        else
                        {
                            texture2D.SetPixel(x, y, pic3.GetPixel(x, y));
                        }
                    }
                }
            }

            //texture2D = ScaleTexture(texture2D, 100, 100);

            byte[] bytes = texture2D.EncodeToPNG();

            FileStream file = File.Open(Application.dataPath + "/" + "tempPng" + ".png", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(bytes);
            file.Close();
            texture2D = null;

            Debug.Log("finish");
        }

        /// <summary>
        /// 缩放Texture2D
        /// </summary>
        /// <param name="source">目标Texture2D</param>
        /// <param name="targetWidth">目标宽</param>
        /// <param name="targetHeight">目标高</param>
        /// <returns>缩放后的Texture2D</returns>
        public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
        {
            Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

            for (int i = 0; i < result.height; ++i)
            {
                for (int j = 0; j < result.width; ++j)
                {
                    Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                    result.SetPixel(j, i, newColor);
                }
            }

            result.Apply();
            return result;
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyKnowledge/1_", false, 1)]
        private static void MenuClicked()
        {
            var texture2D = Resources.Load<Texture2D>("28");
            GetAllTexturePixels(texture2D);
        }
#endif
    }
}