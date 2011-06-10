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
			Fraction<int> frac = new Fraction<int>();

			frac = new Fraction<int>(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Fraction<int>(25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = Fraction<int>.FromFloatingPoint(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = Fraction<int>.FromFloatingPoint(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = Fraction<int>.FromFloatingPoint(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Fraction<int>(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<int>(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<int>(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction<int>(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<int>(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<int>(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction<int>(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction<int>(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Fraction<int>.FromFloatingPoint(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = Fraction<int>.FromFloatingPoint(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = Fraction<int>.FromFloatingPoint(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = Fraction<int>.FromFloatingPoint(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = Fraction<int>.FromFloatingPoint(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = Fraction<int>.FromFloatingPoint(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = Fraction<int>.FromFloatingPoint(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = Fraction<int>.FromFloatingPoint(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = Fraction<int>.FromFloatingPoint(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = Fraction<int>.FromFloatingPoint(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = Fraction<int>.FromFloatingPoint(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Fraction<int>(6);
			Assert.AreEqual(frac.ToString(), "6");

			Fraction<int> divisor = new Fraction<int>(4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction<int>(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Fraction<int>(2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction<int>(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Fraction<int>(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = Fraction<int>.FromFloatingPoint(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = Fraction<int>.FromFloatingPoint(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = Fraction<int>.FromFloatingPoint(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = Fraction<int>.FromFloatingPoint(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Fraction<int>.FromFloatingPoint(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = Fraction<int>.FromFloatingPoint(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Fraction<int> frac1 = new Fraction<int>(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Fraction<int> frac2 = Fraction<int>.FromFloatingPoint(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Fraction<int>)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = 0;
			Assert.AreEqual(frac.ToString(), "0");

			frac = 1;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Fraction<int>(0);
			Assert.AreEqual(frac, Fraction<int>.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = -1;
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Fraction<int>(0);
			Assert.AreEqual(frac, Fraction<int>.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Fraction<int>(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Fraction<int>.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Fraction<int>)2.5).ToString(), "3");

			frac = Fraction<int>.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Fraction<int>)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Fraction<int>)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = 15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Fraction<int>)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Fraction<int>)0.25).ToString(), "1/4");

			frac += (Fraction<int>)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}

		[TestMethod]
		public void Fraction64Test()
		{
			Fraction<long> frac = new Fraction<long>();

			frac = new Fraction<long>(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Fraction<long>(25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = Fraction<long>.FromFloatingPoint(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = Fraction<long>.FromFloatingPoint(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = Fraction<long>.FromFloatingPoint(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Fraction<long>(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<long>(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<long>(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction<long>(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<long>(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<long>(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction<long>(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction<long>(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Fraction<long>.FromFloatingPoint(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = Fraction<long>.FromFloatingPoint(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = Fraction<long>.FromFloatingPoint(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = Fraction<long>.FromFloatingPoint(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = Fraction<long>.FromFloatingPoint(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = Fraction<long>.FromFloatingPoint(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = Fraction<long>.FromFloatingPoint(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = Fraction<long>.FromFloatingPoint(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = Fraction<long>.FromFloatingPoint(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = Fraction<long>.FromFloatingPoint(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = Fraction<long>.FromFloatingPoint(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Fraction<long>(6);
			Assert.AreEqual(frac.ToString(), "6");

			Fraction<long> divisor = new Fraction<long>(4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction<long>(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Fraction<long>(2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction<long>(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Fraction<long>(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = Fraction<long>.FromFloatingPoint(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = Fraction<long>.FromFloatingPoint(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = Fraction<long>.FromFloatingPoint(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = Fraction<long>.FromFloatingPoint(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Fraction<long>.FromFloatingPoint(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = Fraction<long>.FromFloatingPoint(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Fraction<long> frac1 = new Fraction<long>(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Fraction<long> frac2 = Fraction<long>.FromFloatingPoint(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Fraction<long>)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = 0;
			Assert.AreEqual(frac.ToString(), "0");

			frac = 1;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Fraction<long>(0);
			Assert.AreEqual(frac, Fraction<long>.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = -1;
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Fraction<long>(0);
			Assert.AreEqual(frac, Fraction<long>.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Fraction<long>(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Fraction<long>.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Fraction<long>)2.5).ToString(), "3");

			frac = Fraction<long>.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Fraction<long>)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Fraction<long>)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = 15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Fraction<long>)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Fraction<long>)0.25).ToString(), "1/4");

			frac += (Fraction<long>)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}

		[TestMethod]
		public void FractionBigTest()
		{
			Fraction<BigInteger> frac = new Fraction<BigInteger>();

			frac = new Fraction<BigInteger>(1, 5);
			Assert.AreEqual(frac.ToString(), "1/5");

			frac = new Fraction<BigInteger>((BigInteger)25);
			Assert.AreEqual(frac.ToString(), "25");

			frac = Fraction<BigInteger>.FromFloatingPoint(0.0);
			Assert.AreEqual(frac.ToString(), "0");

			frac = Fraction<BigInteger>.FromFloatingPoint(0.25);
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = Fraction<BigInteger>.FromFloatingPoint(9.25);
			Assert.AreEqual(frac.ToString(), "37/4");

			frac = new Fraction<BigInteger>(int.MaxValue, 1);
			string compareTo = string.Format("{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<BigInteger>(1, int.MaxValue);
			compareTo = string.Format("1/{0}", int.MaxValue);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<BigInteger>(int.MaxValue, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction<BigInteger>(int.MinValue + 1, 1);
			compareTo = string.Format("{0}", int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<BigInteger>(1, int.MinValue + 1);
			compareTo = string.Format("-1/{0}", System.Math.Abs(int.MinValue + 1));
			Assert.AreEqual(frac.ToString(), compareTo);

			frac = new Fraction<BigInteger>(int.MinValue + 1, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "1");

			frac = new Fraction<BigInteger>(int.MaxValue, int.MinValue + 1);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = new Fraction<BigInteger>(int.MinValue + 1, int.MaxValue);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Fraction<BigInteger>.FromFloatingPoint(0.025);
			Assert.AreEqual(frac.ToString(), "1/40");

			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 2.0);
			Assert.AreEqual(frac.ToString(), "1/2");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 3.0);
			Assert.AreEqual(frac.ToString(), "1/3");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 4.0);
			Assert.AreEqual(frac.ToString(), "1/4");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 5.0);
			Assert.AreEqual(frac.ToString(), "1/5");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 6.0);
			Assert.AreEqual(frac.ToString(), "1/6");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 7.0);
			Assert.AreEqual(frac.ToString(), "1/7");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 8.0);
			Assert.AreEqual(frac.ToString(), "1/8");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 9.0);
			Assert.AreEqual(frac.ToString(), "1/9");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 10.0);
			Assert.AreEqual(frac.ToString(), "1/10");
			frac = Fraction<BigInteger>.FromFloatingPoint(1 / 49.0);
			Assert.AreEqual(frac.ToString(), "1/49");

			frac = new Fraction<BigInteger>((BigInteger)6);
			Assert.AreEqual(frac.ToString(), "6");

			Fraction<BigInteger> divisor = new Fraction<BigInteger>((BigInteger)4);
			Assert.AreEqual(divisor.ToString(), "4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "2");

			frac = new Fraction<BigInteger>(9, 4);
			Assert.AreEqual(frac.ToString(), "9/4");

			divisor = new Fraction<BigInteger>((BigInteger)2);
			Assert.AreEqual(divisor.ToString(), "2");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/4");

			frac = new Fraction<BigInteger>(5, 12);
			Assert.AreEqual(frac.ToString(), "5/12");

			divisor = new Fraction<BigInteger>(1, 4);
			Assert.AreEqual(divisor.ToString(), "1/4");

			frac %= divisor;
			Assert.AreEqual(frac.ToString(), "1/6");

			frac = Fraction<BigInteger>.FromFloatingPoint(1.0);
			Assert.AreEqual(frac.ToString(), "1");

			frac = Fraction<BigInteger>.FromFloatingPoint(2.0);
			Assert.AreEqual(frac.ToString(), "2");

			frac = Fraction<BigInteger>.FromFloatingPoint(-2.0);
			Assert.AreEqual(frac.ToString(), "-2");

			frac = Fraction<BigInteger>.FromFloatingPoint(-1.0);
			Assert.AreEqual(frac.ToString(), "-1");

			frac = Fraction<BigInteger>.FromFloatingPoint(0.5);
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = Fraction<BigInteger>.FromFloatingPoint(1.5);
			Assert.AreEqual(frac.ToString(), "3/2");

			for (int numerator = -100; numerator < 100; numerator++)
			{
				for (int denominator = -100; denominator < 100; denominator++)
				{
					Fraction<BigInteger> frac1 = new Fraction<BigInteger>(numerator, denominator);

					double dbl = (double)numerator / (double)denominator;
					Fraction<BigInteger> frac2 = Fraction<BigInteger>.FromFloatingPoint(dbl);

					Assert.AreEqual(frac1, frac2);
				}
			}

			frac = (Fraction<BigInteger>)Convert.ToDouble("6,25");
			Assert.AreEqual(frac.ToString(), "25/4");

			frac = BigInteger.Zero;
			Assert.AreEqual(frac.ToString(), "0");

			frac = BigInteger.One;
			Assert.AreEqual(frac.ToString(), "1");

			frac /= new Fraction<BigInteger>(BigInteger.Zero);
			Assert.AreEqual(frac, Fraction<BigInteger>.PositiveInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol);

			frac = BigInteger.Negate(BigInteger.One);
			Assert.AreEqual(frac.ToString(), "-1");

			frac /= new Fraction<BigInteger>(BigInteger.Zero);
			Assert.AreEqual(frac, Fraction<BigInteger>.NegativeInfinity);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol);

			frac = new Fraction<BigInteger>(0, 0);
			Assert.AreEqual(frac.ToString(), NumberFormatInfo.CurrentInfo.NaNSymbol);

			frac = Fraction<BigInteger>.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac + (Fraction<BigInteger>)2.5).ToString(), "3");

			frac = Fraction<BigInteger>.Parse("1/2");
			Assert.AreEqual(frac.ToString(), "1/2");

			frac = (Fraction<BigInteger>)Double.Parse("22,5");
			Assert.AreEqual(frac.ToString(), "45/2");

			frac = (Fraction<BigInteger>)10.25;
			Assert.AreEqual(frac.ToString(), "41/4");

			frac = (BigInteger)15;
			Assert.AreEqual(frac.ToString(), "15");

			frac = (Fraction<BigInteger>)0.5;
			Assert.AreEqual(frac.ToString(), "1/2");

			Assert.AreEqual((frac - (Fraction<BigInteger>)0.25).ToString(), "1/4");

			frac += (Fraction<BigInteger>)0.5;
			Assert.AreEqual(frac.ToString(), "1");
		}
	}
}