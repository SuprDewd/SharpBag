using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Math;

namespace SharpBag.Tests
{
	[TestClass]
	public class BigDecimalTests
	{
		[TestMethod]
		public void Constructors()
		{
			BigDecimal a = BigDecimal.Parse("123"),
					   b = BigDecimal.Parse("123.456"),
					   c = BigDecimal.Parse("-123"),
					   d = BigDecimal.Parse("-123.456");

			Assert.AreEqual(a, new BigDecimal((int)123));
			Assert.AreEqual(a, new BigDecimal((long)123));
			Assert.AreEqual(a, new BigDecimal((BigInteger)123));
			Assert.AreEqual(a, new BigDecimal((double)123));
			Assert.AreEqual(a, new BigDecimal((float)123));

			Assert.AreEqual(a, new BigDecimal((int)123, 0));
			Assert.AreEqual(a, new BigDecimal((long)123, 0));
			Assert.AreEqual(a, new BigDecimal((BigInteger)123, 0));
			Assert.AreEqual(a, new BigDecimal((double)123, 0));
			Assert.AreEqual(a, new BigDecimal((float)123, 0));

			Assert.AreEqual(a, (BigDecimal)((int)123));
			Assert.AreEqual(a, (BigDecimal)((long)123));
			Assert.AreEqual(a, (BigDecimal)((BigInteger)123));
			Assert.AreEqual(a, (BigDecimal)((double)123));
			Assert.AreEqual(a, (BigDecimal)((float)123));

			Assert.AreEqual(b, new BigDecimal((float)123.456));
			Assert.AreEqual(b, new BigDecimal((double)123.456));

			Assert.AreEqual(b, new BigDecimal((float)123.456, 3));
			Assert.AreEqual(b, new BigDecimal((double)123.456, 3));

			Assert.AreEqual(b, (BigDecimal)((float)123.456));
			Assert.AreEqual(b, (BigDecimal)((double)123.456));

			Assert.AreEqual(c, new BigDecimal((int)(-123)));
			Assert.AreEqual(c, new BigDecimal((long)(-123)));
			Assert.AreEqual(c, new BigDecimal((BigInteger)(-123)));
			Assert.AreEqual(c, new BigDecimal((double)(-123)));
			Assert.AreEqual(c, new BigDecimal((float)(-123)));

			Assert.AreEqual(c, new BigDecimal((int)(-123), 0));
			Assert.AreEqual(c, new BigDecimal((long)(-123), 0));
			Assert.AreEqual(c, new BigDecimal((BigInteger)(-123), 0));
			Assert.AreEqual(c, new BigDecimal((double)(-123), 0));
			Assert.AreEqual(c, new BigDecimal((float)(-123), 0));

			Assert.AreEqual(c, (BigDecimal)((int)(-123)));
			Assert.AreEqual(c, (BigDecimal)((long)(-123)));
			Assert.AreEqual(c, (BigDecimal)((BigInteger)(-123)));
			Assert.AreEqual(c, (BigDecimal)((double)(-123)));
			Assert.AreEqual(c, (BigDecimal)((float)(-123)));

			Assert.AreEqual(d, new BigDecimal((float)(-123.456)));
			Assert.AreEqual(d, new BigDecimal((double)(-123.456)));

			Assert.AreEqual(d, new BigDecimal((float)(-123.456), 3));
			Assert.AreEqual(d, new BigDecimal((double)(-123.456), 3));

			Assert.AreEqual(d, (BigDecimal)((float)(-123.456)));
			Assert.AreEqual(d, (BigDecimal)((double)(-123.456)));
		}

