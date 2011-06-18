using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	/// <summary>
	/// A BigInteger calculator.
	/// </summary>
	public class BigIntegerCalculator : Calculator<BigInteger>
	{
		/// <summary>
		/// Adds the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The added numbers.</returns>
		public override BigInteger Add(BigInteger a, BigInteger b)
		{
			return a + b;
		}

		/// <summary>
		/// Subtracts the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The subtracted numbers.</returns>
		public override BigInteger Subtract(BigInteger a, BigInteger b)
		{
			return a - b;
		}

		/// <summary>
		/// Multiplies the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The multiplied numbers.</returns>
		public override BigInteger Multiply(BigInteger a, BigInteger b)
		{
			return a * b;
		}

		/// <summary>
		/// Divides the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The divided numbers.</returns>
		public override BigInteger Divide(BigInteger a, BigInteger b)
		{
			return a / b;
		}

		/// <summary>
		/// Calculates the remainder of the first number divided by the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The remainder of the first number divided by the second number..</returns>
		public override BigInteger Modulo(BigInteger a, BigInteger b)
		{
			return a % b;
		}

		/// <summary>
		/// Negates the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The negated number.</returns>
		public override BigInteger Negate(BigInteger n)
		{
			return -n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(byte n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override byte ConvertToByte(BigInteger n)
		{
			return (byte)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(short n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override short ConvertToShort(BigInteger n)
		{
			return (short)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(ushort n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ushort ConvertToUShort(BigInteger n)
		{
			return (ushort)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(int n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int ConvertToInt(BigInteger n)
		{
			return (int)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(uint n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override uint ConvertToUInt(BigInteger n)
		{
			return (uint)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(long n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override long ConvertToLong(BigInteger n)
		{
			return (long)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(ulong n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ulong ConvertToULong(BigInteger n)
		{
			return (ulong)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(float n)
		{
			return (BigInteger)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override float ConvertToFloat(BigInteger n)
		{
			return (float)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(double n)
		{
			return (BigInteger)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double ConvertToDouble(BigInteger n)
		{
			return (double)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(decimal n)
		{
			return (BigInteger)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override decimal ConvertToDecimal(BigInteger n)
		{
			return (decimal)n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(BigInteger n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger ConvertToBigInteger(BigInteger n)
		{
			return n;
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger Convert(string n)
		{
			return BigInteger.Parse(n);
		}

		/// <summary>
		/// Calculates the ceiling of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The ceiling.</returns>
		public override BigInteger Ceiling(BigInteger n)
		{
			return n;
		}

		/// <summary>
		/// Calculates the floor of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The floor.</returns>
		public override BigInteger Floor(BigInteger n)
		{
			return n;
		}

		/// <summary>
		/// Raises the number to the specified power.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <param name="power">The power.</param>
		/// <returns>The number raised to the specified power.</returns>
		public override BigInteger Pow(BigInteger n, BigInteger power)
		{
			return BigInteger.Pow(n, (int)power);
		}

		/// <summary>
		/// Calculates the greatest common divisor of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The greatest common divisor.</returns>
		public override BigInteger Gcd(BigInteger a, BigInteger b)
		{
			return BagMath.Gcd(a, b);
		}

		/// <summary>
		/// Calculates the least common multiple of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The least common multiple.</returns>
		public override BigInteger Lcm(BigInteger a, BigInteger b)
		{
			return BagMath.Lcm(a, b);
		}

		/// <summary>
		/// Calculates the square root of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The square root.</returns>
		public override BigInteger Sqrt(BigInteger n)
		{
			return n.Sqrt();
		}

		/// <summary>
		/// Compares the first number to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override int Compare(BigInteger a, BigInteger b)
		{
			return a.CompareTo(b);
		}

		/// <summary>
		/// A one.
		/// </summary>
		public override BigInteger One
		{
			get { return BigInteger.One; }
		}

		/// <summary>
		/// A negative one.
		/// </summary>
		public override BigInteger NegativeOne
		{
			get { return BigInteger.MinusOne; }
		}

		/// <summary>
		/// A zero.
		/// </summary>
		public override BigInteger Zero
		{
			get { return BigInteger.Zero; }
		}
	}
}