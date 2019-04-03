using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//Unity下不可运行，日后在其他工程中测试
namespace ForOffer
{
    namespace ReflectionAndProgramArea
    {
        public class ReflectionAndProgramAreaExample
        {
#if UNITY_EDITOR
            //[UnityEditor.MenuItem("ForOffer/3.ReflectionAndProgramAreaExample", false, 3)]
#endif
            static void MenuCilcked()
            {
                String assambly = Assembly.GetEntryAssembly().FullName;
                AppDomain domain = AppDomain.CreateDomain("NemDomain");

                A.Number = 10;
                String nameOfA = typeof(A).FullName;
                A a = domain.CreateInstanceAndUnwrap(assambly, nameOfA) as A;
                a.SetNumber(20);
                Debug.LogFormat("Number in class A is {0}", A.Number);
                //10

                B.Number = 10;
                String nameOfB = typeof(B).FullName;
                B b = domain.CreateInstanceAndUnwrap(assambly, nameOfA) as B;
                b.SetNumber(20);
                Debug.LogFormat("Number in class B is {0}", B.Number);
                //20
            }
        }
        //a实际上只是一个代理实例（Proxy），指向位于NewDomain域中实例。
        //b在穿越应用程序域边界时，会完整的复制实例。会把实例b复制到默认的应用程序域。
        //调用SetNumber也是在缺省的应用程序域的进行

        [SerializeField]
        internal class A : MarshalByRefObject
        {
            public static int Number;

            public void SetNumber(int value)
            {
                Number = value;
            }
        }

        [SerializeField]
        internal class B
        {
            public static int Number;

            public void SetNumber(int value)
            {
                Number = value;
            }
        }
    }
}