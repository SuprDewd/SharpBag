using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	public class BigIntegerCalculator : Calculator<BigInteger>
	{
		public override BigInteger Add(BigInteger a, BigInteger b)
		{
			return a + b;
		}

		public override BigInteger Multiply(BigInteger a, BigInteger b)
		{
			return a * b;
		}

		public override BigInteger Divide(BigInteger a, BigInteger b)
		{
			return a / b;
		}

		public override BigInteger Modulo(BigInteger a, BigInteger b)
		{
			return a % b;
		}

		public override BigInteger Negate(BigInteger n)
		{
			return -n;
		}

		public override BigInteger Convert(byte n)
		{
			return n;
		}

		public override byte ConvertToByte(BigInteger n)
		{
			return (byte)n;
		}

		public override BigInteger Convert(short n)
		{
			return n;
		}

		public override short ConvertToShort(BigInteger n)
		{
			return (short)n;
		}

		public override BigInteger Convert(ushort n)
		{
			return n;
		}

		public override ushort ConvertToUShort(BigInteger n)
		{
			return (ushort)n;
		}

		public override BigInteger Convert(int n)
		{
			return n;
		}

		public override int ConvertToInt(BigInteger n)
		{
			return (int)n;
		}

		public override BigInteger Convert(uint n)
		{
			return n;
		}

		public override uint ConvertToUInt(BigInteger n)
		{
			return (uint)n;
		}

		public override BigInteger Convert(long n)
		{
			return n;
		}

		public override long ConvertToLong(BigInteger n)
		{
			return (long)n;
		}

		public override BigInteger Convert(ulong n)
		{
			return n;
		}

		public override ulong ConvertToULong(BigInteger n)
		{
			return (ulong)n;
		}

		public override BigInteger Convert(float n)
		{
			return (BigInteger)n;
		}

		public override float ConvertToFloat(BigInteger n)
		{
			return (float)n;
		}

		public override BigInteger Convert(double n)
		{
			return (BigInteger)n;
		}

		public override double ConvertToDouble(BigInteger n)
		{
			return (double)n;
		}

		public override BigInteger Convert(decimal n)
		{
			return (BigInteger)n;
		}

		public override decimal ConvertToDecimal(BigInteger n)
		{
			return (decimal)n;
		}

		public override BigInteger Convert(BigInteger n)
		{
			return n;
		}

		public override BigInteger ConvertToBigInteger(BigInteger n)
		{
			return n;
		}

		public override BigInteger Convert(string n)
		{
			return BigInteger.Parse(n);
		}

		public override BigInteger Ceiling(BigInteger n)
		{
			return n;
		}

		public override BigInteger Floor(BigInteger n)
		{
			return n;
		}

		public override BigInteger Pow(BigInteger n, BigInteger power)
		{
			return BigInteger.Pow(n, (int)power);
		}

		public override BigInteger Gcd(BigInteger a, BigInteger b)
		{
			return BagMath.Gcd(a, b);
		}

		public override BigInteger Lcm(BigInteger a, BigInteger b)
		{
			return BagMath.Lcm(a, b);
		}

		public override int Compare(BigInteger a, BigInteger b)
		{
			return a.CompareTo(b);
		}

		public override BigInteger One
		{
			get { return BigInteger.One; }
		}

		public override BigInteger NegativeOne
		{
			get { return BigInteger.MinusOne; }
		}

		public override BigInteger Zero
		{
			get { return BigInteger.Zero; }
		}
	}
}