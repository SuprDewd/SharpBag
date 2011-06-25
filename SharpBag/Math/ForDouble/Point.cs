using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.ForDouble
{
	/// <summary>
	/// A point.
	/// </summary>
	public struct Point
	{
		private double _X;

		/// <summary>
		/// The X coordinate.
		/// </summary>
		public double X { get { return _X; } }

		private double _Y;

		/// <summary>
		/// The Y coordinate.
		/// </summary>
		public double Y { get { return _Y; } }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="x">The X coordinate.</param>
		/// <param name="y">The Y coordinate.</param>
		public Point(double x, double y)
		{
			_X = x;
			_Y = y;
		}
	}
}