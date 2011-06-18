using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	/// <summary>
	/// A Fraction Calculator.
	/// </summary>
	/// <typeparam name="T">The type of fraction.</typeparam>
	public class FractionCalculator<T> : Calculator<Fraction<T>>
	{
		static FractionCalculator()
		{
			if (Calculator == null) Calculator = CalculatorFactory.GetInstanceFor<T>();
		}

		private static Calculator<T> _Calculator;

		/// <summary>
		/// The Calculator.
		/// </summary>
		public static Calculator<T> Calculator
		{
			get { return _Calculator; }
			set { _Calculator = value; }
		}

		/// <summary>
		/// Adds the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The added numbers.</returns>
		public override Fraction<T> Add(Fraction<T> a, Fraction<T> b)
		{
			return a + b;
		}

		/// <summary>
		/// Subtracts the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The subtracted numbers.</returns>
		public override Fraction<T> Subtract(Fraction<T> a, Fraction<T> b)
		{
			return a - b;
		}

		/// <summary>
		/// Multiplies the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The multiplied numbers.</returns>
		public override Fraction<T> Multiply(Fraction<T> a, Fraction<T> b)
		{
			return a * b;
		}

		/// <summary>
		/// Divides the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The divided numbers.</returns>
		public override Fraction<T> Divide(Fraction<T> a, Fraction<T> b)
		{
			return a / b;
		}

		/// <summary>
		/// Calculates the remainder of the first number divided by the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The remainder of the first number divided by the second number..</returns>
		public override Fraction<T> Modulo(Fraction<T> a, Fraction<T> b)
		{
			return a % b;
		}

		/// <summary>
		/// Negates the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The negated number.</returns>
		public override Fraction<T> Negate(Fraction<T> n)
		{
			return -n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(byte n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override byte ConvertToByte(Fraction<T> n)
		{
			return Calculator.ConvertToByte(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(short n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override short ConvertToShort(Fraction<T> n)
		{
			return Calculator.ConvertToShort(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(ushort n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ushort ConvertToUShort(Fraction<T> n)
		{
			return Calculator.ConvertToUShort(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(int n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int ConvertToInt(Fraction<T> n)
		{
			return Calculator.ConvertToInt(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(uint n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override uint ConvertToUInt(Fraction<T> n)
		{
			return Calculator.ConvertToUInt(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(long n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override long ConvertToLong(Fraction<T> n)
		{
			return Calculator.ConvertToLong(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(ulong n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ulong ConvertToULong(Fraction<T> n)
		{
			return Calculator.ConvertToULong(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(float n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override float ConvertToFloat(Fraction<T> n)
		{
			return Calculator.ConvertToFloat(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(double n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double ConvertToDouble(Fraction<T> n)
		{
			return Calculator.ConvertToDouble(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(decimal n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override decimal ConvertToDecimal(Fraction<T> n)
		{
			return Calculator.ConvertToDecimal(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(BigInteger n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger ConvertToBigInteger(Fraction<T> n)
		{
			return Calculator.ConvertToBigInteger(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override Fraction<T> Convert(string n)
		{
			return Calculator.Convert(n);
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override string ConvertToString(Fraction<T> n)
		{
			return Calculator.ConvertToString(Calculator.Divide(n.Numerator, n.Denominator));
		}

		/// <summary>
		/// Calculates the ceiling of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The ceiling.</returns>
		public override Fraction<T> Ceiling(Fraction<T> n)
		{
			return n.Ceiling;
		}

		/// <summary>
		/// Calculates the floor of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The floor.</returns>
		public override Fraction<T> Floor(Fraction<T> n)
		{
			return n.Floor;
		}

		/// <summary>
		/// Raises the number to the specified power.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <param name="power">The power.</param>
		/// <returns>The number raised to the specified power.</returns>
		public override Fraction<T> Pow(Fraction<T> n, Fraction<T> power)
		{
			return n.Pow(Calculator.Divide(power.Numerator, power.Denominator));
		}

		/// <summary>
		/// Calculates the greatest common divisor of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The greatest common divisor.</returns>
		public override Fraction<T> Gcd(Fraction<T> a, Fraction<T> b)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Calculates the least common multiple of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The least common multiple.</returns>
		public override Fraction<T> Lcm(Fraction<T> a, Fraction<T> b)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Calculates the square root of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The square root.</returns>
		public override Fraction<T> Sqrt(Fraction<T> n)
		{
			return n.Sqrt();
		}

		/// <summary>
		/// Compares the first number to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override int Compare(Fraction<T> a, Fraction<T> b)
		{
			return a.CompareTo(b);
		}

		/// <summary>
		/// A one.
		/// </summary>
		public override Fraction<T> One
		{
			get { return Fraction<T>.One; }
		}

		/// <summary>
		/// A negative one.
		/// </summary>
		public override Fraction<T> NegativeOne
		{
			get { return Fraction<T>.NegativeOne; }
		}

		/// <summary>
		/// A zero.
		/// </summary>
		public override Fraction<T> Zero
		{
			get { return Fraction<T>.Zero; }
		}
	}
}