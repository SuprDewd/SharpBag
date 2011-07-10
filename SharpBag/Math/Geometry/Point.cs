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

		/// <summary>
		/// Implements the operator +.
		/// </summary>
		/// <param name="a">Point a.</param>
		/// <param name="b">Point b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Point operator +(Point a, Point b) { return new Point(a.X + b.X, a.Y + b.Y); }

		/// <summary>
		/// Implements the operator -.
		/// </summary>
		/// <param name="a">Point a.</param>
		/// <param name="b">Point b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Point operator -(Point a, Point b) { return new Point(a.X - b.X, a.Y - b.Y); }

		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="a">Point a.</param>
		/// <param name="b">Point b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Point operator *(Point a, Point b) { return new Point(a.X * b.X, a.Y * b.Y); }

		/// <summary>
		/// Implements the operator /.
		/// </summary>
		/// <param name="a">Point a.</param>
		/// <param name="b">Point b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Point operator /(Point a, Point b) { return new Point(a.X / b.X, a.Y / b.Y); }

		/// <summary>
		/// Calculates the dot product.
		/// </summary>
		/// <param name="a">Point a.</param>
		/// <param name="b">Point b.</param>
		/// <returns>The dot product.</returns>
		public static double DotProduct(Point a, Point b) { return a.X * b.X + a.Y * b.Y; }

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="a">Point a.</param>
		/// <param name="b">Point b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(Point a, Point b) { return a.Equals(b); }

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="a">Point a.</param>
		/// <param name="b">Point b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(Point a, Point b) { return !a.Equals(b); }

		#endregion Operators

		#region Methods

		/// <summary>
		/// Calculates the distance from the current instance to the specified point.
		/// </summary>
		/// <param name="other">The specified point.</param>
		/// <returns>The distance.</returns>
		public double DistanceTo(Point other)
		{
			return Math.Sqrt(Math.Pow(other.X - this.X, 2) + Math.Pow(other.Y - this.Y, 2));
		}

		/// <summary>
		/// Calculates the slope from the current instance to the specified point.
		/// </summary>
		/// <param name="other">The specified point.</param>
		/// <returns>The slope.</returns>
		public double SlopeTo(Point other)
		{
			return (other.Y - this.Y) / (other.X - this.X);
		}

		#endregion Methods

		#region Casts

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="SharpBag.Math.Geometry.Point"/>.
		/// </summary>
		/// <param name="d">The double.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator Point(double d) { return new Point(d, d); }

		/// <summary>
		/// Performs an implicit conversion from <see cref="SharpBag.Math.Geometry.Point"/> to <see cref="System.Drawing.PointF"/>.
		/// </summary>
		/// <param name="p">The point.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator System.Drawing.PointF(Point p) { return new System.Drawing.PointF((float)p.X, (float)p.Y); }

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.Drawing.PointF"/> to <see cref="SharpBag.Math.Geometry.Point"/>.
		/// </summary>
		/// <param name="p">The point.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator Point(System.Drawing.PointF p) { return new Point(p.X, p.Y); }

		#endregion Casts

		#region Other

		/// <summary>
		/// Compares the point to the specified point.
		/// </summary>
		/// <param name="other">The specified point.</param>
		/// <returns>The result.</returns>
		public int CompareTo(Point other)
		{
			if (this.X != other.X) return this.X.CompareTo(other.X);
			return this.Y.CompareTo(other.Y);
		}

		/// <summary>
		/// Determines whether the point is equal to the specified point.
		/// </summary>
		/// <param name="other">The specified point.</param>
		/// <returns>Whether the point is equal to the specified point.</returns>
		public bool Equals(Point other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		/// <summary>
		/// Determines whether the point is equal to the specified point.
		/// </summary>
		/// <param name="obj">The specified point.</param>
		/// <returns>Whether the point is equal to the specified point.</returns>
		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(Point) && this.Equals((Point)obj);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.X, this.Y);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "(" + this.X + ", " + this.Y + ")";
		}

		#endregion Other
	}
}