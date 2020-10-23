using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RHFramework.Tests
{
    public class HotUpdateTest
    {


        [UnityTest]
        public IEnumerator HotUpdateTestSimpleTest()
        {
            HotUpdateMgr.Instance.HasNewVersionRes(needUpdate => 
            {
                if (needUpdate)
                {
                    HotUpdateMgr.Instance.UpdateRes(() =>
                    {
                        Debug.Log("继续");
                        Assert.IsTrue(true);
                    });
                }
                else
                {
                    Debug.Log("继续");
                    Assert.IsTrue(true);
                }
            });
            yield return null;
        }
    }
}
