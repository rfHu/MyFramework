using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用两个栈实现队列，实现队尾插入和队首删除
namespace ForOffer
{
    public class CQueue<T>
    {
        private Stack<T> stack1 = new Stack<T>();
        private Stack<T> stack2 = new Stack<T>();

        void appendTail(T node)
        {
            stack2.Push(node);
        }

        T deleteHead()
        {
            if (stack1.Count == 0 && stack2.Count == 0)
            {
                return default(T);
            }

            if (stack1.Count == 0)
            {
                while (stack2.Count > 0)
                {
                    stack1.Push(stack2.Pop());
                }
            }
            var temp = stack1.Pop();
            return temp;
        }
    }
}