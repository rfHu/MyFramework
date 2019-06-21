using System;
using System.Collections;
using System.Collections.Generic;

//给定两个大小为 m 和 n 的有序数组 nums1 和 nums2。

//请你找出这两个有序数组的中位数，并且要求算法的时间复杂度为 O(log(m + n))。

namespace LeetCode
{
    public class FindMedianSortedArraysSolution
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int m = nums1.Length;
            int n = nums2.Length;
            if (m > n) //保证m <= n
            {
                var tempArr = nums1; nums1 = nums2; nums2 = tempArr;
                var tempNum = m; m = n; n = tempNum;
            }

            int iMin = 0, iMax = m, halfLen = (m + n + 1) / 2;

            while (iMin <= iMax)
            {
                int i = (iMin + iMax) / 2;
                int j = halfLen - i;

                if (i < iMax && nums2[j - 1] > nums1[i])
                {
                    iMin = i + 1;   //i 太小，在大的一边再次折半查找；
                }
                else if (i > iMin && nums1[i - 1] > nums2[j])
                {
                    iMax = i - 1;   //i 太大，再小的一边再次折半查找；
                }
                else                //i 的值正好
                {
                    int maxLeft = 0; //左半边最大值；
                    if (i == 0) { maxLeft = nums2[j - 1]; }                 
                    else if (j == 0) { maxLeft = nums1[i - 1]; }            
                    else { maxLeft = Math.Max(nums1[i - 1], nums2[j - 1]); }   

                    if ((m + n) % 2 == 1) { return maxLeft; }           //合计为奇数个元素

                    int minRight = 0;
                    if (i == m) { minRight = nums2[j]; }
                    else if (j == n) { minRight = nums1[i]; }
                    else { minRight = Math.Min(nums2[j], nums1[i]); }

                    return (maxLeft + minRight) / 2.0d;
                }
            }

            return 0.0d;
        }
    }
}