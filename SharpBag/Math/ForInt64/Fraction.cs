using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SharpBag.Math;

namespace SharpBag.Math.ForInt64
{
	using System;
	using System.Numerics;

	/// <summary>
	/// A rational fraction.
	/// </summary>
	/// <remarks>http://www.codeproject.com/KB/recipes/fractiion.aspx</remarks>
	public struct Fraction : Fraction<long>, IComparable<Fraction>, IEquatable<Fraction>, ICloneable
	{
		#region Properties

		private long _Numerator;

		/// <summary>
		/// The numerator.
		/// </summary>
		public long Numerator { get { return _Numerator; } private set { _Numerator = value; } }

		private long _Denominator;

		/// <summary>
		/// The denominator.
		/// </summary>
		public long Denominator { get { return _Denominator; } private set { _Denominator = value; if (this.AutoReduce) this.Reduce(); } }

		private static bool _DefaultAutoReduce = true;

		/// <summary>
		/// The default value of auto-reduce.
		/// </summary>
		public static bool DefaultAutoReduce { get { return _DefaultAutoReduce; } set { _DefaultAutoReduce = value; } }

		private bool _AutoReduce;

		/// <summary>
		/// Whether to automatically reduce the fraction.
		/// </summary>
		public bool AutoReduce { get { return _AutoReduce; } set { _AutoReduce = value; if (value) this.Reduce(); } }

		/// <summary>
		/// Returns the reciprocal of the fraction.
		/// </summary>
		public Fraction Reciprocal
		{
			get
			{
				return new Fraction(this.Denominator, this.Numerator, _AutoReduce);
			}
		}

		/// <summary>
		/// Returns the number of wholes in the fraction.
		/// </summary>
		public Fraction Wholes
		{
			get
			{
				return new Fraction(this.Numerator / this.Denominator, 1);
			}
		}

		/// <summary>
		/// Returns the remainder of the fraction.
		/// </summary>
		public Fraction Remainder
		{
			get
			{
				return new Fraction(this.Numerator % this.Denominator, this.Denominator);
			}
		}

		/// <summary>
		/// Returns the floor of the fraction.
		/// </summary>
		public Fraction Floor
		{
			get
			{
				return new Fraction((this.Numerator / this.Denominator) * this.Denominator, this.Denominator);
			}
		}

		/// <summary>
		/// Returns the ceiling of the fraction.
		/// </summary>
		public Fraction Ceiling
		{
			get
			{
				return new Fraction((long)Math.Ceiling((double)this.Numerator / this.Denominator) * this.Denominator, this.Denominator);
			}
		}

		/// <summary>
		/// Returns the reduced version of the fraction.
		/// </summary>
		public Fraction Reduced
		{
			get
			{
				return this.AutoReduce ? this : new Fraction(this.Numerator, this.Denominator, true);
			}
		}

		/// <summary>
		/// Returns the partial quotients of the fraction.
		/// </summary>
		public IEnumerable<long> PartialQuotients
		{
			get
			{
				Fraction fract = new Fraction(this);
				fract.AutoReduce = true;

				while (fract.Denominator > 1)
				{
					long wholes = fract.Numerator / fract.Denominator;
					yield return wholes;
					fract = new Fraction(fract.Denominator, fract.Numerator - (wholes * fract.Denominator));
				}

				if (fract.Denominator != 0) yield return fract.Numerator;
			}
		}

		#endregion Properties

		#region Static Instances

		/// <summary>
		/// A fraction that represents positive one.
		/// </summary>
		public static readonly Fraction PositiveOne = new Fraction(1, 1);

		/// <summary>
		/// A fraction that represents negative one.
		/// </summary>
		public static readonly Fraction NegativeOne = new Fraction(-1, 1);

		/// <summary>
		/// A fraction that represents zero.
		/// </summary>
		public static readonly Fraction Zero = new Fraction(0, 1);

		/// <summary>
		/// A fraction that represents positive infinity.
		/// </summary>
		public static readonly Fraction PositiveInfinity = new Fraction(1, 0);

		/// <summary>
		/// A fraction that represents negative infinity.
		/// </summary>
		public static readonly Fraction NegativeInfinity = new Fraction(-1, 0);

