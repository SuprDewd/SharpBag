using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Strings
{
    /// <summary>
    /// Extension methods for the BagStrings namespace.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Reverses the current string instance.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The current string instance reversed.</returns>
        public static string Reverse(this string s)
        {
            if (s == null) return null;

            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }

        /// <summary>
        /// Reverses the current string instance using XORing.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The current string instance reversed.</returns>
        public static string ReverseXor(this string s)
        {
            if (s == null) return null;
            char[] charArray = s.ToCharArray();
            int len = s.Length - 1;

            for (int i = 0; i < len; i++, len--)
            {
                charArray[i] ^= charArray[len];
                charArray[len] ^= charArray[i];
                charArray[i] ^= charArray[len];
            }

            return new string(charArray);
        }

        /// <summary>
        /// Outputs the enumerable as a pretty string.
        /// </summary>
        /// <typeparam name="T">The type of elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="before">A string to prepend to the output.</param>
        /// <param name="delimiter">A string to insert in between the elements.</param>
        /// <param name="after">A string to append to the output.</param>
        /// <returns>The current instance as a pretty string.</returns>
        public static string ToStringPretty<T>(this IEnumerable<T> source, string before = "", string delimiter = ", ", string after = "")
        {
            if (source == null) throw new ArgumentNullException("source");

            StringBuilder result = new StringBuilder();
            result.Append(before);

            bool firstElement = true;
            foreach (T elem in source)
            {
                if (firstElement) firstElement = false;
                else result.Append(delimiter);

                result.Append(elem.ToString());
            }

            result.Append(after);
            return result.ToString();
        }
    }
}
