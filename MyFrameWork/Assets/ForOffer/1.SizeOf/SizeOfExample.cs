using System.Runtime.InteropServices;
using UnityEngine;

namespace ForOffer
{
    namespace SizeOf
    {
        public class SizeOfExample
        {
#if UNITY_EDITOR
            [UnityEditor.MenuItem("ForOffer/1.sizeof", false, 1)]
#endif
            static void MenuCilcked()
            {
                Debug.Log("struct a.size: " + Marshal.SizeOf(new StructA()));
                //输出为1

                Debug.Log("struct a2.size: " + Marshal.SizeOf(new StructA2()));
                //输出为0            

                Debug.Log("struct a3.size: " + Marshal.SizeOf(new StructA3()));
                //输出为1

                Debug.Log("class a.size: " + Marshal.SizeOf(new ClassA()));
                //输出结果为0

                Debug.Log("class a2.size: " + Marshal.SizeOf(new ClassA2()));
                //输出结果为0

                Debug.Log("class a3.size: " + Marshal.SizeOf(new ClassA3()));
                //输出结果为0            

                Debug.Log("class a4.size: " + Marshal.SizeOf(new ClassA4()));
                //输出结果为0

            }
        }

        struct StructA { }

        [StructLayout(LayoutKind.Sequential)]
        struct StructA2 { }

        struct StructA3
        {
            public StructA3(int a) { }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ClassA { }

        /// <summary>
        /// 有构造函数和析构函数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        class ClassA2
        {
            public ClassA2() { }

            ~ClassA2() { }
        }

        /// <summary>
        /// 有一个方法
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        class ClassA3
        {
            public ClassA3() { }

            ~ClassA3() { }

            public void Func1() { }
        }

        /// <summary>
        /// 有一个虚方法
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        class ClassA4
        {
            public ClassA4() { }

            ~ClassA4() { }

            public virtual int Func1()
            {
                return 1;
            }
        }
    }
}