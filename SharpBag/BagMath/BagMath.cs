using System;
using System.Drawing;

namespace SharpBag.BagMath
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
                PointArray[i] = new PointF((float)(Center.X + Radius * Math.Cos(2 * Math.PI * i / Points)), (float)(Center.Y + Radius * Math.Sin(2 * Math.PI * i / Points)));
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
                PointArray[i] = new Point((int)Math.Round(Center.X + Radius * Math.Cos(2 * Math.PI * i / Points)), (int)Math.Round(Center.Y + Radius * Math.Sin(2 * Math.PI * i / Points)));
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
        /// Finds the greatest common divisor (gcd) of two integers.
        /// </summary>
        /// <param name="a">An integer.</param>
        /// <param name="b">An integer.</param>
        /// <returns>The greatest commond divisor (gcd) of the two integers.</returns>
        public static int Gcd(int a, int b)
        {
            return GreatestCommonDivisor(a, b);
        }

        /// <summary>
        /// Finds the greatest common divisor (gcd) of two integers.
        /// </summary>
        /// <param name="a">An integer.</param>
        /// <param name="b">An integer.</param>
        /// <returns>The greatest commond divisor (gcd) of the two integers.</returns>
        private static int GreatestCommonDivisor(int a, int b)
        {
            while (true)
            {
                if (a % b == 0) return b;

                int c = a % b;
                a = b;
                b = c;
            }
        }

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
    }
}
