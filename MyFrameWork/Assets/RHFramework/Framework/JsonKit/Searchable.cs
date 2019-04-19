using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RHFramework
{
    public interface ISearchable<T>
    {
        T Key();
    }

    /// <summary>
    /// 转化为
    /// </summary>
    [Serializable]
    public abstract class JsonDataIDName : ISearchable<string>
    {
        public string id;
        public string name;

        public string Key()
        {
            return id;
        }
    }

    [Serializable]
    public abstract class JsonDataKV<V> : ISearchable<string>
    {
        public string id;
        public V data;
        
        public string Key()
        {
            return id;
        }

        public V Value()
        {
            return data;
        }
    }

    [Serializable]
    public abstract class JsonDataList<T> where T : ISearchable<string>
    {
        public List<T> dataList = new List<T>();
    }
    
}
