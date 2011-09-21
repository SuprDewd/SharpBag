using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
    /// <summary>
    /// An type of intersection.
    /// </summary>
    public enum IntersectionType
    {
        /// <summary>
        /// Something is not intersected.
        /// </summary>
        NotIntersected = 1,
        /// <summary>
        /// Something is joined.
        /// </summary>
        Joined = 6,
        /// <summary>
        /// Something is intersected.
        /// </summary>
        Intersected = 4
    }
}