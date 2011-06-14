using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	/// <summary>
	/// A generic calculator.
	/// </summary>
	/// <typeparam name="T">The type to calculate.</typeparam>
	public abstract class Calculator<T>
	{
		#region Operations

		/// <summary>
		/// Adds the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The added numbers.</returns>
		public abstract T Add(T a, T b);

		/// <summary>
		/// Subtracts the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The subtracted numbers.</returns>
		public virtual T Subtract(T a, T b)
		{
			return Add(a, Negate(b));
		}

		/// <summary>
		/// Multiplies the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The multiplied numbers.</returns>
		public abstract T Multiply(T a, T b);

		/// <summary>
		/// Divides the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The divided numbers.</returns>
		public abstract T Divide(T a, T b);

		/// <summary>
		/// Calculates the remainder of the first number divided by the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The remainder of the first number divided by the second number..</returns>
		public abstract T Modulo(T a, T b);

		/// <summary>
		/// Negates the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The negated number.</returns>
		public abstract T Negate(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(byte n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract byte ConvertToByte(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(short n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract short ConvertToShort(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(ushort n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract ushort ConvertToUShort(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(int n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract int ConvertToInt(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(uint n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract uint ConvertToUInt(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(long n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract long ConvertToLong(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(ulong n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract ulong ConvertToULong(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(float n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract float ConvertToFloat(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(double n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract double ConvertToDouble(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(decimal n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract decimal ConvertToDecimal(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(BigInteger n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract BigInteger ConvertToBigInteger(T n);

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public abstract T Convert(string n);

		/// <summary>
		/// Converts the number to string.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The string.</returns>
		public virtual string ConvertToString(T n)
		{
			return n.ToString();
		}

		#endregion Operations

		#region Functions

		/// <summary>
		/// Calculates the ceiling of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The ceiling.</returns>
		public abstract T Ceiling(T n);

		/// <summary>
		/// Calculates the floor of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The floor.</returns>
		public abstract T Floor(T n);

		/// <summary>
		/// Raises the number to the specified power.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <param name="power">The power.</param>
		/// <returns>The number raised to the specified power.</returns>
		public abstract T Pow(T n, T power);

		/// <summary>
		/// Calculates the greatest common divisor of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The greatest common divisor.</returns>
		public abstract T Gcd(T a, T b);

		/// <summary>
		/// Calculates the least common multiple of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The least common multiple.</returns>
		public abstract T Lcm(T a, T b);

		/// <summary>
		/// Calculates the square root of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The square root.</returns>
		public abstract T Sqrt(T n);

		/// <summary>
		/// Calculates the absolute value of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The absolute value of the number.</returns>
		public virtual T Abs(T n)
		{
			if (this.GreaterThanOrEqual(n, this.Zero)) return n;
			return this.Negate(n);
		}

		#endregion Functions

		#region Comparisons

		/// <summary>
		/// Checks whether the first number is greater than the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public virtual bool GreaterThan(T a, T b)
		{
			return Compare(a, b) > 0;
		}

		/// <summary>
		/// Checks whether the first number is greater than or equal to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public virtual bool GreaterThanOrEqual(T a, T b)
		{
			return Compare(a, b) >= 0;
		}

		/// <summary>
		/// Checks whether the first number is less than the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public virtual bool LessThan(T a, T b)
		{
			return Compare(a, b) < 0;
		}

		/// <summary>
		/// Checks whether the first number is less than or equal to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public virtual bool LessThanOrEqual(T a, T b)
		{
			return Compare(a, b) <= 0;
		}

		/// <summary>
		/// Checks whether the first number is equal to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public virtual bool Equal(T a, T b)
		{
			return Compare(a, b) == 0;
		}

		/// <summary>
		/// Compares the first number to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public abstract int Compare(T a, T b);

		#endregion Comparisons

		#region Constants

		/// <summary>
		/// A one.
		/// </summary>
		public abstract T One { get; }

		/// <summary>
		/// A negative one.
		/// </summary>
		public abstract T NegativeOne { get; }

		/// <summary>
		/// A zero.
		/// </summary>
		public abstract T Zero { get; }

		#endregion Constants
	}
}