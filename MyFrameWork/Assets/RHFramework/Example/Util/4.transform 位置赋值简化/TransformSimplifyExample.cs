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

            gameObject.transform.SetLocalPosX(5);
            gameObject.transform.SetLocalPosY(5);
            gameObject.transform.SetLocalPosZ(5);

            gameObject.transform.Identity();

            var parentTrans = new GameObject("Parent").transform;
            var childTrans = new GameObject("Child").transform;

            parentTrans.AddChild(childTrans);
        }
#endif
    }
}