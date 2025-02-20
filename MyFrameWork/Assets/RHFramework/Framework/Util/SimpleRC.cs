﻿
namespace RHFramework
{
    public interface IRefCounter
    {
        int RefCount { get; }

        void Retain(object refOwner = null);

        void Release(object refOwner = null);
    }

    public class SimpleRC : IRefCounter
    {
        public int RefCount { get; private set; }

        public void Retain(object refOwner = null)
        {
            RefCount++;
        }

        public void Release(object refOwner = null)
        {
            RefCount--;

            if (RefCount == 0)
            {
                OnZeroRef();
            }
        }

        protected virtual void OnZeroRef()
        {
        }
    }
}