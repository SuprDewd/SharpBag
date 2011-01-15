using System.Collections.Generic;
using System.Drawing;
using S = System;

#if DOTNET4
using System.Diagnostics.Contracts;
using System.Numerics;
#endif

namespace SharpBag.Math
{
    /// <summary>
    /// A static class containing methods for doing calculations.
    /// </summary>
    public static class BagMath
    {
        #region PointsInCircleF & PointsInCircle & all overloads

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<PointF> PointsInCircleF(int radius)
        {
#if DOTNET4
            Contract.Requires(radius > 0);
#endif
            return PointsInCircleF(radius, new PointF(radius, radius), 360);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="center">The center-point of the circle.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<PointF> PointsInCircleF(int radius, PointF center)
        {
#if DOTNET4
            Contract.Requires(radius > 0);
#endif
            return PointsInCircleF(radius, center, 360);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="points">Number of points to return.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<PointF> PointsInCircleF(int radius, int points)
        {
#if DOTNET4
            Contract.Requires(radius > 0);
            Contract.Requires(points > 0);
#endif
            return PointsInCircleF(radius, new PointF(radius, radius), points);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="center">The center-point of the circle.</param>
        /// <param name="points">Number of points to return.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<PointF> PointsInCircleF(int radius, PointF center, int points)
        {
#if DOTNET4
            Contract.Requires(radius > 0);
            Contract.Requires(points > 0);
#endif

            for (int i = 0; i < points; i++)
            {
                yield return new PointF((float)(center.X + radius * S.Math.Cos(2 * S.Math.PI * i / points)), (float)(center.Y + radius * S.Math.Sin(2 * S.Math.PI * i / points)));
            }
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<Point> PointsInCircle(int radius)
        {
#if DOTNET4
            Contract.Requires(radius > 0);
#endif
            return PointsInCircle(radius, new Point(radius, radius), 360);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="center">The center-point of the circle.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<Point> PointsInCircle(int radius, Point center)
        {
#if DOTNET4
            Contract.Requires(radius > 0);
#endif
            return PointsInCircle(radius, center, 360);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="points">Number of points to return.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<Point> PointsInCircle(int radius, int points)
        {
#if DOTNET4
            Contract.Requires(radius > 0);
            Contract.Requires(points > 0);
#endif
            return PointsInCircle(radius, new Point(radius, radius), points);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="center">The center-point of the circle.</param>
        /// <param name="points">Number of points to return.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<Point> PointsInCircle(int radius, Point center, int points)
        {
#if DOTNET4
            Contract.Requires(radius > 0);
            Contract.Requires(points > 0);
#endif
            for (int i = 0; i < points; i++)
            {
                yield return new Point((int)S.Math.Round(center.X + radius * S.Math.Cos(2 * S.Math.PI * i / points)), (int)S.Math.Round(center.Y + radius * S.Math.Sin(2 * S.Math.PI * i / points)));
            }
        }

        #endregion PointsInCircleF & PointsInCircle & all overloads

        /// <summary>
        /// Calculates wave length from the average fluctuation time.
        /// </summary>
        /// <param name="tz">The average fluctuation time (Tz).</param>
        /// <returns>The length of the wave.</returns>
        public static double WaveLength(double tz)
        {
            return (tz * tz) * 1.56;
        }

        /// <summary>
        /// Finds the greatest common divisor (gcd) of two integers.
        /// </summary>
        /// <param name="a">An integer.</param>
        /// <param name="b">An integer.</param>
        /// <returns>The greatest commond divisor (gcd) of the two integers.</returns>
        public static int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }

            return a == 0 ? b : a;
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
        /// <param name="limit">The highest number to check.</param>
        /// <returns>The primes.</returns>
        public static IEnumerable<ulong> AtkinSieve(ulong limit)
        {
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
            return n * thousand * thousand;
        }

        /// <summary>
        /// Returns the correct value of the current instance, as if the current instance were in G's.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="thousand">The value of a one thousand.</param>
        /// <returns>The correct value of the current instance.</returns>
        public static int G(this int n, int thousand = 1000)
        {
            return n * thousand * thousand * thousand;
        }

        /// <summary>
        /// Returns the correct value of the current instance, as if the current instance were in T's.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <param name="thousand">The value of a one thousand.</param>
        /// <returns>The correct value of the current instance.</returns>
        public static int T(this int n, int thousand = 1000)
        {
            return n * thousand * thousand * thousand * thousand;
        }

        #endregion Sizes
    }
}