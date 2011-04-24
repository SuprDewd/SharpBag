using System;
using System.Linq.Expressions;

namespace SharpBag
{
    #region Marc Gravell - http://www.yoda.arachsys.com/csharp/miscutil/index.html

    /// <summary>
    /// Generic operators.
    /// </summary>
    public static class Operator
    {
        /// <summary>
        /// Whether the specified variable has a value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value">The variable.</param>
        /// <returns>Whether the specified variable has a value.</returns>
        public static bool HasValue<T>(T value)
        {
            return Operator<T>.NullOp.HasValue(value);
        }

        /// <summary>
        /// Add the specified variable to the specified accumulator, if it has a value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="accumulator">The accumulator.</param>
        /// <param name="value">The variable.</param>
        /// <returns>Whether the specified value was added to the accumulator.</returns>
        public static bool AddIfNotNull<T>(ref T accumulator, T value)
        {
            return Operator<T>.NullOp.AddIfNotNull(ref accumulator, value);
        }

        /// <summary>
        /// Negate the specified value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The result.</returns>
        public static T Negate<T>(T value)
        {
            return Operator<T>.Negate(value);
        }

        /// <summary>
        /// Bitwise Not the value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The result.</returns>
        public static T Not<T>(T value)
        {
            return Operator<T>.Not(value);
        }

        /// <summary>
        /// Bitwise Or the values.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static T Or<T>(T value1, T value2)
        {
            return Operator<T>.Or(value1, value2);
        }

        /// <summary>
        /// Bitwise And the values.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static T And<T>(T value1, T value2)
        {
            return Operator<T>.And(value1, value2);
        }

        /// <summary>
        /// Bitwise Xor the values.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static T Xor<T>(T value1, T value2)
        {
            return Operator<T>.Xor(value1, value2);
        }

        /// <summary>
        /// Convert the value to the specified type.
        /// </summary>
        /// <typeparam name="TFrom">The type to convert from.</typeparam>
        /// <typeparam name="TTo">The type to convert to.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The result.</returns>
        public static TTo Convert<TFrom, TTo>(TFrom value)
        {
            return Operator<TFrom, TTo>.Convert(value);
        }

        /// <summary>
        /// Add the values.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static T Add<T>(T value1, T value2)
        {
            return Operator<T>.Add(value1, value2);
        }

        /// <summary>
        /// Add the values.
        /// </summary>
        /// <typeparam name="TArg1">The first value.</typeparam>
        /// <typeparam name="TArg2">The second value.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static TArg1 AddAlternative<TArg1, TArg2>(TArg1 value1, TArg2 value2)
        {
            return Operator<TArg2, TArg1>.Add(value1, value2);
        }

        /// <summary>
        /// Subtract the values.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static T Subtract<T>(T value1, T value2)
        {
            return Operator<T>.Subtract(value1, value2);
        }

        /// <summary>
        /// Subtract the values.
        /// </summary>
        /// <typeparam name="TArg1">The first value.</typeparam>
        /// <typeparam name="TArg2">The second value.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static TArg1 SubtractAlternative<TArg1, TArg2>(TArg1 value1, TArg2 value2)
        {
            return Operator<TArg2, TArg1>.Subtract(value1, value2);
        }

        /// <summary>
        /// Multiply the values.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static T Multiply<T>(T value1, T value2)
        {
            return Operator<T>.Multiply(value1, value2);
        }

        /// <summary>
        /// Multiply the values.
        /// </summary>
        /// <typeparam name="TArg1">The first value.</typeparam>
        /// <typeparam name="TArg2">The second value.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static TArg1 MultiplyAlternative<TArg1, TArg2>(TArg1 value1, TArg2 value2)
        {
            return Operator<TArg2, TArg1>.Multiply(value1, value2);
        }

        /// <summary>
        /// Divide the values.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static T Divide<T>(T value1, T value2)
        {
            return Operator<T>.Divide(value1, value2);
        }

        /// <summary>
        /// Divide the values.
        /// </summary>
        /// <typeparam name="TArg1">The first value.</typeparam>
        /// <typeparam name="TArg2">The second value.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static TArg1 DivideAlternative<TArg1, TArg2>(TArg1 value1, TArg2 value2)
        {
            return Operator<TArg2, TArg1>.Divide(value1, value2);
        }

