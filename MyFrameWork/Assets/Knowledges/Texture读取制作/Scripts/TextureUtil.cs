using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public static partial class TextureUtil
    {
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

        public static Texture2D DrawTexture2DColor(Texture2D texture2D, Color color)
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
    }
}