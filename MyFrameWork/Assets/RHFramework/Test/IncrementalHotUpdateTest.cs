using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TestTools;

namespace RHFramework.Tests
{
    public class IncrementalHotUpdateTest
    {
        [UnityTest]
        public IEnumerator IncrementalHotUpdateSimpleTest()
        {
            Application.OpenURL(Application.persistentDataPath);

            var finished = false;

            IncrementHotUpdateMgr.Instance.HasNewVersionRes((needDownloadResVersion) => 
            {
                if (needDownloadResVersion.AssetBundleNames.Count != 0)
                {
                    IncrementHotUpdateMgr.Instance.UpdateRes(needDownloadResVersion.AssetBundleNames, () => 
                    {
                        finished = true;
                        Debug.Log("更新完毕，继续");
                        Assert.IsTrue(true);
                    },error=> 
                    {
                        Debug.Log(error);
                    });
                }
                else
                {
                    finished = true;
                    Debug.Log("无需更新，继续");
                    Assert.IsTrue(true);
                }
            });

            while (!finished)
            {
                yield return null;
            }
        }
    }
}