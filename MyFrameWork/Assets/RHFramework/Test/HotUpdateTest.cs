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
            // 弹出 persistentDataPath 目录
            Application.OpenURL(Application.persistentDataPath);

            var finished = false;

            HotUpdateMgr.Instance.CheckState(() =>
            {
                Debug.Log(HotUpdateMgr.Instance.State);

                HotUpdateMgr.Instance.HasNewVersionRes(needUpdate =>
                {
                    if (needUpdate)
                    {
                        HotUpdateMgr.Instance.UpdateRes(() =>
                        {
                            Debug.Log("继续");
                            Assert.IsTrue(true);
                            finished = true;
                        });
                    }
                    else
                    {
                        Debug.Log("不需要热更新");
                        Debug.Log("继续");
                        Assert.IsTrue(true);
                        finished = true;
                    }
                });
            });

            while (!finished) 
            {
                yield return null;
            }
        }
    }
}
