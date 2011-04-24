using System;
using System.Linq.Expressions;

namespace SharpBag
{
    #region Marc Gravell - http://www.yoda.arachsys.com/csharp/miscutil/index.html

    internal static class ExpressionUtil
    {
        public static Func<TArg1, TResult> CreateExpression<TArg1, TResult>(Func<Expression, UnaryExpression> body)
        {
            ParameterExpression inp = Expression.Parameter(typeof(TArg1), "inp");

            try
            {
                return Expression.Lambda<Func<TArg1, TResult>>(body(inp), inp).Compile();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return delegate { throw new InvalidOperationException(msg); };
            }
        }

        public static Func<TArg1, TArg2, TResult> CreateExpression<TArg1, TArg2, TResult>(Func<Expression, Expression, BinaryExpression> body, bool castArgsToResultOnFailure = false)
        {
            ParameterExpression lhs = Expression.Parameter(typeof(TArg1), "lhs");
            ParameterExpression rhs = Expression.Parameter(typeof(TArg2), "rhs");

            try
            {
                try
                {
                    return Expression.Lambda<Func<TArg1, TArg2, TResult>>(body(lhs, rhs), lhs, rhs).Compile();
                }
                catch (InvalidOperationException)
                {
                    if (castArgsToResultOnFailure && !(typeof(TArg1) == typeof(TResult) && typeof(TArg2) == typeof(TResult)))
                    {
                        Expression castLhs = typeof(TArg1) == typeof(TResult) ? (Expression)lhs : (Expression)Expression.Convert(lhs, typeof(TResult));
                        Expression castRhs = typeof(TArg2) == typeof(TResult) ? (Expression)rhs : (Expression)Expression.Convert(rhs, typeof(TResult));

                        return Expression.Lambda<Func<TArg1, TArg2, TResult>>(body(castLhs, castRhs), lhs, rhs).Compile();
                    }
                    else throw;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return delegate { throw new InvalidOperationException(msg); };
            }
        }
    }

    #endregion Marc Gravell - http://www.yoda.arachsys.com/csharp/miscutil/index.html
}