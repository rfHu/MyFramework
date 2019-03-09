using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadPrefabExample : MonoBehaviour
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("RHFramework/Example/20.UnloadPrefabExample", false, 20)]
#endif
    static void MenuCilcked()
    {
        UnityEditor.EditorApplication.isPlaying = true;

        new GameObject().AddComponent<UnloadPrefabExample>();
    }

    IEnumerator Start()
    {
        var gamePanel = Resources.Load("GamePanel");

        yield return new WaitForSeconds(5.0f);

        gamePanel = null;
        
        Resources.UnloadUnusedAssets();
        Debug.Log("gamePanel Unloaded");
    }
}
