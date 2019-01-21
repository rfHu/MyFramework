using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public partial class TransformSimplify
    {
        public static void AddChild(Transform parentTrans, Transform childTrans)
        {
            childTrans.SetParent(parentTrans);
        }
    }

    public partial class GameObjectSimplify
    {
        public static void Show(Transform transform)
        {
            transform.gameObject.SetActive(true);
        }

        public static void Hide(Transform transform)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public class PartialKeyWord
    {

    }
}