		[TestMethod]
		public void Parsing()
		{
			BigDecimal a = new BigDecimal(123),
					   b = new BigDecimal(123.456),
					   c = new BigDecimal(-123),
					   d = new BigDecimal(-123.456);

			Assert.AreEqual(a, BigDecimal.Parse("123"));
			Assert.AreEqual(a, BigDecimal.Parse("123.0"));
			Assert.AreEqual(a, BigDecimal.Parse("123.00"));
			Assert.AreEqual(a, BigDecimal.Parse("123.000"));
			Assert.AreEqual(a, BigDecimal.Parse("123,0"));
			Assert.AreEqual(a, BigDecimal.Parse("123,00"));
			Assert.AreEqual(a, BigDecimal.Parse("123,000"));

			Assert.AreEqual(b, BigDecimal.Parse("123.456"));
			Assert.AreEqual(b, BigDecimal.Parse("123.4560"));
			Assert.AreEqual(b, BigDecimal.Parse("123.45600"));
			Assert.AreEqual(b, BigDecimal.Parse("123.456000"));
			Assert.AreEqual(b, BigDecimal.Parse("123,456"));
			Assert.AreEqual(b, BigDecimal.Parse("123,4560"));
			Assert.AreEqual(b, BigDecimal.Parse("123,45600"));
			Assert.AreEqual(b, BigDecimal.Parse("123,456000"));

			Assert.AreEqual(a, BigDecimal.Parse("123", 0));
			Assert.AreEqual(a, BigDecimal.Parse("123.0", 0));
			Assert.AreEqual(a, BigDecimal.Parse("123.00", 0));
			Assert.AreEqual(a, BigDecimal.Parse("123.000", 0));
			Assert.AreEqual(a, BigDecimal.Parse("123,0", 0));
			Assert.AreEqual(a, BigDecimal.Parse("123,00", 0));
			Assert.AreEqual(a, BigDecimal.Parse("123,000", 0));

			Assert.AreEqual(b, BigDecimal.Parse("123.456", 3));
			Assert.AreEqual(b, BigDecimal.Parse("123.4560", 3));
			Assert.AreEqual(b, BigDecimal.Parse("123.45600", 3));
			Assert.AreEqual(b, BigDecimal.Parse("123.456000", 3));
			Assert.AreEqual(b, BigDecimal.Parse("123,456", 3));
			Assert.AreEqual(b, BigDecimal.Parse("123,4560", 3));
			Assert.AreEqual(b, BigDecimal.Parse("123,45600", 3));
			Assert.AreEqual(b, BigDecimal.Parse("123,456000", 3));

			Assert.AreEqual(c, BigDecimal.Parse("-123"));
			Assert.AreEqual(c, BigDecimal.Parse("-123.0"));
			Assert.AreEqual(c, BigDecimal.Parse("-123.00"));
			Assert.AreEqual(c, BigDecimal.Parse("-123.000"));
			Assert.AreEqual(c, BigDecimal.Parse("-123,0"));
			Assert.AreEqual(c, BigDecimal.Parse("-123,00"));
			Assert.AreEqual(c, BigDecimal.Parse("-123,000"));

			Assert.AreEqual(d, BigDecimal.Parse("-123.456"));
			Assert.AreEqual(d, BigDecimal.Parse("-123.4560"));
			Assert.AreEqual(d, BigDecimal.Parse("-123.45600"));
			Assert.AreEqual(d, BigDecimal.Parse("-123.456000"));
			Assert.AreEqual(d, BigDecimal.Parse("-123,456"));
			Assert.AreEqual(d, BigDecimal.Parse("-123,4560"));
			Assert.AreEqual(d, BigDecimal.Parse("-123,45600"));
			Assert.AreEqual(d, BigDecimal.Parse("-123,456000"));

			Assert.AreEqual(c, BigDecimal.Parse("-123", 0));
			Assert.AreEqual(c, BigDecimal.Parse("-123.0", 0));
			Assert.AreEqual(c, BigDecimal.Parse("-123.00", 0));
			Assert.AreEqual(c, BigDecimal.Parse("-123.000", 0));
			Assert.AreEqual(c, BigDecimal.Parse("-123,0", 0));
			Assert.AreEqual(c, BigDecimal.Parse("-123,00", 0));
			Assert.AreEqual(c, BigDecimal.Parse("-123,000", 0));

			Assert.AreEqual(d, BigDecimal.Parse("-123.456", 3));
			Assert.AreEqual(d, BigDecimal.Parse("-123.4560", 3));
			Assert.AreEqual(d, BigDecimal.Parse("-123.45600", 3));
			Assert.AreEqual(d, BigDecimal.Parse("-123.456000", 3));
			Assert.AreEqual(d, BigDecimal.Parse("-123,456", 3));
			Assert.AreEqual(d, BigDecimal.Parse("-123,4560", 3));
			Assert.AreEqual(d, BigDecimal.Parse("-123,45600", 3));
			Assert.AreEqual(d, BigDecimal.Parse("-123,456000", 3));
		}

