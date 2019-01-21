using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{
    public class TransformLocalPosSimplify
    {

        public static void SetLocalPosX(Transform transform, float x)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosY(Transform transform, float y)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosZ(Transform transform, float z)
        {
            var localPos = transform.localPosition;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosXY(Transform transform, float x, float y)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosXZ(Transform transform, float x, float z)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosYZ(Transform transform, float y, float z)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            localPos.z = z;
            transform.localPosition = localPos;
        }

#if UNITY_EDITOR
        [MenuItem("RHFramework/10.Transform 位置简化")]
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();

            SetLocalPosX(gameObject.transform, 5);
            SetLocalPosY(gameObject.transform, 5);
            SetLocalPosZ(gameObject.transform, 5);
        }
#endif
    }
}