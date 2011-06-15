using System;

namespace WaveletStudio.AlgebraicInterpreter
{
    #region ABSTRACT EXPRESSION BASE CLASSES

    public abstract class Expression
    {
        protected readonly double ZeroThreshold = 1.0 * Math.Pow(10.0, -14.0);

        protected bool ThisIsBound;

        protected Expression()
        {
            ThisIsBound = false;
        }

        public bool IsBound()
        {
            return ThisIsBound;
        }

        public void Bind(params object[] arguments)
        {
            InnerBind(arguments);
            ThisIsBound = true;
        }

        public double Evaluate(ExpressionContext context)
        {
            var result = InnerEvaluate(context);

            return Math.Abs(result) <= ZeroThreshold ? 0 : result;
        }

        protected abstract void InnerBind(params object[] arguments);
        protected abstract double InnerEvaluate(ExpressionContext context);
    }

    public abstract class ConstantExpression : Expression
    {
        protected ConstantExpression()
        {
            ThisIsBound = true;
        }

        protected sealed override void InnerBind(params object[] arguments) { }
    }

    public abstract class BinaryExpression : Expression
    {
        protected Expression Operand1;
        protected Expression Operand2;

        protected sealed override void InnerBind(params object[] arguments)
        {
            Operand1 = (Expression)arguments[0];
            Operand2 = (Expression)arguments[1];
        }
    }

    public abstract class FunctionExpression : Expression
    {
        protected Expression Operand;

        protected sealed override void InnerBind(params object[] arguments)
        {
            Operand = (Expression)arguments[0];
        }
    }

    #endregion

    #region VARIABLE AND NUMERIC EXPRESSIONS

    public sealed class VariableExpression : Expression
    {
        private Variable _variable;

        protected override void InnerBind(params object[] arguments)
        {
            _variable = (Variable)arguments[0];
        }

        protected override double InnerEvaluate(ExpressionContext context)
        {
            return context.Lookup(_variable);
        }
    }

    public sealed class NumericExpression : Expression
    {
        private double _value;

        protected override void InnerBind(params object[] arguments)
        {
            _value = (double)arguments[0];
        }

        protected override double InnerEvaluate(ExpressionContext context)
        {
            return _value;
        }
    }

    #endregion

    #region CONSTANT EXPRESSIONS

    public sealed class PiExpression : ConstantExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.PI;
        }
    }

    public sealed class EExpression : ConstantExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.E;
        }
    }

    #endregion

    #region BINARY EXPRESSIONS

    public sealed class AddExpression : BinaryExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Operand1.Evaluate(context) + Operand2.Evaluate(context);
        }
    }

    public sealed class SubtractExpression : BinaryExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Operand1.Evaluate(context) - Operand2.Evaluate(context);
        }
    }

    public sealed class MulitplyExpression : BinaryExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Operand1.Evaluate(context) * Operand2.Evaluate(context);
        }
    }

    public sealed class DivideExpression : BinaryExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Operand1.Evaluate(context) / Operand2.Evaluate(context);
        }
    }

    public sealed class PowerExpression : BinaryExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Pow(Operand1.Evaluate(context), Operand2.Evaluate(context));
        }
    }

    #endregion

    #region FUNCTION EXPRESSIONS

    public sealed class AbsExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Abs(Operand.Evaluate(context));
        }
    }

    public sealed class LogExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Log(Operand.Evaluate(context), Math.E);
        }
    }

    public sealed class Log2Expression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Log(Operand.Evaluate(context), 2);
        }
    }

    public sealed class Log10Expression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Log10(Operand.Evaluate(context));
        }
    }

    public sealed class ExpExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Exp(Operand.Evaluate(context));
        }
    }

    public sealed class Exp2Expression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Pow(Operand.Evaluate(context), Math.E);
        }
    }

    public sealed class Exp10Expression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Pow(Operand.Evaluate(context), 10.0);
        }
    }

    public sealed class SinExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Sin(Operand.Evaluate(context));
        }
    }

    public sealed class CosExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Cos(Operand.Evaluate(context));
        }
    }

    public sealed class TanExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Tan(Operand.Evaluate(context));
        }
    }

    public sealed class ArcSinExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Asin(Operand.Evaluate(context));
        }
    }

    public sealed class ArcCosExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Acos(Operand.Evaluate(context));
        }
    }

    public sealed class ArcTanExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Atan(Operand.Evaluate(context));
        }
    }

    public sealed class SinhExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Sinh(Operand.Evaluate(context));
        }
    }

    public sealed class CoshExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Cosh(Operand.Evaluate(context));
        }
    }

    public sealed class TanhExpression : FunctionExpression
    {
        protected override double InnerEvaluate(ExpressionContext context)
        {
            return Math.Tanh(Operand.Evaluate(context));
        }
    }

    #endregion

    #region PAREN AND NULL EXPRESSIONS

    public sealed class LeftParenExpression : Expression
    {
        protected override void InnerBind(params object[] arguments) { }

        protected override double InnerEvaluate(ExpressionContext context)
        {
            return double.NaN;
        }
    }

    public sealed class RightParenExpression : Expression
    {
        protected override void InnerBind(params object[] arguments) { }

        protected override double InnerEvaluate(ExpressionContext context)
        {
            return double.NaN;
        }
    }

    public sealed class NullExpression : Expression
    {
        protected override void InnerBind(params object[] arguments) { }

        protected override double InnerEvaluate(ExpressionContext context)
        {
            return double.NaN;
        }
    }

    #endregion
}
