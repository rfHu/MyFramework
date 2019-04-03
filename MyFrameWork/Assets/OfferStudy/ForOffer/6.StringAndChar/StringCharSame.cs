using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine; 

namespace ForOffer
{
    namespace AboutString
    {
        public class StringCharSame
        {
#if UNITY_EDITOR
            [UnityEditor.MenuItem("ForOffer/6.SameOrNot", false, 6)]
#endif
            static void MenuCilcked()
            {
                string str1 = "hello world";
                string str2 = "hello world";

                object obj1 = str1;
                object obj2 = str2;

                char[] str3 = str1.ToCharArray();
                char[] str4 = str1.ToCharArray();
                char[] str5 = str2.ToCharArray();


                Debug.LogFormat("{0}, {1}, same:{2}", str1, str2, str1 == str2);//true
                Debug.LogFormat("{0}, {1}, same:{2}", obj1, obj2, obj1 == obj2);//true
                Debug.LogFormat("{0}, {1}, same:{2}", obj1, obj2, obj1.Equals(obj2));//true
                Debug.LogFormat("{0}, {1}, same:{2}", str3, str4, str3 == str4);//false
                Debug.LogFormat("{0}, {1}, same:{2}", str3, str5, str3 == str5);//false

                //obj的对比与预期不符
                //初步得出结论，当有多个字符串变量包含了同样的字符串实际值时，CLR可能不会为它们重复地分配内存，而是让它们统统指向同一个字符串对象实例
                //String类有immutable的特性。任何对字符串的更改都是返回新的String对象。所以相同指向同一实例完全合理

                //此机制的实现，CLR维护了一个叫Intern Pool的表
                //使用字面量声明的字符串会进入驻留池，而其他方式声明的字符串并不会进入。例子如下：

                StringBuilder sb = new StringBuilder();
                sb.Append("He").Append("llo");

                string str6 = "Hello";
                string str7 = sb.ToString();

                Debug.LogFormat("{0}, {1}, same:{2}", str6, str7, (object)str6 == (object)str7);//false

                //由于s2不是通过字面量声明的，CLR在为sb.ToString()方法的返回值分配内存时，并不会到驻留池中去检查是否有值为“Hello”的字符串已经存在了
                //为了让编程者能够强制CLR检查驻留池，以避免冗余的字符串副本，String类的设计者提供了一个名为Intern的类方法。例子如下：
                StringBuilder sb2 = new StringBuilder();
                sb2.Append("He").Append("llo");

                string str8 = "Hello";
                string str9 = String.Intern(sb2.ToString());

                Debug.LogFormat("{0}, {1}, same:{2}", str8, str9, (object)str8 == (object)str9);//true

                //要注意：这样不能省却字符串内存分配操作，因为作为参数的字符串已经被分配了一次内存了
                //但是随着时间的流逝，参数所引用的那个副本会被垃圾回收掉，这样对于该字符串内存中就不存在冗余了
                //副作用：即使已经不存在任何其它引用指向驻留池中的字符串了，这个字符串仍然不一定会被垃圾回收掉
                //也就是说即使驻留池中的字符串已经没有用处了，它可能也要等到CLR终结时才被销毁。使用Intern方法的时候，也应该考虑到这个特殊的行为
            }
        }
    }
}