		/// <summary>
		/// A fraction that represents NaN (not a number).
		/// </summary>
		public static readonly Fraction NaN = new Fraction(0, 0);

		/// <summary>
		/// A fraction that represents the maximum value of a fraction.
		/// </summary>
		public static readonly Fraction MaxValue = new Fraction(Int32.MaxValue, 1);

		/// <summary>
		/// A fraction that represents the minimum value of a fraction.
		/// </summary>
		public static readonly Fraction MinValue = new Fraction(Int32.MinValue, 1);

		#endregion Static Instances

		#region Static Methods

		/// <summary>
		/// Determines whether the fraction is NaN.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>Whether the fraction is NaN.</returns>
		public static bool IsNaN(Fraction fraction)
		{
			Fraction reduced = fraction.Reduced;
			return reduced.Numerator == 0 && reduced.Denominator == 0;
		}

		/// <summary>
		/// Determines whether the fraction is positive infinity.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>Whether the fraction is positive infinity.</returns>
		public static bool IsPositiveInfinity(Fraction fraction)
		{
			Fraction reduced = fraction.Reduced;
			return reduced.Numerator == 1 && reduced.Denominator == 0;
		}

		/// <summary>
		/// Determines whether the fraction is negative infinity.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>Whether the fraction is negative infinity.</returns>
		public static bool IsNegativeInfinity(Fraction fraction)
		{
			Fraction reduced = fraction.Reduced;
			return reduced.Numerator == -1 && reduced.Denominator == 0;
		}

		/// <summary>
		/// Determines whether the fraction is infinity.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>Whether the fraction is infinity.</returns>
		public static bool IsInfinity(Fraction fraction)
		{
			Fraction reduced = fraction.Reduced;
			return IsPositiveInfinity(reduced) || IsNegativeInfinity(reduced);
		}

		/// <summary>
		/// Returns the partial quotients of the square root of the specified number.
		/// </summary>
		/// <param name="s">The number.</param>
		/// <returns>The partial quotients of the square root of the specified number.</returns>
		public static IEnumerable<long> PartialQuotientsOfSquareRootOf(long s)
		{
			long m = 0,
			  d = 1,
			  a0 = (int)Math.Sqrt(s),
			  a = a0;

			while (true)
			{
				yield return a;
				m = (d * a) - m;
				d = (s - (m * m)) / d;
				a = (a0 + m) / d;
			}
		}

		/// <summary>
		/// Finds the recurring cycle of the specified fraction.
		/// </summary>
		/// <param name="fraction">The specified fraction.</param>
		/// <returns>The cycle start and cycle length. Null if no cycle was found.</returns>
		public static Tuple<int, int> RecurringCycle(Fraction fraction)
		{
			long max = fraction.Denominator;
			List<Fraction> remainders = new List<Fraction> { fraction };
			for (int i = 0; i < max; i++)
			{
				fraction = new Fraction((fraction.Numerator * 10) % fraction.Denominator, fraction.Denominator);
				if (fraction.Numerator == 0) return null;

				for (int j = 0; j < i; j++)
				{
					if (fraction == remainders[j])
					{
						int length = i - j + 1;
						return Tuple.Create(j, length == 2 && remainders[i] == remainders[i - 1] ? 1 : length);
					}
				}

				remainders.Add(fraction);
			}

			return null;
		}

		/// <summary>
		/// Raises the fraction to the specified power.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <param name="power">The power.</param>
		/// <returns>The fraction raised to the specified power.</returns>
		public static Fraction Pow(Fraction fraction, int power)
		{
			return new Fraction((long)Math.Pow(fraction.Numerator, power), (long)Math.Pow(fraction.Denominator, power));
		}

		/// <summary>
		/// Computes the square root of the fraction.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The square root.</returns>
		public static Fraction Sqrt(Fraction fraction)
		{
			return new Fraction((long)Math.Sqrt(fraction.Numerator * fraction.Denominator), fraction.Denominator);
		}

		#endregion Static Methods

