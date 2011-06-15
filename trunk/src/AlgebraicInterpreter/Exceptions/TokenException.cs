using System;
using System.Runtime.Serialization;

namespace WaveletStudio.AlgebraicInterpreter.Exceptions
{
    public class TokenException : AlgebraicException
    {
        private readonly int _index;

        public TokenException(int index)
        {
            _index = index;
        }
        public TokenException(int index, string message)
            : base(message)
        {
            _index = index;
        }
        public TokenException(int index, string message, Exception innerException)
            : base(message, innerException)
        {
            _index = index;
        }
        public TokenException(int index, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _index = index;
        }

        public int Index
        {
            get { return _index; }
        }
    }
}
