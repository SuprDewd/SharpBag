using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	public class FractionCalculator<T> : Calculator<Fraction<T>>
	{
		static FractionCalculator()
		{
			if (Calculator == null) Calculator = CalculatorFactory.GetInstanceFor<T>();
		}

		private static Calculator<T> _Calculator;

		public static Calculator<T> Calculator
		{
			get { return _Calculator; }
			set { _Calculator = value; }
		}

		public override Fraction<T> Add(Fraction<T> a, Fraction<T> b)
		{
			return a + b;
		}

		public override Fraction<T> Subtract(Fraction<T> a, Fraction<T> b)
		{
			return a - b;
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
			return Calculator.Convert(n);
		}

		public override byte ConvertToByte(Fraction<T> n)
		{
			return Calculator.ConvertToByte(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(short n)
		{
			return Calculator.Convert(n);
		}

		public override short ConvertToShort(Fraction<T> n)
		{
			return Calculator.ConvertToShort(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(ushort n)
		{
			return Calculator.Convert(n);
		}

		public override ushort ConvertToUShort(Fraction<T> n)
		{
			return Calculator.ConvertToUShort(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(int n)
		{
			return Calculator.Convert(n);
		}

		public override int ConvertToInt(Fraction<T> n)
		{
			return Calculator.ConvertToInt(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(uint n)
		{
			return Calculator.Convert(n);
		}

		public override uint ConvertToUInt(Fraction<T> n)
		{
			return Calculator.ConvertToUInt(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(long n)
		{
			return Calculator.Convert(n);
		}

		public override long ConvertToLong(Fraction<T> n)
		{
			return Calculator.ConvertToLong(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(ulong n)
		{
			return Calculator.Convert(n);
		}

		public override ulong ConvertToULong(Fraction<T> n)
		{
			return Calculator.ConvertToULong(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(float n)
		{
			return Calculator.Convert(n);
		}

		public override float ConvertToFloat(Fraction<T> n)
		{
			return Calculator.ConvertToFloat(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(double n)
		{
			return Calculator.Convert(n);
		}

		public override double ConvertToDouble(Fraction<T> n)
		{
			return Calculator.ConvertToDouble(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(decimal n)
		{
			return Calculator.Convert(n);
		}

		public override decimal ConvertToDecimal(Fraction<T> n)
		{
			return Calculator.ConvertToDecimal(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(BigInteger n)
		{
			return Calculator.Convert(n);
		}

		public override BigInteger ConvertToBigInteger(Fraction<T> n)
		{
			return Calculator.ConvertToBigInteger(Calculator.Divide(n.Numerator, n.Denominator));
		}

		public override Fraction<T> Convert(string n)
		{
			return Calculator.Convert(n);
		}

		public override string ConvertToString(Fraction<T> n)
		{
			return Calculator.ConvertToString(Calculator.Divide(n.Numerator, n.Denominator));
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
			return n.Pow(Calculator.Divide(power.Numerator, power.Denominator));
		}

		public override Fraction<T> Gcd(Fraction<T> a, Fraction<T> b)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Lcm(Fraction<T> a, Fraction<T> b)
		{
			throw new NotImplementedException();
		}

		public override Fraction<T> Sqrt(Fraction<T> n)
		{
			return n.Sqrt();
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