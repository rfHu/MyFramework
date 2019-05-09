using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using UnityEngine;

namespace RHFramework
{

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

        public static void CreatePartImagesFromRGB(List<Bitmap> RGBBitmaps, string savePath, string savename)
        {
            //获得新图宽高
            var width = RGBBitmaps[0].Width;
            var height = RGBBitmaps[0].Height;

            List<Bitmap> createBitmaps = new List<Bitmap>();

            //计算像素
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var picLayerNum = -1;

                    for (int i = 0; i < RGBBitmaps.Count; i++)
                    {
                        System.Drawing.Color tempColor = RGBBitmaps[i].GetPixel(x, y);

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
                        while (picLayerNum + 1 > createBitmaps.Count)
                        {
                            var image = new Bitmap(width, height);
                            image = DrawWholeBitmapColor(image, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                            createBitmaps.Add(image);
                        }
                        createBitmaps[0].SetPixel(x, y, ColorTranslator.FromHtml("#ffffff"));
                        createBitmaps[picLayerNum].SetPixel(x, y, ColorTranslator.FromHtml("#ffffff"));
                    }
                }
            }

            for (int i = 0; i < createBitmaps.Count; i++)
            {
                SaveBitmap2PNG(createBitmaps[i], string.Format("{0}/partimg_{1}_{2}.png", savePath, savename, i));

                createBitmaps[i].Dispose();
                createBitmaps[i] = null;
            }

            createBitmaps = null;
            Debug.Log("finish");
        }

        public static List<Bitmap> CreatePart2RGB(List<Bitmap> partBitmaps)
        {
            //获得新图宽高
            var width = partBitmaps[0].Width;
            var height = partBitmaps[0].Height;

            var partNum = partBitmaps.Count;
            var createNum = Mathf.CeilToInt(partBitmaps.Count / 3.0f);

            List<Bitmap> createBitmaps = new List<Bitmap>();

            while (createBitmaps.Count < createNum)
            {
                var bitmap = new Bitmap(width, height);
                DrawWholeBitmapColor(bitmap, System.Drawing.Color.FromArgb(0, 0, 0));
                createBitmaps.Add(bitmap);
            }

            Bitmap partBG = new Bitmap(width, height);
            DrawWholeBitmapColor(partBG, System.Drawing.Color.FromArgb(0, 0, 0, 0));

            for (int i = 0; i < createNum; i++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (i * 3 + 2 < partNum && partBitmaps[i * 3 + 2].GetPixel(x, y).R > 240)
                        {
                            createBitmaps[i].SetPixel(x, y, System.Drawing.Color.FromArgb(255, 0, 0));
                            partBG.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 255));
                        }
                        else if (i * 3 + 1 < partNum && partBitmaps[i * 3 + 1].GetPixel(x, y).R > 240)
                        {
                            createBitmaps[i].SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 0));
                            partBG.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 255));
                        }
                        else if (i * 3 < partNum && partBitmaps[i * 3].GetPixel(x, y).R > 240)
                        {
                            createBitmaps[i].SetPixel(x, y, System.Drawing.Color.FromArgb(0, 0, 255));
                            partBG.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            createBitmaps[i].SetPixel(x, y, System.Drawing.Color.Black);
                        }
                    }
                }
            }

            createBitmaps.Add(partBG);

            return createBitmaps;
        }

        public static Bitmap Black2Trans(this Bitmap bitmap)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (bitmap.GetPixel(i,j).R < 10)
                    {
                        bitmap.SetPixel(i, j, System.Drawing.Color.FromArgb(0, 0, 0, 0));
                    }
                }
            }

            return bitmap;
        }

        public static Bitmap DrawWholeBitmapColor(Bitmap bitmap, System.Drawing.Color color)
        {
            var colors = new System.Drawing.Color[bitmap.Width * bitmap.Height];

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bitmap.SetPixel(i, j, color);
                }
            }

            return bitmap;
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

        private static void SaveBitmap2PNG(Bitmap bitmap, string path)
        {
            bitmap.Save(path);
        }
    }
}