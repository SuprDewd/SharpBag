using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math
{
#if DOTNET4

	using System.Diagnostics.Contracts;
	using System.Globalization;
	using System.Numerics;

	/// <summary>
	/// An arbitrary big decimal.
	/// </summary>
	public struct BigDecimal : IComparable<BigDecimal>, IEquatable<BigDecimal>
	{
		#region Properties

		private const int DefaultPrecision = 32;

		private const int ExtraPrecision = 20;

		private const int Radix = 10;

		private const int RadixPow2 = 100;

		private int _Precision;

		/// <summary>
		/// The maximum amount of digits after the decimal point.
		/// </summary>
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

		private bool _Normalized;

		private bool Normalized { get { return _Normalized; } set { _Normalized = value; } }

		private bool _UsingDefaultPrecision;

		private bool UsingDefaultPrecision { get { return _UsingDefaultPrecision; } set { _UsingDefaultPrecision = value; } }

		#endregion Properties

		#region Static Instances

		/// <summary>
		/// A positive one.
		/// </summary>
		public static readonly BigDecimal One = new BigDecimal(1);

		/// <summary>
		/// A negative one.
		/// </summary>
		public static readonly BigDecimal MinusOne = new BigDecimal(-1);

		/// <summary>
		/// A zero.
		/// </summary>
		public static readonly BigDecimal Zero = new BigDecimal(0);

		#endregion Static Instances

		#region Constructors / Factories

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		public BigDecimal(int value) : this(value, DefaultPrecision, true) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		/// <param name="precision">The maximum amount of digits after the decimal point.</param>
		public BigDecimal(int value, int precision) : this(value, precision, false) { }

		private BigDecimal(int value, int precision, bool defaultPrecision) : this(value, 0, precision, false, defaultPrecision) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		public BigDecimal(long value) : this(value, DefaultPrecision, true) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		/// <param name="precision">The maximum amount of digits after the decimal point.</param>
		public BigDecimal(long value, int precision) : this(value, precision, false) { }

		private BigDecimal(long value, int precision, bool defaultPrecision) : this(value, 0, precision, false, defaultPrecision) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		public BigDecimal(BigInteger value) : this(value, DefaultPrecision, true) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		/// <param name="precision">The maximum amount of digits after the decimal point.</param>
		public BigDecimal(BigInteger value, int precision) : this(value, precision, false) { }

		private BigDecimal(BigInteger value, int precision, bool defaultPrecision) : this(value, 0, precision, false, defaultPrecision) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		public BigDecimal(float value) : this(value, DefaultPrecision, false, true) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		/// <param name="precision">The maximum amount of digits after the decimal point.</param>
		public BigDecimal(float value, int precision) : this(value, precision, false, false) { }

		private BigDecimal(float value, int precision, bool normalized, bool defaultPrecision)
		{
			Contract.Requires(precision >= 0);
			string[] srep = value.ToString("R").Split('E');
			BigDecimal parsed = BigDecimal.Parse(srep[0]);

			_ToStringCache = null;
			_Mantissa = parsed.Mantissa;
			_Exponent = srep.Length == 2 ? (parsed.Exponent + Convert.ToInt32(srep[1])) : parsed.Exponent;
			_Precision = precision;
			_Normalized = normalized;
			_UsingDefaultPrecision = defaultPrecision;

			this.FixPrecision();
			this.Normalize();
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		public BigDecimal(double value) : this(value, DefaultPrecision, false, true) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		/// <param name="precision">The maximum amount of digits after the decimal point.</param>
		public BigDecimal(double value, int precision) : this(value, precision, false, false) { }

		private BigDecimal(double value, int precision, bool normalized, bool defaultPrecision)
		{
			Contract.Requires(precision >= 0);
			string[] srep = value.ToString("R").Split('E');
			BigDecimal parsed = BigDecimal.Parse(srep[0]);

			_ToStringCache = null;
			_Mantissa = parsed.Mantissa;
			_Exponent = srep.Length == 2 ? (parsed.Exponent + Convert.ToInt32(srep[1])) : parsed.Exponent;
			_Precision = precision;
			_Normalized = normalized;
			_UsingDefaultPrecision = defaultPrecision;

			this.FixPrecision();
			this.Normalize();
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		public BigDecimal(decimal value) : this(value, DefaultPrecision, false, true) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		/// <param name="precision">The maximum amount of digits after the decimal point.</param>
		public BigDecimal(decimal value, int precision) : this(value, precision, false, false) { }

		private BigDecimal(decimal value, int precision, bool normalized, bool defaultPrecision)
		{
			Contract.Requires(precision >= 0);
			string[] srep = value.ToString().Split('E');
			BigDecimal parsed = BigDecimal.Parse(srep[0]);

			_ToStringCache = null;
			_Mantissa = parsed.Mantissa;
			_Exponent = srep.Length == 2 ? (parsed.Exponent + Convert.ToInt32(srep[1])) : parsed.Exponent;
			_Precision = precision;
			_Normalized = normalized;
			_UsingDefaultPrecision = defaultPrecision;

			this.FixPrecision();
			this.Normalize();
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		public BigDecimal(BigDecimal value) : this(value.Mantissa, value.Exponent, value.Precision, value.Normalized, value.UsingDefaultPrecision) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The value of the decimal.</param>
		/// <param name="precision">The maximum amount of digits after the decimal point.</param>
		public BigDecimal(BigDecimal value, int precision) : this(value.Mantissa, value.Exponent, precision, value.Normalized, false) { }

		private BigDecimal(BigInteger value, int exponent, int precision, bool normalized, bool defaultPrecision)
		{
			Contract.Requires(precision >= 0);
			_ToStringCache = null;
			_Mantissa = value;
			_Exponent = exponent;
			_Precision = precision;
			_Normalized = normalized;
			_UsingDefaultPrecision = defaultPrecision;

			this.FixPrecision();
			this.Normalize();
		}

		/// <summary>
		/// Parse the specified string.
		/// </summary>
		/// <param name="value">The specified string.</param>
		/// <returns>The BigDecimal represented by the string.</returns>
		public static BigDecimal Parse(string value)
		{
			value = value.Trim();
			int exp = 0;
			BigInteger mantissa = 0;
			bool foundComma = false;
			int i = 0;
			bool neg = false;
			if (value[0] == CultureInfo.CurrentCulture.NumberFormat.NegativeSign[0])
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

			bool defaultP = false;
			int precision = -exp;
			if (precision < DefaultPrecision)
			{
				precision = DefaultPrecision;
				defaultP = true;
			}

			return new BigDecimal(neg ? -mantissa : mantissa, exp, precision, false, defaultP);
		}

		/// <summary>
		/// Parse the specified string.
		/// </summary>
		/// <param name="value">The specified string.</param>
		/// <param name="precision">The maximum amount of digits after the decimal point.</param>
		/// <returns>The BigDecimal represented by the string.</returns>
		public static BigDecimal Parse(string value, int precision)
		{
			Contract.Requires(precision >= 0);
			value = value.Trim();
			int exp = 0;
			BigInteger mantissa = 0;
			bool foundComma = false;
			int i = 0;
			bool neg = false;
			if (value[0] == CultureInfo.CurrentCulture.NumberFormat.NegativeSign[0])
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

			return new BigDecimal(neg ? -mantissa : mantissa, exp, precision, false, false);
		}

		#endregion Constructors / Factories

		#region Operators

		/// <summary>
		/// The + operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>Left + Right</returns>
		public static BigDecimal operator +(BigDecimal left, BigDecimal right) { return left.Add(right); }

		/// <summary>
		/// The - operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>Left - Right</returns>
		public static BigDecimal operator -(BigDecimal left, BigDecimal right) { return left.Subtract(right); }

		/// <summary>
		/// The * operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>Left * Right</returns>
		public static BigDecimal operator *(BigDecimal left, BigDecimal right) { return left.Multiply(right); }

		/// <summary>
		/// The / operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>Left / Right</returns>
		public static BigDecimal operator /(BigDecimal left, BigDecimal right) { return left.Divide(right); }

		/// <summary>
		/// The - operator.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>-Left</returns>
		public static BigDecimal operator -(BigDecimal value) { return value.Negate(); }

		private BigDecimal Add(BigDecimal right, bool normalize)
		{
			BigDecimal.Normalize(ref this, ref right);
			BigDecimal result = new BigDecimal(this.Mantissa + right.Mantissa, this.Exponent, BigDecimal.PrecisionFor(ref this, ref right), false, this.UsingDefaultPrecision != right.UsingDefaultPrecision || this.UsingDefaultPrecision);

			if (normalize)
			{
				this.Normalize();
				right.Normalize();
			}

			return result;
		}

		private BigDecimal Add(BigDecimal right)
		{
			return this.Add(right, true);
		}

		private BigDecimal Subtract(BigDecimal right, bool normalize)
		{
			BigDecimal.Normalize(ref this, ref right);
			BigDecimal result = new BigDecimal(this.Mantissa - right.Mantissa, this.Exponent, BigDecimal.PrecisionFor(ref this, ref right), false, this.UsingDefaultPrecision != right.UsingDefaultPrecision || this.UsingDefaultPrecision);

			if (normalize)
			{
				this.Normalize();
				right.Normalize();
			}

			return result;
		}

		private BigDecimal Subtract(BigDecimal right)
		{
			return this.Subtract(right, true);
		}

		private BigDecimal Negate()
		{
			return new BigDecimal(-this.Mantissa, this.Exponent, this.Precision, this.Normalized, this.UsingDefaultPrecision);
		}

		private BigDecimal Multiply(BigDecimal right)
		{
			int z = this.Exponent + right.Exponent;
			BigInteger m = this.Mantissa * right.Mantissa;
			return new BigDecimal(m, z, BigDecimal.PrecisionFor(ref this, ref right), false, this.UsingDefaultPrecision != right.UsingDefaultPrecision || this.UsingDefaultPrecision);
		}

		private BigDecimal Divide(BigDecimal right)
		{
			int precision = BigDecimal.PrecisionFor(ref this, ref right) + BigDecimal.ExtraPrecision + 2,
				exponent = this.Exponent - right.Exponent,
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
				lastRemainder = remainder;
				if (mantissa != 0) iterations++;
			}

			return new BigDecimal(pos ? mantissa : -mantissa, exponent, precision - BigDecimal.ExtraPrecision - 2, false, this.UsingDefaultPrecision != right.UsingDefaultPrecision || this.UsingDefaultPrecision);
		}

		/// <summary>
		/// Raise the BigDecimal to the specified power.
		/// </summary>
		/// <param name="value">The BigDecimal.</param>
		/// <param name="power">The power.</param>
		/// <returns>The BigDecimal raised to the specified power.</returns>
		public static BigDecimal Pow(BigDecimal value, int power)
		{
			if (power < 0) return BigDecimal.Reciprocal(BigDecimal.Pow(value, -power));
			if (power == 0) return new BigDecimal(1, 0, value.Precision, true, value.UsingDefaultPrecision);
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

		/// <summary>
		/// Raise the BigDecimal to the specified power.
		/// </summary>
		/// <param name="value">The BigDecimal.</param>
		/// <param name="power">The power.</param>
		/// <returns>The BigDecimal raised to the specified power.</returns>
		public static BigDecimal Pow(BigDecimal value, BigDecimal power)
		{
			if (power < 0) return BigDecimal.Reciprocal(BigDecimal.Pow(value, -power));
			if (power == 0) return new BigDecimal(1, 0, value.Precision, true, value.UsingDefaultPrecision);
			if (power == 1) return new BigDecimal(value);
			return BigDecimal.Exp(BigDecimal.Ln(value) * power);
		}

		/// <summary>
		/// The base-10 logarithm of the BigDecimal.
		/// </summary>
		/// <param name="value">The BigDecimal.</param>
		/// <returns>The base-10 logarithm of the BigDecimal.</returns>
		public static BigDecimal Log10(BigDecimal value)
		{
			if (value < 1) throw new ArgumentOutOfRangeException("value");
			int a = (int)BigInteger.Log10(value.Mantissa) + value.Exponent;
			BigDecimal m = value;
			BigInteger mantissa = a;
			int digits = 0,
				precision = value.Precision + BigDecimal.ExtraPrecision;

			while (digits < precision)
			{
				// m = BigDecimal.Pow(new BigDecimal(m, value.Precision).Divide(BigInteger.Pow(10, a)), 10);
				m = BigDecimal.Pow(m / BigInteger.Pow(10, a), 10);
				a = (int)BigInteger.Log10(m.Mantissa) + m.Exponent;
				mantissa = mantissa * 10 + a;
				digits++;
			}

			return new BigDecimal(mantissa, -digits, value.Precision, false, value.UsingDefaultPrecision) /*.RoundLastDigit()*/;
		}

		/// <summary>
		/// Calculates the natural logarithm of the value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The natural logarithm.</returns>
		public static BigDecimal Ln(BigDecimal value)
		{
			return BigDecimal.Log10(value) / Constants.Log10EBig(value.Precision);
		}

		/// <summary>
		/// The logarithm of the BigDecimal in the specified base.
		/// </summary>
		/// <param name="value">The BigDecimal.</param>
		/// <param name="logBase">The specified base.</param>
		/// <returns>The logarithm of the BigDecimal in the specified base.</returns>
		public static BigDecimal Log(BigDecimal value, BigDecimal logBase)
		{
			int precision = BigDecimal.PrecisionFor(ref value, ref logBase);
			return BigDecimal.Log10(value.WithPrecision(precision)) / BigDecimal.Log10(logBase.WithPrecision(precision));
		}

		/// <summary>
		/// The square root of the BigDecimal.
		/// </summary>
		/// <param name="value">The BigDecimal.</param>
		/// <returns>The square root.</returns>
		public static BigDecimal Sqrt(BigDecimal value)
		{
			Contract.Requires(value >= 0);
			if (value.Mantissa == 0) return new BigDecimal(0, 0, value.Precision, true, value.UsingDefaultPrecision);
			BigDecimal sqrt = new BigDecimal(1, value.Precision + 1), last, two = new BigDecimal(2, value.Precision + 1);

			do
			{
				last = sqrt;
				sqrt = sqrt - ((sqrt * sqrt) - value) / (two * sqrt);
			}
			while (sqrt != last);

			return sqrt.WithPrecision(value.Precision);
		}

		/// <summary>
		/// The reciprocal of the BigDecimal.
		/// </summary>
		/// <param name="value">The BigDecimal.</param>
		/// <returns>The reciprocal of the BigDecimal.</returns>
		public static BigDecimal Reciprocal(BigDecimal value)
		{
			return BigDecimal.One / value;
		}

		/// <summary>
		/// Calculates the exponential function of the BigDecimal.
		/// </summary>
		/// <param name="value">The BigDecimal.</param>
		/// <returns>e ^ x</returns>
		public static BigDecimal Exp(BigDecimal value)
		{
			BigDecimal result = value.WithPrecision(value.Precision + 4) + BigDecimal.One,
					   lastResult, factorial = BigDecimal.One;

			int n = 2;

			do
			{
				lastResult = result;
				factorial = factorial * n;
				result = result + BigDecimal.Pow(value, n) / factorial;
				n++;
			} while (result != lastResult);

			result = result.WithPrecision(value.Precision);
			result._UsingDefaultPrecision = value._UsingDefaultPrecision;
			return BigDecimal.Round(result, value.Precision + 1);
		}

		/// <summary>
		/// Rounds the BigDecimal.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="digits">The amount of digits to keep.</param>
		/// <returns>The rounded value.</returns>
		public static BigDecimal Round(BigDecimal value, int digits = 0)
		{
			Contract.Requires(digits >= 0);
			if (digits >= value.Precision + BigDecimal.ExtraPrecision) return value;
			int prec = value.Precision;
			value = new BigDecimal(value.Mantissa, value.Exponent, digits + 1, false, false).WithoutExtraPrecision();

			if (-value.Exponent >= value.Precision)
			{
				int last = (int)(value.Mantissa % 10);
				if (last >= 5) value.Mantissa += 10 - last;
				else if (last >= 1) value.Mantissa -= last;
			}

			return new BigDecimal(value.Mantissa, value.Exponent, prec, false, value._UsingDefaultPrecision);
		}

		#endregion Operators

		#region Ordering

		/// <summary>
		/// The > operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>True if Left > Right</returns>
		public static bool operator >(BigDecimal left, BigDecimal right) { return left.CompareTo(right) > 0; }

		/// <summary>
		/// The >= operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>True if Left >= Right</returns>
		public static bool operator >=(BigDecimal left, BigDecimal right) { return left.CompareTo(right) >= 0; }

		/// <summary>
		/// The &lt; operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>True if Left %lt; Right</returns>
		public static bool operator <(BigDecimal left, BigDecimal right) { return left.CompareTo(right) < 0; }

		/// <summary>
		/// The &lt;= operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>True if Left %lt;= Right</returns>
		public static bool operator <=(BigDecimal left, BigDecimal right) { return left.CompareTo(right) <= 0; }

		/// <summary>
		/// The == operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>True if Left == Right</returns>
		public static bool operator ==(BigDecimal left, BigDecimal right) { return left.Equals(right); }

		/// <summary>
		/// The != operator.
		/// </summary>
		/// <param name="left">The left BigDecimal.</param>
		/// <param name="right">The right BigDecimal.</param>
		/// <returns>True if Left != Right</returns>
		public static bool operator !=(BigDecimal left, BigDecimal right) { return !left.Equals(right); }

		private bool Equals(BigDecimal other, bool normalize)
		{
			BigDecimal.Normalize(ref this, ref other);
			bool eq = this.WithoutExtraPrecision().Mantissa.Equals(other.WithoutExtraPrecision().Mantissa);

			if (normalize)
			{
				this.Normalize();
				other.Normalize();
			}

			return eq;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">Another BigDecimal.</param>
		/// <returns>Wheter this is equal to the other BigDecimal.</returns>
		public bool Equals(BigDecimal other)
		{
			return this.Equals(other, true);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">Another BigDecimal.</param>
		/// <returns>Wheter this is equal to the other BigDecimal.</returns>
		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(BigDecimal) && this.Equals((BigDecimal)obj);
		}

		/// <summary>
		/// IComparable.CompareTo()
		/// </summary>
		/// <param name="other">Another BigDecimal.</param>
		/// <returns>The order of the BigDecimals.</returns>
		public int CompareTo(BigDecimal other)
		{
			BigDecimal left = this.WithoutExtraPrecision(),
					   right = other.WithoutExtraPrecision();

			BigDecimal.Normalize(ref left, ref right);
			return left.Mantissa.CompareTo(right.Mantissa);
		}

		#endregion Ordering

		#region Casting

		/// <summary>
		/// A casting operator.
		/// </summary>
		/// <param name="n">A value.</param>
		/// <returns>The result.</returns>
		public static implicit operator BigDecimal(int n) { return new BigDecimal(n); }

		/// <summary>
		/// A casting operator.
		/// </summary>
		/// <param name="n">A value.</param>
		/// <returns>The result.</returns>
		public static implicit operator BigDecimal(long n) { return new BigDecimal(n); }

		/// <summary>
		/// A casting operator.
		/// </summary>
		/// <param name="n">A value.</param>
		/// <returns>The result.</returns>
		public static implicit operator BigDecimal(BigInteger n) { return new BigDecimal(n); }

		/// <summary>
		/// A casting operator.
		/// </summary>
		/// <param name="n">A value.</param>
		/// <returns>The result.</returns>
		public static implicit operator BigDecimal(float n) { return new BigDecimal(n); }

		/// <summary>
		/// A casting operator.
		/// </summary>
		/// <param name="n">A value.</param>
		/// <returns>The result.</returns>
		public static implicit operator BigDecimal(double n) { return new BigDecimal(n); }

		/// <summary>
		/// A casting operator.
		/// </summary>
		/// <param name="n">A value.</param>
		/// <returns>The result.</returns>
		public static implicit operator BigDecimal(decimal n) { return new BigDecimal(n); }

		/// <summary>
		/// Performs an explicit conversion from <see cref="SharpBag.Math.BigDecimal"/> to <see cref="System.Numerics.BigInteger"/>.
		/// </summary>
		/// <param name="n">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator BigInteger(BigDecimal n)
		{
			n = BigDecimal.Round(n, 0);
			return n.Mantissa * BigInteger.Pow(BigDecimal.Radix, n.Exponent);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="SharpBag.Math.BigDecimal"/> to <see cref="System.Int32"/>.
		/// </summary>
		/// <param name="n">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator int(BigDecimal n) { return (int)(BigInteger)n; }

		/// <summary>
		/// Performs an explicit conversion from <see cref="SharpBag.Math.BigDecimal"/> to <see cref="System.Int64"/>.
		/// </summary>
		/// <param name="n">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator long(BigDecimal n) { return (long)(BigInteger)n; }

		/// <summary>
		/// Performs an explicit conversion from <see cref="SharpBag.Math.BigDecimal"/> to <see cref="System.Decimal"/>.
		/// </summary>
		/// <param name="n">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator decimal(BigDecimal n)
		{
			string s = n.ToString();
			decimal res = 0, mod = 1;
			bool found = false;

			for (int i = 0; i < s.Length; i++)
			{
				if (found) res += (s[i] - '0') * (mod /= 10);
				else if (s[i] == ',' || s[i] == '.') found = true;
				else res = res * 10 + (s[i] - '0');
			}

			return res;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="SharpBag.Math.BigDecimal"/> to <see cref="System.Double"/>.
		/// </summary>
		/// <param name="n">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator double(BigDecimal n)
		{
			string s = n.ToString();
			double res = 0, mod = 1;
			bool found = false;

			for (int i = 0; i < s.Length; i++)
			{
				if (found) res += (s[i] - '0') * (mod /= 10);
				else if (s[i] == ',' || s[i] == '.') found = true;
				else res = res * 10 + (s[i] - '0');
			}

			return res;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="SharpBag.Math.BigDecimal"/> to <see cref="System.Single"/>.
		/// </summary>
		/// <param name="n">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator float(BigDecimal n)
		{
			string s = n.ToString();
			float res = 0, mod = 1;
			bool found = false;

			for (int i = 0; i < s.Length; i++)
			{
				if (found) res += (s[i] - '0') * (mod /= 10);
				else if (s[i] == ',' || s[i] == '.') found = true;
				else res = res * 10 + (s[i] - '0');
			}

			return res;
		}

		#endregion Casting

		#region Other

		private void FixPrecision()
		{
			if (this.Exponent < 0)
			{
				int n = -this.Exponent - this.Precision - BigDecimal.ExtraPrecision;
				if (n > 0)
				{
					this.Mantissa /= BigInteger.Pow(10, n);
					this.Exponent += n;
				}
			}
		}

		private BigDecimal WithoutExtraPrecision()
		{
			BigInteger mantissa = this.Mantissa;
			int exponent = this.Exponent;

			if (exponent < 0)
			{
				int n = -exponent - this.Precision;
				if (n > 0)
				{
					mantissa /= BigInteger.Pow(10, n);
					exponent += n;
				}
			}

			return new BigDecimal(mantissa, exponent, this.Precision, false, this.UsingDefaultPrecision);
		}

		private void Expand()
		{
			if (this.Exponent > 0)
			{
				this.Mantissa *= BigInteger.Pow(10, this.Exponent);
				this.Exponent = 0;
			}
		}

		private void Normalize()
		{
			if (!this.Normalized)
			{
				if (this.Mantissa != 0)
				{
					while (this.Mantissa % RadixPow2 == 0)
					{
						this.Exponent += 2;
						this.Mantissa /= RadixPow2;
					}

					if (this.Mantissa % Radix == 0)
					{
						this.Exponent++;
						this.Mantissa /= Radix;
					}
				}
				else
				{
					this.Exponent = 0;
				}

				this.Normalized = true;
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

		private static int PrecisionFor(ref BigDecimal a, ref BigDecimal b)
		{
			if (a.UsingDefaultPrecision == b.UsingDefaultPrecision) return a.Precision > b.Precision ? a.Precision : b.Precision;
			else return a.UsingDefaultPrecision ? b.Precision : a.Precision;
		}

		/// <summary>
		/// Returns a BigDecimal with the specified precision.
		/// </summary>
		/// <param name="precision">The precision.</param>
		/// <returns>A BigDecimal with the specified precision.</returns>
		public BigDecimal WithPrecision(int precision)
		{
			Contract.Requires(precision >= 0);
			return new BigDecimal(this, precision);
		}

		/// <summary>
		/// Object.GetHashCode();
		/// </summary>
		/// <returns>The hash code of the current instance.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Mantissa, this.Exponent);
		}

		private string _ToStringCache;

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the BigDecimal.</returns>
		public override string ToString()
		{
			if (_ToStringCache != null) return _ToStringCache;

			BigDecimal withoutExtra = this.WithoutExtraPrecision();
			bool positive = withoutExtra.Mantissa >= 0;
			StringBuilder sb = new StringBuilder((positive ? withoutExtra.Mantissa : -withoutExtra.Mantissa).ToString());

			int exp = withoutExtra.Exponent,
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

			if (!positive) sb.Insert(0, CultureInfo.CurrentCulture.NumberFormat.NegativeSign);
			string res = sb.ToString();
			if (comma) res = res.TrimEnd('0');
			if (res[res.Length - 1] == '.') res = res.Substring(0, res.Length - 1);
			return _ToStringCache = res;
		}

		#endregion Other
	}

#endif
}