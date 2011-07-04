using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
	using System;

	public struct LineSegment : IEquatable<LineSegment>
	{
		#region Properties

		private Point _From;
		private Point _To;

		public Point From { get { return _From; } }

		public Point To { get { return _To; } }

		public double Distance { get { return this.From.DistanceTo(this.To); } }

		public double Slope { get { return this.From.SlopeTo(this.To); } }

		public bool IsHorizontal { get { return this.From.Y == this.To.Y; } }

		public bool IsVertical { get { return this.From.X == this.To.X; } }

		#endregion Properties

		#region Constructors

		public LineSegment(Point from, Point to)
		{
			_From = from;
			_To = to;
		}

		#endregion Constructors

		#region Methods

		public IntersectionType Intersects(LineSegment other)
		{
			// TODO: Fix...
			/*double thisSlope = this.Slope,
				   thisYIntercept = thisSlope * -this.From.X,
				   otherSlope = other.Slope,
				   otherYIntercept = otherSlope * -other.From.X,
				   x = -(thisYIntercept - otherYIntercept) / (thisSlope - otherSlope);

			return x * thisSlope + thisYIntercept == x * otherSlope + otherYIntercept;*/

			int a = this.CompareTo(other.From);
			if (a == 0) return IntersectionType.Joined;
			int b = this.CompareTo(other.To);
			if (b == 0) return IntersectionType.Joined;
			int c = other.CompareTo(this.From);
			if (c == 0) return IntersectionType.Joined;
			int d = other.CompareTo(this.To);
			if (d == 0) return IntersectionType.Joined;

			return (a != b && c != d) ? IntersectionType.Intersected : IntersectionType.NotIntersected;
		}

		public bool Contains(Point other)
		{
			Point a = this.From,
				  b = this.To,
				  c = other;

			double crossproduct = (c.Y - a.Y) * (b.X - a.X) - (c.X - a.X) * (b.Y - a.Y);
			if (Math.Abs(crossproduct) > Double.Epsilon) return false;

			double dotproduct = (c.X - a.X) * (b.X - a.X) + (c.Y - a.Y) * (b.Y - a.Y);
			if (dotproduct < 0) return false;

			double squaredLengthBA = (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
			if (dotproduct > squaredLengthBA) return false;

			return true;
		}

		public LineSegment Reverse()
		{
			return new LineSegment(this.To, this.From);
		}

		#endregion Methods

		#region Other

		public int CompareTo(Point point)
		{
			return ((this.To.X - this.From.X) * (point.Y - this.From.Y) - (this.To.Y - this.From.Y) * (point.X - this.From.X)).CompareTo(0);
		}

		public bool Equals(LineSegment other)
		{
			if (this.From.Equals(other.From)) return this.To.Equals(other.To);
			else return this.From.Equals(other.To) && this.To.Equals(other.From);
		}

		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(LineSegment) && this.Equals((LineSegment)obj);
		}

		public override int GetHashCode()
		{
			return this.From.GetHashCode() ^ this.To.GetHashCode();
		}

		public override string ToString()
		{
			return this.From + " -> " + this.To;
		}

		#endregion Other
	}
}