using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.corporealabstract.alpha
{
    public class AlphaLengthEncoder : BaseEncoder<int>
    {
        private int _defaultEncoded;
        private char _defaultDecoded;

        public char EncodingChar { get; }

        private Regex onlyEncodedChar;

        public override int DefaultEncoded
        {
            get => _defaultEncoded;
            set => _defaultEncoded = value;
        }

        public override char DefaultDecoded
        {
            get => _defaultDecoded;
            set => _defaultDecoded = value;
        }

        public AlphaLengthEncoder(char encodingChar,
                                  int defaultEncoded,
                                  char defaultDecoded)
        {
            _defaultEncoded = defaultEncoded;
            _defaultDecoded = defaultDecoded;

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
