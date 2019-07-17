using UnityEngine;

namespace RHFramework
{
    public class MonoSingletonExample : MonoSingleton<MonoSingletonExample>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/Util/9.MonoSingletonExample", false, 9)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        private static void Example()
        {
            var initInstance = MonoSingletonExample.Instance;
            initInstance = MonoSingletonExample.Instance;
        }
    }
}