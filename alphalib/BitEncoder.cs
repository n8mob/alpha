using System;
using System.Collections.Generic;

namespace com.corporealabstract.alpha
{
    public interface BitEncoder<TEnc, TDec>
    {
        TEnc DefaultEncoded { get; set; }
        TDec DefaultDecoded { get; set; }
        TEnc Encode(TDec c);
        TDec Decode(TEnc value);
        string MakeBitString(TDec c);
        string MakeBitString(string s);
        TDec ReadBitString(string charBits);
    }
}
