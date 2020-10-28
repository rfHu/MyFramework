using NUnit.Framework;
using System.IO;
using UnityEngine;

namespace RHFramework
{
    public class NewBehaviourScript : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Playground")]
        static void Test() 
        {
            var path = Application.dataPath + "/RHFramework/Framework/ResKit/HotUpdate/RemoteResVersion.json";
            var jsonContent = JsonUtility.ToJson(new ResVersion());

            File.WriteAllText(path, jsonContent);
            UnityEditor.AssetDatabase.Refresh();
        }

        [Test]
        public void PlayMode() 
        {
            //Debug.Log(HotUpdateMgr.Instance.GetLocalResVersion());
        }
#endif
    }
}