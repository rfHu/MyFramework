using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public partial class MathUtil
    {
        public static T GetRandomValueFrom<T>(params T[] values)
        {
            return values[Random.Range(0, values.Length)];
        }


#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/9.从若干值中取一个随机值",false , 9)]
        private static void MenuClicked2()
        {
            Debug.Log(GetRandomValueFrom(1, 2, 3));
            Debug.Log(GetRandomValueFrom("ssss", "dfds"));
            Debug.Log(GetRandomValueFrom(5.66, 3.4, 77.56));
        }
#endif
    }
}