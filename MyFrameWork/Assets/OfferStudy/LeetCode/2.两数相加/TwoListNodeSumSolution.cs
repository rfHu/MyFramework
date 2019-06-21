using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//给出两个 非空 的链表用来表示两个非负的整数。
//其中，它们各自的位数是按照 逆序 的方式存储的，并且它们的每个节点只能存储 一位 数字。
//如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。
//您可以假设除了数字 0 之外，这两个数都不会以 0 开头。

namespace LeetCode
{
    namespace TwoListNodeSum
    {
        public class TwoListNodeSumTest
        {
#if UNITY_EDITOR
            [UnityEditor.MenuItem("Leetcode/2.TwoListNode", false, 2)]
#endif
            static void MenuCilcked()
            {
                var l1 = new ListNode(5);
                var l2 = new ListNode(5);

                var solution = new TwoListNodeSumSolution3();
                solution.AddTwoNumbers(l1, l2);
            }
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        /// <summary>
        /// 构想1，不可行，int不适用超出范围的数字
        /// </summary>
        public class TwoListNodeSumSolution1
        {
            public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                var sum = ListNodeToInt(l1) + ListNodeToInt(l2);
                return IntToListNode(sum);
            }

            public static ListNode IntToListNode(int num)
            {
                var left = num / 10;
                var currentNum = num % 10;

                var listNode = new ListNode(currentNum);
                if (left != 0)
                {
                    listNode.next = IntToListNode(left);
                }

                return listNode;
            }

            public static string ListNodeToStr(ListNode listNode)
            {
                var str = listNode.val.ToString();
                if (listNode.next != null)
                {
                    str = ListNodeToStr(listNode.next) + str;
                }

                return str;
            }

            public static int ListNodeToInt(ListNode listNode)
            {
                return int.Parse(ListNodeToStr(listNode));
            }
        }

        /// <summary>
        /// 利用逻辑实现进一操作赋值给新链表
        /// </summary>
        public class TwoListNodeSumSolution2
        {
            public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                ListNode dummyHead = new ListNode(0);
                ListNode p = l1, q = l2, curr = dummyHead;
                int carry = 0;
                while (p != null || q != null)
                {
                    int x = (p != null) ? p.val : 0;
                    int y = (q != null) ? q.val : 0;
                    int sum = carry + x + y;
                    carry = sum / 10;
                    curr.next = new ListNode(sum % 10);
                    curr = curr.next;
                    if (p != null) p = p.next;
                    if (q != null) q = q.next;
                }
                if (carry > 0)
                {
                    curr.next = new ListNode(carry);
                }
                return dummyHead.next;
            }
        }

        public class TwoListNodeSumSolution3
        {
            public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                var listNodeResult = new ListNode(0);
                var p = l1;
                var q = l2;
                var r = listNodeResult;
                bool addOne = false;

                while(p != null || q != null || addOne)
                {
                    var sum = (p == null ? 0 : p.val) + (q == null ? 0 : q.val);

                    r.val = (sum + (addOne ? 1 : 0)) % 10;

                    addOne = ((sum + (addOne ? 1 : 0)) / 10) >= 1;
                    p = (p == null)? null : p.next;
                    q = (q == null) ? null : q.next;
                    if (p != null || q != null || addOne)
                    {
                        r.next = new ListNode(0);
                        r = r.next;
                    }
                }

                return listNodeResult;
            }
        }
    }
}