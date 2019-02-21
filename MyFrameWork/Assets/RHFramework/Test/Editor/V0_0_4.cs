using NUnit.Framework;

namespace RHFramework
{
    public class V0_0_4
    {
        class SingletonTestClass : Singleton<SingletonTestClass>
        {
            private SingletonTestClass() { }
        }
        [Test]
        public void SingletonTest()
        {
            var instanceA = SingletonTestClass.Instance;
            var instanceB = SingletonTestClass.Instance;
            Assert.AreEqual(instanceA.GetHashCode(), instanceB.GetHashCode());
        }
    }
}