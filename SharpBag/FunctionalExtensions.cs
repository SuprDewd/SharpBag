using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;

namespace SharpBag
{
    /// <summary>
    /// Functional methods.
    /// </summary>
    public static class FunctionalExtensions
    {
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
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<int>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<int>>().Any());

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
        public static IEnumerable<int> To(this int start, int end, Func<int, int> step)
        {
            Contract.Requires(step != null);
            Contract.Ensures(Contract.Result<IEnumerable<int>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<int>>().Any());

            if (start < end) for (int i = start; i <= end; i = step(i)) yield return i;
            else if (start > end) for (int i = start; i >= end; i = step(i)) yield return i;
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
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<long>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<long>>().Any());

            if (start < end) for (long i = start; i <= end; i += step) yield return i;
            else if (start > end) for (long i = start; i >= end; i -= step) yield return i;
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
        public static IEnumerable<long> To(this long start, long end, Func<long, long> step)
        {
            Contract.Requires(step != null);
            Contract.Ensures(Contract.Result<IEnumerable<long>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<long>>().Any());

            if (start < end) for (long i = start; i <= end; i = step(i)) yield return i;
            else if (start > end) for (long i = start; i >= end; i = step(i)) yield return i;
            else yield return end;
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        /// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
        public static IEnumerable<BigInteger> To(this BigInteger start, BigInteger end)
        {
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>().Any());
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
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>().Any());
            if (start < end) for (BigInteger i = start; i <= end; i += step) yield return i;
            else if (start > end) for (BigInteger i = start; i >= end; i -= step) yield return i;
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
        public static IEnumerable<BigInteger> To(this BigInteger start, BigInteger end, Func<BigInteger, BigInteger> step)
        {
            Contract.Requires(step != null);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>().Any());
            if (start < end) for (BigInteger i = start; i <= end; i = step(i)) yield return i;
            else if (start > end) for (BigInteger i = start; i >= end; i = step(i)) yield return i;
            else yield return end;
        }

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
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<char>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<char>>().Any());

            if (start < end) for (int i = start; i <= end; i += step) yield return (char)i;
            else if (start > end) for (int i = start; i >= end; i -= step) yield return (char)i;
            else yield return end;
        }

        /// <summary>
        /// Generates chars that range from the current instance to end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The char to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        /// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
        public static IEnumerable<char> To(this char start, char end, Func<int, int> step)
        {
            Contract.Requires(step != null);
            Contract.Ensures(Contract.Result<IEnumerable<char>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<char>>().Any());

            if (start < end) for (int i = start; i <= end; i = step(i)) yield return (char)i;
            else if (start > end) for (int i = start; i >= end; i = step(i)) yield return (char)i;
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
            Contract.Requires(i >= 0);
            Contract.Requires(f != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            for (int j = 0; j < i; j++) yield return f();
        }

        /// <summary>
        /// Memoizes the current instance.
        /// </summary>
        /// <param name="func">The current instance.</param>
        /// <param name="memo">The memo.</param>
        public static Func<TIn, TOut> Memoize<TIn, TOut>(this Func<TIn, TOut> func, IDictionary<TIn, TOut> memo = null)
        {
            Contract.Requires(func != null);

            if (memo == null) memo = new Dictionary<TIn, TOut>();
            return i =>
            {
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
        public static Func<TIn, TOut> Memoize<TIn, TOut>(this Func<Func<TIn, TOut>, TIn, TOut> func, IDictionary<TIn, TOut> memo = null)
        {
            Contract.Requires(func != null);

            if (memo == null) memo = new Dictionary<TIn, TOut>();
            Func<TIn, TOut> recFunc = null;
            recFunc = i =>
            {
                TOut o;
                if (!memo.TryGetValue(i, out o)) memo.Add(i, o = func(recFunc, i));
                return o;
            };

            return recFunc;
        }

        /// <summary>
        /// Returns a function that returns true if both of the specified functions return true.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="f1">The current instance.</param>
        /// <param name="f2">Another function.</param>
        /// <returns>A boolean.</returns>
        public static Func<T, bool> And<T>(this Func<T, bool> f1, Func<T, bool> f2)
        {
            return o => f1(o) && f2(o);
        }

        /// <summary>
        /// Returns a function that returns true if either of the specified functions return true.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="f1">The current instance.</param>
        /// <param name="f2">Another function.</param>
        /// <returns>A boolean.</returns>
        public static Func<T, bool> Or<T>(this Func<T, bool> f1, Func<T, bool> f2)
        {
            return o => f1(o) || f2(o);
        }

        /// <summary>
        /// Returns a function that returns the opposite of the current instance when called.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="f">The current instance.</param>
        /// <returns>A function that returns the opposite of the current instance when called.</returns>
        public static Func<T, bool> Not<T>(this Func<T, bool> f)
        {
            return o => !f(o);
        }

        /// <summary>
        /// Returns a function, that when called, returns the specified value or the default specified value, depending on the return value of the expression function.
        /// </summary>
        /// <typeparam name="TIn">The input type.</typeparam>
        /// <typeparam name="TOut">The output type.</typeparam>
        /// <param name="expression">The expression function.</param>
        /// <param name="obj">The value.</param>
        /// <param name="def">The default value.</param>
        /// <returns>A function, that when called, returns the specified value or the default specified value, depending on the return value of the expression function.</returns>
        public static Func<TIn, TOut> Then<TIn, TOut>(this Func<TIn, bool> expression, TOut obj, TOut def = default(TOut))
        {
            return o => expression(o) ? obj : def;
        }

        #region Unfold overloads

        /// <summary>
        /// Unfolds the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the unfolded values.</typeparam>
        /// <param name="f">The function to unfold.</param>
        /// <returns>The unfolded values.</returns>
        public static IEnumerable<T> Unfold<T>(this Func<int, Option<T>> f)
        {
            Contract.Requires(f != null);

            int index = 0;
            Option<T> res = f(index++);

            while (res != null && res.IsSome)
            {
                yield return res.Value;
                res = f(index++);
            }
        }

        /// <summary>
        /// Unfolds the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the unfolded values.</typeparam>
        /// <param name="f">The function to unfold.</param>
        /// <returns>The unfolded values.</returns>
        public static IEnumerable<T> Unfold<T>(this Func<Option<T>> f)
        {
            Contract.Requires(f != null);

            Option<T> res = f();

            while (res != null && res.IsSome)
            {
                yield return res.Value;
                res = f();
            }
        }

        /// <summary>
        /// Unfolds the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the unfolded values.</typeparam>
        /// <param name="f">The function to unfold.</param>
        /// <param name="seed">The seed to feed the function.</param>
        /// <param name="step">A function to apply on every unfolded value, to update the seed.</param>
        /// <returns>The unfolded values.</returns>
        public static IEnumerable<T> Unfold<T>(this Func<T, Option<T>> f, T seed, Func<T, T> step = null)
        {
            Contract.Requires(f != null);

            if (step == null) step = s => s;
            Option<T> res = f(seed);

            while (res != null && res.IsSome)
            {
                yield return res.Value;
                seed = step(seed);
                res = f(seed);
            }
        }

        /// <summary>
        /// Unfolds the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the unfolded values.</typeparam>
        /// <param name="f">The function to unfold.</param>
        /// <returns>The unfolded values.</returns>
        public static IEnumerable<T> Unfold<T>(this Func<int, T> f)
        {
            Contract.Requires(f != null);

            int index = 0;
            while (true) yield return f(index++);
        }

        /// <summary>
        /// Unfolds the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the unfolded values.</typeparam>
        /// <param name="f">The function to unfold.</param>
        /// <returns>The unfolded values.</returns>
        public static IEnumerable<T> Unfold<T>(this Func<T> f)
        {
            Contract.Requires(f != null);

            while (true) yield return f();
        }

        /// <summary>
        /// Unfolds the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the unfolded values.</typeparam>
        /// <param name="f">The function to unfold.</param>
        /// <param name="seed">The seed to feed the function.</param>
        /// <param name="step">A function to apply on every unfolded value, to update the seed.</param>
        /// <returns>The unfolded values.</returns>
        public static IEnumerable<T> Unfold<T>(this Func<T, T> f, T seed, Func<T, T> step = null)
        {
            Contract.Requires(f != null);

            if (step == null) step = s => s;

            while (true)
            {
                yield return f(seed);
                seed = step(seed);
            }
        }

        /// <summary>
        /// Unfolds the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the unfolded values.</typeparam>
        /// <typeparam name="T2">The type of the seed.</typeparam>
        /// <param name="f">The function to unfold.</param>
        /// <param name="open">The opener.</param>
        /// <param name="close">The closer.</param>
        /// <returns>The unfolded values.</returns>
        public static IEnumerable<T> Unfold<T, T2>(this Func<T2, Option<T>> f, Func<T2> open, Action<T2> close)
        {
            Contract.Requires(f != null);
            Contract.Requires(open != null);
            Contract.Requires(close != null);

            T2 o = open();

            try
            {
                Option<T> res = f(o);

                while (res != null && res.IsSome)
                {
                    yield return res.Value;
                    res = f(o);
                }
            }
            finally
            {
                close(o);
            }
        }

        /// <summary>
        /// Unfolds the current instance, and then disposes it.
        /// </summary>
        /// <typeparam name="T">The type of the unfolded values.</typeparam>
        /// <typeparam name="T2">The type of the seed.</typeparam>
        /// <param name="f">The function to unfold.</param>
        /// <param name="open">The opener.</param>
        /// <returns>The unfolded values.</returns>
        public static IEnumerable<T> Unfold<T, T2>(this Func<T2, Option<T>> f, Func<T2> open) where T2 : IDisposable
        {
            Contract.Requires(f != null);
            Contract.Requires(open != null);

            T2 o = open();

            using (o)
            {
                Option<T> res = f(o);

                while (res != null && res.IsSome)
                {
                    yield return res.Value;
                    res = f(o);
                }
            }
        }

        /// <summary>
        /// Unfolds the current instance using the unfolder.
        /// </summary>
        /// <typeparam name="T">The type of the current instance.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <param name="unfolder">The unfolder.</param>
        /// <returns>The unfolded elements.</returns>
        public static IEnumerable<T> Unfold<T>(this T item, Func<T, T> unfolder)
        {
            yield return item;
            while (true) yield return item = unfolder(item);
        }

        #endregion Unfold overloads

        /// <summary>
        /// Returns a new Matcher with the current instance as the target.
        /// </summary>
        /// <typeparam name="TIn">The type of the current instance.</typeparam>
        /// <typeparam name="TOut">The result of the match.</typeparam>
        /// <param name="value">The current instance.</param>
        /// <returns>The result of the match.</returns>
        public static Matcher<TIn, TOut> Match<TIn, TOut>(this TIn value)
        {
            return new Matcher<TIn, TOut>(value);
        }

        /// <summary>
        /// Performs a function composition on the specified functions.
        /// </summary>
        /// <typeparam name="T">The type of the input.</typeparam>
        /// <typeparam name="TTemp">The type of the temporary results.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="f">The f function.</param>
        /// <param name="g">The g function.</param>
        /// <returns></returns>
        public static Func<T, TResult> Compose<T, TTemp, TResult>(this Func<TTemp, TResult> f, Func<T, TTemp> g)
        {
            return x => f(g(x));
        }
    }
}