        /// <summary>
        /// Check the values for equality.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static bool Equal<T>(T value1, T value2)
        {
            return Operator<T>.Equal(value1, value2);
        }

        /// <summary>
        /// Check the values for inequality.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static bool NotEqual<T>(T value1, T value2)
        {
            return Operator<T>.NotEqual(value1, value2);
        }

        /// <summary>
        /// Check whether the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static bool GreaterThan<T>(T value1, T value2)
        {
            return Operator<T>.GreaterThan(value1, value2);
        }

        /// <summary>
        /// Check whether the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static bool LessThan<T>(T value1, T value2)
        {
            return Operator<T>.LessThan(value1, value2);
        }

        /// <summary>
        /// Check whether the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static bool GreaterThanOrEqual<T>(T value1, T value2)
        {
            return Operator<T>.GreaterThanOrEqual(value1, value2);
        }

        /// <summary>
        /// Check whether the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>The result.</returns>
        public static bool LessThanOrEqual<T>(T value1, T value2)
        {
            return Operator<T>.LessThanOrEqual(value1, value2);
        }

        /// <summary>
        /// Divide the values.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value">The first value.</param>
        /// <param name="divisor">The second value.</param>
        /// <returns>The result.</returns>
        public static T DivideInt32<T>(T value, int divisor)
        {
            return Operator<int, T>.Divide(value, divisor);
        }
    }

    /// <summary>
    /// Generic operators.
    /// </summary>
    /// <typeparam name="TValue">The value.</typeparam>
    /// <typeparam name="TResult">The result.</typeparam>
    public static class Operator<TValue, TResult>
    {
        private static readonly Func<TValue, TResult> convert;

        /// <summary>
        /// Convert from the value to the result.
        /// </summary>
        public static Func<TValue, TResult> Convert { get { return convert; } }

        static Operator()
        {
            convert = ExpressionUtil.CreateExpression<TValue, TResult>(body => Expression.Convert(body, typeof(TResult)));
            add = ExpressionUtil.CreateExpression<TResult, TValue, TResult>(Expression.Add, true);
            subtract = ExpressionUtil.CreateExpression<TResult, TValue, TResult>(Expression.Subtract, true);
            multiply = ExpressionUtil.CreateExpression<TResult, TValue, TResult>(Expression.Multiply, true);
            divide = ExpressionUtil.CreateExpression<TResult, TValue, TResult>(Expression.Divide, true);
        }

        private static readonly Func<TResult, TValue, TResult> add, subtract, multiply, divide;

        /// <summary>
        /// Add the values.
        /// </summary>
        public static Func<TResult, TValue, TResult> Add { get { return add; } }

        /// <summary>
        /// Subtract the values.
        /// </summary>
        public static Func<TResult, TValue, TResult> Subtract { get { return subtract; } }

        /// <summary>
        /// Multiply the values.
        /// </summary>
        public static Func<TResult, TValue, TResult> Multiply { get { return multiply; } }

        /// <summary>
        /// Divide the values.
        /// </summary>
        public static Func<TResult, TValue, TResult> Divide { get { return divide; } }
    }

