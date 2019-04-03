using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//给定一个整数数组 nums 和一个目标值 target，请你在该数组中找出和为目标值的那 两个 整数，并返回他们的数组下标。

//你可以假设每种输入只会对应一个答案。但是，你不能重复利用这个数组中同样的元素。

public class TwoSumSolution
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Leetcode/1.TwoSum", false, 1)]
#endif
    static void MenuCilcked()
    {
        var nums = new int[] {3,2,4 };

        var arr = TwoSum2(nums, 6);
        foreach (var num in arr)
        {
            Debug.Log(num);
        }
    }

    /// <summary>
    /// 蛮力破解
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private static int[] TwoSum(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    var arr = new int[2];
                    arr[0] = i;
                    arr[1] = j;
                    return arr;
                }
            }
        }
        throw new ArgumentException("No two sum solution");
    }

    /// <summary>
    /// 两边遍历字典
    /// </summary>
    /// 与原题解不同由于key不可重复，不能以nums[i]为key，可能出现重复错误，此写法很差，仅仅用于仿写
    private static int[] TwoSum2(int[] nums, int target)
    {
        var dic = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            dic.Add(i, nums[i]);
        }

        for (int i = 0; i < dic.Count; i++)
        {
            if (dic.ContainsValue(target - nums[i]))
            {
                var another = dic.Where((kv) => kv.Key > i && kv.Value == target - nums[i]);

                if (another != null && another.Count() != 0)
                {
                    return new int[] { i, another.First().Key };
                }
            }
        }
        throw new ArgumentException("No two sum solution");
    }

    /// <summary>
    /// 一次遍历字典法
    /// </summary>
    /// 如果是两个相同值之和，可直接返回，不需要担心添加重复Key问题
    /// 如果量相同值和不为结果，则只保留最早一个值和它的下标即可
    public int[] TwoSum3(int[] nums, int target)
    {

        Dictionary<int, int> contain = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (contain.ContainsKey(target - nums[i]))
                return new int[] { contain[target - nums[i]], i };
            else if (!contain.ContainsKey(nums[i]))
                contain.Add(nums[i], i);
        }
        throw new ArgumentException("No two sum solution");
    }
}
