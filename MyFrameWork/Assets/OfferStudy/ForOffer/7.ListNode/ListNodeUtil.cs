using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    T value;
    Node<T> next;

    public Node(T value)
    {
        this.value = value;
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

    public bool isThisValue(T outValue)
    {
        return value.Equals(outValue);
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


}
