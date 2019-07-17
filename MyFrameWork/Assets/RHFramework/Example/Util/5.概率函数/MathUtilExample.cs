using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class MathUtilExample 
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/Util/5.概率判断 和 随机函数", false, 5)]
        private static void MenuClicked()
        {
            Debug.Log(MathUtil.Percent(50));

            Debug.Log(MathUtil.GetRandomValueFrom(1, 2, 3));
            Debug.Log(MathUtil.GetRandomValueFrom("ssss", "dfds"));
            Debug.Log(MathUtil.GetRandomValueFrom(5.66, 3.4, 77.56));
        }
#endif
    }
}