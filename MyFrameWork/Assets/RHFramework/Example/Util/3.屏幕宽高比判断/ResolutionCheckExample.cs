using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class ResolutionCheckExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/3.屏幕宽高比", false, 3)]
        private static void MenuClicked()
        {
            Debug.Log(ResolutionCheck.IsPadResolution() ? "是Pad宽高比" : "不是Pad宽高比");
            Debug.Log(ResolutionCheck.IsPhoneResolution() ? "是Phone宽高比" : "不是Phone宽高比");
            Debug.Log(ResolutionCheck.IsPhone15Resolution() ? "是Phone4s宽高比" : "不是Phone4s宽高比");
            Debug.Log(ResolutionCheck.IsPhoneXResolution() ? "是PhoneX宽高比" : "不是PhoneX宽高比");
        }
#endif
    }
}