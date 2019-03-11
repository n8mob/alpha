using System;
using System.Collections;
using System.Collections.Generic;

namespace com.corporealabstract.alpha
{
    public class AlphaLengthDeserializer : IEnumerator<char>
    {
        int curStart = -1;
        char curChar = '1';
        char otherChar = '0';

        private AlphaLengthMessage Message { get; set; }

        public AlphaLengthDeserializer(AlphaLengthMessage message)
        {
            Message = message;
        }

        public bool MoveNext()
        {
            if (curStart < 0)
            {
                Reset();
                curStart = 0;
                return true;
            }
            else
            {
                curStart = Message.Code.IndexOf(otherChar, curStart);

                char temp = curChar;
                curChar = otherChar;
                otherChar = temp;

                return curStart >= 0;
            }
        }

        object IEnumerator.Current => Current;

        public char Current
        {
            get
            {
                if (curStart < 0)
                {
                    // seems like string.GetEnumerator().Current returns null before initialization...
                    // should we do the same?
                    throw new InvalidOperationException("Enumeration has not started, Call MoveNext");
                }

                var nextOther = Message.Code.IndexOf(otherChar, curStart);

                if (nextOther < 0)
                {
                    nextOther = Message.Code.Length;
                }

                var nextLen = nextOther - curStart;

                return Message.Serializer.Encoders[curChar].Decode(nextLen);
            }
        }

        public void Dispose()
        {
            Message = null;
        }

        public void Reset()
        {
            curStart = -1;
            curChar = '1';
            otherChar = '0';
        }
    }
}
