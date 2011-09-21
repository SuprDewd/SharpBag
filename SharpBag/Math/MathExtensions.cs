using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using SharpBag.Strings;

namespace SharpBag.Math
{
    /// <summary>
    /// Extension methods for the BagMath class.
    /// </summary>
    public static class MathExtensions
    {
        #region ToInfinity

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<long> ToInfinity(this long start, long step = 1)
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
        public static IEnumerable<int> ToInfinity(this int start, int step = 1)
        {
            for (var current = start; ; current += step)
                yield return current;
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
        public static IEnumerable<BigInteger> ToInfinity(this BigInteger start, BigInteger step)
        {
            for (var current = start; ; current += step)
                yield return current;
        }

        #endregion ToInfinity

        #region Divisors

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
                    if (i != sqrt) yield return n / i;
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
                    if (i != sqrt) yield return n / i;
                }
            }
        }

        /// <summary>
        /// The divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The divisors.</returns>
        public static IEnumerable<BigInteger> Divisors(this BigInteger n)
        {
            BigInteger sq = n.Sqrt();
            for (BigInteger i = 1; i <= sq; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (sq != i) yield return n / i;
                }
            }
        }

        #endregion Divisors

        #region DivisorCount

        /// <summary>
        /// Returns the number of divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The number of divisors of the current instance.</returns>
        public static int DivisorCount(this int n)
        {
            Contract.Requires(n >= 0);

            int count = 2, sqrt = (int)System.Math.Sqrt(n);
            for (int i = 2; i <= sqrt; i++) if (n % i == 0) count += 2;
            return sqrt * sqrt == n ? count - 1 : count;
        }

        /// <summary>
        /// Returns the number of divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="primes">Primes.</param>
        /// <returns>The number of divisors of the current instance.</returns>
        /// <remarks>The largest prime needed is p * p > n.</remarks>
        public static int DivisorCount(this int n, IEnumerable<int> primes)
        {
            Contract.Requires(n >= 0);
            Contract.Requires(primes != null);

            if (n <= 1) return 1;
            int count = 1;

            foreach (int prime in primes)
            {
                if (prime * prime > n)
                {
                    count *= 2;
                    break;
                }

                int exp = 1;
                while (n % prime == 0)
                {
                    exp++;
                    n /= prime;
                }

                if (exp > 1) count *= exp;
                if (n == 1) break;
            }

            return count;
        }

        /// <summary>
        /// Returns the number of divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The number of divisors of the current instance.</returns>
        public static long DivisorCount(this long n)
        {
            Contract.Requires(n >= 0);

            long count = 2, sqrt = (long)System.Math.Sqrt(n);
            for (long i = 2; i <= sqrt; i++) if (n % i == 0) count += 2;
            return sqrt * sqrt == n ? count - 1 : count;
        }

        /// <summary>
        /// Returns the number of divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="primes">Primes.</param>
        /// <returns>The number of divisors of the current instance.</returns>
        /// <remarks>The largest prime needed is p * p > n.</remarks>
        public static long DivisorCount(this long n, IEnumerable<long> primes)
        {
            Contract.Requires(n >= 0);
            Contract.Requires(primes != null);

            if (n <= 1) return 1;
            long count = 1;

            foreach (long prime in primes)
            {
                if (prime * prime > n)
                {
                    count *= 2;
                    break;
                }

                long exp = 1;
                while (n % prime == 0)
                {
                    exp++;
                    n /= prime;
                }

                if (exp > 1) count *= exp;
                if (n == 1) break;
            }

            return count;
        }

        /// <summary>
        /// Returns the number of divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The number of divisors of the current instance.</returns>
        public static BigInteger DivisorCount(this BigInteger n)
        {
            Contract.Requires(n >= 0);
            BigInteger count = 2, sq;
            for (BigInteger i = 2; (sq = i * i) <= n; i++)
            {
                if (n % i == 0) count += 2;
                if (sq == n) count--;
            }

            return count;
        }

        /// <summary>
        /// Returns the number of divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="primes">Primes.</param>
        /// <returns>The number of divisors of the current instance.</returns>
        /// <remarks>The largest prime needed is pMax * pMax > n.</remarks>
        public static BigInteger DivisorCount(this BigInteger n, IEnumerable<BigInteger> primes)
        {
            Contract.Requires(n >= 0);
            Contract.Requires(primes != null);
            if (n <= 1) return 1;
            BigInteger count = 1;

            foreach (BigInteger prime in primes)
            {
                if (prime * prime > n)
                {
                    count *= 2;
                    break;
                }

                BigInteger exp = BigInteger.One;
                while (n % prime == 0)
                {
                    exp++;
                    n /= prime;
                }

                if (exp > 1) count *= exp;
                if (n == 1) break;
            }

            return count;
        }

        #endregion DivisorCount

        #region DivisorSum

        /// <summary>
        /// Calculates the sum of the divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The sum of the divisors of the current instance.</returns>
        public static int DivisorSum(this int n)
        {
            Contract.Requires(n >= 0);

            int sum = 1, p = 2;

            while (p * p <= n && n > 1)
            {
                if (n % p == 0)
                {
                    int j = p * p;
                    n /= p;

                    while (n % p == 0)
                    {
                        j *= p;
                        n /= p;
                    }

                    sum *= j - 1;
                    sum /= p - 1;
                }

                p = p == 2 ? 3 : p + 2;
            }

            if (n > 1) sum *= n + 1;
            return sum;
        }

        /// <summary>
        /// Calculates the sum of the divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The sum of the divisors of the current instance.</returns>
        public static long DivisorSum(this long n)
        {
            Contract.Requires(n >= 0);

            long sum = 1, p = 2;

            while (p * p <= n && n > 1)
            {
                if (n % p == 0)
                {
                    long j = p * p;
                    n /= p;

                    while (n % p == 0)
                    {
                        j *= p;
                        n /= p;
                    }

                    sum *= j - 1;
                    sum /= p - 1;
                }

                p = p == 2 ? 3 : p + 2;
            }

            if (n > 1) sum *= n + 1;
            return sum;
        }

        /// <summary>
        /// Calculates the sum of the divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The sum of the divisors of the current instance.</returns>
        public static BigInteger DivisorSum(this BigInteger n)
        {
            Contract.Requires(n >= 0);

            BigInteger sum = 1, p = 2;

            while (p * p <= n && n > 1)
            {
                if (n % p == 0)
                {
                    BigInteger j = p * p;
                    n /= p;

                    while (n % p == 0)
                    {
                        j *= p;
                        n /= p;
                    }

                    sum *= j - 1;
                    sum /= p - 1;
                }

                p = p == 2 ? 3 : p + 2;
            }

            if (n > 1) sum *= n + 1;
            return sum;
        }

        #endregion DivisorSum

        #region ProperDivisors

        /// <summary>
        /// The proper divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The proper divisors.</returns>
        public static IEnumerable<int> ProperDivisors(this int n)
        {
            if (n == 1) yield break;

            int sqrt = (int)System.Math.Sqrt(n);
            bool perf = sqrt * sqrt == n;

            yield return 1;

            for (int i = 2; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (i != sqrt || !perf) yield return n / i;
                }
            }
        }

        /// <summary>
        /// The proper divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The proper divisors.</returns>
        public static IEnumerable<long> ProperDivisors(this long n)
        {
            if (n == 1) yield break;

            long sqrt = (long)System.Math.Sqrt(n);
            bool perf = sqrt * sqrt == n;

            yield return 1;

            for (long i = 2; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (i != sqrt || !perf) yield return n / i;
                }
            }
        }

        /// <summary>
        /// The proper divisors of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The proper divisors.</returns>
        public static IEnumerable<BigInteger> ProperDivisors(this BigInteger n)
        {
            if (n == 1) yield break;

            BigInteger sq;
            for (BigInteger i = 1; (sq = i * i) <= n; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    if (sq != n && i != 1) yield return n / i;
                }
            }
        }

        #endregion ProperDivisors

        #region PrimeFactors

        #region Momerath - http://www.dreamincode.net/code/snippet5562.htm

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<int> PrimeFactors(this int i)
        {
            Contract.Requires(i >= 0);

            int divisor = 2;

            while (divisor <= i)
            {
                if (i % divisor == 0)
                {
                    yield return divisor;
                    i /= divisor;
                }
                else divisor = divisor == 2 ? 3 : divisor + 2;
            }
        }

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="primes">An array of primes.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<int> PrimeFactors(this int i, IEnumerable<int> primes)
        {
            Contract.Requires(i >= 0);

            if (i <= 1) yield break;

            foreach (int prime in primes.TakeWhile(p => p <= i))
            {
                while (i % prime == 0)
                {
                    yield return prime;
                    i /= prime;
                }

                if (i == 1) yield break;
            }
        }

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<long> PrimeFactors(this long i)
        {
            Contract.Requires(i >= 0);

            long divisor = 2;

            while (divisor <= i)
            {
                if (i % divisor == 0)
                {
                    yield return divisor;
                    i /= divisor;
                }
                else divisor = divisor == 2 ? 3 : divisor + 2;
            }
        }

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="primes">An array of primes.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<long> PrimeFactors(this long i, IEnumerable<long> primes)
        {
            Contract.Requires(i >= 0);

            if (i <= 1) yield break;

            foreach (long prime in primes.TakeWhile(p => p <= i))
            {
                while (i % prime == 0)
                {
                    yield return prime;
                    i /= prime;
                }

                if (i == 1) yield break;
            }
        }

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<BigInteger> PrimeFactors(this BigInteger i)
        {
            Contract.Requires(i >= 0);

            BigInteger divisor = 2;

            while (divisor <= i)
            {
                if (i % divisor == 0)
                {
                    yield return divisor;
                    i /= divisor;
                }
                else divisor = divisor == 2 ? 3 : divisor + 2;
            }
        }

        /// <summary>
        /// Calculates the prime factors of the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="primes">An array of primes.</param>
        /// <returns>The prime factors of the current instance.</returns>
        public static IEnumerable<BigInteger> PrimeFactors(this BigInteger i, IEnumerable<BigInteger> primes)
        {
            Contract.Requires(i >= 0);

            if (i <= 1) yield break;

            foreach (BigInteger prime in primes.TakeWhile(p => p <= i))
            {
                while (i % prime == 0)
                {
                    yield return prime;
                    i /= prime;
                }

                if (i == 1) yield break;
            }
        }

        #endregion Momerath - http://www.dreamincode.net/code/snippet5562.htm

        #endregion PrimeFactors

        #region Digits

        /// <summary>
        /// Returns the digits of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The digits.</returns>
        public static IEnumerable<int> Digits(this int n)
        {
            return n.ToString().Select(i => i - '0');
        }

        /// <summary>
        /// Returns the digits of the current instance in the specified base.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="baseNum"></param>
        /// <returns>The digits.</returns>
        public static IEnumerable<int> DigitsBase(this int n, int baseNum)
        {
            if (n == 0) yield return 0;
            else while (n > 0)
                {
                    yield return n % baseNum;
                    n /= baseNum;
                }
        }

        /// <summary>
        /// Returns the digits of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The digits.</returns>
        public static IEnumerable<int> Digits(this long n)
        {
            return n.ToString().Select(i => i - '0');
        }

        /// <summary>
        /// Returns the digits of the current instance in the specified base.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="baseNum"></param>
        /// <returns>The digits.</returns>
        public static IEnumerable<int> DigitsBase(this long n, int baseNum)
        {
            if (n == 0) yield return 0;
            else while (n > 0)
                {
                    yield return (int)(n % baseNum);
                    n /= baseNum;
                }
        }

        /// <summary>
        /// Returns the digits of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The digits.</returns>
        public static IEnumerable<int> Digits(this BigInteger n)
        {
            return n.ToString().Select(i => i - '0');
        }

        /// <summary>
        /// Returns the digits of the current instance in the specified base.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="baseNum"></param>
        /// <returns>The digits.</returns>
        public static IEnumerable<int> DigitsBase(this BigInteger n, int baseNum)
        {
            if (n == 0) yield return 0;
            else while (n > 0)
                {
                    yield return (int)(n % baseNum);
                    n /= baseNum;
                }
        }

        #endregion Digits

        #region Reverse

        /// <summary>
        /// Reverses the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed current instance.</returns>
        public static long Reverse(this int i)
        {
            Contract.Requires(i != Int32.MinValue);
            return Convert.ToInt64(System.Math.Abs(i).ToString().Reverse());
        }

        /// <summary>
        /// Reverses the current instance in the specified base.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="baseNum">The specified base.</param>
        /// <returns>The reversed current instance in the specifed base.</returns>
        public static long ReverseBase(this int i, int baseNum)
        {
            long rev = 0;

            while (i > 0)
            {
                rev = rev * baseNum + i % baseNum;
                i /= baseNum;
            }

            return rev;
        }

        /// <summary>
        /// Reverses the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed current instance.</returns>
        public static BigInteger Reverse(this long i)
        {
            Contract.Requires(i != Int64.MinValue);
            return BigInteger.Parse(System.Math.Abs(i).ToString().Reverse());
        }

        /// <summary>
        /// Reverses the current instance in the specified base.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="baseNum">The specified base.</param>
        /// <returns>The reversed current instance in the specifed base.</returns>
        public static BigInteger ReverseBase(this long i, int baseNum)
        {
            BigInteger rev = 0;

            while (i > 0)
            {
                rev = rev * baseNum + i % baseNum;
                i /= baseNum;
            }

            return rev;
        }

        /// <summary>
        /// Reverses the current instance.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <returns>The reversed current instance.</returns>
        public static BigInteger Reverse(this BigInteger i)
        {
            return BigInteger.Parse(BigInteger.Abs(i).ToString().Reverse());
        }

        /// <summary>
        /// Reverses the current instance in the specified base.
        /// </summary>
        /// <param name="i">The current instance.</param>
        /// <param name="baseNum">The specified base.</param>
        /// <returns>The reversed current instance in the specifed base.</returns>
        public static BigInteger ReverseBase(this BigInteger i, int baseNum)
        {
            BigInteger rev = 0;

            while (i > 0)
            {
                rev = rev * baseNum + i % baseNum;
                i /= baseNum;
            }

            return rev;
        }

        #endregion Reverse

        #region Pow

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

        #endregion Pow

        #region IsEven

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

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is even.</returns>
        public static bool IsEven(this BigInteger n)
        {
            return (n & 1) == 0;
        }

        #endregion IsEven

        #region IsOdd

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

        /// <summary>
        /// Whether the current instance is even.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is odd.</returns>
        public static bool IsOdd(this BigInteger n)
        {
            return (n & 1) == 1;
        }

        #endregion IsOdd

        #region IsPrime

        /// <summary>
        /// Whether the current instance is prime.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="checkCache">Whether to check cache for primality.</param>
        /// <returns>Whether the current instance is prime.</returns>
        public static bool IsPrime(this int n, bool checkCache = true)
        {
            return BagMath.IsPrime(n, checkCache);
        }

        /// <summary>
        /// Whether the current instance is prime.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="checkCache">Whether to check cache for primality.</param>
        /// <returns>Whether the current instance is prime.</returns>
        public static bool IsPrime(this long n, bool checkCache = true)
        {
            return BagMath.IsPrime(n, checkCache);
        }

        /// <summary>
        /// Whether the current instance is prime.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="checkCache">Whether to check cache for primality.</param>
        /// <returns>Whether the current instance is prime.</returns>
        public static bool IsPrime(this BigInteger n, bool checkCache = true)
        {
            return BagMath.IsPrime(n, checkCache);
        }

        #endregion IsPrime

        #region IsPandigital

        #region Andras Vass - http://stackoverflow.com/questions/2484892/fastest-algorithm-to-check-if-a-number-is-pandigital

        /// <summary>
        /// Returns whether the current instance is pandigital.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is pandigital.</returns>
        public static bool IsPandigital(this int n)
        {
            Contract.Requires(n >= 0);

            int digits = 0, count = 0, tmp;

            for (; n > 0; n /= 10, ++count)
            {
                if ((tmp = digits) == (digits |= 1 << (n - ((n / 10) * 10) - 1))) return false;
            }

            return digits == (1 << count) - 1;
        }

        /// <summary>
        /// Returns whether the current instance is pandigital.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is pandigital.</returns>
        public static bool IsPandigital(this long n)
        {
            Contract.Requires(n >= 0);

            int digits = 0, count = 0, tmp;

            for (; n > 0; n /= 10, ++count)
            {
                if ((tmp = digits) == (digits |= 1 << (int)(n - ((n / 10) * 10) - 1))) return false;
            }

            return digits == (1L << count) - 1;
        }

        /// <summary>
        /// Returns whether the current instance is pandigital.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>Whether the current instance is pandigital.</returns>
        public static bool IsPandigital(this BigInteger n)
        {
            Contract.Requires(n >= 0);
            int digits = 0, count = 0, tmp;

            for (; n > 0; n /= 10, ++count)
            {
                if ((tmp = digits) == (digits |= 1 << (int)(n - ((n / 10) * 10) - 1))) return false;
            }

            return digits == (1L << count) - 1;
        }

        #endregion Andras Vass - http://stackoverflow.com/questions/2484892/fastest-algorithm-to-check-if-a-number-is-pandigital

        #endregion IsPandigital

        #region Concat

        /// <summary>
        /// Concatenates the current instance and the specified integer.
        /// </summary>
        /// <param name="a">The current instance.</param>
        /// <param name="b">The specified integer.</param>
        /// <returns>The concatenated integer.</returns>
        public static long Concat(this int a, int b)
        {
            Contract.Requires(a >= 0);
            Contract.Requires(b >= 0);

            int c = b;
            while (c > 0)
            {
                a *= 10;
                c /= 10;
            }

            return (long)a + b;
        }

        /// <summary>
        /// Concatenates the current instance and the specified integer.
        /// </summary>
        /// <param name="a">The current instance.</param>
        /// <param name="b">The specified integer.</param>
        /// <returns>The concatenated integer.</returns>
        public static BigInteger Concat(this long a, long b)
        {
            Contract.Requires(a >= 0);
            Contract.Requires(b >= 0);
            long c = b;
            while (c > 0)
            {
                a *= 10;
                c /= 10;
            }

            return (BigInteger)a + b;
        }

        /// <summary>
        /// Concatenates the current instance and the specified integer.
        /// </summary>
        /// <param name="a">The current instance.</param>
        /// <param name="b">The specified integer.</param>
        /// <returns>The concatenated integer.</returns>
        public static BigInteger Concat(this BigInteger a, BigInteger b)
        {
            Contract.Requires(a >= 0);
            Contract.Requires(b >= 0);
            BigInteger c = b;
            while (c > 0)
            {
                a *= 10;
                c /= 10;
            }

            return (BigInteger)a + b;
        }

        #endregion Concat

        #region IsProbablePrime

        #region Kristian Edlund - http://www.mathblog.dk/2011/project-euler-58-primes-diagonals-spiral/

        /// <summary>
        /// Determines whether the specified number is probably prime.
        /// </summary>
        /// <param name="n">The specifed number.</param>
        /// <returns>
        ///   <c>true</c> if the specified number is probably prime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProbablePrime(this int n)
        {
            Contract.Requires(n >= 0);
            if (n < 1373653) return n.IsProbablePrime(new int[] { 2, 3 });
            else if (n < 9080191) return n.IsProbablePrime(new int[] { 31, 73 });
            else return n.IsProbablePrime(new int[] { 2, 7, 61 });
        }

        /// <summary>
        /// Determines whether the specified number is probably prime.
        /// </summary>
        /// <param name="n">The specifed number.</param>
        /// <param name="possibleWitnesses">Possible witnesses of the numbers non-primeness.</param>
        /// <returns>
        ///   <c>true</c> if the specified number is probably prime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProbablePrime(this int n, int[] possibleWitnesses)
        {
            Contract.Requires(n >= 0);
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            if (n < 9) return true;

            for (int i = 0; i < possibleWitnesses.Length; i++)
            {
                int t = 0, a = possibleWitnesses[i], u = n - 1;

                while ((u & 1) == 0)
                {
                    t++;
                    u >>= 1;
                }

                long xi1 = a.ModPow(u, n), xi2;
                for (int j = 0; j < t; j++)
                {
                    xi2 = xi1 * xi1 % n;
                    if ((xi2 == 1) && (xi1 != 1) && (xi1 != (n - 1))) return false;
                    xi1 = xi2;
                }

                if (xi1 != 1) return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified number is probably prime.
        /// </summary>
        /// <param name="n">The specifed number.</param>
        /// <returns>
        ///   <c>true</c> if the specified number is probably prime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProbablePrime(this long n)
        {
            Contract.Requires(n >= 0);
            if (n < 1373653) return n.IsProbablePrime(new int[] { 2, 3 });
            if (n < 9080191) return n.IsProbablePrime(new int[] { 31, 73 });
            if (n < 4759123141) return n.IsProbablePrime(new int[] { 2, 7, 61 });
            if (n < 2152302898747) return n.IsProbablePrime(new int[] { 2, 3, 5, 7, 11 });
            if (n < 3474749660383) return n.IsProbablePrime(new int[] { 2, 3, 5, 7, 11, 13 });
            if (n < 341550071728321) return n.IsProbablePrime(new int[] { 2, 3, 5, 7, 11, 13, 17 });

            throw new ArgumentOutOfRangeException("No predefined witnesses for n >= 341.550.071.728.321");
        }

        /// <summary>
        /// Determines whether the specified number is probably prime.
        /// </summary>
        /// <param name="n">The specifed number.</param>
        /// <param name="possibleWitnesses">Possible witnesses of the numbers non-primeness.</param>
        /// <returns>
        ///   <c>true</c> if the specified number is probably prime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProbablePrime(this long n, int[] possibleWitnesses)
        {
            Contract.Requires(n >= 0);
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            if (n < 9) return true;

            for (int i = 0; i < possibleWitnesses.Length; i++)
            {
                int t = 0;
                long u = n - 1, a = possibleWitnesses[i];

                while ((u & 1) == 0)
                {
                    t++;
                    u >>= 1;
                }

                long xi1 = a.ModPow(u, n), xi2;
                for (int j = 0; j < t; j++)
                {
                    xi2 = xi1 * xi1 % n;
                    if ((xi2 == 1) && (xi1 != 1) && (xi1 != (n - 1))) return false;
                    xi1 = xi2;
                }

                if (xi1 != 1) return false;
            }

            return true;
        }

        private static readonly BigInteger LimitA = 1373653;
        private static readonly BigInteger LimitB = 9080191;
        private static readonly BigInteger LimitC = BigInteger.Parse("4759123141");
        private static readonly BigInteger LimitD = BigInteger.Parse("2152302898747");
        private static readonly BigInteger LimitE = BigInteger.Parse("3474749660383");
        private static readonly BigInteger LimitF = BigInteger.Parse("341550071728321");

        /// <summary>
        /// Determines whether the specified number is probably prime.
        /// </summary>
        /// <param name="n">The specifed number.</param>
        /// <returns>
        ///   <c>true</c> if the specified number is probably prime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProbablePrime(this BigInteger n)
        {
            Contract.Requires(n >= 0);
            if (n < LimitA) return n.IsProbablePrime(new int[] { 2, 3 });
            if (n < LimitB) return n.IsProbablePrime(new int[] { 31, 73 });
            if (n < LimitC) return n.IsProbablePrime(new int[] { 2, 7, 61 });
            if (n < LimitD) return n.IsProbablePrime(new int[] { 2, 3, 5, 7, 11 });
            if (n < LimitE) return n.IsProbablePrime(new int[] { 2, 3, 5, 7, 11, 13 });
            if (n < LimitF) return n.IsProbablePrime(new int[] { 2, 3, 5, 7, 11, 13, 17 });

            throw new ArgumentOutOfRangeException("No predefined witnesses for n >= 341.550.071.728.321");
        }

        /// <summary>
        /// Determines whether the specified number is probably prime.
        /// </summary>
        /// <param name="n">The specifed number.</param>
        /// <param name="possibleWitnesses">Possible witnesses of the numbers non-primeness.</param>
        /// <returns>
        ///   <c>true</c> if the specified number is probably prime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProbablePrime(this BigInteger n, int[] possibleWitnesses)
        {
            Contract.Requires(n >= 0);
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            if (n < 9) return true;

            for (int i = 0; i < possibleWitnesses.Length; i++)
            {
                int t = 0, a = possibleWitnesses[i];
                BigInteger u = n - 1;

                while ((u & 1) == 0)
                {
                    t++;
                    u >>= 1;
                }

                BigInteger xi1 = BigInteger.ModPow(a, u, n), xi2;
                for (int j = 0; j < t; j++)
                {
                    xi2 = xi1 * xi1 % n;
                    if ((xi2 == 1) && (xi1 != 1) && (xi1 != (n - 1))) return false;
                    xi1 = xi2;
                }

                if (xi1 != 1) return false;
            }

            return true;
        }

        #endregion Kristian Edlund - http://www.mathblog.dk/2011/project-euler-58-primes-diagonals-spiral/

        #endregion IsProbablePrime

        #region BitLength

        /// <summary>
        /// Returns how many bits are in the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>How many bits are in the current instance.</returns>
        public static int BitLength(this int n)
        {
            Contract.Requires(n >= 0);

            if (n == 0) return 1;
            return (int)System.Math.Log(n, 2) + 1;
        }

        /// <summary>
        /// Returns how many bits are in the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>How many bits are in the current instance.</returns>
        public static int BitLength(this long n)
        {
            Contract.Requires(n >= 0);

            if (n == 0) return 1;
            return (int)System.Math.Log(n, 2) + 1;
        }

        /// <summary>
        /// Returns how many bits are in the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>How many bits are in the current instance.</returns>
        public static int BitLength(this BigInteger n)
        {
            Contract.Requires(n >= 0);
            if (n == 0) return 1;
            return (int)BigInteger.Log(n, 2) + 1;
        }

        #endregion BitLength

        #region Sqrt

        /// <summary>
        /// Calculates the square root of the current instance.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The square root of the current instance.</returns>
        public static BigInteger Sqrt(this BigInteger n)
        {
            Contract.Requires(n >= 0);
            if (n <= 1) return n;
            BigInteger root = n, lastRoot = BigInteger.Zero;
            int count = 0;
            int bitLength = (n.BitLength() + 1) / 2;
            root = n >> bitLength;

            do
            {
                if (lastRoot > root)
                {
                    if (count++ > 1000)
                    {
                        return root;
                    }
                }

                lastRoot = root;
                root = (BigInteger.Divide(n, root) + root) >> 1;
            }
            while ((root ^ lastRoot) != 0);

            return root;
        }

        #endregion Sqrt

        #region ModPow

        #region Kristian Edlund - http://www.mathblog.dk/2011/project-euler-58-primes-diagonals-spiral/

        /// <summary>
        /// Modular exponentation.
        /// </summary>
        /// <param name="b">The base.</param>
        /// <param name="e">The exponent.</param>
        /// <param name="m">The modulo.</param>
        /// <returns>The base raised to the exponent with the specified modulo.</returns>
        public static int ModPow(this int b, int e, int m)
        {
            Contract.Requires(e >= 0);
            Contract.Requires(m > 0);
            long d = 1;
            int k = 0, te = e;

            while (te > 0)
            {
                k++;
                te >>= 1;
            }

            for (int i = k - 1; i >= 0; i--)
            {
                d = d * d % m;
                if (((e >> i) & 1) > 0) d = d * b % m;
            }

            return (int)d;
        }

        /// <summary>
        /// Modular exponentation.
        /// </summary>
        /// <param name="b">The base.</param>
        /// <param name="e">The exponent.</param>
        /// <param name="m">The modulo.</param>
        /// <returns>The base raised to the exponent with the specified modulo.</returns>
        public static long ModPow(this long b, long e, long m)
        {
            Contract.Requires(e >= 0);
            Contract.Requires(m > 0);
            long d = 1, te = e;
            int k = 0;

            while (te > 0)
            {
                k++;
                te >>= 1;
            }

            for (int i = k - 1; i >= 0; i--)
            {
                d = d * d % m;
                if (((e >> i) & 1) > 0) d = d * b % m;
            }

            return d;
        }

        #endregion Kristian Edlund - http://www.mathblog.dk/2011/project-euler-58-primes-diagonals-spiral/

        #endregion ModPow
    }
}