using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
	public interface IPolygon
	{
		#region Properties

		IEnumerable<Point> Points { get; }

		double Circumference { get; }

		double Area { get; }

		#endregion Properties

		#region Methods

		bool Contains(Point point);

		bool Contains(IPolygon polygon);

		bool Intersects(IPolygon polygon);

		#endregion Methods
	}
}