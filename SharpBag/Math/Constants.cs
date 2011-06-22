using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math
{
	/// <summary>
	/// Mathematical constants.
	/// </summary>
	public static class Constants
	{
		#region The Golden Ratio

		private static double _GoldenRatio = 1.618033988749894848204586834365638117720d;

		/// <summary>
		/// The golden ratio.
		/// </summary>
		/// <remarks>(1 + Sqrt(5)) / 2</remarks>
		public static double GoldenRatio { get { return _GoldenRatio; } }

		#endregion The Golden Ratio
	}
}