		[TestMethod]
		public void Operations()
		{
			Assert.AreEqual(BigDecimal.Parse("357.556"), BigDecimal.Parse("123.456") + BigDecimal.Parse("234.1"));
			Assert.AreEqual(BigDecimal.Parse("110.644"), BigDecimal.Parse("-123.456") + BigDecimal.Parse("234.1"));
			Assert.AreEqual(BigDecimal.Parse("-110.644"), BigDecimal.Parse("123.456") + BigDecimal.Parse("-234.1"));
			Assert.AreEqual(BigDecimal.Parse("-357.556"), BigDecimal.Parse("-123.456") + BigDecimal.Parse("-234.1"));

			Assert.AreEqual(BigDecimal.Parse("-110.644"), BigDecimal.Parse("123.456") - BigDecimal.Parse("234.1"));
			Assert.AreEqual(BigDecimal.Parse("-357.556"), BigDecimal.Parse("-123.456") - BigDecimal.Parse("234.1"));
			Assert.AreEqual(BigDecimal.Parse("357.556"), BigDecimal.Parse("123.456") - BigDecimal.Parse("-234.1"));
			Assert.AreEqual(BigDecimal.Parse("110.644"), BigDecimal.Parse("-123.456") - BigDecimal.Parse("-234.1"));

			Assert.AreEqual(BigDecimal.Parse("28901.0496"), BigDecimal.Parse("123.456") * BigDecimal.Parse("234.1"));
			Assert.AreEqual(BigDecimal.Parse("-28901.0496"), BigDecimal.Parse("-123.456") * BigDecimal.Parse("234.1"));
			Assert.AreEqual(BigDecimal.Parse("-28901.0496"), BigDecimal.Parse("123.456") * BigDecimal.Parse("-234.1"));
			Assert.AreEqual(BigDecimal.Parse("28901.0496"), BigDecimal.Parse("-123.456") * BigDecimal.Parse("-234.1"));

			Assert.AreEqual(BigDecimal.Parse("0.527364"), BigDecimal.Parse("123.456", 6) / BigDecimal.Parse("234.1", 6));
			Assert.AreEqual(BigDecimal.Parse("-0.527364"), BigDecimal.Parse("-123.456", 6) / BigDecimal.Parse("234.1", 6));
			Assert.AreEqual(BigDecimal.Parse("-0.527364"), BigDecimal.Parse("123.456", 6) / BigDecimal.Parse("-234.1", 6));
			Assert.AreEqual(BigDecimal.Parse("0.527364"), BigDecimal.Parse("-123.456", 6) / BigDecimal.Parse("-234.1", 6));
			Assert.AreEqual(BigDecimal.Parse("1234,25341"), BigDecimal.Parse("15467061,5789088633") / BigDecimal.Parse("12531,51213"));
		}

		[TestMethod]
		public void MathFunctions()
		{
			Assert.AreEqual(BigDecimal.Parse("1"), BigDecimal.Pow(BigDecimal.Parse("652143.2345"), 0));
			Assert.AreEqual(BigDecimal.Parse("652143.2345"), BigDecimal.Pow(BigDecimal.Parse("652143.2345"), 1));
			Assert.AreEqual(BigDecimal.Parse("425290798304,12199025"), BigDecimal.Pow(BigDecimal.Parse("652143.2345"), 2));
			Assert.AreEqual(BigDecimal.Parse("277350516809137229,404212463625"), BigDecimal.Pow(BigDecimal.Parse("652143.2345"), 3));
			Assert.AreEqual(BigDecimal.Parse("180872263122157371938031,6239536210950625"), BigDecimal.Pow(BigDecimal.Parse("652143.2345"), 4));
			Assert.AreEqual(BigDecimal.Parse("117954622703818777153687476808,40213892149072965625"), BigDecimal.Pow(BigDecimal.Parse("652143.2345"), 5));
			Assert.AreEqual(BigDecimal.Parse("5917195494324256212594722323841113987805852925729648131715357554854642,266878206602289799726256649098076189394775390625"), BigDecimal.Pow(BigDecimal.Parse("652143.2345", 100), 12));

			Assert.AreEqual(BigDecimal.Parse("0"), BigDecimal.Log10(BigDecimal.Parse("1")));
			Assert.AreEqual(BigDecimal.Parse("1"), BigDecimal.Log10(BigDecimal.Parse("10")));
			Assert.AreEqual(BigDecimal.Parse("2"), BigDecimal.Log10(BigDecimal.Parse("100")));
			Assert.AreEqual(BigDecimal.Parse("3"), BigDecimal.Log10(BigDecimal.Parse("1000")));
			Assert.AreEqual(BigDecimal.Parse("1,185981"), BigDecimal.Log10(BigDecimal.Parse("15,34551", 6)));
		}
	}
}