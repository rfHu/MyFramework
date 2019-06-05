using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ForOffer
{
    namespace ChainBinaryTree
    {
        public class TreeNode<T>
        {
            private T data;
            private TreeNode<T> lChild;
            private TreeNode<T> rChild;


            public TreeNode(T val, TreeNode<T> lp, TreeNode<T> rp)
            {
                data = val;
                lChild = lp;
                rChild = rp;
            }

            public TreeNode(TreeNode<T> lp, TreeNode<T> rp)
            {
                data = default(T);
                lChild = lp;
                rChild = rp;
            }

            public TreeNode(T val)
            {
                data = val;
                lChild = null;
                rChild = null;
            }

            public TreeNode()
            {
                data = default(T);
                lChild = null;
                rChild = null;
            }

            public T Data
            {
                get { return data; }
                set { data = value; }
            }

            public TreeNode<T> LChild
            {
                get { return lChild; }
                set { lChild = value; }
            }

            public TreeNode<T> RChild
            {
                get { return rChild; }
                set { rChild = value; }
            }
        }

        public class LinkBinaryTree<T>
        {
            private TreeNode<T> head;

            public TreeNode<T> Head
            {
                get
                {
                    return head;
                }

                set
                {
                    head = value;
                }
            }

            public LinkBinaryTree()
            {
                head = null;
            }

            public LinkBinaryTree(T val)
            {
                TreeNode<T> p = new TreeNode<T>(val);
                head = p;
            }

            public LinkBinaryTree(T val, TreeNode<T> lp, TreeNode<T> rp)
            {
                TreeNode<T> p = new TreeNode<T>(val, lp, rp);
                head = p;
            }

            //判断是否是空二叉树
            public bool IsEmpty()
            {
                return head == null;
            }

            //根节点
            public TreeNode<T> Root()
            {
                return head;
            }

            public TreeNode<T> GetLChild(TreeNode<T> p)
            {
                return p.LChild;
            }

            public TreeNode<T> GetRChild(TreeNode<T> p)
            {
                return p.RChild;
            }

            //将结点p的左子树插入新结点val，原左子树变为新节点的左子树
            public void InsertL(T val, TreeNode<T> p)
            {
                TreeNode<T> tmp = new TreeNode<T>(val);
                tmp.LChild = p.LChild;
                p.LChild = tmp;
            }

            public void InsertR(T val, TreeNode<T> p)
            {
                TreeNode<T> tmp = new TreeNode<T>(val);
                tmp.RChild = p.RChild;
                p.RChild = tmp;
            }

            //p非空 则删除其左子树
            public TreeNode<T> DeleteL(TreeNode<T> p)
            {
                if (p == null || p.LChild == null)
                {
                    return null;
                }
                TreeNode<T> tmp = p.LChild;
                p.LChild = null;
                return tmp;
            }

            public TreeNode<T> DeleteR(TreeNode<T> p)
            {
                if (p == null || p.RChild == null)
                {
                    return null;
                }
                TreeNode<T> tmp = p.RChild;
                p.RChild = null;
                return tmp;
            }

            //在二叉树中查找值为value的结点
            public TreeNode<T> Search(TreeNode<T> root, T value)
            {
                TreeNode<T> p = root;
                if (p == null)
                {
                    return null;
                }
                //前序查找
                if (p.Data.Equals(value))
                {
                    return p;
                }
                if (p.LChild != null)
                {
                    return Search(p.LChild, value);
                }
                if (p.RChild != null)
                {
                    return Search(p.RChild, value);
                }
                return null;
            }

            //判断是否叶子结点
            public bool IsLeaf(TreeNode<T> p)
            {
                return (p != null && p.LChild == null && p.RChild == null);
            }

            //前序遍历（根节点->左->右）
            public void PreOrder(TreeNode<T> ptr)
            {
                if (IsEmpty())
                {
                    Debug.Log("Tree is Empty");
                    return;
                }
                if (ptr != null)
                {
                    Debug.Log(ptr);
                    PreOrder(ptr.LChild);
                    PreOrder(ptr.RChild);
                }
            }

            //中序遍历
            public void InOrder(TreeNode<T> ptr)
            {
                if (IsEmpty())
                {
                    Debug.Log("Tree is Empty");
                    return;
                }
                if (ptr != null)
                {
                    InOrder(ptr.LChild);
                    Debug.Log(ptr);
                    InOrder(ptr.RChild);
                }
            }

            //后序遍历
            public void PastOrder(TreeNode<T> ptr)
            {
                if (IsEmpty())
                {
                    Debug.Log("Tree is Empty");
                    return;
                }
                if (ptr != null)
                {
                    PastOrder(ptr.LChild);
                    PastOrder(ptr.RChild);
                    Debug.Log(ptr);
                }
            }

            //层序遍历
            public void LevelOrder(TreeNode<T> root)
            {
                if (root == null)
                {
                    return;
                }

                Queue<TreeNode<T>> que = new Queue<TreeNode<T>>();
                que.Enqueue(root);
                while (!(que.Count == 0))
                {
                    TreeNode<T> tmp = que.Dequeue();
                    Debug.Log(tmp);
                    if (tmp.LChild != null)
                    {
                        que.Enqueue(tmp.LChild);
                    }
                    if (tmp.RChild != null)
                    {
                        que.Enqueue(tmp.RChild);
                    }
                }

            }

            //根据前序和中序重构二叉树
            //思路：前序首位是根节点，在中序中找到根节点，左右分别是左右子树的前序中序，递归其根节点直到无子树
            public TreeNode<T> Construct(T[] preOrder, T[] inOrder)
            {
                if (preOrder == null || inOrder == null || preOrder.Length == 0 || inOrder.Length == 0 || preOrder.Length != inOrder.Length)
                {
                    return null;
                }

                TreeNode<T> root = new TreeNode<T>(preOrder[0]);

                int i = 0;
                for (i = 0; i < inOrder.Length; i++)
                {
                    if (inOrder[i].Equals(preOrder[0]))
                    {
                        break;
                    }
                }

                //左子树前序和中序
                T[] leftPre = preOrder.Skip(1).Take(i).ToArray();
                T[] leftIn = inOrder.Skip(0).Take(i).ToArray();

                //右子树前序和中序
                T[] rightPre = preOrder.Skip(i + 1).ToArray();
                T[] rightIn = inOrder.Skip(i + 1).ToArray();

                //递归当前左子树
                root.LChild = Construct(leftPre, leftIn);

                //递归当前右子树
                root.RChild = Construct(rightPre, rightIn);

                return root;
            }

            //随记：前序和后序可以重构出唯一二叉树吗？
            //不一定可以
            //前序{3，1,2}，后序{2,1,3}。可构成两种二叉树不唯一；
            //前序{3,1,2}，后序{1,2,3}。则有唯一二叉树。
        }
    }
}