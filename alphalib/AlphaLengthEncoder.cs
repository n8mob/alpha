using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.corporealabstract.alpha
{
    public class AlphaLengthEncoder : BaseEncoder<int>
    {
        Regex onlyOnes = new Regex("^1*$");

        public string PunctuationCharacters { get; set; }

        public override int DefaultEncoded { get; set; }
        public override char DefaultDecoded { get; set; }
        public override string MappedEncoding { get; set; }

        public override string MakeBitString(char c)
        {
            if (PunctuationCharacters.Contains(c))
            {
                return new string('0', Encode(c)) + '0';
            }

            return new string('1', Encode(c)) + '0';
        }

        public override char ReadBitString(string charBits)
        {
            if (!onlyOnes.IsMatch(charBits))
            {
                throw new ArgumentException("Encoded character must be all '1's");
            }

            return Decode(charBits.Length);
        }

        public override IEnumerable<KeyValuePair<char, int>> ReadEncoding(string input)
        {
            var charEncodings = input.Split('\n');

            foreach (var charEncoding in charEncodings)
            {
                var encodingParts = charEncoding.Split(ENCODING_SPLITTERS, StringSplitOptions.RemoveEmptyEntries);
                char c = encodingParts[0][0];
                byte b = Convert.ToByte(encodingParts[1]);

                yield return new KeyValuePair<char, int>(c, b);
            }
        }
    }
}
