using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	/// <summary>
	/// A calculator with all operations checked for overflow.
	/// </summary>
	/// <typeparam name="T">The type to calculate.</typeparam>
	public class CheckedCalculator<T> : Calculator<T>
	{
		/// <summary>
		/// The unchecked calculator.
		/// </summary>
		public Calculator<T> Calculator { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="calculator">The calculator to use.</param>
		public CheckedCalculator(Calculator<T> calculator)
		{
			this.Calculator = calculator;
		}

		/// <summary>
		/// Adds the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The added numbers.</returns>
		public override T Add(T a, T b)
		{
			checked
			{
				return this.Calculator.Add(a, b);
			}
		}

		/// <summary>
		/// Subtracts the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The subtracted numbers.</returns>
		public override T Subtract(T a, T b)
		{
			checked
			{
				return this.Calculator.Subtract(a, b);
			}
		}

		/// <summary>
		/// Multiplies the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The multiplied numbers.</returns>
		public override T Multiply(T a, T b)
		{
			checked
			{
				return this.Calculator.Multiply(a, b);
			}
		}

		/// <summary>
		/// Divides the specified numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The divided numbers.</returns>
		public override T Divide(T a, T b)
		{
			checked
			{
				return this.Calculator.Divide(a, b);
			}
		}

		/// <summary>
		/// Calculates the remainder of the first number divided by the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The remainder of the first number divided by the second number..</returns>
		public override T Modulo(T a, T b)
		{
			checked
			{
				return this.Calculator.Modulo(a, b);
			}
		}

		/// <summary>
		/// Negates the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The negated number.</returns>
		public override T Negate(T n)
		{
			checked
			{
				return this.Calculator.Negate(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(byte n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override byte ConvertToByte(T n)
		{
			checked
			{
				return this.Calculator.ConvertToByte(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(short n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override short ConvertToShort(T n)
		{
			checked
			{
				return this.Calculator.ConvertToShort(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(ushort n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ushort ConvertToUShort(T n)
		{
			checked
			{
				return this.Calculator.ConvertToUShort(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(int n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override int ConvertToInt(T n)
		{
			checked
			{
				return this.Calculator.ConvertToInt(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(uint n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override uint ConvertToUInt(T n)
		{
			checked
			{
				return this.Calculator.ConvertToUInt(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(long n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override long ConvertToLong(T n)
		{
			checked
			{
				return this.Calculator.ConvertToLong(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(ulong n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override ulong ConvertToULong(T n)
		{
			checked
			{
				return this.Calculator.ConvertToULong(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(float n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override float ConvertToFloat(T n)
		{
			checked
			{
				return this.Calculator.ConvertToFloat(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(double n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override double ConvertToDouble(T n)
		{
			checked
			{
				return this.Calculator.ConvertToDouble(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(decimal n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override decimal ConvertToDecimal(T n)
		{
			checked
			{
				return this.Calculator.ConvertToDecimal(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(BigInteger n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override BigInteger ConvertToBigInteger(T n)
		{
			checked
			{
				return this.Calculator.ConvertToBigInteger(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override T Convert(string n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		/// <summary>
		/// Converts the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The converted number.</returns>
		public override string ConvertToString(T n)
		{
			return this.Calculator.ConvertToString(n);
		}

		/// <summary>
		/// Compares the first number to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override int Compare(T a, T b)
		{
			checked
			{
				return this.Calculator.Compare(a, b);
			}
		}

		/// <summary>
		/// Checks whether the first number is equal to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override bool Equal(T a, T b)
		{
			return this.Calculator.Equal(a, b);
		}

		/// <summary>
		/// Checks whether the first number is greater than the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override bool GreaterThan(T a, T b)
		{
			return this.Calculator.GreaterThan(a, b);
		}

		/// <summary>
		/// Checks whether the first number is greater than or equal to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override bool GreaterThanOrEqual(T a, T b)
		{
			return this.Calculator.GreaterThanOrEqual(a, b);
		}

		/// <summary>
		/// Checks whether the first number is less than the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override bool LessThan(T a, T b)
		{
			return this.Calculator.LessThan(a, b);
		}

		/// <summary>
		/// Checks whether the first number is less than or equal to the second number.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The result.</returns>
		public override bool LessThanOrEqual(T a, T b)
		{
			return this.Calculator.LessThanOrEqual(a, b);
		}

		/// <summary>
		/// Calculates the ceiling of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The ceiling.</returns>
		public override T Ceiling(T n)
		{
			checked
			{
				return this.Calculator.Ceiling(n);
			}
		}

		/// <summary>
		/// Calculates the floor of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The floor.</returns>
		public override T Floor(T n)
		{
			checked
			{
				return this.Calculator.Floor(n);
			}
		}

		/// <summary>
		/// Raises the number to the specified power.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <param name="power">The power.</param>
		/// <returns>The number raised to the specified power.</returns>
		public override T Pow(T n, T power)
		{
			checked
			{
				return this.Calculator.Pow(n, power);
			}
		}

		/// <summary>
		/// Calculates the greatest common divisor of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The greatest common divisor.</returns>
		public override T Gcd(T a, T b)
		{
			checked
			{
				return this.Calculator.Gcd(a, b);
			}
		}

		/// <summary>
		/// Calculates the least common multiple of the numbers.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The least common multiple.</returns>
		public override T Lcm(T a, T b)
		{
			checked
			{
				return this.Calculator.Lcm(a, b);
			}
		}

		/// <summary>
		/// Calculates the square root of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The square root.</returns>
		public override T Sqrt(T n)
		{
			checked
			{
				return this.Calculator.Sqrt(n);
			}
		}

		/// <summary>
		/// Calculates the absolute value of the number.
		/// </summary>
		/// <param name="n">The number.</param>
		/// <returns>The absolute value of the number.</returns>
		public override T Abs(T n)
		{
			checked
			{
				return this.Calculator.Abs(n);
			}
		}

		/// <summary>
		/// A one.
		/// </summary>
		public override T One
		{
			get { return this.Calculator.One; }
		}

		/// <summary>
		/// A negative one.
		/// </summary>
		public override T NegativeOne
		{
			get { return this.Calculator.NegativeOne; }
		}

		/// <summary>
		/// A zero.
		/// </summary>
		public override T Zero
		{
			get { return this.Calculator.Zero; }
		}
	}
}