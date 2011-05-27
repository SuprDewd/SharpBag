using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	public class Int32Calculator : Calculator<int>
	{
		public override int Add(int a, int b)
		{
			return a + b;
		}

		public override int Multiply(int a, int b)
		{
			return a * b;
		}

		public override int Divide(int a, int b)
		{
			return a / b;
		}

		public override int Modulo(int a, int b)
		{
			return a % b;
		}

		public override int Negate(int n)
		{
			return -n;
		}

		public override int Convert(byte n)
		{
			return n;
		}

		public override byte ConvertToByte(int n)
		{
			return (byte)n;
		}

		public override int Convert(short n)
		{
			return n;
		}

		public override short ConvertToShort(int n)
		{
			return (short)n;
		}

		public override int Convert(ushort n)
		{
			return n;
		}

		public override ushort ConvertToUShort(int n)
		{
			return (ushort)n;
		}

		public override int Convert(int n)
		{
			return n;
		}

		public override int ConvertToInt(int n)
		{
			return n;
		}

		public override int Convert(uint n)
		{
			return (int)n;
		}

		public override uint ConvertToUInt(int n)
		{
			return (uint)n;
		}

		public override int Convert(long n)
		{
			return (int)n;
		}

		public override long ConvertToLong(int n)
		{
			return n;
		}

		public override int Convert(ulong n)
		{
			return (int)n;
		}

		public override ulong ConvertToULong(int n)
		{
			return (ulong)n;
		}

		public override int Convert(float n)
		{
			return (int)n;
		}

		public override float ConvertToFloat(int n)
		{
			return n;
		}

		public override int Convert(double n)
		{
			return (int)n;
		}

		public override double ConvertToDouble(int n)
		{
			return n;
		}

		public override int Convert(decimal n)
		{
			return (int)n;
		}

		public override decimal ConvertToDecimal(int n)
		{
			return n;
		}

		public override int Convert(BigInteger n)
		{
			return (int)n;
		}

		public override BigInteger ConvertToBigInteger(int n)
		{
			return n;
		}

		public override int Convert(string n)
		{
			return System.Convert.ToInt32(n);
		}

		public override int Ceiling(int n)
		{
			return n;
		}

		public override int Floor(int n)
		{
			return n;
		}

		public override int Pow(int n, int power)
		{
			return (int)System.Math.Pow(n, power);
		}

		public override int Gcd(int a, int b)
		{
			return BagMath.Gcd(a, b);
		}

		public override int Lcm(int a, int b)
		{
			return BagMath.Lcm(a, b);
		}

		public override int Compare(int a, int b)
		{
			return a.CompareTo(b);
		}

		public override int One
		{
			get { return 1; }
		}

		public override int NegativeOne
		{
			get { return -1; }
		}

		public override int Zero
		{
			get { return 0; }
		}
	}
}