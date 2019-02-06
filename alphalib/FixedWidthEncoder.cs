using System;
using System.Collections.Generic;

namespace com.corporealabstract.alpha
{
    public class FixedWidthEncoder : BaseEncoder<byte>
    {
        private byte _defaultEncoded;
        private char _defaultDecoded;

        public byte Width { get; }

        public override byte DefaultEncoded
        {
            get => _defaultEncoded;
            set => _defaultEncoded = value;
        }

        public override char DefaultDecoded
        {
            get => _defaultDecoded;
            set => _defaultDecoded = value;
        }

        public FixedWidthEncoder(byte width, byte defaultEncoded = 0, char defaultDecoded = '?')
        {
            Width = width;
            _defaultEncoded = defaultEncoded;
            _defaultDecoded = defaultDecoded;
        }

        public override string MakeBitString(char c) => Convert.ToString(Encode(c), 2).PadLeft(Width, '0');

        public override char ReadBitString(string charBits) => Decode(Convert.ToByte(charBits, 2));
    }
}
