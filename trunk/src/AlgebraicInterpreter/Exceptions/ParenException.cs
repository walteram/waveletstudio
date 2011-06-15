using System;
using System.Runtime.Serialization;

namespace WaveletStudio.AlgebraicInterpreter.Exceptions
{
    public class ParenException : AlgebraicException
    {
        public ParenException() { }
        public ParenException(string message)
            : base(message) { }
        public ParenException(string message, Exception innerException)
            : base(message, innerException) { }
        public ParenException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
