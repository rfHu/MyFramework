using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameObjectActiveImprovments 
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
    [MenuItem("RHFramework/13.GameObject 显隐")]
    private static void MenuClicked()
    {
        GameObject gameObject = new GameObject();
        Hide(gameObject);
    }
#endif
}

//仅为示例，把代码方法调整成自己习惯的方式，以提升编程舒适度
