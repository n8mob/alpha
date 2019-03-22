using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace com.corporealabstract.alpha.tests
{
    public class FixedWidthDeserializerTest
    {
        private FixedWidthMessage m1;
        private FixedWidthDeserializer unitUnderTest;
        private FixedWidthEncoder encoder1;

        [SetUp]
        public void SetUp()
        {
            encoder1 = new FixedWidthEncoder(2, 0, 'A');
            encoder1.Encoding = new Dictionary<char, byte>()
            {
                {' ', 0},
                {'A', 1},
                {'B', 2},
                {'C', 3}
            };
            
            m1 = new FixedWidthMessage(encoder1, "011011");
            unitUnderTest = new FixedWidthDeserializer(m1);
        }

        [Test]
        public void MinimalTest()
        {
            unitUnderTest.MoveNext();
            Assert.AreEqual('A', unitUnderTest.Current);
        }

        [Test]
        public void MainTest()
        {
            Assert.Throws(typeof(InvalidOperationException), () => { var unused = unitUnderTest.Current; });

            unitUnderTest.MoveNext();

            Assert.AreEqual('A', unitUnderTest.Current);
            Assert.IsTrue(unitUnderTest.MoveNext());
            Assert.AreEqual('B', unitUnderTest.Current);
            Assert.IsTrue(unitUnderTest.MoveNext());
            Assert.AreEqual('C', unitUnderTest.Current);
            Assert.IsFalse(unitUnderTest.MoveNext(), "MoveNext after last letter");
        }

        [Test]
        public void PartialLetterTest()
        {
            var m2 = new FixedWidthMessage(encoder1, "01010");

            unitUnderTest = new FixedWidthDeserializer(m2);
            
            Assert.IsTrue(unitUnderTest.MoveNext(), "Initial MoveNext()");

            Assert.AreEqual('A', unitUnderTest.Current, "First Letter");
            Assert.IsTrue(unitUnderTest.MoveNext(), "Second MoveNext()");
            Assert.AreEqual('A', unitUnderTest.Current, "Second Letter");
            Assert.IsFalse(unitUnderTest.MoveNext(), "MoveNext should fail on partial letter");
            
        }
    }
}