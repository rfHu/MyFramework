using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForOffer
{
    namespace SingletonExample
    {
        //只生成一个实例，
        //构造函数应为私有
        //定义一个静态实例，在需要时创建它

        public sealed class Singleton1
        {
            private Singleton1(){}

            private static Singleton1 instance = null;
            public static Singleton1 Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new Singleton1();
                    }

                    return instance;
                }
            }
        }
        //此代码可以在单线程的环境正常工作，但是多线程下会出问题，如两个线程都判定null创建两个实例

        public sealed class Singleton2
        {
            private Singleton2() { }

            private static readonly object syncObj = new object();

            private static Singleton2 instance = null;
            public static Singleton2 Instance
            {
                get
                {
                    lock (syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton2();
                        }
                    }
                    return instance;
                }
            }
        }
        //此代码可运行在多线程，当第一个线程上锁，第二个线程只能等待，这时第一个线程创建实例，第二个线程后来在进入不会再创建实例
        //此方法并不完美，加锁是一个非常耗时的操作，应该尽力避免

        public sealed class Singleton3
        {
            private Singleton3() { }

            private static readonly object syncObj = new object();

            private static Singleton3 instance = null;
            public static Singleton3 Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (syncObj)
                        {
                            if (instance == null)
                            {
                                instance = new Singleton3();
                            }
                        } 
                    }
                    return instance;
                }
            }
        }
        //我们在加锁前先进行判断实例是否存在，不存在则加锁，否则无需加锁，因此只有第一次访问instance时会加锁
        //不过这样代码复杂了很多，我们应当找到更优解

        public sealed class Singleton4
        {
            private Singleton4() { }

            private static Singleton4 instance = new Singleton4();
            public static Singleton4 Instance
            {
                get
                {
                    return instance;
                }
            }
        }
        //静态构造函数，可以保证只调用一次
        //但是这个时机并不是程序员可以控制的，加设我们在第一次使用此单例前就使用了类中一个静态方法，会造成过早创建实例，降低内存使用效率
        
        public sealed class Singleton5
        {
            private Singleton5() { }
            
            public static Singleton5 Instance
            {
                get
                {
                    return Nested.instance;
                }
            }

            class Nested
            {
                static Nested() { }

                internal static readonly Singleton5 instance = new Singleton5();
            }
        }
        //只有调用Instance时才会触发调用Nested创造单例
    }
}