using System;
using UnityEngine;

/* 
** 给定一个字符串 s，找到 s 中最长的回文子串。你可以假设 s 的最大长度为 1000。
*/

namespace LeetCode
{
    public class Solution
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Leetcode/5.TwoSum", false, 5)]
#endif
        static void MenuCilcked()
        {
            string s = "babbab";
            Debug.Log(LongestPalindrome(s));
        }


        public static string LongestPalindrome(string s)
        {
            string biggestLengthStr = "";

            for (int i = 0; i < s.Length; i++)
            {
                int leftlength = i - 0;
                int rightLength = s.Length - 1 - i;
                int maxHalfLength = Math.Min(leftlength, rightLength);

                if (maxHalfLength * 2 + 1 <= biggestLengthStr.Length)
                    continue;

                for (int j = 0; j <= maxHalfLength; j++)
                {
                    if (s[i - j] == s[i + j])
                    {
                        if ((2 * j + 1) > biggestLengthStr.Length)
                        {
                            biggestLengthStr = s.Substring(i - j, 2 * j + 1);
                        }
                    }
                }
            }

            for (int k = 0; k < s.Length - 1; k++)
            {
                if (s[k] != s[k + 1])
                    continue;

                int leftlength = k - 0;
                int rightLength = s.Length - 1 - (k + 1);
                int maxHalfLength = Math.Min(leftlength, rightLength);

                if (maxHalfLength * 2 + 2 <= biggestLengthStr.Length)
                    continue;
                
                for (int l = 0; l <= maxHalfLength; l++)
                {
                    if (s[k - l] == s[k + 1 + l])
                    {
                        if (2 * (l + 1) > biggestLengthStr.Length)
                        {
                            biggestLengthStr = s.Substring(k - l, 2 * (l + 1));
                        }
                    }
                }
            }

            return biggestLengthStr;
        }

        /// <summary>
        /// 官方解
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestPalindrome1(String s)
        {
            if (s == null || s.Length < 1) return "";
            int start = 0, end = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int len1 = ExpandAroundCenter(s, i, i);
                int len2 = ExpandAroundCenter(s, i, i + 1);
                int len = Math.Max(len1, len2);
                if (len > end - start)
                {
                    start = i - (len - 1) / 2;
                    end = i + len / 2;
                }
            }
            return s.Substring(start, end + 1);
        }

        private static int ExpandAroundCenter(string s, int left, int right)
        {
            int L = left, R = right;
            while (L >= 0 && R < s.Length && s[L] == s[R])
            {
                L--;
                R++;
            }
            return R - L - 1;
        }
    }
}