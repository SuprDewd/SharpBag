using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
    using System;

    /// <summary>
    /// A line segment.
    /// </summary>
    public struct LineSegment : IEquatable<LineSegment>
    {
        #region Properties

        private Point _From;
        private Point _To;

        /// <summary>
        /// The starting point of the line segment.
        /// </summary>
        public Point From { get { return _From; } }

        /// <summary>
        /// The ending point of the line segment.
        /// </summary>
        public Point To { get { return _To; } }

        /// <summary>
        /// The length of the line segment.
        /// </summary>
        public double Length { get { return this.From.DistanceTo(this.To); } }

        /// <summary>
        /// The slope of the line segment.
        /// </summary>
        public double Slope { get { return this.From.SlopeTo(this.To); } }

        /// <summary>
        /// Whether the line segment is horizontal.
        /// </summary>
        public bool IsHorizontal { get { return this.From.Y == this.To.Y; } }

        /// <summary>
        /// Whether the line segment is vertical.
        /// </summary>
        public bool IsVertical { get { return this.From.X == this.To.X; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> struct.
        /// </summary>
        /// <param name="from">The starting point.</param>
        /// <param name="to">The end point.</param>
        public LineSegment(Point from, Point to)
        {
            _From = from;
            _To = to;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Determines how the line segment intersects the specified line segment.
        /// </summary>
        /// <param name="other">The other line segment.</param>
        /// <returns>How the line segment intersects the specified line segment.</returns>
        public IntersectionType Intersects(LineSegment other)
        {
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

        /// <summary>
        /// Determines whether the line segment contains the specified point.
        /// </summary>
        /// <param name="other">The specified point.</param>
        /// <returns>Whether the line segment contains the specified point.</returns>
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

        /// <summary>
        /// Returns a reversed line segment.
        /// </summary>
        /// <returns>The reversed line segment.</returns>
        public LineSegment Reverse()
        {
            return new LineSegment(this.To, this.From);
        }

        #endregion Methods

        #region Other

        /// <summary>
        /// Compare the line segment to the specified point.
        /// </summary>
        /// <param name="point">The specified point.</param>
        /// <returns>The result.</returns>
        public int CompareTo(Point point)
        {
            return ((this.To.X - this.From.X) * (point.Y - this.From.Y) - (this.To.Y - this.From.Y) * (point.X - this.From.X)).CompareTo(0);
        }

        /// <summary>
        /// Whether the line segment is equal to the specified line segment.
        /// </summary>
        /// <param name="other">The specified line segment.</param>
        /// <returns>Whether the line segment is equal to the specified line segment.</returns>
        public bool Equals(LineSegment other)
        {
            if (this.From.Equals(other.From)) return this.To.Equals(other.To);
            else return this.From.Equals(other.To) && this.To.Equals(other.From);
        }

        /// <summary>
        /// Whether the line segment is equal to the specified line segment.
        /// </summary>
        /// <param name="obj">The specified line segment.</param>
        /// <returns>Whether the line segment is equal to the specified line segment.</returns>
        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(LineSegment) && this.Equals((LineSegment)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Utils.Hash(this.From, this.To);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.From + " -> " + this.To;
        }

        #endregion Other
    }
}