		#region Constructors / Factories

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		public Fraction(long numerator, long denominator) : this(numerator, denominator, Fraction.DefaultAutoReduce) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		/// <param name="autoReduce">Whether to automatically reduce the fraction.</param>
		public Fraction(long numerator, long denominator, bool autoReduce)
		{
			_Numerator = numerator;
			_Denominator = denominator;
			_AutoReduce = autoReduce;

			if (autoReduce) this.Reduce();
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="wholeNumber">The number of wholes.</param>
		public Fraction(long wholeNumber) : this(wholeNumber, 1, Fraction.DefaultAutoReduce) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="wholeNumber">The number of wholes.</param>
		/// <param name="autoReduce">Whether to automatically reduce the fraction.</param>
		public Fraction(long wholeNumber, bool autoReduce) : this(wholeNumber, 1, autoReduce) { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		public Fraction(Fraction numerator, Fraction denominator)
		{
			this = numerator / denominator;
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="other">Another fraction to copy.</param>
		public Fraction(Fraction<long> other)
		{
			_Numerator = other.Numerator;
			_Denominator = other.Denominator;
			_AutoReduce = other.AutoReduce;
		}

		/// <summary>
		/// Returns the fraction represented by the partial quotients.
		/// </summary>
		/// <param name="terms">The partial quotients of the continued fraction.</param>
		/// <returns>The fraction represented by the partial quotients.</returns>
		public static Fraction FromPartialQuotients(long[] terms)
		{
			Fraction fract = new Fraction(terms[terms.Length - 1], 1);

			for (long i = terms.Length - 2; i >= 0; i--)
			{
				fract = new Fraction((terms[i] * fract.Numerator) + fract.Denominator, fract.Numerator);
			}

			return fract;
		}

		/// <summary>
		/// Returns the fraction represented by the floting point number.
		/// </summary>
		/// <param name="value">The floating point number.</param>
		/// <returns>The fraction represented by the floting point number.</returns>
		public static Fraction FromFloatingPoint(double value)
		{
			if (Double.IsPositiveInfinity(value)) return Fraction.PositiveInfinity;
			if (Double.IsNegativeInfinity(value)) return Fraction.NegativeInfinity;
			if (Double.IsNaN(value)) return Fraction.NaN;

			int sign = 1;
			if (value < 0)
			{
				value = -value;
				sign = -sign;
			}

			long fractionNumerator = (long)value;
			int maxIterations = 594;
			double fractionDenominator = 1,
				previousDenominator = 0,
				remainingDigits = value,
				scratch;

			while (remainingDigits != Math.Floor(remainingDigits) && Math.Abs(value - (double)(fractionNumerator) / fractionDenominator) > Double.Epsilon)
			{
				remainingDigits = 1.0 / (remainingDigits - Math.Floor(remainingDigits));
				scratch = fractionDenominator;
				fractionDenominator = (Math.Floor(remainingDigits) * fractionDenominator) + previousDenominator;
				fractionNumerator = (long)(value * fractionDenominator + 0.5);
				previousDenominator = scratch;
				if (maxIterations-- < 0) break;
			}

			return new Fraction(fractionNumerator * sign, (long)fractionDenominator);
		}

		/// <summary>
		/// Parse the specified string.
		/// </summary>
		/// <param name="fraction">The fraction as a string.</param>
		/// <returns>The parsed fraction.</returns>
		public static Fraction Parse(string fraction)
		{
			int slash = fraction.IndexOf('/');
			if (slash == -1) return new Fraction(Convert.ToInt64(fraction));
			return new Fraction(Convert.ToInt64(fraction.Substring(0, slash)), Convert.ToInt64(fraction.Substring(slash + 1)));
		}

		#endregion Constructors / Factories

		#region Helpers

		private void Reduce()
		{
			if (this.Denominator < 0)
			{
				this.Numerator = -this.Numerator;
				this.Denominator = -this.Denominator;
			}

			if (this.Denominator == 1) return;
			if (this.Denominator == 0)
			{
				this.Numerator = (this.Numerator == 0) ? 0 : (this.Numerator < 0) ? -1 : 1;
				return;
			}

			long gcd = BagMath.Gcd(this.Numerator, this.Denominator);
			if (gcd <= 1) return;
			this.Numerator = this.Numerator / gcd;
			this.Denominator = this.Denominator / gcd;
		}

		#endregion Helpers

		#region Operators

		/// <summary>
		/// The addition operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The added fractions.</returns>
		public static Fraction operator +(Fraction left, Fraction right)
		{
			if (left.Denominator == right.Denominator) return new Fraction(left.Numerator + right.Numerator, left.Denominator, left.AutoReduce || right.AutoReduce);
			return new Fraction((left.Numerator * right.Denominator) + (right.Numerator * left.Denominator), left.Denominator * right.Denominator, left.AutoReduce || right.AutoReduce);
		}

		/// <summary>
		/// The subtraction operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The subtracted fractions.</returns>
		public static Fraction operator -(Fraction left, Fraction right)
		{
			if (left.Denominator == right.Denominator) return new Fraction(left.Numerator - right.Numerator, left.Denominator, left.AutoReduce || right.AutoReduce);
			return new Fraction((left.Numerator * right.Denominator) - (right.Numerator * left.Denominator), left.Denominator * right.Denominator, left.AutoReduce || right.AutoReduce);
		}

		/// <summary>
		/// The negation operator.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The negated fraction.</returns>
		public static Fraction operator -(Fraction fraction)
		{
			return new Fraction(-fraction.Numerator, fraction.Denominator, fraction.AutoReduce);
		}

		/// <summary>
		/// The multiplication operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The fractions multiplied.</returns>
		public static Fraction operator *(Fraction left, Fraction right)
		{
			return new Fraction(left.Numerator * right.Numerator, left.Denominator * right.Denominator, left.AutoReduce || right.AutoReduce);
		}

		/// <summary>
		/// The division operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The fractions divided.</returns>
		public static Fraction operator /(Fraction left, Fraction right)
		{
			return left * right.Reciprocal;
		}

		/// <summary>
		/// The modulo operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The left fraction modulo the right fraction.</returns>
		public static Fraction operator %(Fraction left, Fraction right)
		{
			return left - (left / right).Wholes * right;
		}

		#endregion Operators

		#region Ordering

		/// <summary>
		/// The greater than operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator >(Fraction left, Fraction right) { return left.CompareTo(right) > 0; }

		/// <summary>
		/// The greater than or equal to operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator >=(Fraction left, Fraction right) { return left.CompareTo(right) >= 0; }

		/// <summary>
		/// The less than operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator <(Fraction left, Fraction right) { return left.CompareTo(right) < 0; }

		/// <summary>
		/// The less than or equal to operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator <=(Fraction left, Fraction right) { return left.CompareTo(right) <= 0; }

		/// <summary>
		/// The equal operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator ==(Fraction left, Fraction right) { return left.Equals(right); }

		/// <summary>
		/// The not equal operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator !=(Fraction left, Fraction right) { return !left.Equals(right); }

		/// <summary>
		/// Whether the fractions are equal.
		/// </summary>
		/// <param name="other">Another fraction.</param>
		/// <returns>Whether the fractions are equal.</returns>
		public bool Equals(Fraction other)
		{
			if (Fraction.IsNaN(other)) return Fraction.IsNaN(this);
			if (Fraction.IsNaN(this)) return false;
			if (Fraction.IsPositiveInfinity(other)) return Fraction.IsPositiveInfinity(this);
			if (Fraction.IsPositiveInfinity(this)) return false;
			if (Fraction.IsNegativeInfinity(other)) return Fraction.IsNegativeInfinity(this);
			if (Fraction.IsNegativeInfinity(this)) return false;

			Fraction thisReduced = this.Reduced,
						otherReduced = other.Reduced;

			return thisReduced.Numerator == otherReduced.Numerator && thisReduced.Denominator == otherReduced.Denominator;
		}

		/// <summary>
		/// Compare the fraction to another fraction.
		/// </summary>
		/// <param name="other">Another fraction.</param>
		/// <returns>The result.</returns>
		public int CompareTo(Fraction other)
		{
			if (Fraction.IsNaN(other)) return 0;
			if (Fraction.IsPositiveInfinity(other)) return Fraction.IsPositiveInfinity(this) ? 0 : -1;
			if (Fraction.IsNegativeInfinity(other)) return Fraction.IsNegativeInfinity(this) ? 0 : 1;

			Fraction thisReduced = this.Reduced,
						otherReduced = other.Reduced;

			if (thisReduced.Denominator == otherReduced.Denominator) return thisReduced.Numerator.CompareTo(otherReduced.Numerator);
			if (thisReduced.Numerator == otherReduced.Numerator) return -thisReduced.Denominator.CompareTo(otherReduced.Denominator);
			return (thisReduced.Numerator * otherReduced.Denominator).CompareTo(otherReduced.Numerator * thisReduced.Denominator);
		}

		#endregion Ordering

		#region Casting

		/// <summary>
		/// Converts the fraction to an Int32 fraction.
		/// </summary>
		/// <returns>The converted fraction.</returns>
		public SharpBag.Math.ForInt32.Fraction AsInt32Fraction() { return new SharpBag.Math.ForInt32.Fraction((int)this.Numerator, (int)this.Denominator, this.AutoReduce); }

		/// <summary>
		/// Converts the fraction to a BigInteger fraction.
		/// </summary>
		/// <returns>The converted fraction.</returns>
		public SharpBag.Math.ForBigInteger.Fraction AsBigIntegerFraction() { return new SharpBag.Math.ForBigInteger.Fraction(this.Numerator, this.Denominator, this.AutoReduce); }

		/// <summary>
		/// An implicit cast operator from an integer to a fraction.
		/// </summary>
		/// <param name="integer">The integer.</param>
		/// <returns>The fraction.</returns>
		public static implicit operator Fraction(int integer)
		{
			return new Fraction(integer);
		}

		/// <summary>
		/// An implicit cast operator from an integer to a fraction.
		/// </summary>
		/// <param name="integer">The integer.</param>
		/// <returns>The fraction.</returns>
		public static implicit operator Fraction(long integer)
		{
			return new Fraction(integer);
		}

		/// <summary>
		/// An explicit cast operator from an integer to a fraction.
		/// </summary>
		/// <param name="integer">The integer.</param>
		/// <returns>The fraction.</returns>
		public static explicit operator Fraction(BigInteger integer)
		{
			return new Fraction((long)integer);
		}

		/// <summary>
		/// An explicit cast operator from a floating point number to a fraction.
		/// </summary>
		/// <param name="floatingPoint">The floating point number.</param>
		/// <returns>The fraction.</returns>
		public static explicit operator Fraction(double floatingPoint)
		{
			return Fraction.FromFloatingPoint(floatingPoint);
		}

		/// <summary>
		/// An explicit cast operator from a fraction to an integer.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The integer.</returns>
		public static explicit operator int(Fraction fraction)
		{
			return (int)(fraction.Numerator / fraction.Denominator);
		}

		/// <summary>
		/// An explicit cast operator from a fraction to a long integer.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The long integer.</returns>
		public static explicit operator long(Fraction fraction)
		{
			return (long)fraction.Numerator / fraction.Denominator;
		}

		/// <summary>
		/// An explicit cast operator from a fraction to a BigInteger.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The BigInteger.</returns>
		public static explicit operator BigInteger(Fraction fraction)
		{
			return (BigInteger)fraction.Numerator / fraction.Denominator;
		}

		/// <summary>
		/// An explicit cast operator from a fraction to a double.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The double.</returns>
		public static explicit operator double(Fraction fraction)
		{
			return (double)fraction.Numerator / fraction.Denominator;
		}

		/// <summary>
		/// An explicit cast operator from a fraction to a decimal.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The decimal.</returns>
		public static explicit operator decimal(Fraction fraction)
		{
			return (decimal)fraction.Numerator / fraction.Denominator;
		}

		/// <summary>
		/// An explicit cast operator from a fraction to a float.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The float.</returns>
		public static explicit operator float(Fraction fraction)
		{
			return (float)fraction.Numerator / fraction.Denominator;
		}

		#endregion Casting

		#region Other

		/// <summary>
		/// Clones the fraction.
		/// </summary>
		/// <returns>The cloned fraction.</returns>
		public object Clone() { return new Fraction(this.Numerator, this.Denominator, this.AutoReduce); }

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the fraction.</returns>
		public override int GetHashCode()
		{
			return (int)(this.Numerator ^ this.Denominator);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other object.</param>
		/// <returns>Whether the object are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(Fraction) && this.Equals((Fraction)obj);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the fraction.</returns>
		public override string ToString()
		{
			if (this.Denominator == 1) return this.Numerator.ToString();
			if (Fraction.IsPositiveInfinity(this)) return NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol;
			if (Fraction.IsNegativeInfinity(this)) return NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol;
			if (Fraction.IsNaN(this)) return NumberFormatInfo.CurrentInfo.NaNSymbol;
			return this.Numerator + "/" + this.Denominator;
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <param name="digits">The maximum number of digits after the comma.</param>
		/// <returns>The string representation of the fraction.</returns>
		public string ToString(int digits)
		{
			if (this.Denominator == 1) return this.Numerator.ToString();
			if (Fraction.IsPositiveInfinity(this)) return NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol;
			if (Fraction.IsNegativeInfinity(this)) return NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol;
			if (Fraction.IsNaN(this)) return NumberFormatInfo.CurrentInfo.NaNSymbol;

			if (digits == 0) return this.Wholes.ToString();
			bool negative = this.Numerator < 0;
			Fraction fraction = new Fraction(negative ? -this.Numerator : this.Numerator, this.Denominator);
			Fraction remainder = fraction.Remainder;
			long wholes = (long)fraction.Wholes;
			if (negative) wholes = -wholes;
			StringBuilder sb = new StringBuilder(wholes.ToString()).Append(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);

			do
			{
				remainder = remainder * 10;
				long remWholes = (long)remainder.Wholes;
				if (remWholes < 0) throw new OverflowException();
				sb.Append(remWholes.ToString());
				digits--;

				if (digits == 0) break;
				remainder = new Fraction((remainder.Numerator % remainder.Denominator), remainder.Denominator);
			}
			while (remainder.Numerator != 0);

			string ret = sb.ToString().TrimEnd('0', CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);
			return ret == "" ? "0" : ret;
		}

		/// <summary>
		/// IFormattable.ToString()
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>The formatted string.</returns>
		/// <remarks>
		/// Format string: type,digits
		/// Type is either D or C.
		/// D is the same as ToString(digits) where digits is the number of digits.
		/// C is is the same as C, but recurring cycles are treated specially.
		/// If digits is not present, 10 will be used.
		/// </remarks>
		/// <example>
		/// String.Format("{0}",     new Fraction(123, 999) / 100) == 41/33300
		/// String.Format("{0:C}",   new Fraction(123, 999) / 100) == 0,00(123)
		/// String.Format("{0:C,2}", new Fraction(123, 999) / 100) == 0,00(123)
		/// String.Format("{0:D}",   new Fraction(123, 999) / 100) == 0,0012312312
		/// String.Format("{0:D,2}", new Fraction(123, 999) / 100) == 0
		/// </example>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			string[] parts = format == null ? new string[] { "" } : format.Split(',');
			int digits = 10;
			if (parts.Length >= 2 && Int32.TryParse(parts[1].Trim(), out digits)) { }
			switch (parts[0].Trim())
			{
				case "C":
					Tuple<int, int> cycle = Fraction.RecurringCycle(this);
					if (cycle == null) return this.ToString(digits);
					string s = this.ToString(cycle.Item1 + cycle.Item2);
					int dot = s.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);
					int n = cycle.Item1 + cycle.Item2 - s.Length - dot - 1;
					if (n < 0) s = s.PadRight(-n + s.Length, '0');
					return s.Substring(0, dot + 1) + s.Substring(dot + 1, cycle.Item1) + "(" + s.Substring(dot + cycle.Item1 + 1, cycle.Item2) + ")";
				case "D": return this.ToString(digits);
				default: return this.ToString();
			}
		}

		#endregion Other
	}
}