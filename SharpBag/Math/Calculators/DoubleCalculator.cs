using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	public class DoubleCalculator : Calculator<double>
	{
		public override double Add(double a, double b)
		{
			return a + b;
		}

		public override double Subtract(double a, double b)
		{
			return a - b;
		}

		public override double Multiply(double a, double b)
		{
			return a * b;
		}

		public override double Divide(double a, double b)
		{
			return a / b;
		}

		public override double Modulo(double a, double b)
		{
			return a % b;
		}

		public override double Negate(double n)
		{
			return -n;
		}

		public override double Convert(byte n)
		{
			return n;
		}

		public override byte ConvertToByte(double n)
		{
			return (byte)n;
		}

		public override double Convert(short n)
		{
			return n;
		}

		public override short ConvertToShort(double n)
		{
			return (short)n;
		}

		public override double Convert(ushort n)
		{
			return n;
		}

		public override ushort ConvertToUShort(double n)
		{
			return (ushort)n;
		}

		public override double Convert(int n)
		{
			return n;
		}

		public override int ConvertToInt(double n)
		{
			return (int)n;
		}

		public override double Convert(uint n)
		{
			return (double)n;
		}

		public override uint ConvertToUInt(double n)
		{
			return (uint)n;
		}

		public override double Convert(long n)
		{
			return (double)n;
		}

		public override long ConvertToLong(double n)
		{
			return (long)n;
		}

		public override double Convert(ulong n)
		{
			return (double)n;
		}

		public override ulong ConvertToULong(double n)
		{
			return (ulong)n;
		}

		public override double Convert(float n)
		{
			return (double)n;
		}

		public override float ConvertToFloat(double n)
		{
			return (float)n;
		}

		public override double Convert(double n)
		{
			return n;
		}

		public override double ConvertToDouble(double n)
		{
			return n;
		}

		public override double Convert(decimal n)
		{
			return (double)n;
		}

		public override decimal ConvertToDecimal(double n)
		{
			return (decimal)n;
		}

		public override double Convert(BigInteger n)
		{
			return (double)n;
		}

		public override BigInteger ConvertToBigInteger(double n)
		{
			return (BigInteger)n;
		}

		public override double Convert(string n)
		{
			return System.Convert.ToInt32(n);
		}

		public override double Ceiling(double n)
		{
			return n;
		}

		public override double Floor(double n)
		{
			return n;
		}

		public override double Pow(double n, double power)
		{
			return System.Math.Pow(n, power);
		}

		public override double Gcd(double a, double b)
		{
			return BagMath.Gcd((int)a, (int)b);
		}

		public override double Lcm(double a, double b)
		{
			return BagMath.Lcm((int)a, (int)b);
		}

		public override double Sqrt(double n)
		{
			return System.Math.Sqrt(n);
		}

		public override int Compare(double a, double b)
		{
			return a.CompareTo(b);
		}

		public override double One
		{
			get { return 1; }
		}

		public override double NegativeOne
		{
			get { return -1; }
		}

		public override double Zero
		{
			get { return 0; }
		}
	}
}