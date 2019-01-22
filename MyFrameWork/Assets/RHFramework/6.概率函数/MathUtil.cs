using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{
    public partial class MathUtil
    {
        public static bool Percent(int percent)
        {
            return Random.Range(0, 100) < percent;
        }

#if UNITY_EDITOR
        [MenuItem("RHFramework/6.概率判断", false, 6)]
        private static void MenuClicked()
        {
            Debug.Log(Percent(50));
        }
#endif
    }
}