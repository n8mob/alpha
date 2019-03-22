using System;
using System.Text.RegularExpressions;

namespace com.corporealabstract.alpha
{
    public class AlphaLengthEncoder : BaseEncoder<int>
    {
        private int defaultEncoded;
        private char defaultDecoded;

        public char EncodingChar { get; }

        private readonly Regex onlyEncodedChar;

        public override int DefaultEncoded
        {
            get => defaultEncoded;
            set => defaultEncoded = value;
        }

        public override char DefaultDecoded
        {
            get => defaultDecoded;
            set => defaultDecoded = value;
        }

        public AlphaLengthEncoder(char encodingChar,
                                  int defaultEncoded,
                                  char defaultDecoded)
        {
            this.defaultEncoded = defaultEncoded;
            this.defaultDecoded = defaultDecoded;

            EncodingChar = encodingChar;

            onlyEncodedChar = new Regex($"^{encodingChar}*$");
        }

        public override string MakeBitString(char c)
        {
            return new string(EncodingChar, Encode(c));
        }

        public override char ReadBitString(string charBits)
        {
            if (!onlyEncodedChar.IsMatch(charBits))
            {
                throw new ArgumentException($"Encoded character must be all '{EncodingChar}'s");
            }

            return Decode(charBits.Length);
        }
    }
}
