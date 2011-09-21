using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
    using System;

    /*public struct Triangle : IPolygon, IEquatable<Triangle>
    {
        #region Properties

        private Point _A;
        private Point _B;
        private Point _C;

        public Point A { get { return _A; } private set { _A = value; } }

        public Point B { get { return _B; } private set { _B = value; } }

        public Point C { get { return _C; } private set { _C = value; } }

        public IEnumerable<Point> Points
        {
            get
            {
                yield return this.A;
                yield return this.B;
                yield return this.C;
            }
        }

        public double Area
        {
            get
            {
                return 0.5 * Math.Abs(this.A.X * this.B.Y + this.B.X * this.C.Y + this.C.X * this.A.Y - this.A.X * this.C.Y - this.C.X * this.B.Y - this.B.X * this.A.Y);
            }
        }

        public double Circumference
        {
            get
            {
                return this.A.DistanceTo(this.B) +
                       this.B.DistanceTo(this.C) +
                       this.C.DistanceTo(this.A);
            }
        }

        #endregion Properties

        #region Constructors

        public Triangle(Point a, Point b, Point c)
        {
            _A = a;
            _B = b;
            _C = c;
        }

        #endregion Constructors

        #region Methods

        public ContainmentType Contains(Point point)
        {
            double myArea = this.Area,
                   area1 = new Triangle(this.A, this.B, point).Area,
                   area2 = new Triangle(this.A, this.C, point).Area,
                   area3 = new Triangle(this.B, this.C, point).Area;

            return (myArea == area1 + area2 + area3) ? ContainmentType.Contained : ContainmentType.NotContained;
        }

        public bool Contains(IPolygon polygon)
        {
            Triangle cur = this;
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

        public bool Equals(Triangle other)
        {
            if (this.A == other.A)
            {
                if (this.B == other.B) return this.C == other.C;
                if (this.B == other.C) return this.C == other.B;
            }
            else if (this.A == other.B)
            {
                if (this.B == other.A) return this.C == other.C;
                if (this.B == other.C) return this.C == other.A;
            }
            else if (this.A == other.C)
            {
                if (this.B == other.B) return this.C == other.A;
                if (this.B == other.A) return this.C == other.B;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Triangle) && this.Equals((Triangle)obj);
        }

        public override int GetHashCode()
        {
            return Utils.Hash(this.A, this.B, this.C);
        }

        public override string ToString()
        {
            return this.A + " -> " + this.B + " -> " + this.C;
        }

        #endregion Other
    }*/
}