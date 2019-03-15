using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class SimpleRCExample : MonoBehaviour
    {
        class Light
        {
            public void Open()
            {
                Debug.Log("灯亮了");
            }

            public void Close()
            {
                Debug.Log("灯灭了");
            }
        }

        class Room : SimpleRC
        {
            Light mLight = new Light();
            
            public void EnterProple()
            {
                if (RefCount == 0)
                {
                    mLight.Open();
                }
                Retain();

                Debug.LogFormat("一个人进入房间，目前房间有{0}人", RefCount);
            }

            public void LeavePeople()
            {
                Release();

                Debug.LogFormat("一个人离开房间，目前房间有{0}人", RefCount);
            }

            protected override void OnZeroRef()
            {
                mLight.Close();
            }
        }


#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Example/22.SimpleRCExample", false, 22)]
#endif
        static void MenuCilcked()
        {
            var room = new Room();

            room.EnterProple();
            room.EnterProple();
            room.EnterProple();

            room.LeavePeople();
            room.LeavePeople();
            room.LeavePeople();
        }
    }
}