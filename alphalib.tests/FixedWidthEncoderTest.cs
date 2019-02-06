using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace com.corporealabstract.alpha.tests
{
    public class FixedWidthEncoderTest
    {
        private Dictionary<char, byte> d1;

        private FixedWidthEncoder fiveBitA1;
        private FixedWidthEncoder weirdEncoding;
        private Dictionary<char, byte> dWeird;

        [SetUp]
        public void SetUp()
        {
            d1 = new Dictionary<char, byte>
            {
                ['A'] = 1,
                ['B'] = 2,
                ['_'] = 0
            };

            dWeird = new Dictionary<char, byte>
            {
                ['A'] = 0x0B,
                ['B'] = 0x71,
                ['C'] = 0xE9,
                ['D'] = 0xFF
            };

            fiveBitA1 = new FixedWidthEncoder(5)
            {
                Encoding = d1
            };

            weirdEncoding = new FixedWidthEncoder(9)
            {
                Encoding = dWeird
            };
        }

        [Test]
        public void MakeBitStringA() => Assert.AreEqual("00001", fiveBitA1.MakeBitString('A'));

        [Test]
        public void MakeBitStringB() => Assert.AreEqual("00010", fiveBitA1.MakeBitString('B'));

        [Test]
        public void EncodeA() => Assert.AreEqual(1, fiveBitA1.Encode('A'));

        [Test]
        public void EncodeB() => Assert.AreEqual(2, fiveBitA1.Encode('B'));

        [Test]
        public void EncodeSpace() => Assert.AreEqual(0, fiveBitA1.Encode('_'));

        [Test]
        public void Decode1() => Assert.AreEqual('A', fiveBitA1.Decode(1));

        [Test]
        public void Decode2() => Assert.AreEqual('B', fiveBitA1.Decode(2));

        [Test]
        public void Weird1() => Assert.AreEqual("000001011", weirdEncoding.MakeBitString('A'));

        [Test]
        public void Weird2() => Assert.AreEqual("011111111", weirdEncoding.MakeBitString('D'));

        [Test]
        public void Weird3() => Assert.AreEqual('D', weirdEncoding.Decode(255));

        [Test]
        public void Weird4() => Assert.AreEqual('?', weirdEncoding.Decode(20));

        [Test]
        public void Weird5() => Assert.AreEqual('D', weirdEncoding.ReadBitString("11111111"));

        [Test]
        public void TooManyBits()
        {
            Assert.Throws(typeof(OverflowException), () => weirdEncoding.ReadBitString("111111111"));
        }
    }
}
