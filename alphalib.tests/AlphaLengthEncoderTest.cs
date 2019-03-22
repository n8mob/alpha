using System.Collections.Generic;
using NUnit.Framework;

namespace com.corporealabstract.alpha.tests
{
    public class AlphaLengthEncoderTest
    {
        private Dictionary<char, int> d1;
        private Dictionary<char, int> d2;

        private AlphaLengthEncoder unitUnderTest;

        [SetUp]
        public void SetUp()
        {
            d1 = new Dictionary<char, int>
            {
                ['A'] = 1,
                ['B'] = 2
            };

            d2 = new Dictionary<char, int>
            {
                ['C'] = 3,
                ['D'] = 4
            };

            unitUnderTest = new AlphaLengthEncoder('1', 0, '?')
            {
                Encoding = d1
            };
        }

        // this tests code that is actually in BaseEncoder
        [Test]
        public void SetEncodingSetsDecodingTest()
        {
            unitUnderTest.Encoding = d1;
            Assert.AreEqual(unitUnderTest.Encode('A'), 1);
            Assert.AreNotEqual(unitUnderTest.Encode('C'), 3);
            Assert.AreEqual(unitUnderTest.Encode('C'), unitUnderTest.DefaultEncoded);
            Assert.AreEqual(unitUnderTest.Decode(1), 'A');

            unitUnderTest.Encoding = d2;
            Assert.AreEqual(unitUnderTest.Encode('C'), 3);
            Assert.AreEqual(unitUnderTest.Decode(3), 'C');
            Assert.AreEqual(unitUnderTest.Decode(1), unitUnderTest.DefaultDecoded);
        }

        [Test]
        public void MakeBitStringA() => Assert.AreEqual("1", unitUnderTest.MakeBitString('A'));

        [Test]
        public void MakeBitStringB() => Assert.AreEqual("11", unitUnderTest.MakeBitString('B'));

        [Test]
        public void EncodePeriod() => Assert.AreEqual(1, unitUnderTest.Encode('A'));

        [Test]
        public void EncodeA() => Assert.AreEqual(1, unitUnderTest.Encode('A'));

        [Test]
        public void EncodeB() => Assert.AreEqual(2, unitUnderTest.Encode('B'));

        [Test]
        public void Decode1() => Assert.AreEqual('A', unitUnderTest.Decode(1));
    }
}
