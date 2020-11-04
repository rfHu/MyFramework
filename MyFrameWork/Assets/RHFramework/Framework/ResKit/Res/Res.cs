using System;
using UnityEngine;

namespace RHFramework
{
    public enum ResState
    {
        Waiting,
        Loading,
        Loaded,
    }

    public abstract class Res : SimpleRC
    {
        private ResState mState;

        public ResState State
        {
            get { return mState; }
            protected set
            {
                mState = value;

                if (mState == ResState.Loaded && mOnLoadedEvent != null)
                {
                    mOnLoadedEvent.Invoke(this);
                }
            }
        }

        public UnityEngine.Object Asset { get; protected set; }

        public string Name { get; protected set; }

        private string mAssetPath;

        public abstract bool LoadSync<T>() where T : UnityEngine.Object;

        public abstract void LoadAsync<T>() where T : UnityEngine.Object;
        
        protected abstract void OnReleaseRes();

        protected override void OnZeroRef()
        {
            OnReleaseRes();
        }


        private event Action<Res> mOnLoadedEvent;

        public void RegisterOnLoadedEvent(Action<Res> onLoaded)
        {
            mOnLoadedEvent += onLoaded;
        }

        public void UnregisterOnLoadedEvent(Action<Res> onLoaded)
        {
            mOnLoadedEvent -= onLoaded;
        }
    }
}