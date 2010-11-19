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
        // 10 digits vs. 52 alphabetic characters (upper & lower);
        // probability of being numeric: 10 / 62 = 0.1612903225806452
        private const double AlphanumericProbabilityNumericAny = 10.0 / 62.0;

        // 10 digits vs. 26 alphabetic characters (upper OR lower);
        // probability of being numeric: 10 / 36 = 0.2777777777777778
        private const double AlphanumericProbabilityNumericCased = 10.0 / 36.0;

        public static bool NextBool(this Random random, double probability = 0.5)
        {
            return random.NextDouble() <= probability;
        }

        public static char NextChar(this Random random, CharType mode)
        {
            switch (mode)
            {
                case CharType.AlphabeticAny:
                    return random.NextAlphabeticChar();
                case CharType.AlphabeticLower:
                    return random.NextAlphabeticChar(false);
                case CharType.AlphabeticUpper:
                    return random.NextAlphabeticChar(true);
                case CharType.AlphanumericAny:
                    return random.NextAlphanumericChar();
                case CharType.AlphanumericLower:
                    return random.NextAlphanumericChar(false);
                case CharType.AlphanumericUpper:
                    return random.NextAlphanumericChar(true);
                case CharType.Numeric:
                    return random.NextNumericChar();
                default:
                    return random.NextAlphanumericChar();
            }
        }

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

        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            return DateTime.FromOADate(random.NextDouble(minValue.ToOADate(), maxValue.ToOADate()));
        }

        public static DateTime NextDateTime(this Random random)
        {
            return random.NextDateTime(DateTime.MinValue, DateTime.MaxValue);
        }

        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            if (maxValue < minValue) throw new ArgumentException("Minimum value must be less than maximum value.");

            double difference = maxValue - minValue;
            if (!double.IsInfinity(difference)) return minValue + (random.NextDouble() * difference);
            else
            {
                // to avoid evaluating to Double.Infinity, we split the range into two halves:
                double halfDifference = (maxValue * 0.5) - (minValue * 0.5);

                // 50/50 chance of returning a value from the first or second half of the range
                if (random.NextBool()) return minValue + (random.NextDouble() * halfDifference);
                else return (minValue + halfDifference) + (random.NextDouble() * halfDifference);
            }
        }

        public static string NextString(this Random random, int numChars, CharType mode)
        {
            char[] chars = new char[numChars];

            for (int i = 0; i < numChars; ++i)
                chars[i] = random.NextChar(mode);

            return new string(chars);
        }

        public static string NextString(this Random random, int numChars)
        {
            return random.NextString(numChars, CharType.AlphanumericAny);
        }

        public static TimeSpan NextTimeSpan(this Random random, TimeSpan minValue, TimeSpan maxValue)
        {
            return TimeSpan.FromMilliseconds(random.NextDouble(minValue.TotalMilliseconds, maxValue.TotalMilliseconds)
            );
        }

        public static TimeSpan NextTimeSpan(this Random random)
        {
            return random.NextTimeSpan(TimeSpan.MinValue, TimeSpan.MaxValue);
        }
    }
}