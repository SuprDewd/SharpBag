using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
	public static class GeometryFactory
	{
		public static SimplePolygon Rectangle(Point a, Point b)
		{
			return new SimplePolygon(a, new Point(a.X, b.Y), b, new Point(b.X, a.Y));
		}

		public static SimplePolygon Square(Point center, double sideLength)
		{
			return GeometryFactory.Rectangle(center - (sideLength / 2), center + (sideLength / 2));
		}
	}
}