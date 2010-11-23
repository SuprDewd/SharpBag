using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Comparers
{
    /// <summary>
    /// A comparer that compares a string containing numbers.
    /// </summary>
    public class AlphaNumberComparer : IComparer<string>
    {
        /// <summary>
        /// The location of the number.
        /// </summary>
        public AlphaNumberSettings Location { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="location">The location of the number.</param>
        public AlphaNumberComparer(AlphaNumberSettings location = AlphaNumberSettings.Leading)
        {
            this.Location = location;
        }

        /// <see cref="IComparer{T}.Compare(T, T)"/>
        public int Compare(string a, string b)
        {
            StringComparer sc = StringComparer.CurrentCultureIgnoreCase;

            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return sc.Compare(a, b);

            
            string numericX = this.Location == AlphaNumberSettings.Leading ? FindLeadingNumber(a) : FindTrailingNumber(a);
            string numericY = this.Location == AlphaNumberSettings.Leading ? FindLeadingNumber(b) : FindTrailingNumber(b);

            if (numericX != string.Empty && numericY != string.Empty)
            {
                if (this.Location == AlphaNumberSettings.Trailing)
                {
                    int stringPartCompareResult = sc.Compare(a.Remove(a.Length - numericX.Length), b.Remove(b.Length - numericY.Length));
                    if (stringPartCompareResult != 0) return stringPartCompareResult;

                    Double nX = Double.Parse(numericX);
                    Double nY = Double.Parse(numericY);
                    return nX.CompareTo(nY);
                }
                else
                {
                    int numberPartCompareResult = Double.Parse(numericX).CompareTo(Double.Parse(numericY));
                    if (numberPartCompareResult != 0) return numberPartCompareResult;

                    return sc.Compare(a, b);
                }
            }
            else return sc.Compare(a, b);
        }

        /// <summary>
        /// Finds a trailing number.
        /// </summary>
        /// <param name="s">The string to search in.</param>
        /// <returns>The number.</returns>
        private static string FindTrailingNumber(string s)
        {
            string numeric = string.Empty;

            for (int i = s.Length - 1; i > -1; i--)
            {
                if (char.IsNumber(s[i])) numeric = s[i] + numeric;
                else break;
            }

            return numeric;
        }

        /// <summary>
        /// Finds a leading number.
        /// </summary>
        /// <param name="s">The string to search in.</param>
        /// <returns>The number.</returns>
        private static string FindLeadingNumber(string s)
        {
            string numeric = string.Empty;

            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsNumber(s[i])) numeric = numeric + s[i];
                else break;
            }

            return numeric;
        }
    }
}