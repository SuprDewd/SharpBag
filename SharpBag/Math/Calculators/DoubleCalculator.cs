using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	/// <summary>
	/// A Double Calculator.
	/// </summary>
	public class DoubleCalculator : Calculator<double>
	{
		/// <summary>
		/// Adds the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The added numbers.</returns>
		public override double Add(double a, double b)
		{
			return a + b;
		}

		/// <summary>
		/// Subtracts the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The subtracted numbers.</returns>
		public override double Subtract(double a, double b)
		{
			return a - b;
		}

		/// <summary>
		/// Multiplies the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The multiplied numbers.</returns>
		public override double Multiply(double a, double b)
		{
			return a * b;
		}

		/// <summary>
		/// Divides the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The divided numbers.</returns>
		public override double Divide(double a, double b)
		{
			return a / b;
		}

		/// <summary>
		/// Calculates the remainder of the first number divided by the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The remainder of the first number divided by the second number..</returns>
		public override double Modulo(double a, double b)
		{
			return a % b;
		}

		/// <summary>
		/// Negates the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The negated number.</returns>
		public override double Negate(double n)
		{
			return -n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(byte n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override byte ConvertToByte(double n)
		{
			return (byte)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(short n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override short ConvertToShort(double n)
		{
			return (short)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(ushort n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ushort ConvertToUShort(double n)
		{
			return (ushort)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int ConvertToInt(double n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(uint n)
		{
			return (double)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override uint ConvertToUInt(double n)
		{
			return (uint)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(long n)
		{
			return (double)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override long ConvertToLong(double n)
		{
			return (long)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(ulong n)
		{
			return (double)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ulong ConvertToULong(double n)
		{
			return (ulong)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(float n)
		{
			return (double)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override float ConvertToFloat(double n)
		{
			return (float)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(double n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double ConvertToDouble(double n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(decimal n)
		{
			return (double)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override decimal ConvertToDecimal(double n)
		{
			return (decimal)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(BigInteger n)
		{
			return (double)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger ConvertToBigInteger(double n)
		{
			return (BigInteger)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double Convert(string n)
		{
			return System.Convert.ToInt32(n);
		}

		/// <summary>
		/// Calculates the ceiling of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The ceiling.</returns>
		public override double Ceiling(double n)
		{
			return n;
		}

		/// <summary>
		/// Calculates the floor of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The floor.</returns>
		public override double Floor(double n)
		{
			return n;
		}

		/// <summary>
		/// Raises the number to the specified power.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <param name="power">The power.</param>
		/// <returns>The number raised to the specified power.</returns>
		public override double Pow(double n, double power)
		{
			return System.Math.Pow(n, power);
		}

		/// <summary>
		/// Calculates the greatest common divisor of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The greatest common divisor.</returns>
		public override double Gcd(double a, double b)
		{
			return BagMath.Gcd((int)a, (int)b);
		}

		/// <summary>
		/// Calculates the least common multiple of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The least common multiple.</returns>
		public override double Lcm(double a, double b)
		{
			return BagMath.Lcm((int)a, (int)b);
		}

		/// <summary>
		/// Calculates the square root of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The square root.</returns>
		public override double Sqrt(double n)
		{
			return System.Math.Sqrt(n);
		}

		/// <summary>
		/// Compares the first number to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override int Compare(double a, double b)
		{
			return a.CompareTo(b);
		}

		/// <summary>
		/// A one.
		/// </summary>
		public override double One
		{
			get { return 1; }
		}

		/// <summary>
		/// A negative one.
		/// </summary>
		public override double NegativeOne
		{
			get { return -1; }
		}

		/// <summary>
		/// A zero.
		/// </summary>
		public override double Zero
		{
			get { return 0; }
		}
	}
}