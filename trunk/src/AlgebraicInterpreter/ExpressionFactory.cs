using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WaveletStudio.AlgebraicInterpreter.Exceptions;

namespace WaveletStudio.AlgebraicInterpreter
{
    internal class ExpressionFactory
    {
        private readonly Dictionary<string, KeyValuePair<Regex, Type>> _associations;

        #region CONSTRUCTOR

        public ExpressionFactory()
        {
            _associations = new Dictionary<string, KeyValuePair<Regex, Type>>();
        }

        #endregion

        #region PUBLIC METHODS

        public void AddAssociation(string pattern, Type type)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new AlgebraicException("The pattern cannot be null or an empty string.");
            if (type == null)
                throw new AlgebraicException("The type cannot be null.");
            if (_associations.ContainsKey(pattern))
                throw new AlgebraicException("The pattern has already been associated with a type.");

            try
            {
                _associations.Add(pattern, new KeyValuePair<Regex,Type>(new Regex(pattern),type));
            }
            catch
            {
                throw new AlgebraicException("The pattern must be a valid regular expression.");
            }
        }

        public bool RemoveAssociation(string pattern)
        {
            return _associations.Remove(pattern);
        }

        public Expression Create(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                foreach (var pair in _associations)
                {
                    if (!pair.Value.Key.IsMatch(token)) 
                        continue;
                    var info = pair.Value.Value.GetConstructor(Type.EmptyTypes);
                    return (Expression)info.Invoke(null);
                }
            }

            return null;
        }

        #endregion
    }
}
