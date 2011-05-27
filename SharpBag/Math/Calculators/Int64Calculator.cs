using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	public class Int64Calculator : Calculator<long>
	{
		public override long Add(long a, long b)
		{
			return a + b;
		}

		public override long Multiply(long a, long b)
		{
			return a * b;
		}

		public override long Divide(long a, long b)
		{
			return a / b;
		}

		public override long Modulo(long a, long b)
		{
			return a % b;
		}

		public override long Negate(long n)
		{
			return -n;
		}

		public override long Convert(byte n)
		{
			return n;
		}

		public override byte ConvertToByte(long n)
		{
			return (byte)n;
		}

		public override long Convert(short n)
		{
			return n;
		}

		public override short ConvertToShort(long n)
		{
			return (short)n;
		}

		public override long Convert(ushort n)
		{
			return n;
		}

		public override ushort ConvertToUShort(long n)
		{
			return (ushort)n;
		}

		public override long Convert(int n)
		{
			return n;
		}

		public override int ConvertToInt(long n)
		{
			return (int)n;
		}

		public override long Convert(uint n)
		{
			return n;
		}

		public override uint ConvertToUInt(long n)
		{
			return (uint)n;
		}

		public override long Convert(long n)
		{
			return n;
		}

		public override long ConvertToLong(long n)
		{
			return n;
		}

		public override long Convert(ulong n)
		{
			return (long)n;
		}

		public override ulong ConvertToULong(long n)
		{
			return (ulong)n;
		}

		public override long Convert(float n)
		{
			return (long)n;
		}

		public override float ConvertToFloat(long n)
		{
			return n;
		}

		public override long Convert(double n)
		{
			return (long)n;
		}

		public override double ConvertToDouble(long n)
		{
			return n;
		}

		public override long Convert(decimal n)
		{
			return (long)n;
		}

		public override decimal ConvertToDecimal(long n)
		{
			return n;
		}

		public override long Convert(BigInteger n)
		{
			return (long)n;
		}

		public override BigInteger ConvertToBigInteger(long n)
		{
			return n;
		}

		public override long Convert(string n)
		{
			return System.Convert.ToInt64(n);
		}

		public override long Ceiling(long n)
		{
			return n;
		}

		public override long Floor(long n)
		{
			return n;
		}

		public override long Pow(long n, long power)
		{
			return (long)System.Math.Pow(n, power);
		}

		public override long Gcd(long a, long b)
		{
			return BagMath.Gcd(a, b);
		}

		public override long Lcm(long a, long b)
		{
			return BagMath.Lcm(a, b);
		}

		public override int Compare(long a, long b)
		{
			return a.CompareTo(b);
		}

		public override long One
		{
			get { return 1; }
		}

		public override long NegativeOne
		{
			get { return -1; }
		}

		public override long Zero
		{
			get { return 0; }
		}
	}
}