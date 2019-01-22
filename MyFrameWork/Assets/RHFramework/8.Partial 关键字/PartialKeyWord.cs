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
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/8.partial 关键字", false, 9)]
#endif
        private static void MenuClicked()
        {
            var parentTrans = new GameObject("Parent").transform;
            var childTrans = new GameObject("Child").transform;

            TransformSimplify.AddChild(parentTrans, childTrans);
            GameObjectSimplify.Hide(childTrans);
        }
    }
}