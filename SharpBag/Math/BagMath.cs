using System.Collections.Generic;
using System.Threading.Tasks;
using S = System;

#if DOTNET4

using System.Numerics;

#endif

namespace SharpBag.Math
{
    /// <summary>
    /// A static class containing methods for doing calculations.
    /// </summary>
    public static class BagMath
    {
        /// <summary>
        /// Finds the greatest common divisor (gcd) of the specified integers.
        /// </summary>
        /// <param name="a">An integer.</param>
        /// <param name="b">An integer.</param>
        /// <returns>The greatest commond divisor (gcd) of the specified integers.</returns>
        public static int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }

            return a == 0 ? b : a;
        }

        /// <summary>
        /// Finds the least common multiple (lcm) of the specified integers.
        /// </summary>
        /// <param name="a">An integer.</param>
        /// <param name="b">An integer.</param>
        /// <returns>The least common multiple (lcm) of the specified integers.</returns>
        public static int Lcm(int a, int b)
        {
            return (a / Gcd(a, b)) * b;
        }

        #region IsPrime overloads

        /// <summary>
        /// Checks whether a number is a prime number or not.
        /// </summary>
        /// <param name="n">The number to test.</param>
        /// <returns>Whether the number is a prime number or not.</returns>
        public static bool IsPrime(int n)
        {
            if (n <= 1) return false;
            if (n < 4) return true;
            if (n % 2 == 0) return false;
            if (n < 9) return true;
            if (n % 3 == 0) return false;

            uint r = (uint)System.Math.Floor(System.Math.Sqrt(n));
            uint f = 5;
            while (f <= r)
            {
                if (n % f == 0) return false;
                if (n % (f + 2) == 0) return false;
                f += 6;
            }

            return true;
        }

        /// <summary>
        /// Checks whether a number is a prime number or not.
        /// </summary>
        /// <param name="n">The number to test.</param>
        /// <returns>Whether the number is a prime number or not.</returns>
        public static bool IsPrime(long n)
        {
            if (n <= 1) return false;
            if (n < 4) return true;
            if (n % 2 == 0) return false;
            if (n < 9) return true;
            if (n % 3 == 0) return false;

            uint r = (uint)S.Math.Floor(S.Math.Sqrt(n));
            uint f = 5;
            while (f <= r)
            {
                if (n % f == 0) return false;
                if (n % (f + 2) == 0) return false;
                f += 6;
            }

            return true;
        }

#if DOTNET4

        /// <summary>
        /// Checks whether a number is a prime number or not.
        /// </summary>
        /// <param name="n">The number to test.</param>
        /// <returns>Whether the number is a prime number or not.</returns>
        public static bool IsPrime(BigInteger n)
        {
            if (n <= 1) return false;
            if (n < 4) return true;
            if (n % 2 == 0) return false;
            if (n < 9) return true;
            if (n % 3 == 0) return false;

            ulong r = (ulong)System.Math.Exp(BigInteger.Log(n) / 2);
            ulong f = 5;
            while (f <= r)
            {
                if (n % f == 0) return false;
                if (n % (f + 2) == 0) return false;
                f += 6;
            }

            return true;
        }

#endif

        #endregion IsPrime overloads

        /// <summary>
        /// Performs an Atkin sieve to find primes.
        /// </summary>
        /// <param name="max">The highest number to check.</param>
        /// <returns>The primes.</returns>
        public static IEnumerable<ulong> AtkinSieve(int max)
        {
            var limit = (ulong)max;
            var isPrime = new bool[limit + 1];
            var sqrt = System.Math.Sqrt(limit);

            for (ulong x = 1; x <= sqrt; x++)
                for (ulong y = 1; y <= sqrt; y++)
                {
                    var n = 4 * x * x + y * y;
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                        isPrime[n] ^= true;

                    n = 3 * x * x + y * y;
                    if (n <= limit && n % 12 == 7)
                        isPrime[n] ^= true;

                    n = 3 * x * x - y * y;
                    if (x > y && n <= limit && n % 12 == 11)
                        isPrime[n] ^= true;
                }

            for (ulong n = 5; n <= sqrt; n++)
                if (isPrime[n])
                {
                    var s = n * n;
                    for (ulong k = s; k <= limit; k += s)
                        isPrime[k] = false;
                }

            yield return 2;
            yield return 3;
            for (ulong n = 5; n <= limit; n += 2)
                if (isPrime[n]) yield return n;
        }

        /// <summary>
        /// Performs an Atkin sieve to find primes, in parallel.
        /// </summary>
        /// <param name="max">The highest number to check.</param>
        /// <returns>The primes.</returns>
        /// <remarks>Parallel is faster for finding primes bigger than 1000.</remarks>
        public static IEnumerable<uint> AtkinSieveParallel(int max)
        {
            var isPrime = new bool[max + 1];
            var sqrt = (int)S.Math.Sqrt(max);

            Parallel.For(1, sqrt, x =>
            {
                uint xx = (uint)(x * x);
                for (uint y = 1; y <= sqrt; y++)
                {
                    var yy = y * y;
                    var n = 4 * xx + yy;
                    if (n <= max && (n % 12 == 1 || n % 12 == 5))
                        isPrime[n] ^= true;

                    n = 3 * xx + yy;
                    if (n <= max && n % 12 == 7)
                        isPrime[n] ^= true;

                    n = 3 * xx - yy;
                    if (x > y && n <= max && n % 12 == 11)
                        isPrime[n] ^= true;
                }
            });

            yield return 2;
            yield return 3;
            for (uint n = 5; n <= sqrt; n++)
            {
                if (isPrime[n])
                {
                    yield return n;
                    uint nn = n * n;
                    for (uint k = nn; k <= max; k += nn)
                        isPrime[k] = false;
                }
            }

            for (uint n = (uint)sqrt + 1; n <= max; n++)
                if (isPrime[n]) yield return n;
        }

        #region CollatzCount overloads

        /// <summary>
        /// Returns the Collatz count.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The Collatz count.</returns>
        public static int CollatzCount(int n)
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
        public static long CollatzCount(long n)
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

#if DOTNET4

        /// <summary>
        /// Returns the Collatz count.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The Collatz count.</returns>
        public static BigInteger CollatzCount(BigInteger n)
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

#endif

        #endregion CollatzCount overloads

        #region Sizes

        /// <summary>
        /// Returns the correct value of the current instance, as if the current instance were in K's.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="thousand">The value of a one thousand.</param>
        /// <returns>The correct value of the current instance.</returns>
        public static int K(this int n, int thousand = 1000)
        {
            return n * thousand;
        }

        /// <summary>
        /// Returns the correct value of the current instance, as if the current instance were in M's.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="thousand">The value of a one thousand.</param>
        /// <returns>The correct value of the current instance.</returns>
        public static int M(this int n, int thousand = 1000)
        {
            return n * (int)S.Math.Pow(thousand, 2);
        }

        /// <summary>
        /// Returns the correct value of the current instance, as if the current instance were in G's.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="thousand">The value of a one thousand.</param>
        /// <returns>The correct value of the current instance.</returns>
        public static int G(this int n, int thousand = 1000)
        {
            return n * (int)S.Math.Pow(thousand, 3);
        }

        /// <summary>
        /// Returns the correct value of the current instance, as if the current instance were in T's.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="thousand">The value of a one thousand.</param>
        /// <returns>The correct value of the current instance.</returns>
        public static int T(this int n, int thousand = 1000)
        {
            return n * (int)S.Math.Pow(thousand, 4);
        }

        #endregion Sizes
    }
}