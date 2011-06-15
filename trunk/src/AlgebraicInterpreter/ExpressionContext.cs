using System.Collections.Generic;

namespace WaveletStudio.AlgebraicInterpreter
{
    public enum Variable
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }

    public sealed class ExpressionContext
    {
        private readonly Dictionary<Variable, double> _bindings;

        public ExpressionContext()
        {
            _bindings = new Dictionary<Variable, double>();
        }

        public void Bind(Variable variable, double value)
        {
            _bindings[variable] = value;
        }

        public double Lookup(Variable variable)
        {
            return _bindings.ContainsKey(variable) ? _bindings[variable] : double.NaN;
        }
    }
}
