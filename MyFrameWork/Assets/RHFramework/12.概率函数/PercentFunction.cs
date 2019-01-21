using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RHFramework
{
    public class PercentFunction
    {
        public static bool Percent(int percent)
        {
            return Random.Range(0, 100) < percent;
        }


#if UNITY_EDITOR
        [MenuItem("RHFramework/12.概率判断")]
        private static void MenuClicked()
        {
            Debug.Log(Percent(50));
        }
#endif
    }
}