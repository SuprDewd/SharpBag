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

		private static double _GoldenRatio = 1.6180339887498948;

		/// <summary>
		/// The golden ratio.
		/// </summary>
		/// <remarks>(1 + Sqrt(5)) / 2</remarks>
		public static double GoldenRatio { get { return _GoldenRatio; } }

		#endregion The Golden Ratio

		#region e

		private static double _e = 2.7182818284590452;

		/// <summary>
		/// The constant e.
		/// </summary>
		public static double e { get { return _e; } }

		#endregion e
	}
}