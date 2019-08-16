using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RHFramework.Tests
{
    public class HotUpdateTest
    {


        [Test]
        public void HotUpdateTestSimpleTest()
        {
            var needUpdate = HotUpdateMgr.Instance.HasNewVersionRes;

            if (needUpdate)
            {
                UpdateRes();
                Debug.Log("继续");
            }
            else
            {
                Debug.Log("继续");
            }

            Assert.IsTrue(true);
        }

        void UpdateRes()
        {
            Debug.Log("开始更新");
            DownloadRes();
            ReplaceLocalRes();
            Debug.Log("结束更新");
        }

        private void DownloadRes()
        {
            Debug.Log("1.下载资源");
        }

        private static void ReplaceLocalRes()
        {
            Debug.Log("2.替换掉本地资源");
        }
    }
}
