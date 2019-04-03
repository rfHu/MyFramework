
using UnityEngine;

namespace ForOffer
{
    namespace BITreeStudy
    {
        class BITreeStudy
        {
#if UNITY_EDITOR
            [UnityEditor.MenuItem("ForOffer/6.BITreeStudy", false, 6)]
#endif
            static void MenuCilcked()
            {
                char[] data = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
                BiTree<char> tree = new BiTree<char>(10);
                for (int i = 0; i < data.Length; i++)
                {
                    tree.Add(data[i]);
                }
                tree.FirstTraversal();

                Debug.Log("---------------");

                tree.MidTraversal();

                Debug.Log("---------------");

                tree.LastTraversal();
            }
        }

        class BiTree<T>
        {
            private T[] data;
            private int count = 0;

            public BiTree(int capacity)
            {
                data = new T[capacity];
            }

            public bool Add(T item)
            {
                if (count >= data.Length)
                {
                    return false;
                }

                data[count] = item;
                count++;
                return true;
            }

            public void FirstTraversal()
            {
                FirstTraversal(0);
            }

            private void FirstTraversal(int index)
            {
                if (index >= data.Length)
                {
                    return;
                }
                int number = index + 1;

                if (data[index].Equals(-1))
                {
                    return;
                }
                Debug.Log(data[index]);

                int leftNumber = number * 2;
                int rightNumber = number * 2 + 1;
                FirstTraversal(leftNumber - 1);
                FirstTraversal(rightNumber - 1);
            }

            public void MidTraversal()
            {
                MidTraversal(0);
            }

            private void MidTraversal(int index)
            {
                if (index >= data.Length)
                {
                    return;
                }
                int number = index + 1;
                int leftNumber = number * 2;
                int rightNumber = number * 2 + 1;


                if (data[index].Equals(-1))
                {
                    return;
                }
                
                MidTraversal(leftNumber - 1);//左
                Debug.Log(data[index]);//当前
                MidTraversal(rightNumber - 1);//右
            }

            public void LastTraversal()
            {
                LastTraversal(0);
            }

            private void LastTraversal(int index)
            {
                if (index >= data.Length)
                {
                    return;
                }
                int number = index + 1;
                int leftNumber = number * 2;
                int rightNumber = number * 2 + 1;


                if (data[index].Equals(-1))
                {
                    return;
                }

                LastTraversal(leftNumber - 1);//左
                LastTraversal(rightNumber - 1);//右
                Debug.Log(data[index]);//当前
            }
        }
    }
}