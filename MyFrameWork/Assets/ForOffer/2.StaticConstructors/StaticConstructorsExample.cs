using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForOffer
{
    namespace StaticConstructors
    {
        public class StaticConstructorsExample
        {
#if UNITY_EDITOR
            [UnityEditor.MenuItem("ForOffer/2.StaticConstructors", false, 2)]
#endif
            static void MenuCilcked()
            {
                B b1 = new B();
                B b2 = new B();
                //顺序132424
                //静态构造函数在类型第一次被使用之前自动调用并且只调用一次
                //静态构造函数先初始化静态类型的变量（非静态会直接编译不通过）
                //然后构造函数先初始化成员变量
            }
        }

        class A
        {
            public A(string text)
            {
                Debug.Log(text);
            }
        }

        class B
        {
            static A a1 = new A("a1");

            A a2 = new A("a2");

            static B()
            {
                a1 = new A("a3");
            }

            public B()
            {
                a2 = new A("a4");
            }
        }
    }
}