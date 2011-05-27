using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math
{
	public abstract class Calculator<T>
	{
		#region Operations

		public abstract T Add(T a, T b);

		public virtual T Subtract(T a, T b)
		{
			return Add(a, Negate(b));
		}

		public abstract T Multiply(T a, T b);

		public abstract T Divide(T a, T b);

		public abstract T Modulo(T a, T b);

		public abstract T Negate(T n);

		public abstract T Convert(byte n);

		public abstract byte ConvertToByte(T n);

		public abstract T Convert(short n);

		public abstract short ConvertToShort(T n);

		public abstract T Convert(ushort n);

		public abstract ushort ConvertToUShort(T n);

		public abstract T Convert(int n);

		public abstract int ConvertToInt(T n);

		public abstract T Convert(uint n);

		public abstract uint ConvertToUInt(T n);

		public abstract T Convert(long n);

		public abstract long ConvertToLong(T n);

		public abstract T Convert(ulong n);

		public abstract ulong ConvertToULong(T n);

		public abstract T Convert(float n);

		public abstract float ConvertToFloat(T n);

		public abstract T Convert(double n);

		public abstract double ConvertToDouble(T n);

		public abstract T Convert(decimal n);

		public abstract decimal ConvertToDecimal(T n);

		public abstract T Convert(BigInteger n);

		public abstract BigInteger ConvertToBigInteger(T n);

		public abstract T Convert(string n);

		public virtual string ConvertToString(T n)
		{
			return n.ToString();
		}

		#endregion Operations

		#region Functions

		public abstract T Ceiling(T n);

		public abstract T Floor(T n);

		public abstract T Pow(T n, T power);

		public abstract T Gcd(T a, T b);

		public abstract T Lcm(T a, T b);

		#endregion Functions

		#region Comparisons

		public virtual bool GreaterThan(T a, T b)
		{
			return Compare(a, b) > 0;
		}

		public virtual bool GreaterThanOrEqual(T a, T b)
		{
			return Compare(a, b) >= 0;
		}

		public virtual bool LessThan(T a, T b)
		{
			return Compare(a, b) < 0;
		}

		public virtual bool LessThanOrEqual(T a, T b)
		{
			return Compare(a, b) <= 0;
		}

		public virtual bool Equal(T a, T b)
		{
			return Compare(a, b) == 0;
		}

		public abstract int Compare(T a, T b);

		#endregion Comparisons

		#region Constants

		public abstract T One { get; }

		public abstract T NegativeOne { get; }

		public abstract T Zero { get; }

		#endregion Constants
	}
}