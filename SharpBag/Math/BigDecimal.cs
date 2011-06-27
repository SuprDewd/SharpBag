using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math
{
	public struct BigDecimal : IComparable<BigDecimal>, IEquatable<BigDecimal>
	{
		#region Properties

		private const int DefaultPrecision = 30;

		private const int Radix = 10;

		private const int RadixPow2 = 100;

		private int _Precision;

		public int Precision
		{
			get { return _Precision; }
			private set
			{
				bool fix = value < _Precision;
				_Precision = value;
				if (fix) this.FixPrecision();
			}
		}

		private BigInteger _Mantissa;

		private BigInteger Mantissa { get { return _Mantissa; } set { _Mantissa = value; } }

		private int _Exponent;

		private int Exponent { get { return _Exponent; } set { _Exponent = value; } }

		#endregion Properties

		#region Static Instances

		public static readonly BigDecimal PositiveOne = new BigDecimal(1);
		public static readonly BigDecimal NegativeOne = new BigDecimal(-1);
		public static readonly BigDecimal Zero = new BigDecimal(0);

		#endregion Static Instances

		#region Constructors / Factories

		public BigDecimal(int value) : this(value, 0, DefaultPrecision) { }

		public BigDecimal(int value, int precision) : this(value, 0, precision) { }

		public BigDecimal(long value) : this(value, 0, DefaultPrecision) { }

		public BigDecimal(long value, int precision) : this(value, 0, precision) { }

		public BigDecimal(BigInteger value, int precision) : this(value, 0, precision) { }

		public BigDecimal(BigInteger value) : this(value, 0, DefaultPrecision) { }

		public BigDecimal(float value) : this(value, DefaultPrecision) { }

		public BigDecimal(float value, int precision)
		{
#if DOTNET4
			Contract.Requires(precision >= 0);
#endif
			string[] srep = value.ToString("R").Split('E');
			BigDecimal parsed = BigDecimal.Parse(srep[0]);

			_ToStringCache = null;
			_Mantissa = parsed.Mantissa;
			_Exponent = srep.Length == 2 ? (parsed.Exponent + Convert.ToInt32(srep[1])) : parsed.Exponent;
			_Precision = precision;

			this.FixPrecision();
			this.Normalize();
		}

		public BigDecimal(double value) : this(value, DefaultPrecision) { }

		public BigDecimal(double value, int precision)
		{
#if DOTNET4
			Contract.Requires(precision >= 0);
#endif
			string[] srep = value.ToString("R").Split('E');
			BigDecimal parsed = BigDecimal.Parse(srep[0]);

			_ToStringCache = null;
			_Mantissa = parsed.Mantissa;
			_Exponent = srep.Length == 2 ? (parsed.Exponent + Convert.ToInt32(srep[1])) : parsed.Exponent;
			_Precision = precision;

			this.FixPrecision();
			this.Normalize();
		}

		public BigDecimal(BigDecimal value) : this(value.Mantissa, value.Exponent, value.Precision) { }

		public BigDecimal(BigDecimal value, int precision) : this(value.Mantissa, value.Exponent, precision) { }

		private BigDecimal(BigInteger value, int exponent, int precision)
		{
#if DOTNET4
			Contract.Requires(precision >= 0);
#endif
			_ToStringCache = null;
			_Mantissa = value;
			_Exponent = exponent;
			_Precision = precision;

			this.FixPrecision();
			this.Normalize();
		}

		public static BigDecimal Parse(string value)
		{
			BigDecimal parsed = BigDecimal.Parse(value, Int32.MaxValue);
			int precision = parsed.Precision;
			if (parsed.Exponent < 0) precision = -parsed.Exponent;
			if (precision < DefaultPrecision) precision = DefaultPrecision;
			if (precision != parsed.Precision) parsed.Precision = precision;
			return parsed;
		}

		public static BigDecimal Parse(string value, int precision)
		{
#if DOTNET4
			Contract.Requires(precision >= 0);
#endif
			value = value.Trim();
			int exp = 0;
			BigInteger mantissa = 0;
			bool foundComma = false;
			int i = 0;
			bool neg = false;
			if (value[0] == '-')
			{
				neg = true;
				i++;
			}

			for (; i < value.Length; i++)
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

			return new BigDecimal(neg ? -mantissa : mantissa, exp, precision);
		}

		#endregion Constructors / Factories

		#region Operators

		public static BigDecimal operator +(BigDecimal left, BigDecimal right) { return left.Add(right); }

		public static BigDecimal operator -(BigDecimal left, BigDecimal right) { return left.Subtract(right); }

		public static BigDecimal operator *(BigDecimal left, BigDecimal right) { return left.Multiply(right); }

		public static BigDecimal operator /(BigDecimal left, BigDecimal right) { return left.Divide(right); }

		public static BigDecimal operator -(BigDecimal value) { return value.Negate(); }

		private BigDecimal Add(BigDecimal right, bool normalize)
		{
			BigDecimal.Normalize(ref this, ref right);
			BigDecimal result = new BigDecimal(this.Mantissa + right.Mantissa, this.Exponent, this.Precision > right.Precision ? this.Precision : right.Precision);

			this.Normalize();
			right.Normalize();

			return result;
		}

		private BigDecimal Add(BigDecimal right)
		{
			return this.Add(right, true);
		}

		private BigDecimal Subtract(BigDecimal right, bool normalize)
		{
			BigDecimal.Normalize(ref this, ref right);
			BigDecimal result = new BigDecimal(this.Mantissa - right.Mantissa, this.Exponent, this.Precision > right.Precision ? this.Precision : right.Precision);

			this.Normalize();
			right.Normalize();

			return result;
		}

		private BigDecimal Subtract(BigDecimal right)
		{
			return this.Subtract(right, true);
		}

		private BigDecimal Negate()
		{
			return new BigDecimal(-this.Mantissa, this.Exponent, this.Precision);
		}

		private BigDecimal Multiply(BigDecimal right)
		{
			int z = this.Exponent + right.Exponent;
			BigInteger m = this.Mantissa * right.Mantissa;
			return new BigDecimal(m, z, this.Precision > right.Precision ? this.Precision : right.Precision);
		}

		private BigDecimal Divide(BigDecimal right)
		{
			int precision = (this.Precision > right.Precision ? this.Precision : right.Precision) + 2,
				exponent = this.Exponent - right.Exponent,
				j = 2,
				iterations = 0;

			bool leftPos = this.Mantissa >= 0,
				 rightPos = right.Mantissa >= 0,
				 pos = !(leftPos ^ rightPos);

			BigInteger remainder,
					   lastRemainder = 0, digit,
					   mantissa = BigInteger.DivRem(leftPos ? this.Mantissa : -this.Mantissa, rightPos ? right.Mantissa : -right.Mantissa, out remainder);

			while (remainder > 0 && iterations < precision)
			{
				exponent--;
				mantissa *= 10;
				remainder *= 10;
				mantissa += digit = BigInteger.DivRem(remainder, rightPos ? right.Mantissa : -right.Mantissa, out remainder);
				if (digit == 0 && lastRemainder * 10 == remainder)
				{
					j++;
					if (j == precision) break;
				}
				else j = 2;
				lastRemainder = remainder;
				iterations++;
			}

			return new BigDecimal(pos ? mantissa : -mantissa, exponent, precision - 2);
		}

		public static BigDecimal Pow(BigDecimal value, int power)
		{
#if DOTNET4
			Contract.Requires(power >= 0);
#endif
			if (power == 0) return new BigDecimal(1, 0, value.Precision);
			if (power == 1) return new BigDecimal(value, value.Precision);
			if (power == 2) return value.Multiply(value);
			if (power == 3) return value.Multiply(value).Multiply(value);

			if (power % 2 == 0)
			{
				BigDecimal temp = BigDecimal.Pow(value, power / 2);
				return temp.Multiply(temp);
			}
			else
			{
				BigDecimal temp = BigDecimal.Pow(value, (power - 1) / 2);
				return temp.Multiply(temp).Multiply(value);
			}
		}

		public static BigDecimal Log10(BigDecimal value)
		{
			if (value < 1) throw new ArgumentOutOfRangeException("value");
			int a = (int)BigInteger.Log10(value.Mantissa) + value.Exponent;
			BigDecimal m = value;
			BigInteger mantissa = a;
			int digits = 0;

			while (digits < value.Precision)
			{
				m = BigDecimal.Pow(new BigDecimal(m, value.Precision).Divide(BigInteger.Pow(10, a)), 10);
				a = (int)BigInteger.Log10(m.Mantissa) + m.Exponent;
				mantissa = mantissa * 10 + a;
				digits++;
			}

			return new BigDecimal(mantissa, -digits, value.Precision).RoundLastDigit();
		}

		public static BigDecimal Log(BigDecimal value, BigDecimal logBase)
		{
			return BigDecimal.Log10(value).Divide(BigDecimal.Log10(logBase));
		}

		/*public static BigDecimal Sqrt(BigDecimal value)
		{
			if (value < 0) throw new ArgumentOutOfRangeException("value");
			BigDecimal sqrt = 0,
					   temp = new BigDecimal(value);
			int digits = 0;
			if (temp.Exponent < 0) temp.Exponent = 0;

			while (digits < value.Precision)
			{
				int cmp = 1;

				do
				{
					sqrt.Mantissa += 1;
					if (cmp == 0) break;
				}
				while ((cmp = BigDecimal.Pow(sqrt, 2).CompareTo(value)) <= 0);

				sqrt.Mantissa = (sqrt.Mantissa - 1) * 10;
				value.Exponent += 2;
				digits++;
			}

			sqrt.Exponent -= digits;
			return sqrt.RoundLastDigit();
		}*/

		#endregion Operators

		#region Ordering

		public static bool operator >(BigDecimal left, BigDecimal right) { return left.CompareTo(right) > 0; }

		public static bool operator >=(BigDecimal left, BigDecimal right) { return left.CompareTo(right) >= 0; }

		public static bool operator <(BigDecimal left, BigDecimal right) { return left.CompareTo(right) < 0; }

		public static bool operator <=(BigDecimal left, BigDecimal right) { return left.CompareTo(right) <= 0; }

		public static bool operator ==(BigDecimal left, BigDecimal right) { return left.Equals(right); }

		public static bool operator !=(BigDecimal left, BigDecimal right) { return !left.Equals(right); }

		private bool Equals(BigDecimal other, bool normalize)
		{
			BigDecimal.Normalize(ref this, ref other);
			bool eq = this.Mantissa.Equals(other.Mantissa);

			this.Normalize();
			other.Normalize();

			return eq;
		}

		public bool Equals(BigDecimal other)
		{
			return this.Equals(other, true);
		}

		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(BigDecimal) && this.Equals((BigDecimal)obj);
		}

		private int CompareTo(BigDecimal other, bool normalize)
		{
			BigDecimal.Normalize(ref this, ref other);
			int cmp = this.Mantissa.CompareTo(other.Mantissa);

			if (normalize)
			{
				this.Normalize();
				other.Normalize();
			}

			return cmp;
		}

		public int CompareTo(BigDecimal other)
		{
			return this.CompareTo(other, true);
		}

		#endregion Ordering

		#region Casting

		public static implicit operator BigDecimal(int n) { return new BigDecimal(n); }

		public static implicit operator BigDecimal(long n) { return new BigDecimal(n); }

		public static implicit operator BigDecimal(BigInteger n) { return new BigDecimal(n); }

		public static implicit operator BigDecimal(float n) { return new BigDecimal(n); }

		public static implicit operator BigDecimal(double n) { return new BigDecimal(n); }

		#endregion Casting

		private BigDecimal RoundLastDigit()
		{
			this.Normalize();
			if (this.Exponent >= 0) return new BigDecimal(this);

			BigInteger mantissa = this.Mantissa;
			int last = (int)(mantissa % 10);
			if (last >= 5) mantissa += 10 - last;
			else mantissa -= last;
			mantissa /= 10;
			return new BigDecimal(mantissa, this.Exponent + 1, this.Precision);
		}

		private void FixPrecision()
		{
			if (this.Exponent < 0)
			{
				int n = -this.Exponent - this.Precision;
				if (n > 0)
				{
					this.Mantissa /= BigInteger.Pow(10, n);
					this.Exponent += n;
				}
			}
		}

		private void Normalize()
		{
			if (this.Mantissa == 0) this.Exponent = 0;
			else
			{
				/*while (this.Mantissa % RadixPow2 == 0)
				{
					this.Exponent += 2;
					this.Mantissa /= RadixPow2;
				}

				if (this.Mantissa % Radix == 0)
				{
					this.Exponent++;
					this.Mantissa /= Radix;
				}*/

				double d = BigInteger.Log10(this.Mantissa);
				int p = (int)d;
				if (d == p)
				{
					this.Exponent += p;
					this.Mantissa /= BigInteger.Pow(10, p);
				}
			}
		}

		private void NormalizeTo(BigDecimal other)
		{
			if (this.Exponent > other.Exponent)
			{
				this.Mantissa *= BigInteger.Pow(Radix, this.Exponent - other.Exponent);
				this.Exponent = other.Exponent;
			}
		}

		private static void Normalize(ref BigDecimal a, ref BigDecimal b)
		{
			if (a.Exponent > b.Exponent) a.NormalizeTo(b);
			else b.NormalizeTo(a);
		}

		public BigDecimal WithPrecision(int precision)
		{
#if DOTNET4
			Contract.Requires(precision >= 0);
#endif
			return new BigDecimal(this, precision);
		}

		public override int GetHashCode()
		{
			return this.Mantissa.GetHashCode() ^ this.Exponent;
		}

		private string _ToStringCache;

		public override string ToString()
		{
			// if (_ToStringCache != null) return _ToStringCache;

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
			return _ToStringCache = res;
		}
	}
}