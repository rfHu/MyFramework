using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using RHFramework;

namespace RHFramework.Tests
{
    public class HotUpdateExtendTest 
    {
        public class TestHotUpdateConfig : HotUpdateConfig 
        {
            public override string HotUpdateAssetBundlesFolder 
            {
                get { return Application.persistentDataPath + "/newAB/"; }
            }
        }

        [UnityTest]
        public IEnumerator HotUpdate_ExpandTest() 
        {
            bool testEnv = true;

            if (testEnv)
            {
                FullHotUpdateMgr.Instance.Config = new TestHotUpdateConfig();
            }
            else 
            {
                FullHotUpdateMgr.Instance.Config = new HotUpdateConfig();
            }

            FullHotUpdateMgr.Instance.CheckState(()=> 
            {
                Debug.Log(FullHotUpdateMgr.Instance.State);

                //Assert.AreEqual(HotUpdateMgr.Instance.State, HotUpdateState.NeverUpdate);
            });

            yield return null;
        }
    }
}