using UnityEngine;

public partial class CommomUtil
{
    public static void CopyText(string text)
    {
        GUIUtility.systemCopyBuffer = text;
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("RHFramework/2.复制文字到剪切板", false, 2)]
#endif
    private static void MenuClicked2()
    {
        CommomUtil.CopyText("复制文本");
    }

}
