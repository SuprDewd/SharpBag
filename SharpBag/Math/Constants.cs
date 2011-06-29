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

		public static BigDecimal TheGoldenRatioBig(int precision)
		{
			if (_TheGoldenRatioBig.Precision < precision)
			{
				_TheGoldenRatioBig = (BigDecimal.PositiveOne + BigDecimal.Sqrt(new BigDecimal(5, precision + 10))) / 2;
			}

			return _TheGoldenRatioBig.WithPrecision(precision);
		}

		public static BigDecimal TheGoldenRatioReciprocalBig(int precision)
		{
			if (_TheGoldenRatioReciprocalBig.Precision < precision)
			{
				_TheGoldenRatioReciprocalBig = TheGoldenRatioBig(precision) - 1;
			}

			return _TheGoldenRatioReciprocalBig.WithPrecision(precision);
		}

		#endregion The Golden Ratio

		#region E

		private static BigDecimal _EBig = BigDecimal.Parse("2.71828182845904523536028747135266249775724709369996");
		private static BigDecimal _Log10EBig = BigDecimal.Parse("0.43429448190325182765112891891660508229439700580367");

		public static BigDecimal EBig(int precision)
		{
			if (_EBig.Precision < precision)
			{
				_EBig = BigDecimal.Exp(BigDecimal.PositiveOne.WithPrecision(precision + 10));
			}

			return _EBig.WithPrecision(precision);
		}

		public static BigDecimal Log10EBig(int precision)
		{
			if (_Log10EBig.Precision < precision)
			{
				_Log10EBig = BigDecimal.Log10(Constants.EBig(precision + 10));
			}

			return _Log10EBig.WithPrecision(precision);
		}

		#endregion E

		#region Square roots

		private static BigDecimal _Sqrt2Big = BigDecimal.Parse("1.41421356237309504880168872420969807856967187537695");
		private static BigDecimal _Sqrt3Big = BigDecimal.Parse("1.73205080756887729352744634150587236694280525381038");
		private static BigDecimal _Sqrt5Big = BigDecimal.Parse("2.23606797749978969640917366873127623544061835961153");

		public static BigDecimal Sqrt2Big(int precision)
		{
			if (_Sqrt2Big.Precision < precision)
			{
				_Sqrt2Big = BigDecimal.Sqrt(new BigDecimal(2, precision + 10));
			}

			return _Sqrt2Big.WithPrecision(precision);
		}

		public static BigDecimal Sqrt3Big(int precision)
		{
			if (_Sqrt3Big.Precision < precision)
			{
				_Sqrt3Big = new BigDecimal(new BigDecimal(3, precision + 10));
			}

			return _Sqrt3Big.WithPrecision(precision);
		}

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

		public static BigDecimal PiBig(int precision)
		{
			if (_PiBig.Precision < precision)
			{
				BigDecimal one = BigDecimal.PositiveOne.WithPrecision(precision + 10),
						   two = new BigDecimal(2, precision + 10),
						   four = new BigDecimal(4, precision + 10),
						   lastA = one,
						   lastB = one / Constants.Sqrt2Big(precision + 10),
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

		#endregion Pi
	}
}