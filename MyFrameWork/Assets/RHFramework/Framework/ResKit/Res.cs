using UnityEngine;

namespace RHFramework
{
    public class Res
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

        private int mReferenceCount = 0;

        public void Retain()
        {
            mReferenceCount++;
        }

        public void Release()
        {
            mReferenceCount--;

            if (mReferenceCount == 0)
            {
                Resources.UnloadAsset(Asset);

                ResLoader.SharedLoadedReses.Remove(this);
            }

            Asset = null;
        }
    }
}