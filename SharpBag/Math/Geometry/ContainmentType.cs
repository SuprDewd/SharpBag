using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math.Geometry
{
    /// <summary>
    /// A type of containment.
    /// </summary>
    public enum ContainmentType
    {
        /// <summary>
        /// Something is not contained.
        /// </summary>
        NotContained = 1,
        /// <summary>
        /// Something is joined.
        /// </summary>
        Joined = 6,
        /// <summary>
        /// Something is contained.
        /// </summary>
        Contained = 4
    }
}