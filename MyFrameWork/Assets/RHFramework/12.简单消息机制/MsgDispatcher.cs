using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class MsgDispatcher
    {
        private static Dictionary<string, Action<object>> RegisteredMsgs = new Dictionary<string, Action<object>>();

        public static void Register(string msgName, Action<object> onMsgReceived)
        {
            RegisteredMsgs.Add(msgName, onMsgReceived);
        }

        public static void UnRegister(string MsgName)
        {
            RegisteredMsgs.Remove(MsgName);
        }

        public static void Send(string msgName, object data)
        {
            RegisteredMsgs[msgName](data);
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/12.简易消息机制", false, 12)]
        private static void MenuClicked()
        {
            Register("MSG1", data => { Debug.LogFormat("MSG1:{0}", data); });

            Send("MSG1", "hello world");

            UnRegister("MSG1");

            Send("MSG1", "hello world");
        }
#endif
    }
}