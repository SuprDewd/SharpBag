using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	public class FractionCalculator<T> : Calculator<Fraction<T>>
	{
		public override Fraction<T> Add(Fraction<T> a, Fraction<T> b)
		{
			return a + b;
		}

		public override Fraction<T> Multiply(Fraction<T> a, Fraction<T> b)
		{
			return a * b;
		}

		public override Fraction<T> Divide(Fraction<T> a, Fraction<T> b)
		{
			return a / b;
		}

		public override Fraction<T> Modulo(Fraction<T> a, Fraction<T> b)
		{
			return a % b;
		}

		public override Fraction<T> Negate(Fraction<T> n)
		{
			return -n;
		}

		public override Fraction<T> Convert(byte n)
		{
			throw new NotImplementedException();
		}

		public override byte ConvertToByte(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(short n)
		{
			throw new NotImplementedException();
		}

		public override short ConvertToShort(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(ushort n)
		{
			throw new NotImplementedException();
		}

		public override ushort ConvertToUShort(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(int n)
		{
			throw new NotImplementedException();
		}

		public override int ConvertToInt(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(uint n)
		{
			throw new NotImplementedException();
		}

		public override uint ConvertToUInt(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(long n)
		{
			throw new NotImplementedException();
		}

		public override long ConvertToLong(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(ulong n)
		{
			throw new NotImplementedException();
		}

		public override ulong ConvertToULong(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(float n)
		{
			throw new NotImplementedException();
		}

		public override float ConvertToFloat(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(double n)
		{
			throw new NotImplementedException();
		}

		public override double ConvertToDouble(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(decimal n)
		{
			throw new NotImplementedException();
		}

		public override decimal ConvertToDecimal(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(BigInteger n)
		{
			throw new NotImplementedException();
		}

		public override BigInteger ConvertToBigInteger(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Convert(string n)
		{
			throw new NotImplementedException();
		}

		public override string ConvertToString(Fraction<T> n)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Ceiling(Fraction<T> n)
		{
			return n.Ceiling;
		}

		public override Fraction<T> Floor(Fraction<T> n)
		{
			return n.Floor;
		}

		public override Fraction<T> Pow(Fraction<T> n, Fraction<T> power)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Gcd(Fraction<T> a, Fraction<T> b)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Lcm(Fraction<T> a, Fraction<T> b)
		{
			throw new NotImplementedException();
		}

		public override int Compare(Fraction<T> a, Fraction<T> b)
		{
			return a.CompareTo(b);
		}

		public override Fraction<T> One
		{
			get { return Fraction<T>.One; }
		}

		public override Fraction<T> NegativeOne
		{
			get { return Fraction<T>.NegativeOne; }
		}

		public override Fraction<T> Zero
		{
			get { return Fraction<T>.Zero; }
		}
	}
}