    /// <summary>
    /// Generic operators.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public static class Operator<T>
    {
        static readonly INullOp<T> nullOp;

        internal static INullOp<T> NullOp { get { return nullOp; } }

        static readonly T zero;

        /// <summary>
        /// The zero for the current type.
        /// </summary>
        public static T Zero { get { return zero; } }

        static readonly Func<T, T> negate, not;
        static readonly Func<T, T, T> or, and, xor;

        /// <summary>
        /// Negate the value.
        /// </summary>
        public static Func<T, T> Negate { get { return negate; } }

        /// <summary>
        /// Bitwise Not the value.
        /// </summary>
        public static Func<T, T> Not { get { return not; } }

        /// <summary>
        /// Bitwise Or the values.
        /// </summary>
        public static Func<T, T, T> Or { get { return or; } }

        /// <summary>
        /// Bitwise And the values.
        /// </summary>
        public static Func<T, T, T> And { get { return and; } }

        /// <summary>
        /// Bitwise Xor the values.
        /// </summary>
        public static Func<T, T, T> Xor { get { return xor; } }

        static readonly Func<T, T, T> add, subtract, multiply, divide;

        /// <summary>
        /// Add the values.
        /// </summary>
        public static Func<T, T, T> Add { get { return add; } }

        /// <summary>
        /// Subtract the values.
        /// </summary>
        public static Func<T, T, T> Subtract { get { return subtract; } }

        /// <summary>
        /// Multiply the values.
        /// </summary>
        public static Func<T, T, T> Multiply { get { return multiply; } }

        /// <summary>
        /// Divide the values.
        /// </summary>
        public static Func<T, T, T> Divide { get { return divide; } }

        static readonly Func<T, T, bool> equal, notEqual, greaterThan, lessThan, greaterThanOrEqual, lessThanOrEqual;

        /// <summary>
        /// Whether the values are equal.
        /// </summary>
        public static Func<T, T, bool> Equal { get { return equal; } }

        /// <summary>
        /// Whether the values are not equal.
        /// </summary>
        public static Func<T, T, bool> NotEqual { get { return notEqual; } }

        /// <summary>
        /// Whether the first value is greater than the second value.
        /// </summary>
        public static Func<T, T, bool> GreaterThan { get { return greaterThan; } }

        /// <summary>
        /// Whether the first value is less than the second value.
        /// </summary>
        public static Func<T, T, bool> LessThan { get { return lessThan; } }

        /// <summary>
        /// Whether the first value is greater than or equal to the second value.
        /// </summary>
        public static Func<T, T, bool> GreaterThanOrEqual { get { return greaterThanOrEqual; } }

        /// <summary>
        /// Whether the first value is less than or equal to the second value.
        /// </summary>
        public static Func<T, T, bool> LessThanOrEqual { get { return lessThanOrEqual; } }

        static Operator()
        {
            add = ExpressionUtil.CreateExpression<T, T, T>(Expression.Add);
            subtract = ExpressionUtil.CreateExpression<T, T, T>(Expression.Subtract);
            divide = ExpressionUtil.CreateExpression<T, T, T>(Expression.Divide);
            multiply = ExpressionUtil.CreateExpression<T, T, T>(Expression.Multiply);

            greaterThan = ExpressionUtil.CreateExpression<T, T, bool>(Expression.GreaterThan);
            greaterThanOrEqual = ExpressionUtil.CreateExpression<T, T, bool>(Expression.GreaterThanOrEqual);
            lessThan = ExpressionUtil.CreateExpression<T, T, bool>(Expression.LessThan);
            lessThanOrEqual = ExpressionUtil.CreateExpression<T, T, bool>(Expression.LessThanOrEqual);
            equal = ExpressionUtil.CreateExpression<T, T, bool>(Expression.Equal);
            notEqual = ExpressionUtil.CreateExpression<T, T, bool>(Expression.NotEqual);

            negate = ExpressionUtil.CreateExpression<T, T>(Expression.Negate);
            and = ExpressionUtil.CreateExpression<T, T, T>(Expression.And);
            or = ExpressionUtil.CreateExpression<T, T, T>(Expression.Or);
            not = ExpressionUtil.CreateExpression<T, T>(Expression.Not);
            xor = ExpressionUtil.CreateExpression<T, T, T>(Expression.ExclusiveOr);

            Type typeT = typeof(T);

            if (typeT.IsValueType && typeT.IsGenericType && (typeT.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                Type nullType = typeT.GetGenericArguments()[0];
                zero = (T)Activator.CreateInstance(nullType);
                nullOp = (INullOp<T>)Activator.CreateInstance(typeof(StructNullOp<>).MakeGenericType(nullType));
            }
            else
            {
                zero = default(T);
                if (typeT.IsValueType) nullOp = (INullOp<T>)Activator.CreateInstance(typeof(StructNullOp<>).MakeGenericType(typeT));
                else nullOp = (INullOp<T>)Activator.CreateInstance(typeof(ClassNullOp<>).MakeGenericType(typeT));
            }
        }
    }

    #endregion Marc Gravell - http://www.yoda.arachsys.com/csharp/miscutil/index.html
}