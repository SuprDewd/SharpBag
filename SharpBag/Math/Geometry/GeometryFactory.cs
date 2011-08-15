using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
    /// <summary>
    /// A factory for geometric figures.
    /// </summary>
    public static class GeometryFactory
    {
        /// <summary>
        /// Creates a rectangle between the specified points.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>A rectangle.</returns>
        public static SimplePolygon Rectangle(Point a, Point b)
        {
            return new SimplePolygon(a, new Point(a.X, b.Y), b, new Point(b.X, a.Y));
        }

        /// <summary>
        /// Creates a square with the specified center and side lengths.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="sideLength">The length of the sides.</param>
        /// <returns>A square.</returns>
        public static SimplePolygon Square(Point center, double sideLength)
        {
            return GeometryFactory.Rectangle(center - (sideLength / 2), center + (sideLength / 2));
        }
    }
}