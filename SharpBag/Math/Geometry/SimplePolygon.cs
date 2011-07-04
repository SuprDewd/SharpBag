using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
	using System;

	public struct SimplePolygon : IPolygon, IEquatable<SimplePolygon>
	{
		#region Properties

		private Point[] _Points;

		public int PointCount { get { return _Points.Length; } }

		public IEnumerable<Point> Points { get { return _Points; } }

		public double Area
		{
			get
			{
				double a = 0;
				for (int i = 0; i < this.PointCount - 1; i++) a += this[i].X * this[i + 1].Y - this[i + 1].X * this[i].Y;
				return Math.Abs(a * 0.5);
			}
		}

		public double Circumference
		{
			get
			{
				double c = this[0].DistanceTo(this[this.PointCount - 1]);
				for (int i = 1; i < this.PointCount; i++) c += this[i - 1].DistanceTo(this[i]);
				return c;
			}
		}

		public Point this[int i] { get { return this._Points[i]; } }

		#endregion Properties

		#region Constructors

		public SimplePolygon(params Point[] points)
		{
			Contract.Requires(points.Length >= 3);
			// TODO: make sure polygon is simple
			_Points = points;
		}

		#endregion Constructors

		#region Methods

		public bool Contains(Point point)
		{
			bool c = false;
			for (int i = 0, j = this.PointCount - 1; i < this.PointCount; j = i++)
			{
				if (((this[i].Y > point.Y) != (this[j].Y > point.Y)) && (point.X < (this[j].X - this[i].X) * (point.Y - this[i].Y) / (this[j].Y - this[i].Y) + this[i].X))
				{
					c = !c;
				}
			}

			return c;
		}

		public bool Contains(IPolygon polygon)
		{
			SimplePolygon cur = this;
			return !this.Points.Any(p => polygon.Contains(p)) &&
					polygon.Points.All(p => cur.Contains(p));
		}

		public bool Intersects(IPolygon polygon)
		{
			bool inside = false, outside = false;
			foreach (Point p in polygon.Points)
			{
				if (this.Contains(p)) inside = true;
				else outside = true;

				if (inside && outside) return true;
			}

			inside = outside = false;
			foreach (Point p in this.Points)
			{
				if (polygon.Contains(p)) inside = true;
				else outside = true;

				if (inside && outside) return true;
			}

			return false;
		}

		#endregion Methods

		#region Other

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

		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(SimplePolygon) && this.Equals((SimplePolygon)obj);
		}

		public override int GetHashCode()
		{
			int hash = 0;
			for (int i = 0; i < this.PointCount; i++) hash ^= this[i].GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(this[0].ToString());
			for (int i = 1; i < this.PointCount; i++) sb.Append(" -> ").Append(this[i].ToString());
			return sb.ToString();
		}

		#endregion Other
	}
}