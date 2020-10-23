using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RHFramework
{
    [Serializable]
    public class ResVersion 
    {
        public int version;
    }

    public class FakeResServer : MonoSingleton<FakeResServer>
    {
        public void GetRemoteResVersion(Action<int> onRemoteResVersionGet) 
        {
            StartCoroutine(RequestRemoteResVersion(remoteResversion => 
            {
                onRemoteResVersionGet(remoteResversion.version);
            }));
        }

        public void DownloadRes(Action<ResVersion> onResDownloaded)
        {
            StartCoroutine(RequestRemoteResVersion(onResDownloaded));
        }

        private IEnumerator RequestRemoteResVersion(Action<ResVersion> onResDownloaded) 
        {
            var path = Application.dataPath + "/RHFramework/Framework/ResKit/HotUpdate/RemoteResVersion.json";
            var www = new WWW(path);
            yield return www;
            var jsonString = www.text;
            var remoteResVersion = JsonUtility.FromJson<ResVersion>(jsonString);
            onResDownloaded(remoteResVersion);
        }
    }
}