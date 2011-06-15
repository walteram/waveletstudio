using System;
using System.Runtime.Serialization;

namespace WaveletStudio.AlgebraicInterpreter.Exceptions
{
    public class AlgebraicException : ApplicationException 
    {
        public AlgebraicException() { }
        public AlgebraicException(string message)
            : base(message) { }
        public AlgebraicException(string message, Exception innerException)
            : base(message, innerException) { }
        public AlgebraicException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }    
}
