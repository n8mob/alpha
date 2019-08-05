using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace com.corporealabstract.alpha
{
    public class AlphaLengthSerializer
    {
        private readonly Regex onlyOnes = new Regex("^1+$");
        private readonly Regex onlyZeros = new Regex("^0+$");

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

        public string MakeBitString(string s, bool lastLetterGetsSpace = false)
        {
            return string.Concat(Serialize(s, lastLetterGetsSpace));
        }

        public IEnumerable<string> Serialize(string s, bool lastLetterGetsSpace)
        {
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];

                var currentIsChar = Encoders['1'].Encoding.ContainsKey(c);
                var currentIsLast = i + 1 == s.Length;
                var nextIsChar = (s.Length > i + 1) && (Encoders['1'].Encoding.ContainsKey(s[i + 1]));
                
                yield return ChooseEncoder(c).MakeBitString(c);
                
                if ((currentIsChar && lastLetterGetsSpace && currentIsLast) ||
                    (currentIsChar && nextIsChar))
                {
                    yield return "0";
                }
            }
        }

        public string MakeBitString(char c)
        {
            return ChooseEncoder(c).MakeBitString(c);
        }

        private AlphaLengthEncoder ChooseEncoder(char c)
        {
            if (Encoders['0'].Encoding.ContainsKey(c))
            {
                return Encoders['0'];
            }
            else if (Encoders['1'].Encoding.ContainsKey(c))
            {
                return Encoders['1'];
            }
            else
            {
                throw new ArgumentException($"No encoding for '{c}'");
            }
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