using UnityEngine;

namespace RHFramework
{
    public class LoadDependencyAsyncExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/29.LoadDependencyAsyncExample", false, 29)]
        static void MenuCilcked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("LoadDependencyAsyncExample").AddComponent<LoadDependencyAsyncExample>();
        }
#endif
        ResLoader mResLoader = new ResLoader();

        private void Start()
        {
            mResLoader.LoadAsync<AssetBundle>("testgo", bundle =>
            {
                var gameObjPrefab = bundle.LoadAsset<GameObject>("GameObject");

                Instantiate(gameObjPrefab);
            });
        }

        private void OnDestroy()
        {
            mResLoader.ReleaseAll();
            mResLoader = null;
        }
    }
}