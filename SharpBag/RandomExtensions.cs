using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag
{
    /// <summary>
    /// Char types.
    /// </summary>
    public enum CharType
    {
        /// <summary>
        /// A lower-case alphabetic char.
        /// </summary>
        AlphabeticLower,
        /// <summary>
        /// An upper-case alphabetic char.
        /// </summary>
        AlphabeticUpper,
        /// <summary>
        /// An alphabetic char.
        /// </summary>
        AlphabeticAny,
        /// <summary>
        /// A lower-case alphanumeric char.
        /// </summary>
        AlphanumericLower,
        /// <summary>
        /// An upper-case alphanumeric char.
        /// </summary>
        AlphanumericUpper,
        /// <summary>
        /// An alphanumeric char.
        /// </summary>
        AlphanumericAny,
        /// <summary>
        /// A numeric char.
        /// </summary>
        Numeric
    }

    /// <summary>
    /// A static class with extensions for the Random class.
    /// </summary>
    public static class RandomExtensions
    {
        private const double AlphanumericProbabilityNumericAny = 10.0 / 62.0;
        private const double AlphanumericProbabilityNumericCased = 10.0 / 36.0;

        /// <summary>
        /// Returns a random boolean.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <param name="probability">The probability of returning true.</param>
        /// <returns>A random boolean.</returns>
        public static bool NextBool(this Random random, double probability = 0.5)
        {
            if (probability == 1) return true;
            if (probability == 0) return false;
            return random.NextDouble() <= probability;
        }

        /// <summary>
        /// Returns a random character.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <param name="mode">What kind of characters are allowed.</param>
        /// <returns>A random character.</returns>
        public static char NextChar(this Random random, CharType mode)
        {
            switch (mode)
            {
                case CharType.AlphabeticAny: return random.NextAlphabeticChar();
                case CharType.AlphabeticLower: return random.NextAlphabeticChar(false);
                case CharType.AlphabeticUpper: return random.NextAlphabeticChar(true);
                case CharType.AlphanumericAny: return random.NextAlphanumericChar();
                case CharType.AlphanumericLower: return random.NextAlphanumericChar(false);
                case CharType.AlphanumericUpper: return random.NextAlphanumericChar(true);
                case CharType.Numeric: return random.NextNumericChar();
                default: return random.NextAlphanumericChar();
            }
        }

        /// <summary>
        /// Returns a random character.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <returns>A random character.</returns>
        public static char NextChar(this Random random)
        {
            return random.NextChar(CharType.AlphanumericAny);
        }

        private static char NextAlphanumericChar(this Random random, bool uppercase)
        {
            bool numeric = random.NextBool(AlphanumericProbabilityNumericCased);

            if (numeric) return random.NextNumericChar();
            else return random.NextAlphabeticChar(uppercase);
        }

        private static char NextAlphanumericChar(this Random random)
        {
            bool numeric = random.NextBool(AlphanumericProbabilityNumericAny);

            if (numeric) return random.NextNumericChar();
            else return random.NextAlphabeticChar(random.NextBool());
        }

        private static char NextAlphabeticChar(this Random random, bool uppercase)
        {
            if (uppercase) return (char)random.Next(65, 91);
            else return (char)random.Next(97, 123);
        }

        private static char NextAlphabeticChar(this Random random)
        {
            return random.NextAlphabeticChar(random.NextBool());
        }

        private static char NextNumericChar(this Random random)
        {
            return (char)random.Next(48, 58);
        }

        /// <summary>
        /// Returns a random DateTime between minValue and maxValue.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <param name="minValue">The lowest value.</param>
        /// <param name="maxValue">The highest value.</param>
        /// <returns>A random DateTime between minValue and maxValue.</returns>
        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            return DateTime.FromOADate(random.NextDouble(minValue.ToOADate(), maxValue.ToOADate()));
        }

        /// <summary>
        /// Returns a random DateTime.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <returns>A random DateTime.</returns>
        public static DateTime NextDateTime(this Random random)
        {
            return random.NextDateTime(DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// Returns a random double between minValue and maxValue.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <param name="minValue">The lowest value.</param>
        /// <param name="maxValue">The highest value.</param>
        /// <returns>A random double between minValue and maxValue.</returns>
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            if (maxValue < minValue) throw new ArgumentException("Minimum value must be less than maximum value.");

            double difference = maxValue - minValue;
            if (!double.IsInfinity(difference)) return minValue + (random.NextDouble() * difference);
            else
            {
                double halfDifference = (maxValue * 0.5) - (minValue * 0.5);

                if (random.NextBool()) return minValue + (random.NextDouble() * halfDifference);
                else return (minValue + halfDifference) + (random.NextDouble() * halfDifference);
            }
        }

        /// <summary>
        /// Returns a random string with the specified length.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <param name="numChars">The length of the string.</param>
        /// <param name="mode">The type of characters in the string.</param>
        /// <returns>A random string with the specified length.</returns>
        public static string NextString(this Random random, int numChars, CharType mode = CharType.AlphanumericAny)
        {
            char[] chars = new char[numChars];

            for (int i = 0; i < numChars; ++i)
                chars[i] = random.NextChar(mode);

            return new string(chars);
        }

        /// <summary>
        /// Returns a random TimeSpan between minValue and maxValue.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>A random TimeSpan between minValue and maxValue.</returns>
        public static TimeSpan NextTimeSpan(this Random random, TimeSpan minValue, TimeSpan maxValue)
        {
            return TimeSpan.FromMilliseconds(random.NextDouble(minValue.TotalMilliseconds, maxValue.TotalMilliseconds));
        }

        /// <summary>
        /// Returns a random TimeSpan.
        /// </summary>
        /// <param name="random">The current instance.</param>
        /// <returns>A random TimeSpan.</returns>
        public static TimeSpan NextTimeSpan(this Random random)
        {
            return random.NextTimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue);
        }
    }
}