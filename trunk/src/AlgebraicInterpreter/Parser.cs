using System;
using System.Collections.Generic;
using WaveletStudio.AlgebraicInterpreter.Exceptions;

namespace WaveletStudio.AlgebraicInterpreter
{
    public sealed class Parser
    {
        private readonly ExpressionFactory _factory;
        private readonly Tokenizer _tokenizer;

        #region CONSTRUCTOR

        public Parser()
        {
            _factory = new ExpressionFactory();
            _tokenizer = new Tokenizer();

            /* canned support */

            //numbers and variables
            AddAssociationInternal(@"^[a-z]$", typeof (VariableExpression));
            AddAssociationInternal(@"^\d+(\.\d+)?$", typeof (NumericExpression));

            //constants
            AddAssociationInternal(@"^PI$", typeof (PiExpression));
            AddAssociationInternal(@"^E$", typeof (EExpression));

            //standard binary operators
            AddAssociationInternal(@"^\+$", typeof (AddExpression));
            AddAssociationInternal(@"^-$", typeof (SubtractExpression));
            AddAssociationInternal(@"^\*$", typeof (MulitplyExpression));
            AddAssociationInternal(@"^/$", typeof (DivideExpression));
            AddAssociationInternal(@"^\^$", typeof (PowerExpression));

            //unary functions
            AddAssociationInternal(@"^abs$", typeof (AbsExpression));
            AddAssociationInternal(@"^ln$", typeof (LogExpression));
            AddAssociationInternal(@"^log$", typeof (LogExpression));
            AddAssociationInternal(@"^log2$", typeof (Log2Expression));
            AddAssociationInternal(@"^log10$", typeof (Log10Expression));
            AddAssociationInternal(@"^exp$", typeof (ExpExpression));
            AddAssociationInternal(@"^exp2$", typeof (Exp2Expression));
            AddAssociationInternal(@"^exp10$", typeof (Exp10Expression));
            AddAssociationInternal(@"^sin$", typeof (SinExpression));
            AddAssociationInternal(@"^cos$", typeof (CosExpression));
            AddAssociationInternal(@"^tan$", typeof (TanExpression));
            AddAssociationInternal(@"^arcsin$", typeof (ArcSinExpression));
            AddAssociationInternal(@"^arccos$", typeof (ArcCosExpression));
            AddAssociationInternal(@"^arctan$", typeof (ArcTanExpression));
            AddAssociationInternal(@"^sinh$", typeof (SinhExpression));
            AddAssociationInternal(@"^cosh$", typeof (CoshExpression));
            AddAssociationInternal(@"^tanh$", typeof (TanhExpression));

            //parens
            AddAssociationInternal(@"^\($", typeof (LeftParenExpression));
            AddAssociationInternal(@"^\)$", typeof (RightParenExpression));
        }

        #endregion

        #region PUBLIC METHODS

        public Expression Parse(string text)
        {
            var copy = text.Replace(" ", string.Empty).Trim();

            var tokens = _tokenizer.Tokenize(copy);
            var expressions = TokensToExpressions(tokens);

            Validator.Validate(expressions); //throws

            RemoveExcessParens(expressions);
            while (expressions.Count > 1)
            {
                var i = DetermineHighestPrecedence(expressions);
                CollapseExpression(expressions, i);
                RemoveExcessParens(expressions);
            }

            return expressions[0];
        }

        public void AddAssociation(string pattern, Type type)
        {
            if (type.BaseType == null)
                return;

            if (type.BaseType.Name != "ConstantExpression" && type.BaseType.Name != "FunctionExpression")
                throw new AlgebraicException(
                    "The type must directly inherit from either ConstantExpression or FunctionExpression.");

            _factory.AddAssociation(pattern, type);
            _tokenizer.AddPattern(pattern);
        }

        #endregion

        #region PRIVATE METHODS

        private void AddAssociationInternal(string pattern, Type type)
        {
            _factory.AddAssociation(pattern, type);
            _tokenizer.AddPattern(pattern);
        }

