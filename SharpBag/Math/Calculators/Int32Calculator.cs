using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	/// <summary>
	/// An Int32 Calculator.
	/// </summary>
	public class Int32Calculator : Calculator<int>
	{
		/// <summary>
		/// Adds the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The added numbers.</returns>
		public override int Add(int a, int b)
		{
			return a + b;
		}

		/// <summary>
		/// Subtracts the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The subtracted numbers.</returns>
		public override int Subtract(int a, int b)
		{
			return a - b;
		}

		/// <summary>
		/// Multiplies the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The multiplied numbers.</returns>
		public override int Multiply(int a, int b)
		{
			return a * b;
		}

		/// <summary>
		/// Divides the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The divided numbers.</returns>
		public override int Divide(int a, int b)
		{
			return a / b;
		}

		/// <summary>
		/// Calculates the remainder of the first number divided by the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The remainder of the first number divided by the second number..</returns>
		public override int Modulo(int a, int b)
		{
			return a % b;
		}

		/// <summary>
		/// Negates the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The negated number.</returns>
		public override int Negate(int n)
		{
			return -n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(byte n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override byte ConvertToByte(int n)
		{
			return (byte)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(short n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override short ConvertToShort(int n)
		{
			return (short)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(ushort n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ushort ConvertToUShort(int n)
		{
			return (ushort)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int ConvertToInt(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(uint n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override uint ConvertToUInt(int n)
		{
			return (uint)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(long n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override long ConvertToLong(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(ulong n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ulong ConvertToULong(int n)
		{
			return (ulong)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(float n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override float ConvertToFloat(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(double n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double ConvertToDouble(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(decimal n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override decimal ConvertToDecimal(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(BigInteger n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger ConvertToBigInteger(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int Convert(string n)
		{
			return System.Convert.ToInt32(n);
		}

		/// <summary>
		/// Calculates the ceiling of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The ceiling.</returns>
		public override int Ceiling(int n)
		{
			return n;
		}

		/// <summary>
		/// Calculates the floor of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The floor.</returns>
		public override int Floor(int n)
		{
			return n;
		}

		/// <summary>
		/// Raises the number to the specified power.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <param name="power">The power.</param>
		/// <returns>The number raised to the specified power.</returns>
		public override int Pow(int n, int power)
		{
			return (int)System.Math.Pow(n, power);
		}

		/// <summary>
		/// Calculates the greatest common divisor of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The greatest common divisor.</returns>
		public override int Gcd(int a, int b)
		{
			return BagMath.Gcd(a, b);
		}

		/// <summary>
		/// Calculates the least common multiple of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The least common multiple.</returns>
		public override int Lcm(int a, int b)
		{
			return BagMath.Lcm(a, b);
		}

		/// <summary>
		/// Calculates the square root of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The square root.</returns>
		public override int Sqrt(int n)
		{
			return (int)System.Math.Sqrt(n);
		}

		/// <summary>
		/// Compares the first number to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override int Compare(int a, int b)
		{
			return a.CompareTo(b);
		}

		/// <summary>
		/// A one.
		/// </summary>
		public override int One
		{
			get { return 1; }
		}

		/// <summary>
		/// A negative one.
		/// </summary>
		public override int NegativeOne
		{
			get { return -1; }
		}

		/// <summary>
		/// A zero.
		/// </summary>
		public override int Zero
		{
			get { return 0; }
		}
	}
}