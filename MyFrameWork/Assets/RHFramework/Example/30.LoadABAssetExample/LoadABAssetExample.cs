using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class LoadABAssetExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/30.LoadABAssetExample", false, 30)]
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("LoadABAssetExample").AddComponent<LoadABAssetExample>();
        }
#endif

        private ResLoader mResLoader = new ResLoader();

        private void Start()
        {
            var squareTexture = mResLoader.LoadSync<Texture2D>("square", "Square");
            Debug.Log(squareTexture.name);

            mResLoader.LoadAsync<GameObject>("testgo", "GameObject", gameObjPrefab => 
            {
                Instantiate(gameObjPrefab);
            });
        }

        private void OnDestroy()
        {
            if (mResLoader != null)
            {
                mResLoader.ReleaseAll();
                mResLoader = null;
            }
        }

        private void OnApplicationQuit()
        {
            if (mResLoader != null)
            {
                mResLoader.ReleaseAll();
                mResLoader = null;
            }
        }
    }
}