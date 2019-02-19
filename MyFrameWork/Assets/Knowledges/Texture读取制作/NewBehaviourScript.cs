using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RHFramework {
    public class NewBehaviourScript : MonoBehaviour
    {
        public static Texture2D DrawIcon(Texture2D[] RGBtextures, Texture2D[] textures, int finalWidth = 100, int finalHeight = 100)
        {
            //获得新图宽高
            var width = RGBtextures[0].width;
            var height = RGBtextures[0].height;

            //生成新texture2D
            var newTexture = new Texture2D(width, height);

            //计算像素
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var picLayerNum = -1;

                    for (int i = 0; i < RGBtextures.Length; i++)
                    {
                        Color tempColor = RGBtextures[i].GetPixel(x, y);

                        if (tempColor == Color.black)
                        {
                            continue;
                        }
                        else
                        {
                            if (tempColor.r != 0)
                            {
                                picLayerNum = i * 3 + 2;
                            }
                            else if (tempColor.g != 0)
                            {
                                picLayerNum = i * 3 + 1;
                            }
                            else if (tempColor.b != 0)
                            {
                                picLayerNum = i * 3;
                            }
                        }
                    }

                    var aimColor = picLayerNum == -1 ? Color.clear : textures[picLayerNum].GetPixel(x, y);

                    newTexture.SetPixel(x, y, aimColor);
                }
            }

            newTexture.Apply();

            newTexture = ScaleTexture(newTexture, finalWidth, finalHeight);

            SaveTexture2PNG(newTexture, Application.dataPath + "/" + "tempPng" + ".png");

            Debug.Log("finish");

            return newTexture;
        }

        private static void SaveTexture2PNG(Texture2D texture, string path)
        {
            byte[] bytes = texture.EncodeToPNG();

            FileStream file = File.Open(path, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(bytes);
            file.Close();

            texture = null;
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

        public static Texture2D DrawTexture2DBG(Texture2D texture2D, Color color)
        {
            var colors = new Color[texture2D.width * texture2D.height];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }

            texture2D.SetPixels(colors);

            texture2D.Apply();
            return texture2D;
        }

        public static void DrawPartPics(Texture2D[] RGBtextures, int finalWidth = 280, int finalHeight = 280)
        {
            //获得新图宽高
            var width = RGBtextures[0].width;
            var height = RGBtextures[0].height;

            List<Texture2D> textures = new List<Texture2D>();

            //计算像素
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var picLayerNum = -1;

                    for (int i = 0; i < RGBtextures.Length; i++)
                    {
                        Color tempColor = RGBtextures[i].GetPixel(x, y);

                        if (tempColor == Color.black)
                        {
                            continue;
                        }
                        else
                        {
                            if (tempColor.r != 0)
                            {
                                picLayerNum = i * 3 + 2;
                            }
                            else if (tempColor.g != 0)
                            {
                                picLayerNum = i * 3 + 1;
                            }
                            else if (tempColor.b != 0)
                            {
                                picLayerNum = i * 3;
                            }
                        }
                    }

                    if (picLayerNum != -1)
                    {
                        while (picLayerNum + 1 > textures.Count)
                        {
                            var texture2D = new Texture2D(width, height);
                            texture2D = DrawTexture2DBG(texture2D, new Color(0.94f, 0.94f, 0.94f));
                            textures.Add(texture2D);
                        }

                        textures[picLayerNum].SetPixel(x, y, new Color(0.4f, 0.4f, 0.4f));
                    }
                }
            }

            for (int i = 0; i < textures.Count; i++)
            {
                textures[i] = ScaleTexture(textures[i], finalWidth, finalHeight);

                SaveTexture2PNG(textures[i], Application.dataPath + "/" + i + ".png");

                textures[i] = null;
            }

            Debug.Log("finish");
        }
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyKnowledge/1_", false, 1)]
#endif
        private static void MenuClicked()
        {
            var texture2D1 = Resources.Load<Texture2D>("28");
            var texture2D2 = Resources.Load<Texture2D>("28_1");
            var pic1 = Resources.Load<Texture2D>("pic3");
            var pic2 = Resources.Load<Texture2D>("pic2");
            var pic3 = Resources.Load<Texture2D>("pic1");
            var pic4 = Resources.Load<Texture2D>("pic5");
            var pic5 = Resources.Load<Texture2D>("pic4");

            //DrawIcon(new Texture2D[] { texture2D1, texture2D2 }, new Texture2D[] { pic1, pic2, pic3, pic4, pic5 });
            DrawPartPics(new Texture2D[] { texture2D1, texture2D2 });

            texture2D1 = texture2D2 = null;
            pic1 = pic2 = pic3 = pic4 = pic5 = null;
            
            DestroyImmediate(texture2D1, true);
            DestroyImmediate(texture2D2, true);
            DestroyImmediate(pic1, true);
            DestroyImmediate(pic2, true);
            DestroyImmediate(pic3, true);
            DestroyImmediate(pic4, true);
            DestroyImmediate(pic5, true);

            Resources.UnloadUnusedAssets();

        }

    }
}