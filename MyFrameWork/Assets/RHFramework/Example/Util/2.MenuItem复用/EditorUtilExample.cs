﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class EditorUtilExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/Util/2.复用 MenuItem", false, 2)]
        private static void MenuClicked6()
        {
            EditorUtil.CallMenuItem("RHFramework/Example/1.复制文字到剪切板");
        }
#endif
    }
}