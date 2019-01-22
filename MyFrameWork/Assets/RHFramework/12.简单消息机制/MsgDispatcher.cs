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
            if (!RegisteredMsgs.ContainsKey(msgName))
            {
                RegisteredMsgs.Add(msgName, _ => { });
            }
            RegisteredMsgs[msgName] += onMsgReceived;
        }

        public static void UnRegisterAll(string msgName)
        {
            RegisteredMsgs.Remove(msgName);
        }

        public static void UnRegister(string msgName, Action<object> onMsgReceived)
        {
            if (RegisteredMsgs.ContainsKey(msgName))
            {
                RegisteredMsgs[msgName] -= onMsgReceived;
            }
            
        }

        public static void Send(string msgName, object data)
        {
            if (RegisteredMsgs.ContainsKey(msgName))
            {
                RegisteredMsgs[msgName](data);
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/12.简易消息机制", false, 12)]
        private static void MenuClicked()
        {
            Register("MSG1", OnMsgReceived);
            Register("MSG1", OnMsgReceived);

            Send("MSG1", "hello world");

            UnRegister("MSG1", OnMsgReceived);

            Send("MSG1", "hello");
        }

        private static void OnMsgReceived(object data)
        {
            Debug.LogFormat("MSG1:{0}", data);
        }
#endif
    }
}