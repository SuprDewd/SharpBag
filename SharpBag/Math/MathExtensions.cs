using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using SharpBag.Strings;

namespace SharpBag.Math
{
    /// <summary>
    /// Extensions methods for the BagMath class.
    /// </summary>
    public static class MathExtensions
    {
        #region ToInfinity overloads

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<long> ToInfinity(this long start)
        {
            return start.ToInfinity(1);
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<int> ToInfinity(this int start)
        {
            return start.ToInfinity(1);
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<BigInteger> ToInfinity(this BigInteger start)
        {
            return start.ToInfinity(1);
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<long> ToInfinity(this long start, long step)
        {
            for (var current = start; ; current += step)
                yield return current;
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<int> ToInfinity(this int start, int step)
        {
            for (var current = start; ; current += step)
                yield return current;
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<BigInteger> ToInfinity(this BigInteger start, BigInteger step)
        {
            for (var current = start; ; current += step)
                yield return current;
        }

        #endregion

        /// <summary>
        /// Checks if the current instance is between, but not equal to, two integers.
        /// </summary>
        /// <param name="n">The current integers.</param>
        /// <param name="min">The lower boundary.</param>
        /// <param name="max">The upper boundary.</param>
        /// <returns>True if the current instance is between, but not equal to, the two integers; otherwise false.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static bool IsBetween(this int n, int min, int max)
        {
            if (min > max) throw new ArgumentException("min must not be greater than max.");
            return (n > min && n < max);
        }

        /// <summary>
        /// Checks if the current instance is between or equal to two integers.
        /// </summary>
        /// <param name="n">The current integers.</param>
        /// <param name="min">The minimum integer.</param>
        /// <param name="max">The maximum integer.</param>
        /// <returns>True if the current instance is between or equal to the two integers; otherwise false.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static bool IsBetweenOrEqualTo(this int n, int min, int max)
        {
            if (min > max) throw new ArgumentException("min must not be greater than max");
            return (n >= min && n <= max);
        }

        #region CollatzCount overloads

        /// <summary>
        /// Returns the Collatz count.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The Collatz count.</returns>
        public static int CollatzCount(this int n)
        {
            if (n == 1) return 1;

            return CollatzCount(n % 2 == 0 ? n / 2 : 3 * n + 1) + 1;
        }

        /// <summary>
        /// Returns the Collatz count.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The Collatz count.</returns>
        public static long CollatzCount(this long n)
        {
            if (n == 1) return 1;

            return CollatzCount(n % 2 == 0 ? n / 2 : 3 * n + 1) + 1;
        }

        /// <summary>
        /// Returns the Collatz count.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The Collatz count.</returns>
        public static BigInteger CollatzCount(this BigInteger n)
        {
            if (n == 1) return 1;

            return CollatzCount(n % 2 == 0 ? n / 2 : 3 * n + 1) + 1;
        }

        #endregion

        #region Round overloads

        /// <summary>
        /// Rounds the current instance.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <returns>The current instance rounded.</returns>
        public static int Round(this double d)
        {
            return (int)System.Math.Round(d);
        }

        /// <summary>
        /// Rounds the current instance.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="digits">Number of digits to keep after the comma.</param>
        /// <returns>The current instance rounded.</returns>
        public static double Round(this double d, int digits)
        {
            return System.Math.Round(d, digits);
        }

        #endregion

        #region Bound overloads

        /// <summary>
        /// Gets the current instance inside the specified boundaries.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="lower">The lower boundary.</param>
        /// <param name="upper">The upper boundary.</param>
        /// <returns>The current instance inside the spcified boundaries.</returns>
        public static int Bound(this int d, int lower, int upper)
        {
            if (d < lower) return lower;
            if (d > upper) return upper;
            return d;
        }

        /// <summary>
        /// Gets the current instance inside the specified boundaries.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="lower">The lower boundary.</param>
        /// <param name="upper">The upper boundary.</param>
        /// <returns>The current instance inside the spcified boundaries.</returns>
        public static double Bound(this double d, double lower, double upper)
        {
            if (d < lower) return lower;
            if (d > upper) return upper;
            return d;
        } 

        #endregion

        #region DivisableBy overloads

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="from">The smallest number.</param>
        /// <param name="to">The largest number.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this int i, int from, int to)
        {
            return i.DivisableBy(from.To(to));
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this int i, IEnumerable<int> nums)
        {
            foreach (var num in nums)
            {
                if (i % num != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this int i, params int[] nums)
        {
            foreach (var num in nums)
            {
                if (i % num != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="from">The smallest number.</param>
        /// <param name="to">The largest number.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this long i, long from, long to)
        {
            return i.DivisableBy(from.To(to));
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this long i, IEnumerable<long> nums)
        {
            foreach (var num in nums)
            {
                if (i % num != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this long i, params long[] nums)
        {
            foreach (var num in nums)
            {
                if (i % num != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="from">The smallest number.</param>
        /// <param name="to">The largest number.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this BigInteger i, BigInteger from, BigInteger to)
        {
            return i.DivisableBy(from.To(to));
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this BigInteger i, IEnumerable<BigInteger> nums)
        {
            foreach (var num in nums)
            {
                if (i % num != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool DivisableBy(this BigInteger i, params BigInteger[] nums)
        {
            foreach (var num in nums)
            {
                if (i % num != 0) return false;
            }

            return true;
        }

        #endregion

        #region Factor overloads

        /// <summary>
        /// The factors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<int> Factors(this int n)
        {
            for (int i = 1; i < n; i++)
            {
                if (n % i == 0) yield return i;
            }

            yield return n;
        }

        /// <summary>
        /// The factors of the current instance, except it self.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<int> FactorsButSelf(this int n)
        {
            for (int i = 1; i < n; i++)
            {
                if (n % i == 0) yield return i;
            }
        }

        /// <summary>
        /// The factors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<long> Factors(this long n)
        {
            for (long i = 1; i < n; i++)
            {
                if (n % i == 0) yield return i;
            }

            yield return n;
        }

        /// <summary>
        /// The factors of the current instance, except it self.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<long> FactorsButSelf(this long n)
        {
            for (long i = 1; i <= n; i++)
            {
                if (n % i == 0) yield return i;
            }
        }

        /// <summary>
        /// The factors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<BigInteger> Factors(this BigInteger n)
        {
            for (BigInteger i = 1; i < n; i++)
            {
                if (n % i == 0) yield return i;
            }

            yield return n;
        }

        /// <summary>
        /// The factors of the current instance, except it self.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<BigInteger> FactorsButSelf(this BigInteger n)
        {
            for (BigInteger i = 1; i <= n; i++)
            {
                if (n % i == 0) yield return i;
            }
        }

        #endregion

        #region Digit overloads

        /// <summary>
        /// Returns the digits of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The digits.</returns>
        public static IEnumerable<short> Digits(this int n)
        {
            return n.ToString().ToCharArray().Select(i => Convert.ToInt16(i.ToString()));
        }

        /// <summary>
        /// Returns the digits of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The digits.</returns>
        public static IEnumerable<short> Digits(this long n)
        {
            return n.ToString().ToCharArray().Select(i => Convert.ToInt16(i.ToString()));
        }

        /// <summary>
        /// Returns the digits of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The digits.</returns>
        public static IEnumerable<short> Digits(this BigInteger n)
        {
            return n.ToString().ToCharArray().Select(i => Convert.ToInt16(i.ToString()));
        }

        #endregion

        #region Reverse overload

        /// <summary>
        /// Reverses the number.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed number.</returns>
        public static long Reverse(this int i)
        {
            return Convert.ToInt64(i.ToString().Reverse());
        }

        /// <summary>
        /// Reverses the number.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed number.</returns>
        public static BigInteger Reverse(this long i)
        {
            return BigInteger.Parse(i.ToString().Reverse());
        }

        /// <summary>
        /// Reverses the number.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed number.</returns>
        public static BigInteger Reverse(this BigInteger i)
        {
            return BigInteger.Parse(i.ToString().Reverse());
        }

        #endregion
    }
}