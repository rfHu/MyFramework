using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class AudioExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/13.AudioManager", false, 13)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("AudioExample").AddComponent<AudioExample>();
        }


#endif

        void Start()
        {
            AudioManager.Instance.PlaySound("getcoin");
            AudioManager.Instance.PlaySound("getcoin");

            Delay(1.0f, () => { AudioManager.Instance.PlayMusic("home", true); });

            Delay(3.0f, () => { AudioManager.Instance.PauseMusic(); });

            Delay(5.0f, () => { AudioManager.Instance.ResumeMusic(); });

            Delay(7.0f, () => { AudioManager.Instance.StopMusic(); });

            Delay(9.0f, () => { AudioManager.Instance.PlayMusic("home", true); });

            Delay(11.0f, () => { AudioManager.Instance.MusicOff(); });

            Delay(13.0f, () => { AudioManager.Instance.MusicOn(); });
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}