using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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

        #endregion ToInfinity overloads

        #region IsBetween methods

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
        [Pure]
        public static bool IsBetweenOrEqualTo(this int n, int min, int max)
        {
            Contract.Requires(min <= max);
            return (n >= min && n <= max);
        }

        /// <summary>
        /// Checks if the current instance is between, but not equal to, two integers.
        /// </summary>
        /// <param name="n">The current integers.</param>
        /// <param name="min">The lower boundary.</param>
        /// <param name="max">The upper boundary.</param>
        /// <returns>True if the current instance is between, but not equal to, the two integers; otherwise false.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static bool IsBetween(this double n, double min, double max)
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
        public static bool IsBetweenOrEqualTo(this double n, double min, double max)
        {
            if (min > max) throw new ArgumentException("min must not be greater than max");
            return (n >= min && n <= max);
        }

        #endregion IsBetween methods

        #region CollatzCount overloads

        /// <summary>
        /// Returns the Collatz count.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The Collatz count.</returns>
        public static int CollatzCount(this int n)
        {
            if (n == 0) return 0;

            int i = 0, c = n;

            while (c != 1)
            {
                i++;

                if (c % 2 == 0) c = c / 2;
                else c = c * 3 + 1;
            }

            return i;
        }

        /// <summary>
        /// Returns the Collatz count.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The Collatz count.</returns>
        public static long CollatzCount(this long n)
        {
            if (n == 0) return 0;

            long i = 0, c = n;

            while (c != 1)
            {
                i++;

                if (c % 2 == 0) c = c / 2;
                else c = c * 3 + 1;
            }

            return i;
        }

        /// <summary>
        /// Returns the Collatz count.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The Collatz count.</returns>
        public static BigInteger CollatzCount(this BigInteger n)
        {
            if (n == 0) return 0;

            BigInteger i = 0, c = n;

            while (c != 1)
            {
                i++;

                if (c % 2 == 0) c = c / 2;
                else c = c * 3 + 1;
            }

            return i;
        }

        #endregion CollatzCount overloads

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
            Contract.Requires(digits.IsBetweenOrEqualTo(0, 15));
            return System.Math.Round(d, digits);
        }

        #endregion Round overloads

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
            return d < lower ? lower : (d > upper ? upper : d);
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
            return d < lower ? lower : (d > upper ? upper : d);
        }

        #endregion Bound overloads

        #region IsDivisableBy overloads

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="from">The smallest number.</param>
        /// <param name="to">The largest number.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this int i, int from, int to)
        {
            return i.IsDivisableBy(from.To(to));
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this int i, IEnumerable<int> nums)
        {
            return nums.All(num => i != 0 && i % num == 0);
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this int i, params int[] nums)
        {
            return nums.All(num => i != 0 && i % num == 0);
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="from">The smallest number.</param>
        /// <param name="to">The largest number.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this long i, long from, long to)
        {
            return i.IsDivisableBy(from.To(to));
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this long i, IEnumerable<long> nums)
        {
            return nums.All(num => i != 0 && i % num == 0);
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this long i, params long[] nums)
        {
            return nums.All(num => i != 0 && i % num == 0);
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="from">The smallest number.</param>
        /// <param name="to">The largest number.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this BigInteger i, BigInteger from, BigInteger to)
        {
            return i.IsDivisableBy(from.To(to));
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this BigInteger i, IEnumerable<BigInteger> nums)
        {
            return nums.All(num => i != 0 && i % num == 0);
        }

        /// <summary>
        /// Whether the current instance is divisable by all the specified numbers.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="nums">The numbers.</param>
        /// <returns>Whether the current instance is divisable by all the specified numbers.</returns>
        public static bool IsDivisableBy(this BigInteger i, params BigInteger[] nums)
        {
            return nums.All(num => i != 0 && i % num == 0);
        }

        #endregion IsDivisableBy overloads

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

        #endregion Factor overloads

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

        #endregion Digit overloads

        #region Reverse overloads

        /// <summary>
        /// Reverses the number.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed number.</returns>
        public static long Reverse(this int i)
        {
            return Convert.ToInt64(System.Math.Abs(i).ToString().Reverse());
        }

        /// <summary>
        /// Reverses the number.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed number.</returns>
        public static BigInteger Reverse(this long i)
        {
            return BigInteger.Parse(System.Math.Abs(i).ToString().Reverse());
        }

        /// <summary>
        /// Reverses the number.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed number.</returns>
        public static BigInteger Reverse(this BigInteger i)
        {
            return BigInteger.Parse(BigInteger.Abs(i).ToString().Reverse());
        }

        #endregion Reverse overloads

        #region Pow overloads

        /// <summary>
        /// Puts the current instance to the specified power.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="power">The specified power.</param>
        /// <returns>The current instance in the specified power.</returns>
        public static int Pow(this int i, int power)
        {
            return (int)System.Math.Pow(i, power);
        }

        /// <summary>
        /// Puts the current instance to the specified power.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="power">The specified power.</param>
        /// <returns>The current instance in the specified power.</returns>
        public static double Pow(this double i, double power)
        {
            return System.Math.Pow(i, power);
        }

        #endregion Pow overloads

        #region Factorial overloads

        /// <summary>
        /// Calculates the factorial of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factorial.</returns>
        public static long Factorial(this int n)
        {
            if (n < 0) return 0;
            if (n == 0) return 1;

            long sum = 1;

            for (int i = n; i >= 2; i--)
            {
                sum *= i;
            }

            return sum;
        }

        #endregion Factorial overloads

        #region IsEven overloads

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is even.</returns>
        public static bool IsEven(this int n)
        {
            return n % 2 == 0;
        }

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is even.</returns>
        public static bool IsEven(this long n)
        {
            return n % 2 == 0;
        }

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is even.</returns>
        public static bool IsEven(this BigInteger n)
        {
            return n % 2 == 0;
        }

        #endregion IsEven overloads

        #region IsOdd overloads

        /// <summary>
        /// Whether the current instance is odd.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is odd.</returns>
        public static bool IsOdd(this int n)
        {
            return n % 2 != 0;
        }

        /// <summary>
        /// Whether the current instance is odd.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is odd.</returns>
        public static bool IsOdd(this long n)
        {
            return n % 2 != 0;
        }

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is odd.</returns>
        public static bool IsOdd(this BigInteger n)
        {
            return n % 2 != 0;
        }

        #endregion IsOdd overloads

        #region IsPrime

        /// <summary>
        /// Whether the current instance is prime.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is prime.</returns>
        public static bool IsPrime(this int n)
        {
            return BagMath.IsPrime(n);
        }

        /// <summary>
        /// Whether the current instance is prime.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is prime.</returns>
        public static bool IsPrime(this long n)
        {
            return BagMath.IsPrime(n);
        }

        /// <summary>
        /// Whether the current instance is prime.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is prime.</returns>
        public static bool IsPrime(this BigInteger n)
        {
            return BagMath.IsPrime(n);
        }

        #endregion IsPrime
    }
}