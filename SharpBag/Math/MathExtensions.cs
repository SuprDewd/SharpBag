using System;
using System.Collections.Generic;
using System.Linq;
using SharpBag.Strings;

#if DOTNET4

using System.Diagnostics.Contracts;
using System.Numerics;

#endif

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

#if DOTNET4

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<BigInteger> ToInfinity(this BigInteger start)
        {
            return start.ToInfinity(1);
        }

#endif

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

#if DOTNET4

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

#endif

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
#if DOTNET4

        [Pure]
#endif

        public static bool IsBetweenOrEqualTo(this int n, int min, int max)
        {
#if DOTNET4
            Contract.Requires(min <= max);
#endif
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
#if DOTNET4
            Contract.Requires(digits.IsBetweenOrEqualTo(0, 15));
#endif
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

#if DOTNET4

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

#endif

        #endregion IsDivisableBy overloads

        #region Factor overloads

        /// <summary>
        /// The factors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<int> Factors(this int n)
        {
            int sqrt = (int)System.Math.Sqrt(n);
            for (int i = 1; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (i != sqrt) yield return n / i;
                }
            }
        }

        /// <summary>
        /// The factors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<long> Factors(this long n)
        {
            long sqrt = (long)System.Math.Sqrt(n);
            for (long i = 1; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (i != sqrt) yield return n / i;
                }
            }
        }

#if DOTNET4

        /// <summary>
        /// The factors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factors.</returns>
        public static IEnumerable<BigInteger> Factors(this BigInteger n)
        {
            BigInteger sq;
            for (BigInteger i = 1; (sq = i * i) <= n; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (sq != n) yield return n / i;
                }
            }
        }

#endif

        #region Momerath - http://www.dreamincode.net/code/snippet5562.htm

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<int> PrimeFactors(this int i)
        {
            int divisor = 2;

            while (divisor <= i)
            {
                if (i % divisor == 0)
                {
                    yield return divisor;
                    i /= divisor;
                }
                else divisor++;
            }
        }

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<long> PrimeFactors(this long i)
        {
            long divisor = 2;

            while (divisor <= i)
            {
                if (i % divisor == 0)
                {
                    yield return divisor;
                    i /= divisor;
                }
                else divisor++;
            }
        }

#if DOTNET4

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<BigInteger> PrimeFactors(this BigInteger i)
        {
            BigInteger divisor = 2;

            while (divisor <= i)
            {
                if (i % divisor == 0)
                {
                    yield return divisor;
                    i /= divisor;
                }
                else divisor++;
            }
        }

#endif

        #endregion Momerath - http://www.dreamincode.net/code/snippet5562.htm

        #endregion Factor overloads

        #region Divisor overloads

        /// <summary>
        /// The divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The divisors.</returns>
        public static IEnumerable<int> Divisors(this int n)
        {
            int sqrt = (int)System.Math.Sqrt(n);
            for (int i = 1; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (i != sqrt && i != 1) yield return n / i;
                }
            }
        }

        /// <summary>
        /// The divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The divisors.</returns>
        public static IEnumerable<long> Divisors(this long n)
        {
            long sqrt = (long)System.Math.Sqrt(n);
            for (long i = 1; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (i != sqrt && i != 1) yield return n / i;
                }
            }
        }

#if DOTNET4

        /// <summary>
        /// The divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The divisors.</returns>
        public static IEnumerable<BigInteger> Divisors(this BigInteger n)
        {
            BigInteger sq;
            for (BigInteger i = 1; (sq = i * i) <= n; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (sq != n && n != 1) yield return n / i;
                }
            }
        }

#endif

        #endregion Divisor overloads

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

#if DOTNET4

        /// <summary>
        /// Returns the digits of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The digits.</returns>
        public static IEnumerable<short> Digits(this BigInteger n)
        {
            return n.ToString().ToCharArray().Select(i => Convert.ToInt16(i.ToString()));
        }

#endif

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

#if DOTNET4

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

#endif

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

        /// <summary>
        /// Calculates the factorial of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The factorial.</returns>
        public static BigInteger FactorialBig(this int n)
        {
            if (n < 0) return 0;
            if (n == 0) return 1;

            BigInteger sum = 1;

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
            return (n & 1) == 0;
        }

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is even.</returns>
        public static bool IsEven(this long n)
        {
            return (n & 1) == 0;
        }

#if DOTNET4

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is even.</returns>
        public static bool IsEven(this BigInteger n)
        {
            return (n & 1) == 0;
        }

#endif

        #endregion IsEven overloads

        #region IsOdd overloads

        /// <summary>
        /// Whether the current instance is odd.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is odd.</returns>
        public static bool IsOdd(this int n)
        {
            return (n & 1) == 1;
        }

        /// <summary>
        /// Whether the current instance is odd.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is odd.</returns>
        public static bool IsOdd(this long n)
        {
            return (n & 1) == 1;
        }

#if DOTNET4

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is odd.</returns>
        public static bool IsOdd(this BigInteger n)
        {
            return (n & 1) == 1;
        }

#endif

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

#if DOTNET4

        /// <summary>
        /// Whether the current instance is prime.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is prime.</returns>
        public static bool IsPrime(this BigInteger n)
        {
            return BagMath.IsPrime(n);
        }

#endif

        #endregion IsPrime
    }
}