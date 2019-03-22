using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace com.corporealabstract.alpha.tests
{
    public class AlphaLengthDeserializerTest
    {
        private const char ZeroWidthSpace = '\u200b';
        private AlphaLengthMessage m1;
        private AlphaLengthSerializer e1;
        private Dictionary<char, int> d1;
        private Dictionary<char, int> d2;
        private AlphaLengthDeserializer unitUnderTest;

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
                [ZeroWidthSpace] = 1,
                ['_'] = 2,
                ['.'] = 3
            };

            e1 = new AlphaLengthSerializer();
            e1.Encoders['1'].Encoding = d1;
            e1.Encoders['0'].Encoding = d2;

            m1 = new AlphaLengthMessage(e1, "1011");

            unitUnderTest = new AlphaLengthDeserializer(m1);
        }

        [Test]
        public void MinimalTest()
        {
            unitUnderTest.MoveNext();
            Assert.AreEqual('A', unitUnderTest.Current);
        }

        [Test]
        public void Test2()
        {
            Assert.Throws(typeof(InvalidOperationException), () => { var unused = unitUnderTest.Current; });
            unitUnderTest.MoveNext();
            Assert.AreEqual('A', unitUnderTest.Current);
            Assert.IsTrue(unitUnderTest.MoveNext());
            Assert.AreEqual(ZeroWidthSpace, unitUnderTest.Current);
            Assert.IsTrue(unitUnderTest.MoveNext());
            Assert.AreEqual('B', unitUnderTest.Current);
            Assert.IsFalse(unitUnderTest.MoveNext());
        }

        [Test]
        public void TestLongMessage()
        {
            var zw = ZeroWidthSpace; // this just makes expectedString shorter
            var expectedString = $"A{zw}B{zw}A{zw}B{zw}B{zw}B{zw}B{zw}B{zw}";
            var expectedBareMessage = "ABABBBBB";
            var expected = expectedString.GetEnumerator();

            var longMessage = new AlphaLengthMessage(e1, "1011010110110110110110");

            var unit2 = new AlphaLengthDeserializer(longMessage);

            while(unit2.MoveNext() && expected.MoveNext())
            {
                Assert.AreEqual(expected.Current, unit2.Current);
            }

            expected.Dispose();

            Assert.AreEqual(expectedString, new string(longMessage.ToArray()));
            var bareMessage = new string(longMessage.Where(c => c != ZeroWidthSpace).ToArray());
            Assert.AreEqual(expectedBareMessage, bareMessage);
        }
    }
}
