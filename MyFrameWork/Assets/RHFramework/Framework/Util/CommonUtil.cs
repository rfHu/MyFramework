using UnityEngine;

public partial class CommomUtil
{
    public static void CopyText(string text)
    {
        GUIUtility.systemCopyBuffer = text;
    }
}
