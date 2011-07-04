using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
	using System;

	/// <summary>
	/// A point.
	/// </summary>
	public struct Point : IEquatable<Point>, IComparable<Point>
	{
		#region Properties

		private double _X;
		private double _Y;

		/// <summary>
		/// The X coordinate.
		/// </summary>
		public double X { get { return _X; } }

		/// <summary>
		/// The Y coordinate.
		/// </summary>
		public double Y { get { return _Y; } }

		#endregion Properties

		#region Constructors

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

		#endregion Constructors

		#region Operators

		public static Point operator +(Point a, Point b) { return new Point(a.X + b.X, a.Y + b.Y); }

		public static Point operator -(Point a, Point b) { return new Point(a.X - b.X, a.Y - b.Y); }

		public static Point operator *(Point a, Point b) { return new Point(a.X * b.X, a.Y * b.Y); }

		public static Point operator /(Point a, Point b) { return new Point(a.X / b.X, a.Y / b.Y); }

		public static double DotProduct(Point a, Point b) { return a.X * b.X + a.Y * b.Y; }

		public static bool operator ==(Point a, Point b) { return a.Equals(b); }

		public static bool operator !=(Point a, Point b) { return !a.Equals(b); }

		#endregion Operators

		#region Methods

		public double DistanceTo(Point other)
		{
			return Math.Sqrt(Math.Pow(other.X - this.X, 2) + Math.Pow(other.Y - this.Y, 2));
		}

		public double SlopeTo(Point other)
		{
			return (other.Y - this.Y) / (other.X - this.X);
		}

		#endregion Methods

		#region Casts

		public static implicit operator Point(double d) { return new Point(d, d); }

		public static implicit operator System.Drawing.PointF(Point p) { return new System.Drawing.PointF((float)p.X, (float)p.Y); }

		public static implicit operator Point(System.Drawing.PointF p) { return new Point(p.X, p.Y); }

		#endregion Casts

		#region Other

		public int CompareTo(Point other)
		{
			if (this.X != other.X) return this.X.CompareTo(other.X);
			return this.Y.CompareTo(other.Y);
		}

		public bool Equals(Point other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(Point) && this.Equals((Point)obj);
		}

		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		public override string ToString()
		{
			return "(" + this.X + ", " + this.Y + ")";
		}

		#endregion Other
	}
}