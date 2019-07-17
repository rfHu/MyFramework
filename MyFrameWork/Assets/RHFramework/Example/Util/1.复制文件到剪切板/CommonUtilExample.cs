using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtilExample
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("RHFramework/Example/1.复制文字到剪切板", false, 1)]
#endif
    private static void MenuClicked2()
    {
        CommomUtil.CopyText("复制文本");
    }

}
