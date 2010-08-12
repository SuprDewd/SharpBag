using System;

namespace SharpBag.BagMath.BagConverters
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
        /// <param name="start_base">The base of the number.</param>
        /// <param name="target_base">The base to convert to.</param>
        /// <returns>The specified number in the specified target base.</returns>
        public static string ToBase(string number, int start_base, int target_base)
        {

            int base10 = ToBase10(number, start_base);
            string rtn = FromBase10(base10, target_base);
            return rtn;

        }

        /// <summary>
        /// Converts the specified number from the specified start base to base 10.
        /// </summary>
        /// <param name="number">The number as a string.</param>
        /// <param name="start_base">The base of the number.</param>
        /// <returns>The number in base 10.</returns>
        public static int ToBase10(string number, int start_base)
        {

            if (start_base < 2 || start_base > 36) return 0;
            if (start_base == 10) return Convert.ToInt32(number);

            char[] chrs = number.ToCharArray();
            int m = chrs.Length - 1;
            int n = start_base;
            int x;
            int rtn = 0;

            foreach (char c in chrs)
            {

                if (char.IsNumber(c))
                    x = int.Parse(c.ToString());
                else
                    x = Convert.ToInt32(c) - 55;

                rtn += x * (Convert.ToInt32(Math.Pow(n, m)));

                m--;

            }

            return rtn;
        }

        /// <summary>
        /// Converts the specified base 10 number to the specified target base.
        /// </summary>
        /// <param name="number">The base 10 number.</param>
        /// <param name="target_base">The target base.</param>
        /// <returns>The target in the target base.</returns>
        public static string FromBase10(int number, int target_base)
        {

            if (target_base < 2 || target_base > 36) return "";
            if (target_base == 10) return number.ToString();

            int n = target_base;
            int q = number;
            int r;
            string rtn = "";

            while (q >= n)
            {

                r = q % n;
                q = q / n;

                if (r < 10)
                    rtn = r.ToString() + rtn;
                else
                    rtn = Convert.ToChar(r + 55).ToString() + rtn;

            }

            if (q < 10)
                rtn = q.ToString() + rtn;
            else
                rtn = Convert.ToChar(q + 55).ToString() + rtn;

            return rtn;
        }
    }
}
