using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    T value;
    Node<T> next;

    public Node(T value)
    {
        this.Value = value;
        next = null;
    }

    public Node<T> Next
    {
        get
        {
            return next;
        }

        set
        {
            next = value;
        }
    }

    public T Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }

    public bool isThisValue(T outValue)
    {
        return Value.Equals(outValue);
    }

    public bool isNotLast()
    {
        return Next != null;
    }
}


public class ListNodeUtil 
{
    /// <summary>
    /// 在末尾添加结点
    /// </summary>
    /// <param name="headNode"></param>
    /// <param name="value"></param>
    void AddToTail<T>(Node<T> headNode, T value)
    {
        Node<T> newNode = new Node<T>(value);

        if (headNode == null)
        {
            headNode = newNode;
        }
        else
        {
            Node<T> curLastNode = headNode;
            while (curLastNode.isNotLast())
            {
                curLastNode = curLastNode.Next;
            }

            curLastNode.Next = newNode;
        }
    }

    /// <summary>
    /// 删除目标结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="headNode"></param>
    /// <param name="value"></param>
    void RemoveNode<T>(Node<T> headNode, T value)
    {
        if (headNode == null)
        {
            return;
        }

        Node<T> toBeDeletedNode = null;
        if (headNode.isThisValue(value))
        {
            toBeDeletedNode = headNode;
        }
        else
        {
            Node<T> tempNode = headNode;
            while (tempNode.Next != null && !tempNode.Next.isThisValue(value))
            {
                tempNode = tempNode.Next;
            }

            if (tempNode.Next != null && tempNode.Next.isThisValue(value))
            {
                toBeDeletedNode = tempNode.Next;
                tempNode.Next = tempNode.Next.Next;
            }
        }

        if (toBeDeletedNode != null)
        {
            toBeDeletedNode = null;
        }
    }

    //题目：输入头结点，从尾到头打印value

    void PrintListReversingly_Interatively<T>(Node<T> headNode)
    {
        Stack<Node<T>> nodes = new Stack<Node<T>>();

        Node<T> tempNode = headNode;
        while (tempNode != null)
        {
            nodes.Push(tempNode);
            tempNode = tempNode.Next;
        }

        while (nodes.Count != 0)
        {
            tempNode = nodes.Pop();
            Debug.Log(tempNode.Value.ToString());
        }
    }

    //用栈实现自然可以想到用递归来实现
    void PrintListReversingly_Recursively<T>(Node<T> headNode)
    {
        if (headNode != null)
        {
            if (headNode.Next != null)
            {
                PrintListReversingly_Recursively(headNode.Next);
            }

            Debug.Log(headNode.Value);
        }
    }

    //递归代码很简洁，但是存在问题：当链表非常长的时候，会导致函数调用的层级很深，有可能导致函数调用栈溢出
    //显式用栈基于循环实现的代码的鲁棒性好一些
    //鲁棒性：及抗变换性，就是说代码更加健壮
}