        private void CollapseExpression(List<Expression> expressions, int i)
        {
            var current = expressions[i];
            Expression previous = new NullExpression();
            Expression next = new NullExpression();
            if (i - 1 >= 0)
                previous = expressions[i - 1];
            if (i + 1 < expressions.Count)
                next = expressions[i + 1];

            if (current is SubtractExpression && !previous.IsBound() && !(previous is RightParenExpression))
            {
                var expression = (SubtractExpression) current;
                var zero = new NumericExpression();
                zero.Bind(0.0);
                expression.Bind(zero, next);
                expressions.RemoveAt(i + 1);
            }
            else if (current is FunctionExpression)
            {
                var expression = (FunctionExpression) current;
                expression.Bind(next);
                expressions.RemoveAt(i + 1);
            }
            else if (current is BinaryExpression)
            {
                var expression = (BinaryExpression) current;
                expression.Bind(previous, next);
                expressions.RemoveAt(i + 1);
                expressions.RemoveAt(i - 1);
            }
        }

        private int DetermineHighestPrecedence(List<Expression> expressions)
        {
            var highest = int.MinValue;
            var maxPrecedence = int.MinValue;
            for (var i = 0; i < expressions.Count; i++)
            {
                if (expressions[i] is LeftParenExpression)
                {
                    highest = int.MinValue;
                    maxPrecedence = int.MinValue;
                }
                else if (expressions[i] is RightParenExpression)
                {
                    break;
                }
                else
                {
                    var precedence = DeterminePrecendence(expressions, i);
                    if (precedence > maxPrecedence)
                    {
                        highest = i;
                        maxPrecedence = precedence;
                    }
                }
            }

            return highest;
        }

        private int DeterminePrecendence(IList<Expression> expressions, int i)
        {
            /* immediate negation=4, function=3, power=2, multiply/divide=1, add/subtract=0 */

            var current = expressions[i];
            Expression previous = new NullExpression();
            Expression next = new NullExpression();
            if (i - 1 >= 0)
                previous = expressions[i - 1];
            if (i + 1 < expressions.Count)
                next = expressions[i + 1];

            var precendence = int.MinValue;
            if (!current.IsBound())
            {
                if (current is SubtractExpression && next.IsBound() && !previous.IsBound() &&
                    !(previous is RightParenExpression))
                {
                    precendence = 4;
                }
                else if (current is FunctionExpression)
                {
                    precendence = 3;
                }
                else if (current is PowerExpression)
                {
                    precendence = 2;
                }
                else if (current is MulitplyExpression || current is DivideExpression)
                {
                    precendence = 1;
                }
                else if (current is AddExpression || current is SubtractExpression)
                {
                    precendence = 0;
                }
            }

            return precendence;
        }

        private void RemoveExcessParens(List<Expression> expressions)
        {
            var flag = true;
            while (flag)
            {
                flag = false;
                for (var i = expressions.Count - 1; i >= 0; i--)
                {
                    if (!(expressions[i] is RightParenExpression))
                        continue;
                    if (i > 0 && expressions[i - 1] is LeftParenExpression)
                    {
                        flag = true;
                        expressions.RemoveAt(i);
                        expressions.RemoveAt(i - 1);
                        i -= 1;
                    }
                    else if (i > 1 && expressions[i - 2] is LeftParenExpression)
                    {
                        flag = true;
                        expressions.RemoveAt(i);
                        expressions.RemoveAt(i - 2);
                        i -= 2;
                    }
                }
            }
        }

        private List<Expression> TokensToExpressions(List<string> tokens)
        {
            var expressions = new List<Expression>();

            for (var i = 0; i < tokens.Count; i++)
            {
                expressions.Add(_factory.Create(tokens[i]));

                if (expressions[i] is NumericExpression)
                    expressions[i].Bind(double.Parse(tokens[i]));
                else if (expressions[i] is VariableExpression)
                    expressions[i].Bind((Variable) Enum.Parse(typeof (Variable), tokens[i]));
            }

            return expressions;
        }

        #endregion
    }
}