using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public partial class MonoBehaviourSimplify
    {
        protected void SendMsg(string msgName, object data)
        {
            MsgDispatcher.Send(msgName, data);
        }

        protected void UnRegisterMsg(string msgName)
        {
            var selectedRecords = mMsgRecorder.FindAll(recorder => recorder.Name == msgName);
            selectedRecords.ForEach(selectRecord =>
            {
                MsgDispatcher.UnRegister(selectRecord.Name,
                selectRecord.OnMsgReceived);
                mMsgRecorder.Remove(selectRecord);
                selectRecord.Recycle();
            });
            selectedRecords.Clear();
        }
        protected void UnRegisterMsg(string msgName, Action<object> onMsgReceived)
        {
            var selectedRecords = mMsgRecorder.FindAll(recorder => recorder.Name == msgName && recorder.OnMsgReceived == onMsgReceived);
            selectedRecords.ForEach(selectRecord =>
            {
                MsgDispatcher.UnRegister(selectRecord.Name,
                selectRecord.OnMsgReceived);
                mMsgRecorder.Remove(selectRecord);
                selectRecord.Recycle();
            });
            selectedRecords.Clear();
        }
    }

    public class UnifyAPIStyle : MonoBehaviourSimplify
    {
        protected override void OnBeforeDestroy()
        {
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/14.统一 API 风格", false, 14)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj").AddComponent<UnifyAPIStyle>();
        }
#endif

        private void Awake()
        {
            RegisterMsg("OK", data => 
            {
                Debug.Log(data);

                UnRegisterMsg("OK");
            });
        }

        private void Start()
        {
            SendMsg("OK", "hello");
            SendMsg("OK", "hello");
        }
    }
}