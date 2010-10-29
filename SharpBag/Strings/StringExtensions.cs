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
    }
}
