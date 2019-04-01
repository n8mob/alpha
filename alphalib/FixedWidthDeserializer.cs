using System;
using System.Collections;
using System.Collections.Generic;

namespace com.corporealabstract.alpha
{
    public class FixedWidthDeserializer : IEnumerator<char>
    {
        private int curStart = -1;
        
        private FixedWidthMessage Message { get; set; }

        public FixedWidthDeserializer(FixedWidthMessage fixedWidthMessage)
        {
            Message = fixedWidthMessage;
        }
        
        public bool MoveNext()
        {
            if (curStart < 0)
            {
                curStart = 0;
                return !string.IsNullOrEmpty(Message.Code);
            }
            else if (curStart + Message.Serializer.Width > Message.Code.Length)
            {
                return false;
            }
            else
            {
                curStart += Message.Serializer.Width;
                return curStart + Message.Serializer.Width <= Message.Code.Length;
            }
        }

        public void Reset()
        {
            curStart = -1;
        }

        public char Current {
            get
            {
                if (curStart < 0)
                {
                    throw new InvalidOperationException("Enumeration has not started, Call MoveNext");
                }
                
                return Message.Serializer.ReadBitString(Message.Code.Substring(curStart, Message.Serializer.Width));
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() => Message = null;
    }
}