using S = System;
using System;
using System.Drawing;
using System.Numerics;

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
        /// <param name="Radius">The radius of the circle.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static PointF[] PointsInCircleF(int Radius)
        {
            if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            return PointsInCircleF(Radius, new PointF(Radius, Radius), 360);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="Radius">The radius of the circle.</param>
        /// <param name="Center">The center-point of the circle.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static PointF[] PointsInCircleF(int Radius, PointF Center)
        {
            if (Center == null) throw new ArgumentNullException("Center", "Center must not be null.");
            if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            return PointsInCircleF(Radius, Center, 360);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="Radius">The radius of the circle.</param>
        /// <param name="Points">Number of points to return.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static PointF[] PointsInCircleF(int Radius, int Points)
        {
            if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            if (Points < 0) throw new ArgumentException("Points must be greater than or equal to 0.");
            return PointsInCircleF(Radius, new PointF(Radius, Radius), Points);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="Radius">The radius of the circle.</param>
        /// <param name="Center">The center-point of the circle.</param>
        /// <param name="Points">Number of points to return.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static PointF[] PointsInCircleF(int Radius, PointF Center, int Points)
        {
            if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            if (Points < 0) throw new ArgumentException("Points must be greater than or equal to 0.");
            PointF[] PointArray = new PointF[Points];
            for (int i = 0; i < Points; i++)
            {
                PointArray[i] = new PointF((float)(Center.X + Radius * S.Math.Cos(2 * S.Math.PI * i / Points)), (float)(Center.Y + Radius * S.Math.Sin(2 * S.Math.PI * i / Points)));
            }
            return PointArray;
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="Radius">The radius of the circle.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Point[] PointsInCircle(int Radius)
        {
            if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            return PointsInCircle(Radius, new Point(Radius, Radius), 360);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="Radius">The radius of the circle.</param>
        /// <param name="Center">The center-point of the circle.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static Point[] PointsInCircle(int Radius, Point Center)
        {
            if (Center == null) throw new ArgumentNullException("Center", "Center must not be null.");
            if (Radius < 0) throw new ArgumentException("Radius must be greater than 0.");
            return PointsInCircle(Radius, Center, 360);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="Radius">The radius of the circle.</param>
        /// <param name="Points">Number of points to return.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Point[] PointsInCircle(int Radius, int Points)
        {
            if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            if (Points < 0) throw new ArgumentException("Points must be greater than or equal to 0.");
            return PointsInCircle(Radius, new Point(Radius, Radius), Points);
        }

        /// <summary>
        /// Find points in a circle with a specified radius.
        /// </summary>
        /// <param name="Radius">The radius of the circle.</param>
        /// <param name="Center">The center-point of the circle.</param>
        /// <param name="Points">Number of points to return.</param>
        /// <returns>An array containing points.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static Point[] PointsInCircle(int Radius, Point Center, int Points)
        {
            if (Center == null) throw new ArgumentNullException("Center", "Center must not be null.");
            if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            if (Points < 0) throw new ArgumentException("Points must be greater than or equal to 0.");
            Point[] PointArray = new Point[Points];
            for (int i = 0; i < Points; i++)
            {
                PointArray[i] = new Point((int)S.Math.Round(Center.X + Radius * S.Math.Cos(2 * S.Math.PI * i / Points)), (int)S.Math.Round(Center.Y + Radius * S.Math.Sin(2 * S.Math.PI * i / Points)));
            }
            return PointArray;
        }
        #endregion

        /// <summary>
        /// Calculates wave length from the average fluctuation time.
        /// </summary>
        /// <param name="Tz">The average fluctuation time (Tz).</param>
        /// <returns>The length of the wave.</returns>
        public static double WaveLength(double Tz)
        {
            return (Tz * Tz) * 1.56;
        }

        /// <summary>
        /// Gets the Pascal triangle entry at the specified row and column.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>The entry.</returns>
        public static long PascalTriangleEntry(int row, int column)
        {
            long current = 1;

            for (int i = 1; i <= column; i++)
            {
                current = (current * (row + 1 - i)) / i;
            }

            return current;
        }

        /// <summary>
        /// Finds the greatest common divisor (gcd) of two integers.
        /// </summary>
        /// <param name="a">An integer.</param>
        /// <param name="b">An integer.</param>
        /// <returns>The greatest commond divisor (gcd) of the two integers.</returns>
        public static int Gcd(int a, int b)
        {
            while (true)
            {
                if (a % b == 0) return b;

                int c = a % b;
                a = b;
                b = c;
            }
        }

        #region IsPrime overloads

        /// <summary>
        /// Checks whether a number is a prime number or not.
        /// </summary>
        /// <param name="candidate">The number to test.</param>
        /// <returns>Whether the number is a prime number or not.</returns>
        public static bool IsPrime(int candidate)
        {
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }
            return candidate != 1;
        }

        /// <summary>
        /// Checks whether a number is a prime number or not.
        /// </summary>
        /// <param name="candidate">The number to test.</param>
        /// <returns>Whether the number is a prime number or not.</returns>
        public static bool IsPrime(long candidate)
        {
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            for (long i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }
            return candidate != 1;
        }

        /// <summary>
        /// Checks whether a number is a prime number or not.
        /// </summary>
        /// <param name="candidate">The number to test.</param>
        /// <returns>Whether the number is a prime number or not.</returns>
        public static bool IsPrime(BigInteger candidate)
        {
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            for (long i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }
            return candidate != 1;
        }

        #endregion

        /// <summary>
        /// Whether the specified year is leap year.
        /// </summary>
        /// <param name="y">The specified year.</param>
        /// <returns>Whether the specified year is leap year.</returns>
        public static bool IsLeapYear(int y)
        {
            return (y % 4 == 0) && (y % 100 != 0) || (y % 400 == 0);
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

        #endregion
    }
}