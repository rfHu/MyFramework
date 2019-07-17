using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class FrameworkExample : MonoBehaviourSimplify
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/2.框架示例", false, 2)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj").AddComponent<FrameworkExample>();
        }
#endif

        private void Awake()
        {
            MsgDispatcher.UnRegisterAll("Do");
            MsgDispatcher.UnRegisterAll("OK");

            RegisterMsg("Do", DoSomething);
            RegisterMsg("Do", DoSomething);
            RegisterMsg("Do1", _ => { });
            RegisterMsg("Do2", _ => { });
            RegisterMsg("Do3", _ => { });

            RegisterMsg("OK", data =>
            {
                Debug.Log(data);

                UnRegisterMsg("OK");
            });
        }

        IEnumerator Start()
        {
            MsgDispatcher.Send("Do", "Hello");

            SendMsg("OK", "hello");
            SendMsg("OK", "hello");

            yield return new WaitForSeconds(1.0f);

            MsgDispatcher.Send("Do", "Hello1");
        }

        void DoSomething(object data)
        {
            Debug.LogFormat("Received Do Msg:{0}", data);
        }

        protected override void OnBeforeDestroy()
        {

        }
    }
}