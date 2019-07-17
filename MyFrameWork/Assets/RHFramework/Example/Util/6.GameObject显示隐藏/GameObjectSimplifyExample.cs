using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class GameObjectSimplifyExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/Util/6.GameObject API简化", false, 6)]
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();
            gameObject.Hide();

            gameObject.Show();
        }
#endif
    }
}
//仅为示例，把代码方法调整成自己习惯的方式，以提升编程舒适度