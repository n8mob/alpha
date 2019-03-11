using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace com.corporealabstract.alpha.tests
{
    public class FixedWidthDeserializerTest
    {
        private FixedWidthMessage m1;
        private FixedWidthDeserializer unitUnderTest;

        [SetUp]
        public void SetUp()
        {
            FixedWidthEncoder e1 = new FixedWidthEncoder(2, 0, 'A');
            e1.Encoding = new Dictionary<char, byte>()
            {
                {' ', 0},
                {'A', 1},
                {'B', 2},
                {'C', 3}
            };
            m1 = new FixedWidthMessage(e1, "011011");
            unitUnderTest = new FixedWidthDeserializer(m1);
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
            Assert.Throws(typeof(InvalidOperationException), () => { var ignore = unitUnderTest.Current; });

            unitUnderTest.MoveNext();

            Assert.AreEqual('A', unitUnderTest.Current);
            Assert.IsTrue(unitUnderTest.MoveNext());
            Assert.AreEqual('B', unitUnderTest.Current);
            Assert.IsTrue(unitUnderTest.MoveNext());
            Assert.AreEqual('C', unitUnderTest.Current);
            Assert.IsFalse(unitUnderTest.MoveNext(), "MoveNext after last letter");
        }
    }
}