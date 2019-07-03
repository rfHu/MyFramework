using UnityEngine;

namespace RHFramework
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private AudioSource mMusicSource = null;

        private AudioListener mAudioListener;

        private void CheckAudioListener()
        {
            if (!mAudioListener)
            {
                mAudioListener = FindObjectOfType<AudioListener>();
            }

            if (!mAudioListener)
            {
                mAudioListener = gameObject.AddComponent<AudioListener>();
            }
        }

        public void PlayMusic(string musicName, bool loop)
        {
            CheckAudioListener();

            if (!mMusicSource)
            {
                mMusicSource = gameObject.AddComponent<AudioSource>();
            }

            var coinSound = Resources.Load<AudioClip>(musicName);

            mMusicSource.clip = coinSound;
            mMusicSource.loop = loop;
            mMusicSource.Play();
        }

        public void StopMusic()
        {
            mMusicSource.Stop();
        }

        public void PauseMusic()
        {
            mMusicSource.Pause();
        }

        public void ResumeMusic()
        {
            mMusicSource.UnPause();
        }

        public void MusicOff()
        {
            mMusicSource.Pause();
            mMusicSource.mute = true;
        }

        public void MusicOn()
        {
            mMusicSource.UnPause();
            mMusicSource.mute = false;
        }

        public void PlaySound(string soundName)
        {
            CheckAudioListener();

            var audioSource = gameObject.AddComponent<AudioSource>();

            var sound = Resources.Load<AudioClip>(soundName);

            audioSource.clip = sound;
            audioSource.Play();
        }

        public void SoundOff()
        {
            var audioSources = GetComponents<AudioSource>();

            foreach (var audioSource in audioSources)
            {
                if (audioSource != mMusicSource)
                {
                    audioSource.Pause();
                    audioSource.mute = true;
                }
            }
        }

        public void SoundOn()
        {
            var audioSources = GetComponents<AudioSource>();

            foreach (var audioSource in audioSources)
            {
                if (audioSource != mMusicSource)
                {
                    audioSource.UnPause();
                    audioSource.mute = false;
                }
            }
        }
    }
}