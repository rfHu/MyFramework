using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForOffer
{
    namespace TDArraySearch
    {
        public class TDArraySearch
        {
            //查找数组中目标数字，如果查找到返回true，否则返回false

            static int[,] arr = new int[4,4]{
                { 1, 2, 8, 9 },
                { 2, 4, 9, 12 },
                { 4, 7, 10, 13 },
                { 6, 8, 11, 15 }
                };

#if UNITY_EDITOR
            [UnityEditor.MenuItem("ForOffer/5.2DArraySearch", false, 5)]
#endif
            static void MenuCilcked()
            {
                Debug.Log(SearchFunction(arr, 7));
                Debug.Log(SearchFunction(arr, 5));
            }

            static bool SearchFunction(int[,] arr, int num)
            {
                var xLength = arr.GetLength(0);
                var yLength = arr.GetLength(1);

                int x = xLength - 1;
                int y = 0;

                do
                {
                    var temp = arr[x,y];
                    if (num < temp)
                    {
                        x -= 1;
                    }
                    else if (num > temp)
                    {
                        y += 1;
                    }
                    else 
                    {
                        Debug.LogFormat("Number is {0}, x = {1}, y = {2}", arr[x,y], x, y);
                        return true;
                    }

                } while (!(x < 0 || y >= yLength));

                Debug.LogFormat("Number {0} can't find", num);
                return false;
            }
            //思路：寻找对比后可以剔除行列的数字进行比较，以缩小范围
        }
    }
}