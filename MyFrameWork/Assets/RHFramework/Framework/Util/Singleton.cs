using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RHFramework
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        protected static T mInstance = null;

        protected Singleton()
        {
        }

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    // 先获取所有非public的构造⽅方法 
                    var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    // 从ctors中获取⽆无参的构造⽅方法 
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null)
                        throw new Exception("Non-public ctor() not found!");
                    // 调⽤用构造⽅方法 
                    mInstance = ctor.Invoke(null) as T;
                }

                return mInstance;
            }
        }
    }
}