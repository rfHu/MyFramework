using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class TransformSimplifyExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/4.Transform API简化", false, 4)]
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();

            TransformSimplify.SetLocalPosX(gameObject.transform, 5);
            TransformSimplify.SetLocalPosY(gameObject.transform, 5);
            TransformSimplify.SetLocalPosZ(gameObject.transform, 5);

            TransformSimplify.Identity(gameObject.transform);

            var parentTrans = new GameObject("Parent").transform;
            var childTrans = new GameObject("Child").transform;

            TransformSimplify.AddChild(parentTrans, childTrans);
        }
#endif
    }
}