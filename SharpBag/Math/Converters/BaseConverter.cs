using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace SharpBag.Math.Converters
{
    /// <summary>
    /// Converts numbers between bases.
    /// </summary>
    public static class BaseConverter
    {
        /// <summary>
        /// Converts the specified number from the specified start base to the specified target base.
        /// </summary>
        /// <param name="number">The number as a string.</param>
        /// <param name="startBase">The base of the number.</param>
        /// <param name="targetBase">The base to convert to.</param>
        /// <returns>The specified number in the specified target base.</returns>
        /// <remarks>Bases must be in the range 2 to 36.</remarks>
        public static string ToBase(string number, int startBase, int targetBase)
        {
            Contract.Requires(!String.IsNullOrEmpty(number));
            return FromBase10(ToBase10(number, startBase), targetBase);
        }

        /// <summary>
        /// Converts the specified number from the specified start base to base 10.
        /// </summary>
        /// <param name="number">The number as a string.</param>
        /// <param name="startBase">The base of the number.</param>
        /// <returns>The number in base 10.</returns>
        /// <remarks>Bases must be in the range 2 to 36.</remarks>
        public static int ToBase10(string number, int startBase)
        {
            Contract.Requires(!String.IsNullOrEmpty(number));
            Contract.Requires(startBase.IsBetweenOrEqualTo(2, 36));
            if (startBase == 10) return Convert.ToInt32(number);

            char[] chrs = number.ToCharArray();
            int m = chrs.Length - 1;
            int n = startBase;
            int x;
            int rtn = 0;

            foreach (char c in chrs)
            {
                if (char.IsNumber(c)) x = int.Parse(c.ToString());
                else x = Convert.ToInt32(c) - 55;

                rtn += x * (Convert.ToInt32(System.Math.Pow(n, m)));

                m--;
            }

            return rtn;
        }

        /// <summary>
        /// Converts the specified base 10 number to the specified target base.
        /// </summary>
        /// <param name="number">The base 10 number.</param>
        /// <param name="targetBase">The target base.</param>
        /// <returns>The target in the target base.</returns>
        /// <remarks>Bases must be in the range 2 to 36.</remarks>
        public static string FromBase10(int number, int targetBase)
        {
            if (targetBase < 2 || targetBase > 36) return "";
            if (targetBase == 10) return number.ToString();

            int n = targetBase;
            int q = number;
            int r;
            StringBuilder rtn = new StringBuilder();

            while (q >= n)
            {
                r = q % n;
                q = q / n;

                rtn.Insert(0, r < 10 ? r.ToString() : Convert.ToChar(r + 55).ToString());
            }

            rtn.Insert(0, q < 10 ? q.ToString() : Convert.ToChar(q + 55).ToString());

            return rtn.ToString();
        }
    }
}