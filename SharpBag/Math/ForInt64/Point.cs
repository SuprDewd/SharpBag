using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.ForInt64
{
	/// <summary>
	/// A point.
	/// </summary>
	public struct Point
	{
		private long _X;

		/// <summary>
		/// The X coordinate.
		/// </summary>
		public long X { get { return _X; } }

		private long _Y;

		/// <summary>
		/// The Y coordinate.
		/// </summary>
		public long Y { get { return _Y; } }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="x">The X coordinate.</param>
		/// <param name="y">The Y coordinate.</param>
		public Point(long x, long y)
		{
			_X = x;
			_Y = y;
		}
	}
}