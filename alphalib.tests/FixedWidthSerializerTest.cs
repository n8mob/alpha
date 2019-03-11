using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace com.corporealabstract.alpha.tests
{
    public class FixedWidthSerializerTest
    {
        private FixedWidthEncoder unitUnderTest;

        [SetUp]
        public void SetUp()
        {
            unitUnderTest = new FixedWidthEncoder(3);
            unitUnderTest.Encoding = new Dictionary<char, byte>()
            {
                [' '] = 0b000,
                ['A'] = 0b001,
                ['B'] = 0b010,
                ['C'] = 0b011,
                ['D'] = 0b100,
                ['E'] = 0b101,
                ['F'] = 0b110,
                ['.'] = 0b111,
            };
        }

        [Test]
        public void TestSpace() => Assert.AreEqual("000", unitUnderTest.MakeBitString(' '));

        [Test]
        public void TestPeriod() => Assert.AreEqual("111", unitUnderTest.MakeBitString('.'));

        [Test]
        public void TestAB() => Assert.AreEqual(
            "001010",
            String.Join("", unitUnderTest.Serialize("AB")));
    }
}