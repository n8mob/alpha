using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.corporealabstract.alpha
{
    public class AlphaLengthSerializer
    {
        private Regex onlyOnes = new Regex("^1+$");
        private Regex onlyZeros = new Regex("^0+$");

        public int DefaultEncoded { get; set; }
        public char DefaultDecoded { get; set; }

        public Dictionary<char, AlphaLengthEncoder> Encoders;

        public AlphaLengthSerializer(int defaultEncoded = 0, char defaultDecoded = '?')
        {
            DefaultEncoded = defaultEncoded;
            DefaultDecoded = defaultDecoded;
            Encoders = new Dictionary<char, AlphaLengthEncoder>
            {
                ['1'] = new AlphaLengthEncoder('1', defaultEncoded, defaultDecoded),
                ['0'] = new AlphaLengthEncoder('0', defaultEncoded, defaultDecoded)
            };
        }

        public string MakeBitString(string s) => string.Concat(s.Select(MakeBitString));

        public string MakeBitString(char c)
        {
            return ChooseEncoder(c).MakeBitString(c) + '0';
        }

        public AlphaLengthEncoder ChooseEncoder(char c)
        {
            return Encoders['0'].Encoding.ContainsKey(c) ? Encoders['0'] : Encoders['1'];
        }

        public char ReadBitString(string charBits)
        {
            if (onlyOnes.IsMatch(charBits))
            {
                return Encoders['1'].Decode(charBits.Length);
            }

            if (onlyZeros.IsMatch(charBits))
            {
                return Encoders['0'].Decode(charBits.Length);
            }

            throw new ArgumentException("Encoded character must be all '1's or all '0's");
        }
    }
}
