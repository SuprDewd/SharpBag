using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.BagMath
{
    /// <summary>
    /// Extensions methods for the BagMath class.
    /// </summary>
    public static class BagMathExt
    {
        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<long> ToInfinity(this long start)
        {
            for (var current = start; ; current += 1)
                yield return current;
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to positive infinity.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<int> ToInfinity(this int start)
        {
            for (var current = start; ; current += 1)
                yield return current;
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

        /// <summary>
        /// Rounds the current instance.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <returns>The current instance rounded.</returns>
        public static int Round(this double d)
        {
            return (int)Math.Round(d);
        }

        /// <summary>
        /// Rounds the current instance.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="digits">Number of digits to keep after the comma.</param>
        /// <returns>The current instance rounded.</returns>
        public static double Round(this double d, int digits)
        {
            return Math.Round(d, digits);
        }

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
    }
}