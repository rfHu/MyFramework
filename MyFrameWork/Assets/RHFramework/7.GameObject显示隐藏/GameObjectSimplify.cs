using UnityEngine;

namespace RHFramework
{
    public partial class GameObjectSimplify
    {
        public static void Show(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public static void Hide(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/7.GameObject 显隐", false, 7)]
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();
            Hide(gameObject);
        }
#endif
    }
}
//仅为示例，把代码方法调整成自己习惯的方式，以提升编程舒适度
