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

		private static double _TheGoldenRatio = 1.618033988749894848204586834365638117720d;

		/// <summary>
		/// The golden ratio.
		/// </summary>
		/// <remarks>(1 + Sqrt(5)) / 2</remarks>
		public static double TheGoldenRatio { get { return _TheGoldenRatio; } }

		private static BigDecimal _TheGoldenRatioBig = BigDecimal.Parse("1.61803398874989484820458683436563811772030917980576");
		private static BigDecimal _TheGoldenRatioReciprocalBig = BigDecimal.Parse("0.61803398874989484820458683436563811772030917980576");

		/// <summary>
		/// The golden ratio.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>The golden ratio.</returns>
		/// <remarks>(1 + Sqrt(5)) / 2</remarks>
		public static BigDecimal TheGoldenRatioBig(int precision)
		{
			if (_TheGoldenRatioBig.Precision < precision)
			{
				_TheGoldenRatioBig = (BigDecimal.One + BigDecimal.Sqrt(new BigDecimal(5, precision + 10))) / 2;
			}

			return _TheGoldenRatioBig.WithPrecision(precision);
		}

		/// <summary>
		/// The reciprocal of the golden ratio.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>The reciprocal of the golden ratio.</returns>
		/// <remarks>1 / ((1 + Sqrt(5)) / 2) or (1 + Sqrt(5)) / 2 - 1</remarks>
		public static BigDecimal TheGoldenRatioReciprocalBig(int precision)
		{
			if (_TheGoldenRatioReciprocalBig.Precision < precision)
			{
				Constants.TheGoldenRatioBig(precision);
				_TheGoldenRatioReciprocalBig = _TheGoldenRatioBig - 1;
			}

			return _TheGoldenRatioReciprocalBig.WithPrecision(precision);
		}

		#endregion The Golden Ratio

		#region E

		private static BigDecimal _EBig = BigDecimal.Parse("2.71828182845904523536028747135266249775724709369996");
		private static BigDecimal _Log10EBig = BigDecimal.Parse("0.43429448190325182765112891891660508229439700580367");

		/// <summary>
		/// The mathematical constant e.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>The mathematical constant e.</returns>
		public static BigDecimal EBig(int precision)
		{
			if (_EBig.Precision < precision)
			{
				_EBig = BigDecimal.Exp(BigDecimal.One.WithPrecision(precision + 10));
			}

			return _EBig.WithPrecision(precision);
		}

		/// <summary>
		/// The base-10 logarithm of the mathematical constant e.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>The base-10 logarithm of the mathematical constant e.</returns>
		public static BigDecimal Log10EBig(int precision)
		{
			if (_Log10EBig.Precision < precision)
			{
				Constants.EBig(precision);
				_Log10EBig = BigDecimal.Log10(_EBig.WithPrecision(precision + 10));
			}

			return _Log10EBig.WithPrecision(precision);
		}

		#endregion E

		#region Square roots

		private static BigDecimal _Sqrt2Big = BigDecimal.Parse("1.41421356237309504880168872420969807856967187537695");
		private static BigDecimal _Sqrt3Big = BigDecimal.Parse("1.73205080756887729352744634150587236694280525381038");
		private static BigDecimal _Sqrt5Big = BigDecimal.Parse("2.23606797749978969640917366873127623544061835961153");

		/// <summary>
		/// The square root of 2.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>The square root of 2.</returns>
		public static BigDecimal Sqrt2Big(int precision)
		{
			if (_Sqrt2Big.Precision < precision)
			{
				_Sqrt2Big = BigDecimal.Sqrt(new BigDecimal(2, precision + 10));
			}

			return _Sqrt2Big.WithPrecision(precision);
		}

		/// <summary>
		/// The square root of 3.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>The square root of 3.</returns>
		public static BigDecimal Sqrt3Big(int precision)
		{
			if (_Sqrt3Big.Precision < precision)
			{
				_Sqrt3Big = new BigDecimal(new BigDecimal(3, precision + 10));
			}

			return _Sqrt3Big.WithPrecision(precision);
		}

		/// <summary>
		/// The square root of 5.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>The square root of 5.</returns>
		public static BigDecimal Sqrt5Big(int precision)
		{
			if (_Sqrt5Big.Precision < precision)
			{
				_Sqrt5Big = new BigDecimal(new BigDecimal(5, precision + 10));
			}

			return _Sqrt5Big.WithPrecision(precision);
		}

		#endregion Square roots

		#region Pi

		private static BigDecimal _PiBig = BigDecimal.Parse("3.14159265358979323846264338327950288419716939937511");
		private static BigDecimal _TwoPiBig = BigDecimal.Parse("6.28318530717958647692528676655900576839433879875021");
		private static BigDecimal _PiDivTwoBig = BigDecimal.Parse("1.57079632679489661923132169163975144209858469968755");
		private static double _TwoPi = 6.283185307179586476925286766559005768394d;
		private static double _PiDivTwo = 1.57079632679489661923132169163975144209858469968755;

		/// <summary>
		/// Two Pi.
		/// </summary>
		/// <remarks>2 * Pi</remarks>
		public static double TwoPi { get { return _TwoPi; } }

		/// <summary>
		/// Pi divided by two.
		/// </summary>
		/// <remarks>Pi / 2</remarks>
		public static double PiDivTwo { get { return _PiDivTwo; } }

		/// <summary>
		/// The mathematical constant Pi.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>The mathematical constant Pi.</returns>
		public static BigDecimal PiBig(int precision)
		{
			if (_PiBig.Precision < precision)
			{
				Constants.Sqrt2Big(precision);

				BigDecimal one = BigDecimal.One.WithPrecision(precision + 10),
						   two = new BigDecimal(2, precision + 10),
						   four = new BigDecimal(4, precision + 10),
						   lastA = one,
						   lastB = one / _Sqrt2Big.WithPrecision(precision + 10),
						   lastT = one / four,
						   lastP = one;

				while (true)
				{
					BigDecimal a = (lastA + lastB) / two,
							   b = BigDecimal.Sqrt(lastA * lastB),
							   temp1 = lastA - a,
							   t = lastT - lastP * (temp1 * temp1),
							   p = two * lastP;

					if (a == lastA && b == lastB)
					{
						_PiBig = BigDecimal.Pow(a + b, 2) / (four * t);
						break;
					}

					lastA = a;
					lastB = b;
					lastT = t;
					lastP = p;
				}
			}

			return _PiBig.WithPrecision(precision);
		}

		/// <summary>
		/// 2 * Pi
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>2 * Pi</returns>
		public static BigDecimal TwoPiBig(int precision)
		{
			if (_TwoPiBig.Precision < precision)
			{
				Constants.PiBig(precision);
				_TwoPiBig = 2 * _PiBig;
			}

			return _TwoPiBig.WithPrecision(precision);
		}

		/// <summary>
		/// Pi / 2
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>Pi / 2</returns>
		public static BigDecimal PiDivTwoBig(int precision)
		{
			if (_PiDivTwoBig.Precision < precision)
			{
				Constants.PiBig(precision);
				_PiDivTwoBig = BigDecimal.Parse("0.5") * _PiBig;
			}

			return _PiDivTwoBig.WithPrecision(precision);
		}

		#endregion Pi
	}
}