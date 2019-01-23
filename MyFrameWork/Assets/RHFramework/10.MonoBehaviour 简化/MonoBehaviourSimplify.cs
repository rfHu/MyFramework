using UnityEngine;

namespace RHFramework
{
    public partial class MonoBehaviourSimplify : MonoBehaviour
    {
        public void Show()
        {
            GameObjectSimplify.Show(gameObject);
        }

        public void Hide()
        {
            GameObjectSimplify.Hide(gameObject);
        }

        public void Identity()
        {
            TransformSimplify.Identity(transform);
        }
    }

    public class Hide : MonoBehaviourSimplify
    {
        private void Awake()
        {
            Hide();
        }

        protected override void OnBeforeDestroy()
        {
            throw new System.NotImplementedException();
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/10.Monobehaivor 简化", false, 10)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            var gameObj = new GameObject("Hide");
            gameObj.AddComponent<Hide>();
        }
#endif
    }
}