using UnityEngine;

namespace RHFramework
{
    public static class StaticThisExtension
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/18.StaticThisExtension",false,18)]
       #endif
        static void MenuClicked()
        {
            new object().Test();
            "string".Test();
        }

        static void Test(this object selfObj)
        {
            Debug.Log(selfObj);
        }
    }
}
