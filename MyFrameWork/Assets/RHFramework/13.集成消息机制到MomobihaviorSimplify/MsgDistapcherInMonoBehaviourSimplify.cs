using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public abstract partial class MonoBehaviourSimplify
    {

        List<MsgRecord> mMsgRecorder = new List<MsgRecord>();

        private class MsgRecord
        {
            private MsgRecord() { }

            public string Name;
            public Action<object> OnMsgReceived;

            static Stack<MsgRecord> mMsgRecordPool = new Stack<MsgRecord>();

            public static MsgRecord Allocate(string msgName, Action<object> onMsgReceived)
            {
                MsgRecord retMsgRecord = null;

                retMsgRecord = mMsgRecordPool.Count > 0 ? mMsgRecordPool.Pop() : new MsgRecord();

                retMsgRecord.Name = msgName;
                retMsgRecord.OnMsgReceived = onMsgReceived;

                return new MsgRecord();
            }

            public void Recycle()
            {
                Name = null;
                OnMsgReceived = null;
                mMsgRecordPool.Push(this);
            }
        }
        
        public void RegisterMsg(string msgName, Action<object> onMsgReceived)
        {
            MsgDispatcher.Register(msgName, onMsgReceived);

            //var msgRecord = MsgRecord.Allocate();

            //msgRecord.Name = msgName;
            //msgRecord.OnMsgReceived = onMsgReceived;

            //mMsgRecorder.Add(msgRecord);

            mMsgRecorder.Add(MsgRecord.Allocate(msgName, onMsgReceived));
        }


        private void OnDestroy()
        {
            OnBeforeDestroy();

            foreach (var msgRecord in mMsgRecorder)
            {
                MsgDispatcher.UnRegister(msgRecord.Name, msgRecord.OnMsgReceived);

                msgRecord.Recycle();
            }

            mMsgRecorder.Clear();
        }

        /// <summary>
        /// 用于存储原本处于OnDestroy中的方法
        /// </summary>
        protected abstract void OnBeforeDestroy();
    }

    public class MsgDistapcherInMonoBehaviourSimplify : MonoBehaviourSimplify
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/13.消息机制集成到MonoBehaviourSimplify", false, 13)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj").AddComponent<MsgDistapcherInMonoBehaviourSimplify>();
        }
#endif

        private void Awake()
        {
            RegisterMsg("Do", DoSomething);
            RegisterMsg("Do", DoSomething);
            RegisterMsg("Do1", _ => { });
            RegisterMsg("Do2", _ => { });
            RegisterMsg("Do3", _ => { });
        }

        IEnumerator Start()
        {
            MsgDispatcher.Send("Do", "Hello");

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