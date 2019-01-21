using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{
    public class TransformIdentity
    {

        private static void Identity(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

#if UNITY_EDITOR
        [MenuItem("RHFramework/11.Transform 重置")]
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();

            TransformLocalPosSimplify.SetLocalPosX(gameObject.transform, 5.5f);

            Identity(gameObject.transform);
        }
#endif
    }
}