using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LeetCode
{
    public class TwoListNodeSumTest
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Leetcode/3.LengthOfLongestSubstring", false, 3)]
#endif
        static void MenuCilcked()
        {
            string s ="abcabcbb";

            Debug.Log(new LengthOfLongestSubstringSolution().LengthOfLongestSubstring(s));
        }
    }


    class LengthOfLongestSubstringSolution
    {
        public int LengthOfLongestSubstring(string s)
        {
            var usedChars = new List<char>();
            var curLongest = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (curLongest >= s.Length - i)
                {
                    break;
                }
                int tempLength = 0;
                for (int j = i; j < s.Length; j++)
                {
                    if (!usedChars.Contains(s[j]))
                    {
                        usedChars.Add(s[j]);
                        tempLength += 1;
                        curLongest = curLongest >= tempLength ? curLongest : tempLength;
                    }
                    else
                    {
                        usedChars.Clear();
                        break;
                    }
                }
            }

            return curLongest;
        }
    }
}
