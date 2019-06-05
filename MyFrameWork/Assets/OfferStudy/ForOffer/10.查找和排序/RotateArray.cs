using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArray
{
    //把一个数组最开始的若干个元素搬到数组的末尾，就是数组的旋转。
    //输入一个递增排序的数组的一个旋转，输出此数组最小元素
    public int RotateArratMin(int[] array)
    {
        int indexStart = 0;
        int indexEnd = array.Length - 1;
        int indexMid = indexStart;
        while (array[indexStart] > array[indexEnd])
        {
            if (indexEnd - indexStart == 1)
            {
                indexMid = indexEnd;
                return array[indexMid];
            }

            indexMid = (indexStart + indexEnd) / 2;
            if (array[indexMid] >= indexStart)
            {
                indexStart = indexMid;
            }
            else if (array[indexMid] <= indexEnd)
            {
                indexEnd = indexMid;
            }
        }
        return array[indexMid];
    }

    //此实现是有问题的，例如：（1,0,1,1,1）和（1,1,1,0,1），我们无法判定取哪一半。只能用顺序查找
    //如下修改：
    public int RotateArratMin2(int[] array)
    {
        int indexStart = 0;
        int indexEnd = array.Length - 1;
        int indexMid = indexStart;
        while (array[indexStart] > array[indexEnd])
        {
            if (indexEnd - indexStart == 1)
            {
                indexMid = indexEnd;
                return array[indexMid];
            }

            indexMid = (indexStart + indexEnd) / 2;

            if (array[indexMid] == array[indexStart] && array[indexMid] == array[indexEnd])
            {
                var temp = array[indexStart];
                for (int i = indexStart + 1; i < indexEnd; i++)
                {
                    if (array[i] < temp)
                    {
                        temp = array[i];
                    }
                }
                return temp;
            }

            if (array[indexMid] >= indexStart)
            {
                indexStart = indexMid;
            }
            else if (array[indexMid] <= indexEnd)
            {
                indexEnd = indexMid;
            }
        }
        return array[indexMid];
    }
}
