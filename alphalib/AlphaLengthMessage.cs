using System.Collections;
using System.Collections.Generic;
namespace com.corporealabstract.alpha
{
    public class AlphaLengthMessage : IEnumerable<char>
    {
        public AlphaLengthSerializer Serializer { get; }

        public string Code { get; }

        public AlphaLengthMessage(AlphaLengthSerializer serializer, string code)
        {
            Serializer = serializer;
            Code = code;
        }

        public IEnumerator<char> GetEnumerator() => new AlphaLengthDeserializer(this);

        IEnumerator IEnumerable.GetEnumerator() => new AlphaLengthDeserializer(this);
    }
}
