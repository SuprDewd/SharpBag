using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math
{
	public struct BigDecimal : IComparable<BigDecimal>, IEquatable<BigDecimal>
	{
		#region Properties

		private const int Radix = 10;

		private BigInteger _Mantissa;

		private BigInteger Mantissa { get { return _Mantissa; } set { _Mantissa = value; } }

		private int _Exponent;

		private int Exponent { get { return _Exponent; } set { _Exponent = value; } }

		#endregion Properties

		#region Constructors / Factories

		public BigDecimal(int value)
		{
			_Mantissa = value;
			_Exponent = 0;
			this.Normalize();
		}

		public BigDecimal(long value)
		{
			_Mantissa = value;
			_Exponent = 0;
			this.Normalize();
		}

		public BigDecimal(BigInteger value)
		{
			_Mantissa = value;
			_Exponent = 0;
			this.Normalize();
		}

		public BigDecimal(double value)
		{
			long floor = (long)value;
			_Mantissa = floor;
			_Exponent = 0;

			value -= floor;
			string s = value.ToString("0.00000000000000000000000");

			for (int i = 2; i < s.Length; i++)
			{
				this.Exponent--;
				this.Mantissa *= 10;
				this.Mantissa += s[i] - '0';
			}

			this.Normalize();
		}

		public BigDecimal(BigDecimal value)
		{
			_Mantissa = value.Mantissa;
			_Exponent = value.Exponent;
		}

		private BigDecimal(BigInteger value, int exponent)
		{
			_Mantissa = value;
			_Exponent = exponent;
			this.Normalize();
		}

		public static BigDecimal Parse(string value)
		{
			int exp = 0;
			BigInteger mantissa = 0;
			bool foundComma = false;

			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '.' || value[i] == ',')
				{
					if (foundComma) throw new FormatException();
					foundComma = true;
					continue;
				}

				if (!Char.IsDigit(value[i])) throw new FormatException();
				if (foundComma) exp--;
				mantissa *= 10;
				mantissa += value[i] - '0';
			}

			return new BigDecimal(mantissa, exp);
		}

		#endregion Constructors / Factories

		#region Operators

		public static BigDecimal operator +(BigDecimal left, BigDecimal right)
		{
			left.NormalizeTo(right);
			right.NormalizeTo(left);

			BigDecimal result = new BigDecimal(left.Mantissa + right.Mantissa, left.Exponent);

			left.Normalize();
			right.Normalize();

			return result;
		}

		public static BigDecimal operator -(BigDecimal left, BigDecimal right)
		{
			left.NormalizeTo(right);
			right.NormalizeTo(left);

			BigDecimal result = new BigDecimal(left.Mantissa - right.Mantissa, left.Exponent);

			left.Normalize();
			right.Normalize();

			return result;
		}

		public static BigDecimal operator *(BigDecimal left, BigDecimal right)
		{
			left.NormalizeTo(right);
			right.NormalizeTo(left);

			int z = left.Exponent + right.Exponent;
			BigInteger m = left.Mantissa * right.Mantissa;

			left.Normalize();
			right.Normalize();

			return new BigDecimal(m, z);
		}

		public static BigDecimal operator /(BigDecimal left, BigDecimal right)
		{
			left.NormalizeTo(right);
			right.NormalizeTo(left);

			int exponent = left.Exponent - right.Exponent;

			BigInteger remainder,
					   mantissa = BigInteger.DivRem(left.Mantissa, right.Mantissa, out remainder);

			int iterations = 200;
			while (remainder > 0 && iterations > 0)
			{
				exponent--;
				mantissa *= 10;
				remainder *= 10;
				mantissa += BigInteger.DivRem(remainder, right.Mantissa, out remainder);
				iterations--;
			}

			return new BigDecimal(mantissa, exponent);
		}

		public static BigDecimal Pow(BigDecimal value, int power)
		{
			if (power == 0) return new BigDecimal(1, 0);
			if (power == 1) return new BigDecimal(value);
			if (power == 2) return value * value;
			if (power == 3) return value * value * value;

			if (power % 2 == 0)
			{
				BigDecimal temp = BigDecimal.Pow(value, power / 2);
				return temp * temp;
			}
			else
			{
				BigDecimal temp = BigDecimal.Pow(value, (power - 1) / 2);
				return temp * temp * value;
			}
		}

		public static BigDecimal Log10(BigDecimal value)
		{
			int a = value.Mantissa.ToString().Length + value.Exponent - 1;
			BigDecimal m = value;
			int iterations = 10;
			BigInteger mantissa = a;
			int exponent = 0;

			for (int i = 0; i < iterations; i++)
			{
				m = BigDecimal.Pow(new BigDecimal(m) / BigInteger.Pow(10, a), 10);
				a = m.Mantissa.ToString().Length + m.Exponent - 1;
				exponent--;
				mantissa = mantissa * 10 + a;
			}

			return new BigDecimal(mantissa, exponent);
		}

		public static BigDecimal Log(BigDecimal value, BigDecimal logBase)
		{
			return BigDecimal.Log10(value) / BigDecimal.Log10(logBase);
		}

		#endregion Operators

		#region Ordering

		public static bool operator >(BigDecimal left, BigDecimal right) { return left.CompareTo(right) > 0; }

		public static bool operator >=(BigDecimal left, BigDecimal right) { return left.CompareTo(right) >= 0; }

		public static bool operator <(BigDecimal left, BigDecimal right) { return left.CompareTo(right) < 0; }

		public static bool operator <=(BigDecimal left, BigDecimal right) { return left.CompareTo(right) <= 0; }

		public static bool operator ==(BigDecimal left, BigDecimal right) { return left.Equals(right); }

		public static bool operator !=(BigDecimal left, BigDecimal right) { return !left.Equals(right); }

		public bool Equals(BigDecimal other)
		{
			this.NormalizeTo(other);
			other.NormalizeTo(this);

			bool eq = this.Mantissa.Equals(other.Mantissa);

			this.Normalize();
			other.Normalize();

			return eq;
		}

		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(BigDecimal) && this.Equals((BigDecimal)obj);
		}

		public int CompareTo(BigDecimal other)
		{
			this.NormalizeTo(other);
			other.NormalizeTo(this);

			int cmp = this.Mantissa.CompareTo(other.Mantissa);

			this.Normalize();
			other.Normalize();

			return cmp;
		}

		#endregion Ordering

		#region Casting

		public static implicit operator BigDecimal(int n) { return new BigDecimal(n); }

		public static implicit operator BigDecimal(long n) { return new BigDecimal(n); }

		public static implicit operator BigDecimal(BigInteger n) { return new BigDecimal(n); }

		#endregion Casting

		private void Normalize()
		{
			if (this.Mantissa == 0)
			{
				this.Exponent = 0;
			}
			else while (this.Mantissa % Radix == 0)
				{
					this.Exponent++;
					this.Mantissa /= Radix;
				}
		}

		private void NormalizeTo(BigDecimal other)
		{
			while (this.Exponent > other.Exponent)
			{
				this.Exponent--;
				this.Mantissa *= Radix;
			}
		}

		public override string ToString()
		{
			bool positive = this.Mantissa >= 0;
			StringBuilder sb = new StringBuilder((positive ? this.Mantissa : -this.Mantissa).ToString());

			int exp = this.Exponent,
				index = sb.Length + exp;

			if (index > sb.Length)
			{
				sb.Append('0', index - sb.Length);
			}
			else if (index <= 0)
			{
				sb.Insert(0, "0", -index + 1);
				index = 1;
			}

			bool comma = false;
			if (index < sb.Length)
			{
				sb.Insert(index, '.');
				comma = true;
			}

			if (!positive) sb.Insert(0, '-');
			string res = sb.ToString();
			if (comma) res = res.TrimEnd('0');
			if (res[res.Length - 1] == '.') res = res.Substring(0, res.Length - 1);
			return res;
		}
	}
}