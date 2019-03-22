using System;

namespace com.corporealabstract.alpha
{
    public class FixedWidthEncoder : BaseEncoder<byte>
    {
        private byte defaultEncoded;
        private char defaultDecoded;

        public byte Width { get; }

        public override byte DefaultEncoded
        {
            get => defaultEncoded;
            set => defaultEncoded = value;
        }

        public override char DefaultDecoded
        {
            get => defaultDecoded;
            set => defaultDecoded = value;
        }

        public FixedWidthEncoder(byte width, byte defaultEncoded = 0, char defaultDecoded = '?')
        {
            Width = width;
            this.defaultEncoded = defaultEncoded;
            this.defaultDecoded = defaultDecoded;
        }

        public override string MakeBitString(char c) => Convert.ToString(Encode(c), 2).PadLeft(Width, '0');

        public override char ReadBitString(string charBits) => Decode(Convert.ToByte(charBits, 2));
    }
}
