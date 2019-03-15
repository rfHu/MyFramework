using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using UnityEngine;

namespace RHFramework {
    public class NewBehaviourScript : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyKnowledge/1_", false, 1)]
#endif
        private static void MenuClicked()
        {
            var texture2D1 = Resources.Load<Texture2D>("28");
            var texture2D2 = Resources.Load<Texture2D>("28_1");

            //DrawIcon(new Texture2D[] { texture2D1, texture2D2 }, new Texture2D[] { pic1, pic2, pic3, pic4, pic5 });
            //RGBImageUtil.DrawPartPics(new Texture2D[] { texture2D1, texture2D2 });
            //RGBImageUtil.DrawPartImg(new Bitmap[] 
            //{
            //    new Bitmap(@"E:\STClone\MyFramework\MyFrameWork\Assets\Knowledges\Texture读取制作\Resources\28.png"),
            //    new Bitmap(@"E:\STClone\MyFramework\MyFrameWork\Assets\Knowledges\Texture读取制作\Resources\28_1.png")
            //});

            texture2D1 = texture2D2 = null;

            DestroyImmediate(texture2D1, true);
            DestroyImmediate(texture2D2, true);

            Resources.UnloadUnusedAssets();

        }

    }

    public static class RGBImageUtil
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
                        UnityEngine.Color tempColor = RGBtextures[i].GetPixel(x, y);

                        if (tempColor == UnityEngine.Color.black)
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

                    var aimColor = picLayerNum == -1 ? UnityEngine.Color.clear : textures[picLayerNum].GetPixel(x, y);

                    newTexture.SetPixel(x, y, aimColor);
                }
            }

            newTexture.Apply();

            newTexture = TextureUtil.ScaleTexture(newTexture, finalWidth, finalHeight);

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

        public static void DrawPartImg(Bitmap[] RGBImages, string savePath, string savename)
        {
            //获得新图宽高
            var width = RGBImages[0].Width;
            var height = RGBImages[0].Height;

            List<Bitmap> images = new List<Bitmap>();

            //计算像素
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var picLayerNum = -1;

                    for (int i = 0; i < RGBImages.Length; i++)
                    {
                        System.Drawing.Color tempColor = RGBImages[i].GetPixel(x, y);

                        if (tempColor == System.Drawing.Color.Black)
                        {
                            continue;
                        }
                        else
                        {
                            if (tempColor.R > 240)
                            {
                                picLayerNum = i * 3 + 3;
                            }
                            else if (tempColor.G > 240)
                            {
                                picLayerNum = i * 3 + 2;
                            }
                            else if (tempColor.B > 240)
                            {
                                picLayerNum = i * 3 + 1;
                            }
                        }
                    }

                    if (picLayerNum != -1)
                    {
                        while (picLayerNum + 1 > images.Count)
                        {
                            var image = new Bitmap(width, height);
                            image = DrawWgoleImageColor(image, System.Drawing.Color.FromArgb(0,0,0,0));
                            images.Add(image);
                        }
                        images[0].SetPixel(x, y, ColorTranslator.FromHtml("#ffffff"));
                        images[picLayerNum].SetPixel(x, y, ColorTranslator.FromHtml("#ffffff"));
                    }
                }
            }

            for (int i = 0; i < images.Count; i++)
            {
                SaveImagePNG(images[i], string.Format("{0}/partimg_{1}_{2}.png", savePath, savename, i));

                images[i].Dispose();
            }

            Debug.Log("finish");
        }

        public static Bitmap DrawWgoleImageColor(Bitmap image, System.Drawing.Color color)
        {
            var colors = new System.Drawing.Color[image.Width * image.Height];

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    image.SetPixel(i,j,color);
                }
            }
            
            return image;
        }

        /// <summary>
        /// 等比缩放至长边等于LongLength
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="longLength"></param>
        /// <returns></returns>
        public static Bitmap UniformScale(Bitmap bitmap, int longLength)
        {
            if (bitmap.Width == bitmap.Height)
            {
                return ScaleToSize(bitmap, longLength, longLength);
            }
            else if (bitmap.Width > bitmap.Height)
            {

                return ScaleToSize(bitmap, longLength, longLength * bitmap.Height / bitmap.Width);
            }
            else if (bitmap.Width < bitmap.Height)
            {

                return ScaleToSize(bitmap, longLength * bitmap.Width / bitmap.Height, longLength);
            }
            else
            {
                return bitmap;
            }
        }

        public static Bitmap ScaleToSize(Bitmap bitmap, int width, int height)
        {
            if (bitmap.Width == width && bitmap.Height == height)
            {
                return bitmap;
            }

            var scaledBitmap = new Bitmap(width, height);
            using (var g = System.Drawing.Graphics.FromImage(scaledBitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bitmap, 0, 0, width, height);
            }

            return scaledBitmap;
        }

        private static void SaveImagePNG(Bitmap image, string path)
        {
            image.Save(path);
        }
    }
}