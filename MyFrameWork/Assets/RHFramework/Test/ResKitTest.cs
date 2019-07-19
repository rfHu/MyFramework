using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RHFramework.Tests
{
    public class ResKitTest
    {
        [Test]
        public void LoadAsyncExceptionTest()
        {
            var resLoader = new ResLoader();

            Assert.Throws<Exception>(() =>
            {
                resLoader.LoadAsync<AssetBundle>("square", squareBundle => { Debug.Log(squareBundle.name); });

                resLoader.LoadSync<AssetBundle>("square");
            });

            resLoader.ReleaseAll();
            resLoader = null;
        }

        [UnityTest] //涉及到异步操作时使用
        public IEnumerator LoadAsyncTest()
        {
            var resLoader = new ResLoader();

            resLoader.LoadAsync<Texture2D>("resources://BigTexture",
                bigTexture => { Assert.AreEqual("BigTexture", bigTexture.name); });

            yield return null;
        }

        [UnityTest]
        public IEnumerator LoadAsyncRefCountTest()
        {
            var resLoader = new ResLoader();
            var loadCount = 0;
            resLoader.LoadAsync<Texture2D>("resources://BigTexture",
                bigTexture => loadCount++);
            resLoader.LoadAsync<Texture2D>("resources://BigTexture",
                bigTexture => loadCount++);

            yield return new WaitUntil(() => loadCount == 2);

            var bigTextureRes = ResMgr.Instance.SharedLoadedReses.Find(res => res.Name == "resources://BigTexture");

            Assert.AreEqual(1, bigTextureRes.RefCount);

            var resLoader2 = new ResLoader();
            resLoader.LoadSync<Texture2D>("resources://BigTexture");

            Assert.AreEqual(1, bigTextureRes.RefCount);

            resLoader.ReleaseAll();
            resLoader = null;
            resLoader2.ReleaseAll();
            resLoader2 = null;

            Assert.False(ResMgr.Instance.SharedLoadedReses.Any(res => res.Name == "resources://BigTexture"));
        }

        [UnityTest]
        public IEnumerator LoadABTest()
        {
            var resLoader = new ResLoader();

            resLoader.LoadAsync<AssetBundle>("testgo", bundle =>
            {
                var gameObjPrefab = bundle.LoadAsset<GameObject>("GameObject");

                Assert.IsNotNull(gameObjPrefab);
            });

            yield return null;

            resLoader.ReleaseAll();
            resLoader = null;
        }

        [UnityTest]
        public IEnumerator LoadAssetSyncTest()
        {
            var resLoader = new ResLoader();

            var squareTexture = resLoader.LoadSync<Texture2D>("square", "Square");

            Assert.IsNotNull(squareTexture);

            resLoader.ReleaseAll();
            resLoader = null;
            yield return null;
        }

        [UnityTest]
        public IEnumerator LoadAssetAsyncTest()
        {
            var resLoader = new ResLoader();

            resLoader.LoadAsync<Texture2D>("square", "Square", squareTesture =>
            {
                Assert.IsNotNull(squareTesture);

                resLoader.ReleaseAll();
                resLoader = null;
            });

            yield return null;
        }
    }
}
