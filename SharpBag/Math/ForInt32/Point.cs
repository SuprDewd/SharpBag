using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.ForInt32
{
	/// <summary>
	/// A point.
	/// </summary>
	public struct Point
	{
		private int _X;

		/// <summary>
		/// The X coordinate.
		/// </summary>
		public int X { get { return _X; } }

		private int _Y;

		/// <summary>
		/// The Y coordinate.
		/// </summary>
		public int Y { get { return _Y; } }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="x">The X coordinate.</param>
		/// <param name="y">The Y coordinate.</param>
		public Point(int x, int y)
		{
			_X = x;
			_Y = y;
		}
	}
}