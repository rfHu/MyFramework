using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
将一个给定字符串根据给定的行数，以从上往下、从左到右进行 Z 字形排列。

比如输入字符串为 "LEETCODEISHIRING" 行数为 3 时，排列如下：

L   C   I   R
E T O E S I I G
E   D   H   N

之后，你的输出需要从左往右逐行读取，产生出一个新的字符串，比如："LCIRETOESIIGEDHN"

行数为 4 时，排列如下：

L     D     R
E   O E   I I
E C   I H   N
T     S     G
*/

namespace LeetCode
{
    public class ZConvert
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Leetcode/6.ZConvert", false, 6)]
        static void MenuCilcked()
        {
            Debug.Log(Convert("A", 1));
        }
#endif


        public static string Convert(string s, int numRows)
        {
            if (numRows == 1)
            {
                return s;
            }

            int loopNum = numRows * 2 - 2;

            var arr_charList = new List<char>[loopNum];

            for (int i = 0; i < arr_charList.Length; i++)
            {
                arr_charList[i] = new List<char>();
            }

            for (int i = 0; i < s.Length; i++)
            {
                var tmpNum = i % loopNum;
                if (tmpNum < numRows)
                {
                    arr_charList[tmpNum].Add(s[i]);
                }
                else
                {
                    arr_charList[loopNum - tmpNum].Add(s[i]);
                }
            }

            var finalStr = "";
            for (int i = 0; i < arr_charList.Length; i++)
            {
                finalStr += new string(arr_charList[i].ToArray());
            }

            return finalStr;
        }
    }
}