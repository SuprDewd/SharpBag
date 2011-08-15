using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
    using System;

    /// <summary>
    /// A circle.
    /// </summary>
    public struct Circle : IEquatable<Circle>
    {
        #region Properties

        private Point _Center;
        private double _Radius;

        /// <summary>
        /// The center of the circle.
        /// </summary>
        public Point Center { get { return _Center; } }

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        public double Radius { get { return _Radius; } }

        /// <summary>
        /// The circumference of the circle.
        /// </summary>
        public double Circumference
        {
            get
            {
                return this.Radius * Constants.TwoPi;
            }
        }

        /// <summary>
        /// The area of the circle.
        /// </summary>
        public double Area
        {
            get
            {
                return this.Radius * this.Radius * Math.PI;
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(Point center, double radius)
        {
            _Center = center;
            _Radius = radius;
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="radiusLine">A line through the radius.</param>
        public Circle(LineSegment radiusLine) : this(radiusLine.From, radiusLine.Length) { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Whether the circle contains the specified point.
        /// </summary>
        /// <param name="point">The specified point.</param>
        /// <returns>Whether the circle contains the specified point.</returns>
        public bool Contains(Point point)
        {
            return this.Center.DistanceTo(point) <= this.Radius;
        }

        /// <summary>
        /// Whether the circle contains the specified circle.
        /// </summary>
        /// <param name="circle">The specified circle.</param>
        /// <returns>Whether the circle contains the specified circle.</returns>
        public bool Contains(Circle circle)
        {
            return this.Center.DistanceTo(circle.Center) + circle.Radius <= this.Radius;
        }

        /// <summary>
        /// Whether the circle intersects the specified circle.
        /// </summary>
        /// <param name="circle">The specified circle.</param>
        /// <returns>Whether the circle intersects the specifiied circle.</returns>
        public bool Intersects(Circle circle)
        {
            double distance = this.Center.DistanceTo(circle.Center);
            return distance <= this.Radius + circle.Radius &&
                   distance + circle.Radius > this.Radius;
        }

        /// <summary>
        /// Whether the circle contains the specified polygon.
        /// </summary>
        /// <param name="polygon">The specified polygon.</param>
        /// <returns>Whether the circle contains the specified polygon.</returns>
        public bool Contains(IPolygon polygon)
        {
            Circle cur = this;
            return polygon.Points.All(p => cur.Contains(p));
        }

        /// <summary>
        /// Whether the circle intersects the specified polygon.
        /// </summary>
        /// <param name="polygon">The specified polygon.</param>
        /// <returns>Whether the circle intersects the specified polygon.</returns>
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

        /// <summary>
        /// IComparable.Equals()
        /// </summary>
        /// <param name="other">The other circle.</param>
        /// <returns>Whether the current instance equals the specified circle.</returns>
        public bool Equals(Circle other)
        {
            return this.Radius == other.Radius && this.Center == other.Center;
        }

        /// <summary>
        /// Object.Equals()
        /// </summary>
        /// <param name="obj">The other circle.</param>
        /// <returns>Whether the current instance equals the specified circle.</returns>
        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Circle) && this.Equals((Circle)obj);
        }

        /// <summary>
        /// Object.GetHashCode()
        /// </summary>
        /// <returns>The hash code of the current instance.</returns>
        public override int GetHashCode()
        {
            return Utils.Hash(this.Radius, this.Center);
        }

        /// <summary>
        /// Object.ToString()
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            return this.Center + " -> " + this.Radius;
        }

        #endregion Other
    }
}