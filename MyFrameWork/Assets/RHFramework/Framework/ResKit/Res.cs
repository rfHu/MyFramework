using UnityEngine;

namespace RHFramework
{
    public class Res : SimpleRC
    {
        public string Name
        {
            get { return Asset.name; }
        }

        public Res(Object asset)
        {
            Asset = asset;
        }
        public Object Asset { get; private set; }

        protected override void OnZeroRef()
        {
            Resources.UnloadAsset(Asset);

            ResLoader.SharedLoadedReses.Remove(this);

            Asset = null;
        }
    }
}