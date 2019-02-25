using UnityEngine;

namespace RHFramework
{
    public class Hide : MonoBehaviourSimplify
    {
        private void Awake()
        {
            this.Hide();
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}
