using System;
using System.Runtime.Serialization;

namespace WaveletStudio.AlgebraicInterpreter.Exceptions
{
    public class SequenceException : AlgebraicException
    {
        private readonly Expression _first;
        private readonly Expression _second;

        public SequenceException(Expression first, Expression second)
        {
            _first = first;
            _second = second;
        }

        public SequenceException(Expression first, Expression second, string message)
            : base(message)
        {
            _first = first;
            _second = second;
        }

        public SequenceException(Expression first, Expression second, string message, Exception innerException)
            : base(message, innerException)
        {
            _first = first;
            _second = second;
        }

        public SequenceException(Expression first, Expression second, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _first = first;
            _second = second;
        }

        public Expression FirstExpression
        {
            get { return _first; }
        }

        public Expression SecondExpression
        {
            get { return _second; }
        }
    }
}
