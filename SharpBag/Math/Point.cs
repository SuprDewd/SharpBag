using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using SharpBag.Math.Calculators;

namespace SharpBag.Math
{
	/// <summary>
	/// A point.
	/// </summary>
	/// <typeparam name="T">The type of numbers to represent the point.</typeparam>
	public struct Point<T>
	{
		static Point()
		{
			if (Calculator == null) Calculator = CalculatorFactory.GetInstanceFor<T>();
		}

		private static Calculator<T> _Calculator;

		/// <summary>
		/// The calculator.
		/// </summary>
		public static Calculator<T> Calculator
		{
			get { return _Calculator; }
			set { _Calculator = value; }
		}

		private T _X;

		/// <summary>
		/// The X coordinate.
		/// </summary>
		public T X { get { return _X; } }

		private T _Y;

		/// <summary>
		/// The Y coordinate.
		/// </summary>
		public T Y { get { return _Y; } }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="x">The X coordinate.</param>
		/// <param name="y">The Y coordinate.</param>
		public Point(T x, T y)
		{
			_X = x;
			_Y = y;
		}
	}
}