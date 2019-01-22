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

    public class Hide : NewBehaviourSimplify
    {
        private void Awake()
        {
            Hide();
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