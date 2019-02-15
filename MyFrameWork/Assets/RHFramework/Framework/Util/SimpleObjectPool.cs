using System.Collections.Generic;

namespace RHFramework
{
    public interface IPool<T>
    {
        T Allocate();

        bool Recycle(T obj);
    }

    public interface IObjectFactory<T>
    {
        T Create();
    }

    public abstract class Pool<T>: IPool<T>
    {
        #region ICountObserverable
        /// <summary>
        /// Gets the current count.
        /// </summary>
        /// <value>The current count.</value>
        public int CurCount
        {
            get { return mCachedStack.Count; }
        }
        #endregion

        protected IObjectFactory<T> mFactory;

        Stack<T> mCachedStack = new Stack<T>();

        /// <summary>
        /// default is 5
        /// </summary>
        protected int mMaxCount = 5;

        public virtual T Allocate()
        {
            return mCachedStack.Count == 0 ? mFactory.Create() : mCachedStack.Pop();
        }

        public abstract bool Recycle(T obj);
    }
}