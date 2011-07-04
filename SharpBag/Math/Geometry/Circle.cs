using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
	using System;

	public struct Circle : IEquatable<Circle>
	{
		#region Properties

		private Point _Center;
		private double _Radius;

		public Point Center { get { return _Center; } }

		public double Radius { get { return _Radius; } }

		public double Circumference
		{
			get
			{
				return this.Radius * Constants.TwoPi;
			}
		}

		public double Area
		{
			get
			{
				return this.Radius * this.Radius * Math.PI;
			}
		}

		#endregion Properties

		#region Constructors

		public Circle(Point center, double radius)
		{
			_Center = center;
			_Radius = radius;
		}

		public Circle(LineSegment radiusLine) : this(radiusLine.From, radiusLine.Distance) { }

		#endregion Constructors

		#region Methods

		public bool Contains(Point point)
		{
			return this.Center.DistanceTo(point) <= this.Radius;
		}

		public bool Contains(Circle circle)
		{
			return this.Center.DistanceTo(circle.Center) + circle.Radius <= this.Radius;
		}

		public bool Intersects(Circle circle)
		{
			double distance = this.Center.DistanceTo(circle.Center);
			return distance <= this.Radius + circle.Radius &&
				   distance + circle.Radius > this.Radius;
		}

		public bool Contains(IPolygon polygon)
		{
			Circle cur = this;
			return polygon.Points.All(p => cur.Contains(p));
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

			return false;
		}

		#endregion Methods

		#region Other

		public bool Equals(Circle other)
		{
			return this.Radius == other.Radius && this.Center == other.Center;
		}

		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(Circle) && this.Equals((Circle)obj);
		}

		public override int GetHashCode()
		{
			return this.Radius.GetHashCode() ^ this.Center.GetHashCode();
		}

		public override string ToString()
		{
			return this.Center + " -> " + this.Radius;
		}

		#endregion Other
	}
}