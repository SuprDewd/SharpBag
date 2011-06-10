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

		public override T Add(T a, T b)
		{
			checked
			{
				return this.Calculator.Add(a, b);
			}
		}

		public override T Subtract(T a, T b)
		{
			checked
			{
				return this.Calculator.Subtract(a, b);
			}
		}

		public override T Multiply(T a, T b)
		{
			checked
			{
				return this.Calculator.Multiply(a, b);
			}
		}

		public override T Divide(T a, T b)
		{
			checked
			{
				return this.Calculator.Divide(a, b);
			}
		}

		public override T Modulo(T a, T b)
		{
			checked
			{
				return this.Calculator.Modulo(a, b);
			}
		}

		public override T Negate(T n)
		{
			checked
			{
				return this.Calculator.Negate(n);
			}
		}

		public override T Convert(byte n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override byte ConvertToByte(T n)
		{
			checked
			{
				return this.Calculator.ConvertToByte(n);
			}
		}

		public override T Convert(short n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override short ConvertToShort(T n)
		{
			checked
			{
				return this.Calculator.ConvertToShort(n);
			}
		}

		public override T Convert(ushort n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override ushort ConvertToUShort(T n)
		{
			checked
			{
				return this.Calculator.ConvertToUShort(n);
			}
		}

		public override T Convert(int n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override int ConvertToInt(T n)
		{
			checked
			{
				return this.Calculator.ConvertToInt(n);
			}
		}

		public override T Convert(uint n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override uint ConvertToUInt(T n)
		{
			checked
			{
				return this.Calculator.ConvertToUInt(n);
			}
		}

		public override T Convert(long n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override long ConvertToLong(T n)
		{
			checked
			{
				return this.Calculator.ConvertToLong(n);
			}
		}

		public override T Convert(ulong n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override ulong ConvertToULong(T n)
		{
			checked
			{
				return this.Calculator.ConvertToULong(n);
			}
		}

		public override T Convert(float n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override float ConvertToFloat(T n)
		{
			checked
			{
				return this.Calculator.ConvertToFloat(n);
			}
		}

		public override T Convert(double n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override double ConvertToDouble(T n)
		{
			checked
			{
				return this.Calculator.ConvertToDouble(n);
			}
		}

		public override T Convert(decimal n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override decimal ConvertToDecimal(T n)
		{
			checked
			{
				return this.Calculator.ConvertToDecimal(n);
			}
		}

		public override T Convert(BigInteger n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override BigInteger ConvertToBigInteger(T n)
		{
			checked
			{
				return this.Calculator.ConvertToBigInteger(n);
			}
		}

		public override T Convert(string n)
		{
			checked
			{
				return this.Calculator.Convert(n);
			}
		}

		public override string ConvertToString(T n)
		{
			return this.Calculator.ConvertToString(n);
		}

		public override int Compare(T a, T b)
		{
			checked
			{
				return this.Calculator.Compare(a, b);
			}
		}

		public override bool Equal(T a, T b)
		{
			return this.Calculator.Equal(a, b);
		}

		public override bool GreaterThan(T a, T b)
		{
			return this.Calculator.GreaterThan(a, b);
		}

		public override bool GreaterThanOrEqual(T a, T b)
		{
			return this.Calculator.GreaterThanOrEqual(a, b);
		}

		public override bool LessThan(T a, T b)
		{
			return this.Calculator.LessThan(a, b);
		}

		public override bool LessThanOrEqual(T a, T b)
		{
			return this.Calculator.LessThanOrEqual(a, b);
		}

		public override T Ceiling(T n)
		{
			checked
			{
				return this.Calculator.Ceiling(n);
			}
		}

		public override T Floor(T n)
		{
			checked
			{
				return this.Calculator.Floor(n);
			}
		}

		public override T Pow(T n, T power)
		{
			checked
			{
				return this.Calculator.Pow(n, power);
			}
		}

		public override T Gcd(T a, T b)
		{
			checked
			{
				return this.Calculator.Gcd(a, b);
			}
		}

		public override T Lcm(T a, T b)
		{
			checked
			{
				return this.Calculator.Lcm(a, b);
			}
		}

		public override T Sqrt(T n)
		{
			checked
			{
				return this.Calculator.Sqrt(n);
			}
		}

		public override T One
		{
			get { return this.Calculator.One; }
		}

		public override T NegativeOne
		{
			get { return this.Calculator.NegativeOne; }
		}

		public override T Zero
		{
			get { return this.Calculator.Zero; }
		}
	}
}