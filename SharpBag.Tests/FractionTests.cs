using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Math;

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
			Fraction frac = new Fraction();

			frac = new Fraction(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Fraction(25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = new Fraction(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = new Fraction(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Fraction(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = new Fraction(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = new Fraction(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = new Fraction(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = new Fraction(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = new Fraction(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = new Fraction(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = new Fraction(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = new Fraction(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = new Fraction(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = new Fraction(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Fraction(6);
			Assert.AreEqual(frac.ToString(), "6");

			Fraction divisor = new Fraction(4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Fraction(2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Fraction(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = new Fraction(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = new Fraction(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = new Fraction(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Fraction frac1 = new Fraction(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Fraction frac2 = new Fraction(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Fraction)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = 0;
			Assert.AreEqual(frac.ToString(), "0");

			frac = 1;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Fraction(0);
			Assert.AreEqual(frac, Fraction.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = -1;
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Fraction(0);
			Assert.AreEqual(frac, Fraction.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Fraction(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Fraction)2.5).ToString(), "3");

			frac = Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Fraction)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Fraction)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = 15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Fraction)0.25).ToString(), "1/4");

			frac += (Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}

		[TestMethod]
		public void Fraction64Test()
		{
			Fraction64 frac = new Fraction();

			frac = new Fraction(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Fraction(25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = new Fraction(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = new Fraction(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Fraction(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = new Fraction(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = new Fraction(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = new Fraction(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = new Fraction(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = new Fraction(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = new Fraction(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = new Fraction(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = new Fraction(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = new Fraction(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = new Fraction(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Fraction(6);
			Assert.AreEqual(frac.ToString(), "6");

			Fraction divisor = new Fraction(4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Fraction(2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Fraction(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = new Fraction(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = new Fraction(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = new Fraction(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Fraction frac1 = new Fraction(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Fraction frac2 = new Fraction(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Fraction)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = 0;
			Assert.AreEqual(frac.ToString(), "0");

			frac = 1;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Fraction(0);
			Assert.AreEqual(frac, Fraction.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = -1;
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Fraction(0);
			Assert.AreEqual(frac, Fraction.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Fraction(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Fraction)2.5).ToString(), "3");

			frac = Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Fraction)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Fraction)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = 15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Fraction)0.25).ToString(), "1/4");

			frac += (Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}

		[TestMethod]
		public void FractionBigTest()
		{
			FractionBig frac = new Fraction();

			frac = new Fraction(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Fraction(25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = new Fraction(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = new Fraction(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Fraction(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = new Fraction(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = new Fraction(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = new Fraction(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = new Fraction(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = new Fraction(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = new Fraction(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = new Fraction(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = new Fraction(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = new Fraction(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = new Fraction(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Fraction(6);
			Assert.AreEqual(frac.ToString(), "6");

			Fraction divisor = new Fraction(4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Fraction(2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Fraction(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = new Fraction(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = new Fraction(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = new Fraction(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Fraction frac1 = new Fraction(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Fraction frac2 = new Fraction(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Fraction)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = 0;
			Assert.AreEqual(frac.ToString(), "0");

			frac = 1;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Fraction(0);
			Assert.AreEqual(frac, Fraction.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = -1;
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Fraction(0);
			Assert.AreEqual(frac, Fraction.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Fraction(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Fraction)2.5).ToString(), "3");

			frac = Fraction.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Fraction)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Fraction)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = 15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Fraction)0.25).ToString(), "1/4");

			frac += (Fraction)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}
	}
}