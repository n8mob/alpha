using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace com.corporealabstract.alpha.tests
{
    public class AlphaLengthSerializerTest
    {
        private AlphaLengthSerializer unitUnderTest;

        [SetUp]
        public void SetUp()
        {
            unitUnderTest = new AlphaLengthSerializer();
            unitUnderTest.Encoders['1'].Encoding = new Dictionary<char, int>
            {
                ['A'] = 1,
                ['B'] = 2
            };

            unitUnderTest.Encoders['0'].Encoding = new Dictionary<char, int>
            {
                ['\u200b'] = 1,
                ['_'] = 2,
                ['.'] = 3
            };
        }

        [Test]
        public void TestUnderscore() => Assert.AreEqual("00", unitUnderTest.MakeBitString('_'));

        [Test]
        public void TestSpace()
        {
            Assert.Throws(typeof(ArgumentException), () => unitUnderTest.MakeBitString(' '));
        }

        [Test]
        public void TestPeriod() => Assert.AreEqual("000", unitUnderTest.MakeBitString('.'));

        [Test]
        public void TestA() => Assert.AreEqual("1", unitUnderTest.MakeBitString('A'));

        [Test]
        public void TestB() => Assert.AreEqual("11", unitUnderTest.MakeBitString('B'));

        [Test]
        public void TestAB() => Assert.AreEqual("1011", unitUnderTest.MakeBitString("AB"));

        [Test]
        public void TestAPeriodBPeriod() => Assert.AreEqual("100011000", unitUnderTest.MakeBitString("A.B."));

        [Test]
        public void ABPointA() => Assert.AreEqual("10110001", unitUnderTest.MakeBitString("AB.A"));

        [Test]
        public void ABSpaceABPeriod() => Assert.AreEqual("1011001011000", unitUnderTest.MakeBitString("AB_AB."));

        [Test]
        public void APointBPointSpaceB() => Assert.AreEqual("1000110000011", unitUnderTest.MakeBitString("A.B._B"));

        [Test]
        public void TestAWithHang() =>
            Assert.AreEqual("10", unitUnderTest.MakeBitString("A", true));

        [Test]
        public void TestBWithHang() =>
            Assert.AreEqual("110", unitUnderTest.MakeBitString("B", true));

        [Test]
        public void TestABWithHang() =>
            Assert.AreEqual("10110", unitUnderTest.MakeBitString("AB", true));

        [Test]
        public void TestAPeriodBPeriodWithHang() =>
            Assert.AreEqual("100011000", unitUnderTest.MakeBitString("A.B.", true));

        [Test]
        public void ABPointAWithHang() =>
            Assert.AreEqual("101100010", unitUnderTest.MakeBitString("AB.A", true));

        [Test]
        public void ABSpaceABPeriodWithHang() =>
            Assert.AreEqual("1011001011000", unitUnderTest.MakeBitString("AB_AB.", true));

        [Test]
        public void APointBPointSpaceBWithHang() =>
            Assert.AreEqual("10001100000110", unitUnderTest.MakeBitString("A.B._B", true));
    }
}
