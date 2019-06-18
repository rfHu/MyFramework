using System;
using System.Collections.Generic;
//用两个队列实现栈，实现后进先出
namespace ForOffer
{
    class CStack<T>
    {
        Queue<T> queue1 = new Queue<T>();
        Queue<T> queue2 = new Queue<T>();

        void Append(T node)
        {
            if (queue2.Count > 0)
            {
                queue2.Enqueue(node);
            }
            else
            {
                queue1.Enqueue(node);
            }
        }

        T Delete()
        {
            if (queue1.Count==0 && queue2.Count == 0)
            {
                return default(T);
            }

            if (queue2.Count > 0)
            {
                while (queue2.Count >1)
                {
                    queue1.Enqueue(queue2.Dequeue());
                }

                return queue2.Dequeue();
            }
            else
            {
                while (queue1.Count > 1)
                {
                    queue2.Enqueue(queue1.Dequeue());
                }

                return queue1.Dequeue();
            }
        }
    }
}
