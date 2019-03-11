using System.Collections;
using System.Collections.Generic;

namespace com.corporealabstract.alpha
{
    public class FixedWidthMessage : IEnumerable<char>
    {
        public FixedWidthEncoder Serializer { get; }
        
        public string Code { get; }

        public FixedWidthMessage(FixedWidthEncoder serializer, string code)
        {
            Serializer = serializer;
            Code = code;
        }
        
        public IEnumerator<char> GetEnumerator() => new FixedWidthDeserializer(this);

        IEnumerator IEnumerable.GetEnumerator() => new FixedWidthDeserializer(this);
    }
}