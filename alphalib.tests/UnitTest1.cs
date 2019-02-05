using NUnit.Framework;

using com.corporealabstract.alpha;

namespace Tests
{
    public class Tests
    {
        private EmptyClass unitUnderTest;

        [SetUp]
        public void Setup()
        {
            unitUnderTest = new EmptyClass();
        }

        [Test]
        public void IsA()
        {
            Assert.AreEqual('A', unitUnderTest.A1);
        }
    }
}