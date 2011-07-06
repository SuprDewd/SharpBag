using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
	/// <summary>
	/// A polygon.
	/// </summary>
	public interface IPolygon
	{
		#region Properties

		/// <summary>
		/// The points in the polygon.
		/// </summary>
		IEnumerable<Point> Points { get; }

		/// <summary>
		/// The circumference of the polygon.
		/// </summary>
		double Circumference { get; }

		/// <summary>
		/// The area of the polygon.
		/// </summary>
		double Area { get; }

		#endregion Properties

		#region Methods

		/// <summary>
		/// Determines how the polygon contains the specified point.
		/// </summary>
		/// <param name="point">The specified point.</param>
		/// <returns>How the polygon contains the specified point.</returns>
		ContainmentType Containment(Point point);

		/// <summary>
		/// Determines whether the polygon contains the specified point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>Whether the polygon contains the specified point.</returns>
		bool Contains(Point point);

		/// <summary>
		/// Determines how the polygon contains the specified polygon.
		/// </summary>
		/// <param name="polygon">The specified polygon.</param>
		/// <returns>How the polygon contains the specified polygon.</returns>
		ContainmentType Containment(IPolygon polygon);

		/// <summary>
		/// Determines whether the polygon contains the specified polygon.
		/// </summary>
		/// <param name="polygon">The polygon.</param>
		/// <returns>Whether the polygon contains the specified polygon.</returns>
		bool Contains(IPolygon polygon);

		/// <summary>
		/// Determines how the polygon intersects the specified polygon.
		/// </summary>
		/// <param name="polygon">The polygon.</param>
		/// <returns>How the polygon intersects the specified polygon.</returns>
		IntersectionType Intersection(IPolygon polygon);

		/// <summary>
		/// Determines whether the polygon intersects the specified polygon.
		/// </summary>
		/// <param name="polygon">The polygon.</param>
		/// <returns>Whether the polygon intersects the specified polygon.</returns>
		bool Intersects(IPolygon polygon);

		#endregion Methods
	}
}