using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace RHFramework
{
    public class HotUpdateConfig
    {
        public virtual string HotUpdateResVersionFilePath
        {
            get { return Application.persistentDataPath + "/AssetBundles/ResVersion.json"; }
        }

        public virtual string HotUpdateAssetBundlesFolder 
        {
            get { return Application.persistentDataPath + "/AssetBundles/"; }
        }

        public virtual string LocalResVersionFilePath 
        {
            get { return Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtil.GetPlatformName() + "/ResVersion.json"; }
        }

        public virtual string LocalAssetBundlesFolder
        {
            get { return Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtil.GetPlatformName() + "/"; }
        }

        public virtual string RemoteResVersionURL 
        {
            get { return Application.dataPath + "/RHFramework/Framework/ResKit/HotUpdate/Remote/ResVersion.json"; }
        }

        public virtual string RemoteAssetBundleURLBase
        {
            get { return Application.dataPath + "/RHFramework/Framework/ResKit/HotUpdate/Remote/"; }
        }

        public virtual ResVersion LoadHotUpdateAssetBundlesFolderResVersion() 
        {
            var persisResVersionFilePath = HotUpdateResVersionFilePath;

            if (!File.Exists(persisResVersionFilePath))
            {
                return null;
            }

            var persistResVersionJson = File.ReadAllText(persisResVersionFilePath);
            var persisResVersion = JsonUtility.FromJson<ResVersion>(persistResVersionJson);

            return persisResVersion;
        }

        public virtual IEnumerator GetStreamingAssetResVersion(Action<ResVersion> getResVersion)
        {
            var localResVersionPath = LocalResVersionFilePath;
            var www = new WWW(localResVersionPath);
            yield return www;

            var streamingResVersion = JsonUtility.FromJson<ResVersion>(www.text);
            getResVersion(streamingResVersion);
        }

        public virtual IEnumerator RequestRemoteResVersion(Action<ResVersion> onResDownloaded)
        {
            var path = RemoteResVersionURL;
            var www = new WWW(path);
            yield return www;
            var jsonString = www.text;
            var remoteResVersion = JsonUtility.FromJson<ResVersion>(jsonString);
            onResDownloaded(remoteResVersion);
        }
    }
}