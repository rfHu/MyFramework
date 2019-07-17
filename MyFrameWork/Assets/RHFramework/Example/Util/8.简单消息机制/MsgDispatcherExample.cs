using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class MsgDispatcherExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/Util/8.简易消息机制", false, 8)]
        private static void MenuClicked()
        {
            //全部清空，确保事例有效
            MsgDispatcher.UnRegisterAll("MSG1");

            MsgDispatcher.Register("MSG1", OnMsgReceived);
            MsgDispatcher.Register("MSG1", OnMsgReceived);

            MsgDispatcher.Send("MSG1", "hello world");

            MsgDispatcher.UnRegister("MSG1", OnMsgReceived);

            MsgDispatcher.Send("MSG1", "hello");
        }

        private static void OnMsgReceived(object data)
        {
            Debug.LogFormat("MSG1:{0}", data);
        }
#endif
    }
}