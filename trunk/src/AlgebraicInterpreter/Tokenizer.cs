using System.Collections.Generic;
using System.Text.RegularExpressions;
using WaveletStudio.AlgebraicInterpreter.Exceptions;

namespace WaveletStudio.AlgebraicInterpreter
{
    internal class Tokenizer
    {
        private readonly Dictionary<string,Regex> _patterns;

        #region CONSTRUCTOR

        public Tokenizer()
        {
            _patterns = new Dictionary<string, Regex>();
        }

        #endregion

        #region PUBLIC METHODS

        public void AddPattern(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) 
                throw new AlgebraicException("The pattern cannot be null or an empty string.");
            if (_patterns.ContainsKey(pattern))
                throw new AlgebraicException("The pattern has already been added to the tokenizer.");

            try
            {
                _patterns.Add(pattern, new Regex(pattern));
            }
            catch
            {
                throw new AlgebraicException("The pattern must be a valid regular expression.");
            }
        }

        public bool RemovePattern(string pattern)
        {
            return _patterns.Remove(pattern);
        }

        public List<string> Tokenize(string text)
        {
            var tokens = new List<string>();

            for (var i = 0; i < text.Length; i++)
            {
                var matched = false;
                for (var j = text.Length - i; j > 0 && !matched; j--)
                {
                    foreach(var pair in _patterns)
                    {
                        if (!pair.Value.IsMatch(text.Substring(i, j))) 
                            continue;
                        matched = true;
                        tokens.Add(text.Substring(i, j));
                        i += j - 1;
                        break;
                    }
                }

                if (!matched)
                {
                    throw new TokenException(i, "Unrecognized character sequence starting at index " + i + ".");
                }
            }

            return tokens;
        }

        #endregion
    }
}
