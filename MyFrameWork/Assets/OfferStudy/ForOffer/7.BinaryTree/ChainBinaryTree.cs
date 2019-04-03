using System;
using System.Collections;
using System.Collections.Generic;
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
        }
    }
}