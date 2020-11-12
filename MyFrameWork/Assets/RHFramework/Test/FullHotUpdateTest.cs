using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RHFramework.Tests
{
    public class FullHotUpdateTest
    {
        [UnityTest]
        public IEnumerator FullHotUpdateTestSimpleTest()
        {
            // 弹出 persistentDataPath 目录
            Application.OpenURL(Application.persistentDataPath);

            var finished = false;

            FullHotUpdateMgr.Instance.CheckState(() =>
            {
                Debug.Log(FullHotUpdateMgr.Instance.State);

                FullHotUpdateMgr.Instance.HasNewVersionRes(needUpdate =>
                {
                    if (needUpdate)
                    {
                        FullHotUpdateMgr.Instance.UpdateRes(() =>
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
