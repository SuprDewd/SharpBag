using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
	using System;

	/// <summary>
	/// A simple polygon.
	/// </summary>
	/// <remarks>A simple polygon is a polygon with no self-intersections.</remarks>
	public struct SimplePolygon : IPolygon, IEquatable<SimplePolygon>
	{
		#region Properties

		private Point[] _Points;

		/// <summary>
		/// Gets the point count.
		/// </summary>
		public int PointCount { get { return _Points.Length; } }

		/// <summary>
		/// Gets the points.
		/// </summary>
		public IEnumerable<Point> Points { get { return _Points; } }

		/// <summary>
		/// Gets the area.
		/// </summary>
		public double Area
		{
			get
			{
				double a = 0;
				for (int i = 0; i < this.PointCount; i++) a += this[i].X * this[(i + 1) % this.PointCount].Y - this[(i + 1) % this.PointCount].X * this[i].Y;
				return Math.Abs(a * 0.5);
			}
		}

		/// <summary>
		/// Gets the circumference.
		/// </summary>
		public double Circumference
		{
			get
			{
				double c = this[0].DistanceTo(this[this.PointCount - 1]);
				for (int i = 1; i < this.PointCount; i++) c += this[i - 1].DistanceTo(this[i]);
				return c;
			}
		}

		/// <summary>
		/// Gets the <see cref="SharpBag.Math.Geometry.Point"/> at the specified index.
		/// </summary>
		public Point this[int i] { get { return this._Points[i]; } }

		#endregion Properties

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplePolygon"/> struct.
		/// </summary>
		/// <param name="points">The points.</param>
		public SimplePolygon(params Point[] points)
		{
			Contract.Requires(SimplePolygon.IsSimplePolygon(points));
			_Points = points;
		}

		#endregion Constructors

		#region Methods

		/// <summary>
		/// Determines whether the specified points represent a simple polygon.
		/// </summary>
		/// <param name="points">The points.</param>
		/// <returns>
		///   <c>true</c> if the specified points represent a simple polygon; otherwise, <c>false</c>.
		/// </returns>
		[Pure]
		public static bool IsSimplePolygon(Point[] points)
		{
			if (points.Length < 3) return false;

			for (int i = 0; i < points.Length - 1; i++)
			{
				LineSegment line = new LineSegment(points[i], points[i + 1]);

				for (int j = i + 1; j < points.Length; j++)
				{
					IntersectionType intersection = new LineSegment(points[j], points[(j + 1) % points.Length]).Intersects(line);
					if (intersection == IntersectionType.Intersected) return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Determines whether the polygon contains the specified point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>
		/// Whether the polygon contains the specified point.
		/// </returns>
		public bool Contains(Point point) { return (this.Containment(point) & ContainmentType.Contained) != 0; }

		/// <summary>
		/// Determines how the polygon contains the specified point.
		/// </summary>
		/// <param name="point">The specified point.</param>
		/// <returns>
		/// How the polygon contains the specified point.
		/// </returns>
		public ContainmentType Containment(Point point)
		{
			bool c = false, ex = false;
			for (int i = 0, j = this.PointCount - 1; i < this.PointCount; j = i++)
			{
				if (((this[i].Y > point.Y) != (this[j].Y > point.Y)) && (point.X < (this[j].X - this[i].X) * (point.Y - this[i].Y) / (this[j].Y - this[i].Y) + this[i].X))
				{
					c = !c;
					ex = ex || this[i].Equals(point);
				}
			}

			if (c) return ex ? ContainmentType.Joined : ContainmentType.Contained;
			else return ContainmentType.NotContained;
		}

		/// <summary>
		/// Determines whether the polygon contains the specified polygon.
		/// </summary>
		/// <param name="polygon">The polygon.</param>
		/// <returns>
		/// Whether the polygon contains the specified polygon.
		/// </returns>
		public bool Contains(IPolygon polygon) { return (this.Containment(polygon) & ContainmentType.Contained) != 0; }

		/// <summary>
		/// Determines how the polygon contains the specified polygon.
		/// </summary>
		/// <param name="polygon">The specified polygon.</param>
		/// <returns>
		/// How the polygon contains the specified polygon.
		/// </returns>
		public ContainmentType Containment(IPolygon polygon)
		{
			SimplePolygon cur = this;
			bool ex = false;

			foreach (Point p in this.Points)
			{
				ContainmentType c = polygon.Containment(p);
				if (c == ContainmentType.Contained) return ContainmentType.NotContained;
				ex = ex || c == ContainmentType.Joined;
			}

			foreach (Point p in polygon.Points)
			{
				ContainmentType c = this.Containment(p);
				if (c == ContainmentType.NotContained) return ContainmentType.NotContained;
				ex = ex || c == ContainmentType.Joined;
			}

			return ex ? ContainmentType.Joined : ContainmentType.Contained;
		}

		/// <summary>
		/// Determines whether the polygon intersects the specified polygon.
		/// </summary>
		/// <param name="polygon">The polygon.</param>
		/// <returns>
		/// Whether the polygon intersects the specified polygon.
		/// </returns>
		public bool Intersects(IPolygon polygon) { return (this.Intersection(polygon) & IntersectionType.Intersected) != 0; }

		/// <summary>
		/// Determines how the polygon intersects the specified polygon.
		/// </summary>
		/// <param name="polygon">The polygon.</param>
		/// <returns>
		/// How the polygon intersects the specified polygon.
		/// </returns>
		public IntersectionType Intersection(IPolygon polygon)
		{
			bool inside = false, outside = false;
			bool ex = true;
			foreach (Point p in polygon.Points)
			{
				ContainmentType c = this.Containment(p);
				if (c == ContainmentType.Contained || c == ContainmentType.Joined) inside = true;
				else outside = true;

				if (c != ContainmentType.Joined) ex = false;
				if (inside && outside) return ex ? IntersectionType.Joined : IntersectionType.Intersected;
			}

			inside = outside = false;
			ex = true;
			foreach (Point p in this.Points)
			{
				ContainmentType c = polygon.Containment(p);
				if (c == ContainmentType.Contained || c == ContainmentType.Joined) inside = true;
				else outside = true;

				if (c != ContainmentType.Joined) ex = false;
				if (inside && outside) return ex ? IntersectionType.Joined : IntersectionType.Intersected;
			}

			return ex ? IntersectionType.Joined : IntersectionType.NotIntersected;
		}

		#endregion Methods

		#region Other

		/// <summary>
		/// Determines whether the current instance is equal to the specified polygon.
		/// </summary>
		/// <param name="other">The specified polygon.</param>
		/// <returns>Whether the current instance is equal to the specified polygon.</returns>
		public bool Equals(SimplePolygon other)
		{
			if (this.PointCount != other.PointCount) return false;

			// TODO: Test if works.
			int i = 0, j = 0;
			for (; i < this.PointCount && j < this.PointCount; i++)
			{
				if (!this[i].Equals(other[(i + j) % other.PointCount]))
				{
					i = -1;
					j++;
				}
			}

			return i == this.PointCount;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(SimplePolygon) && this.Equals((SimplePolygon)obj);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			int hash = 0;
			for (int i = 0; i < this.PointCount; i++) hash ^= this[i].GetHashCode();
			return hash;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(this[0].ToString());
			for (int i = 1; i < this.PointCount; i++) sb.Append(" -> ").Append(this[i].ToString());
			return sb.ToString();
		}

		#endregion Other
	}
}