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
                {
                    continue;
                }

                int leftlength = k - 0;
                int rightLength = s.Length - 1 - (k + 1);
                int maxHalfLength = Math.Min(leftlength, rightLength);
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
    }
}