using UnityEngine;

namespace RHFramework
{
    public class Hide : MonoBehaviourSimplify
    {
        private void Awake()
        {
            Hide();
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}
