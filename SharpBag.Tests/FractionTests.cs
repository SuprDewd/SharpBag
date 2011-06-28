using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Math.ForInt32;
using SharpBag.Math.ForLong;

#if DOTNET4

using SharpBag.Math.ForBigInteger;

#endif

namespace SharpBag.Tests
{
	/// <summary>
	/// http://www.codeproject.com/KB/recipes/fractiion.aspx
	/// </summary>
	[TestClass]
	public class FractionTests
	{
		[TestMethod]
		public void FractionTest()
		{
			Math.ForInt32.Fraction frac = new Math.ForInt32.Fraction();

			frac = new Math.ForInt32.Fraction(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Math.ForInt32.Fraction(25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Math.ForInt32.Fraction(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForInt32.Fraction(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForInt32.Fraction(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Math.ForInt32.Fraction(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForInt32.Fraction(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForInt32.Fraction(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Math.ForInt32.Fraction(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Math.ForInt32.Fraction(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = Math.ForInt32.Fraction.FromFloatingPoint(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Math.ForInt32.Fraction(6);
			Assert.AreEqual(frac.ToString(), "6");

			Math.ForInt32.Fraction divisor = new Math.ForInt32.Fraction(4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Math.ForInt32.Fraction(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Math.ForInt32.Fraction(2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Math.ForInt32.Fraction(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Math.ForInt32.Fraction(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = Math.ForInt32.Fraction.FromFloatingPoint(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Math.ForInt32.Fraction frac1 = new Math.ForInt32.Fraction(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Math.ForInt32.Fraction frac2 = Math.ForInt32.Fraction.FromFloatingPoint(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Math.ForInt32.Fraction)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = 0;
			Assert.AreEqual(frac.ToString(), "0");

			frac = 1;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Math.ForInt32.Fraction(0);
			Assert.AreEqual(frac, Math.ForInt32.Fraction.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = -1;
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Math.ForInt32.Fraction(0);
			Assert.AreEqual(frac, Math.ForInt32.Fraction.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Math.ForInt32.Fraction(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Math.ForInt32.Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Math.ForInt32.Fraction)2.5).ToString(), "3");

			frac = Math.ForInt32.Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Math.ForInt32.Fraction)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Math.ForInt32.Fraction)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = 15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Math.ForInt32.Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Math.ForInt32.Fraction)0.25).ToString(), "1/4");

			frac += (Math.ForInt32.Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}

		[TestMethod]
		public void Fraction64Test()
		{
			Math.ForInt64.Fraction frac = new Math.ForInt64.Fraction();

			frac = new Math.ForInt64.Fraction(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Math.ForInt64.Fraction(25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Math.ForInt64.Fraction(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForInt64.Fraction(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForInt64.Fraction(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Math.ForInt64.Fraction(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForInt64.Fraction(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForInt64.Fraction(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Math.ForInt64.Fraction(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Math.ForInt64.Fraction(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = Math.ForInt64.Fraction.FromFloatingPoint(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Math.ForInt64.Fraction(6);
			Assert.AreEqual(frac.ToString(), "6");

			Math.ForInt64.Fraction divisor = new Math.ForInt64.Fraction(4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Math.ForInt64.Fraction(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Math.ForInt64.Fraction(2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Math.ForInt64.Fraction(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Math.ForInt64.Fraction(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = Math.ForInt64.Fraction.FromFloatingPoint(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Math.ForInt64.Fraction frac1 = new Math.ForInt64.Fraction(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Math.ForInt64.Fraction frac2 = Math.ForInt64.Fraction.FromFloatingPoint(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Math.ForInt64.Fraction)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = 0;
			Assert.AreEqual(frac.ToString(), "0");

			frac = 1;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Math.ForInt64.Fraction(0);
			Assert.AreEqual(frac, Math.ForInt64.Fraction.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = -1;
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Math.ForInt64.Fraction(0);
			Assert.AreEqual(frac, Math.ForInt64.Fraction.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Math.ForInt64.Fraction(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Math.ForInt64.Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Math.ForInt64.Fraction)2.5).ToString(), "3");

			frac = Math.ForInt64.Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Math.ForInt64.Fraction)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Math.ForInt64.Fraction)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = 15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Math.ForInt64.Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Math.ForInt64.Fraction)0.25).ToString(), "1/4");

			frac += (Math.ForInt64.Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}

#if DOTNET4

		[TestMethod]
		public void FractionBigTest()
		{
			Math.ForBigInteger.Fraction frac = new Math.ForBigInteger.Fraction();

			frac = new Math.ForBigInteger.Fraction(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Math.ForBigInteger.Fraction((BigInteger)25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Math.ForBigInteger.Fraction(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForBigInteger.Fraction(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForBigInteger.Fraction(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Math.ForBigInteger.Fraction(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForBigInteger.Fraction(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Math.ForBigInteger.Fraction(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Math.ForBigInteger.Fraction(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Math.ForBigInteger.Fraction(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Math.ForBigInteger.Fraction((BigInteger)6);
			Assert.AreEqual(frac.ToString(), "6");

			Math.ForBigInteger.Fraction divisor = new Math.ForBigInteger.Fraction((BigInteger)4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Math.ForBigInteger.Fraction(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Math.ForBigInteger.Fraction((BigInteger)2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Math.ForBigInteger.Fraction(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Math.ForBigInteger.Fraction(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = Math.ForBigInteger.Fraction.FromFloatingPoint(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Math.ForBigInteger.Fraction frac1 = new Math.ForBigInteger.Fraction(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Math.ForBigInteger.Fraction frac2 = Math.ForBigInteger.Fraction.FromFloatingPoint(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Math.ForBigInteger.Fraction)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = BigInteger.Zero;
			Assert.AreEqual(frac.ToString(), "0");

			frac = BigInteger.One;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Math.ForBigInteger.Fraction(BigInteger.Zero);
			Assert.AreEqual(frac, Math.ForBigInteger.Fraction.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = BigInteger.Negate(BigInteger.One);
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Math.ForBigInteger.Fraction(BigInteger.Zero);
			Assert.AreEqual(frac, Math.ForBigInteger.Fraction.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Math.ForBigInteger.Fraction(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Math.ForBigInteger.Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Math.ForBigInteger.Fraction)2.5).ToString(), "3");

			frac = Math.ForBigInteger.Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Math.ForBigInteger.Fraction)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Math.ForBigInteger.Fraction)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = (BigInteger)15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Math.ForBigInteger.Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Math.ForBigInteger.Fraction)0.25).ToString(), "1/4");

			frac += (Math.ForBigInteger.Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}

#endif
	}
}