using System.Collections.Generic;
using WaveletStudio.AlgebraicInterpreter.Exceptions;

namespace WaveletStudio.AlgebraicInterpreter
{
    internal class Validator
    {
        private static readonly int[,] LegalSequences = 
                {
                    { 0, 1, 1, 0, 0, 1 },   //follow numeric, variable, constant
                    { 1, 0, 1, 1, 1, 0 },   //follow binary operators except subtract
                    { 1, 0, 1, 1, 1, 0 },   //follow subtract
                    { 1, 0, 1, 1, 1, 0 },   //follow function
                    { 1, 0, 1, 1, 1, 0 },   //follow opening parenthesis
                    { 0, 1, 1, 0, 0, 1 }    //follow closing parenthesis
                };        

        public static void Validate(List<Expression> expressions)
        {
            SequenceCheck(expressions);
            ParenCheck(expressions);
        }

        private static void SequenceCheck(List<Expression> expressions)
        {
            BeginCheck(expressions[0]);
            EndCheck(expressions[expressions.Count-1]);

            for (var i = 0; i < expressions.Count - 1; i++)
            {
                var first = GetTypeIndex(expressions[i]);
                var second = GetTypeIndex(expressions[i + 1]);

                if (LegalSequences[first, second] != 1)
                    throw new SequenceException(expressions[i], expressions[i + 1], "An invalid character sequence exists.");
            }
        }

        private static int GetTypeIndex(Expression expression)
        {
            if (expression is NumericExpression || expression is VariableExpression || expression is ConstantExpression)
                return 0;
            if (expression is BinaryExpression && !(expression is SubtractExpression))
                return 1;
            if (expression is SubtractExpression)
                return 2;
            if (expression is FunctionExpression)
                return 3;
            if (expression is LeftParenExpression)
                return 4;
            return 5;
        }

        private static void BeginCheck(Expression expression)
        {
            if (expression is RightParenExpression)
                throw new SequenceException(null, expression, "The expression cannot begin with a closing parenthesis.");
            if (expression is BinaryExpression && !(expression is SubtractExpression))
                throw new SequenceException(null, expression, " The expression cannot being with a binary operator.");
        }

        private static void EndCheck(Expression expression)
        {
            if (expression is LeftParenExpression)
                throw new SequenceException(expression, null, "The expression cannot end with an opening parenthesis.");
            if (expression is BinaryExpression || expression is FunctionExpression)
                throw new SequenceException(expression, null, "The expression cannot end with an operator or function of any kind.");
        }

        private static void ParenCheck(List<Expression> expressions)
        {
            int i;
            var counter = 0;
            for (i = 0; i < expressions.Count; i++)
            {
                if (expressions[i] is LeftParenExpression)
                {
                    counter++;
                }
                else if (expressions[i] is RightParenExpression)
                {
                    counter--;
                    if (counter < 0)
                    {
                        throw new ParenException("A closing parenthesis does not match an opening parenthesis.");
                    }
                }
            }

            if (counter != 0)
            {
                throw new ParenException("A closing parenthesis is missing.");
            }
        }
    }
}
