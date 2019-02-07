using System;
using System.Collections.Generic;
using System.Linq;

namespace com.corporealabstract.alpha
{
    public abstract class BaseEncoder<TEnc> : BitEncoder<TEnc, char>
    {
        protected Dictionary<char, TEnc> encoding = new Dictionary<char, TEnc>();
        private Dictionary<TEnc, char> decoding = new Dictionary<TEnc, char>();

        public virtual Dictionary<char, TEnc> Encoding
        {
            get => encoding;
            set
            {
                encoding = value;
                decoding.Clear();
                foreach (var item in value)
                {
                    decoding[item.Value] = item.Key;
                }
            }
        }

        public abstract TEnc DefaultEncoded { get; set; }
        public abstract char DefaultDecoded { get; set; }

        public char Decode(TEnc value) => decoding.ContainsKey(value) ? decoding[value] : DefaultDecoded;

        public TEnc Encode(char c) => Encoding.ContainsKey(c) ? Encoding[c] : DefaultEncoded;

        public string MakeBitString(string s) => string.Concat(s.Select(MakeBitString));

        public abstract string MakeBitString(char c);

        public abstract char ReadBitString(string charBits);
    }
}
