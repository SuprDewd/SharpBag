using System;
using System.Collections.Generic;
using System.Linq;

#if DOTNET4
using System.Numerics;
using System.Diagnostics.Contracts;
#endif

namespace SharpBag
{
    /// <summary>
    /// Functional methods.
    /// </summary>
    public static class Functional
    {
        /// <summary>
        /// Performs an action on each element of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="action">The action to perform on each element.</param>
        /// <returns>The current instance.</returns>
        /// <remarks>Igor Ostrovsky - http://igoro.com/archive/extended-linq-additional-operators-for-linq-to-objects/</remarks>
        public static IEnumerable<T> Iter<T>(this IEnumerable<T> source, Action<T> action)
        {
#if DOTNET4
            Contract.Requires(source != null);
            Contract.Requires(action != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
            foreach (T elem in source)
            {
                action(elem);
                yield return elem;
            }
        }

        /// <summary>
        /// Performs an action on each element of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="action">The action to perform on each element.</param>
        /// <remarks>Igor Ostrovsky - http://igoro.com/archive/extended-linq-additional-operators-for-linq-to-objects/</remarks>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
#if DOTNET4
            Contract.Requires(source != null);
            Contract.Requires(action != null);
#endif
            foreach (T elem in source) action(elem);
        }

        #region To overloads

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        /// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
        public static IEnumerable<int> To(this int start, int end, int step = 1)
        {
#if DOTNET4
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<int>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<int>>().Any());
#endif
            if (start < end) for (int i = start; i <= end; i += step) yield return i;
            else if (start > end) for (int i = start; i >= end; i -= step) yield return i;
            else yield return end;
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        /// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
        public static IEnumerable<long> To(this long start, long end, long step = 1)
        {
#if DOTNET4
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<long>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<long>>().Any());
#endif

            if (start < end) for (long i = start; i <= end; i += step) yield return i;
            else if (start > end) for (long i = start; i >= end; i -= step) yield return i;
            else yield return end;
        }

#if DOTNET4

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        /// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
        public static IEnumerable<BigInteger> To(this BigInteger start, BigInteger end)
        {
#if DOTNET4
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>().Any());
#endif

            return start.To(end, BigInteger.One);
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        /// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
        public static IEnumerable<BigInteger> To(this BigInteger start, BigInteger end, BigInteger step)
        {
#if DOTNET4
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>().Any());
#endif

            if (start < end) for (BigInteger i = start; i <= end; i += step) yield return i;
            else if (start > end) for (BigInteger i = start; i >= end; i -= step) yield return i;
            else yield return end;
        }

#endif

        /// <summary>
        /// Generates chars that range from the current instance to end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The char to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        /// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
        public static IEnumerable<char> To(this char start, char end, int step = 1)
        {
#if DOTNET4
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<char>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<char>>().Any());
#endif
            if (start < end) for (int i = start; i <= end; i += step) yield return (char)i;
            else if (start > end) for (int i = start; i >= end; i -= step) yield return (char)i;
            else yield return end;
        }

        #endregion To overloads

        /// <summary>
        /// Executes the specified function N times where N is the value of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the value returned from the function.</typeparam>
        /// <param name="i">The current instance.</param>
        /// <param name="f">The function to execute.</param>
        /// <returns>An enumerable with the returned values of the function.</returns>
        /// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
        public static IEnumerable<T> Times<T>(this int i, Func<T> f)
        {
#if DOTNET4
            Contract.Requires(i >= 0);
            Contract.Requires(f != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif

            for (int j = 0; j < i; j++) yield return f();
        }
        
        /// <summary>
        /// Memoizes the current instance.
        /// </summary>
        /// <param name="func">The current instance.</param>
        /// <param name="memo">The memo.</param>
        public static Func<TIn, TOut> Memoize<TIn, TOut>(this Func<TIn, TOut> func, IDictionary<TIn, TOut> memo = null)
        {
#if DOTNET4
            Contract.Requires(func != null);
#endif
            if (memo == null) memo = new Dictionary<TIn, TOut>();
            return i => {
                TOut o;
                if (!memo.TryGetValue(i, out o)) memo.Add(i, o = func(i));
                return o;
            };
        }

        /// <summary>
        /// Memoizes the current instance.
        /// This overload allows for recursive memoization.
        /// </summary>
        /// <param name="func">The current instance.</param>
        /// <param name="memo">The memo.</param>
        public static Func<TIn, TOut> Memoize<TIn, TOut>(this Func<TIn, Func<TIn, TOut>, TOut> func, IDictionary<TIn, TOut> memo = null)
        {
#if DOTNET4
            Contract.Requires(func != null);
#endif
            if (memo == null) memo = new Dictionary<TIn, TOut>();
            Func<TIn, TOut> recFunc = null;
            recFunc = i =>
            {
                TOut o;
                if (!memo.TryGetValue(i, out o)) memo.Add(i, o = func(i, recFunc));
                return o;
            };

            return recFunc;
        }
    }
}