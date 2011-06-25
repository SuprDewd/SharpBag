using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.ForBigInteger
{
	/// <summary>
	/// A point.
	/// </summary>
	public struct Point
	{
		private BigInteger _X;

		/// <summary>
		/// The X coordinate.
		/// </summary>
		public BigInteger X { get { return _X; } }

		private BigInteger _Y;

		/// <summary>
		/// The Y coordinate.
		/// </summary>
		public BigInteger Y { get { return _Y; } }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="x">The X coordinate.</param>
		/// <param name="y">The Y coordinate.</param>
		public Point(BigInteger x, BigInteger y)
		{
			_X = x;
			_Y = y;
		}
	}
}