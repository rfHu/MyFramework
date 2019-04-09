using System;
using UnityEngine;

namespace RHFramework
{
    public abstract class Res : SimpleRC
    {
        public UnityEngine.Object Asset { get; protected set; }

        public string Name { get; protected set; }

        private string mAssetPath;

        public abstract bool LoadSync();

        public abstract void LoadAsync(Action<Res> OnLoaded);
        
        protected abstract void OnReleaseRes();

        protected override void OnZeroRef()
        {
            OnReleaseRes();
        }
    }
}