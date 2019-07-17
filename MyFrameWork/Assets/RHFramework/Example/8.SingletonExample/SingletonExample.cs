using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework {
    public class SingletonExample : Singleton<SingletonExample>
    {
        private SingletonExample()
        {
            Debug.Log("SingletonExample ctor");
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/8.SingletonExample", false, 8)]
#endif
        private static void MenuClicked()
        {
            var initInstance = SingletonExample.Instance;
            initInstance = SingletonExample.Instance;
        }
    }
}