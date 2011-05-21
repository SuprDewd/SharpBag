using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math
{
	/// <summary>
	/// A BigInteger fraction.
	/// </summary>
	/// <remarks>http://www.codeproject.com/KB/recipes/fractiion.aspx</remarks>
	public struct FractionBig : IComparable<FractionBig>, IEquatable<FractionBig>, ICloneable
	{
		#region Properties

		private BigInteger _Numerator;

		/// <summary>
		/// The numerator.
		/// </summary>
		public BigInteger Numerator { get { return _Numerator; } private set { _Numerator = value; } }

		private BigInteger _Denominator;

		/// <summary>
		/// The denominator.
		/// </summary>
		public BigInteger Denominator { get { return _Denominator; } private set { _Denominator = value; } }

		/// <summary>
		/// Returns the convergents of the fraction.
		/// </summary>
		public IEnumerable<BigInteger> Convergents
		{
			get
			{
				FractionBig fract = this;

				while (fract.Denominator > 1)
				{
					BigInteger wholes = fract.Numerator / fract.Denominator;
					yield return wholes;
					fract = new FractionBig(fract.Denominator, fract.Numerator - wholes * fract.Denominator);
				}

				if (fract.Denominator != 0) yield return fract.Numerator;
			}
		}

		/// <summary>
		/// Returns the reciprocal of the fraction.
		/// </summary>
		public FractionBig Reciprocal { get { return new FractionBig(this.Denominator, this.Numerator); } }

		/// <summary>
		/// Returns the number of wholes in the fraction.
		/// </summary>
		public BigInteger Wholes
		{
			get
			{
				return this.Numerator / this.Denominator;
			}
		}

		/// <summary>
		/// Returns the remainder of the fraction.
		/// </summary>
		public BigInteger Remainder
		{
			get
			{
				return this.Numerator % this.Denominator;
			}
		}

		#endregion Properties

		#region Static Instances

		/// <summary>
		/// A fraction that represents positive infinity.
		/// </summary>
		public static readonly FractionBig PositiveInfinity = new FractionBig(1, 0);

		/// <summary>
		/// A fraction that represents negative infinity.
		/// </summary>
		public static readonly FractionBig NegativeInfinity = new FractionBig(-1, 0);

		/// <summary>
		/// A fraction that represents NaN (not a number).
		/// </summary>
		public static readonly FractionBig NaN = new FractionBig(0, 0);

		#endregion Static Instances

		#region Constructors

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		public FractionBig(BigInteger numerator, BigInteger denominator)
		{
			_Numerator = numerator;
			_Denominator = denominator;
			this.Reduce();
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="wholeNumber">The number of wholes.</param>
		public FractionBig(BigInteger wholeNumber)
		{
			_Numerator = wholeNumber;
			_Denominator = 1;
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		public FractionBig(FractionBig numerator, FractionBig denominator)
		{
			this = numerator / denominator;
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="other">Another fraction to copy.</param>
		public FractionBig(FractionBig other)
		{
			_Numerator = other.Numerator;
			_Denominator = other.Denominator;
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="value">The floating poBigInteger representation of the fraction.</param>
		public FractionBig(double value)
		{
			if (Double.IsPositiveInfinity(value)) this = FractionBig.PositiveInfinity;
			else if (Double.IsNegativeInfinity(value)) this = FractionBig.NegativeInfinity;
			else if (Double.IsNaN(value)) this = FractionBig.NaN;
			else
			{
				int sign = 1, maxIterations = 594;
				if (value < 0)
				{
					value = -value;
					sign = -sign;
				}

				ulong fractionNumerator = (ulong)value;
				double fractionDenominator = 1,
					previousDenominator = 0,
					remainingDigits = value,
					scratch;

				while (remainingDigits != System.Math.Floor(remainingDigits) && System.Math.Abs(value - (fractionNumerator / fractionDenominator)) > Double.Epsilon)
				{
					remainingDigits = 1.0 / (remainingDigits - System.Math.Floor(remainingDigits));
					scratch = fractionDenominator;
					fractionDenominator = (System.Math.Floor(remainingDigits) * fractionDenominator) + previousDenominator;
					fractionNumerator = (ulong)(value * fractionDenominator + 0.5);
					previousDenominator = scratch;
					if (maxIterations-- < 0) break;
				}

				_Numerator = ((BigInteger)fractionNumerator) * sign;
				_Denominator = (BigInteger)fractionDenominator;
				this.Reduce();
			}
		}

		#endregion Constructors

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
				this.Numerator = this.Numerator == 0 ? 0 : this.Numerator < 0 ? -1 : 1;
				return;
			}

			BigInteger gcd = BagMath.Gcd(this.Numerator, this.Denominator);
			if (gcd <= 1) return;
			this.Numerator /= gcd;
			this.Denominator /= gcd;
		}

		#endregion Helpers

		#region Static Factories

		/// <summary>
		/// Returns the fraction represented by the convergents.
		/// </summary>
		/// <param name="convergents">The convergents.</param>
		/// <returns>The fraction represented by the convergents.</returns>
		public static FractionBig FromConvergents(BigInteger[] convergents)
		{
			FractionBig fract = new FractionBig(convergents[convergents.Length - 1], 1);

			for (int i = convergents.Length - 2; i >= 0; i--)
			{
				fract = new FractionBig(convergents[i] * fract.Numerator + fract.Denominator, fract.Numerator);
			}

			return fract;
		}

		/// <summary>
		/// Parse the specified string.
		/// </summary>
		/// <param name="fraction">The fraction as a string.</param>
		/// <returns>The parsed fraction.</returns>
		public static FractionBig Parse(string fraction)
		{
			int slash = fraction.IndexOf('/');
			if (slash == -1) return new FractionBig(BigInteger.Parse(fraction));
			return new FractionBig(BigInteger.Parse(fraction.Substring(0, slash)), BigInteger.Parse(fraction.Substring(slash + 1)));
		}

		#endregion Static Factories

		#region Operators

		/// <summary>
		/// The addition operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The added fractions.</returns>
		public static FractionBig operator +(FractionBig left, FractionBig right)
		{
			if (left.Denominator == right.Denominator) return new FractionBig(left.Numerator + right.Numerator, left.Denominator);
			return new FractionBig(left.Numerator * right.Denominator + right.Numerator * left.Denominator, left.Denominator * right.Denominator);
		}

		/// <summary>
		/// The subtraction operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The subtracted fractions.</returns>
		public static FractionBig operator -(FractionBig left, FractionBig right)
		{
			if (left.Denominator == right.Denominator) return new FractionBig(left.Numerator - right.Numerator, left.Denominator);
			return new FractionBig(left.Numerator * right.Denominator - right.Numerator * left.Denominator, left.Denominator * right.Denominator);
		}

		/// <summary>
		/// The multiplication operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The fractions multiplied.</returns>
		public static FractionBig operator *(FractionBig left, FractionBig right) { return new FractionBig(left.Numerator * right.Numerator, left.Denominator * right.Denominator); }

		/// <summary>
		/// The division operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The fractions divided.</returns>
		public static FractionBig operator /(FractionBig left, FractionBig right) { return left * right.Reciprocal; }

		/// <summary>
		/// The modulo operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The left fraction modulo the right fraction.</returns>
		public static FractionBig operator %(FractionBig left, FractionBig right) { return left - (left / right).Wholes * right; }

		/// <summary>
		/// An explicit cast operator from a fraction to a double.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The double.</returns>
		public static explicit operator double(FractionBig fraction) { return (double)fraction.Numerator / (double)fraction.Denominator; }

		/// <summary>
		/// An explicit cast operator from a fraction to a decimal.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The decimal.</returns>
		public static explicit operator decimal(FractionBig fraction) { return (decimal)fraction.Numerator / (decimal)fraction.Denominator; }

		/// <summary>
		/// An explicit cast operator from a fraction to a float.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The float.</returns>
		public static explicit operator float(FractionBig fraction) { return (float)fraction.Numerator / (float)fraction.Denominator; }

		/// <summary>
		/// An explicit cast operator from a fraction to an integer.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The integer.</returns>
		public static explicit operator int(FractionBig fraction) { return (int)fraction.Numerator / (int)fraction.Denominator; }

		/// <summary>
		/// An explicit cast operator from a fraction to a long integer.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The long integer.</returns>
		public static explicit operator long(FractionBig fraction) { return (long)fraction.Numerator / (long)fraction.Denominator; }

		/// <summary>
		/// An explicit cast operator from a fraction to a BigInteger.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The BigInteger.</returns>
		public static explicit operator BigInteger(FractionBig fraction) { return fraction.Numerator / fraction.Denominator; }

		/// <summary>
		/// An implicit cast operator from a whole number to a fraction.
		/// </summary>
		/// <param name="wholeNumber">The whole number.</param>
		/// <returns>The fraction.</returns>
		public static implicit operator FractionBig(BigInteger wholeNumber) { return new FractionBig(wholeNumber); }

		/// <summary>
		/// An implicit cast operator from a whole number to a fraction.
		/// </summary>
		/// <param name="wholeNumber">The whole number.</param>
		/// <returns>The fraction.</returns>
		public static implicit operator FractionBig(long wholeNumber) { return new FractionBig((BigInteger)wholeNumber); }

		/// <summary>
		/// An implicit cast operator from a whole number to a fraction.
		/// </summary>
		/// <param name="wholeNumber">The whole number.</param>
		/// <returns>The fraction.</returns>
		public static implicit operator FractionBig(int wholeNumber) { return new FractionBig((BigInteger)wholeNumber); }

		/// <summary>
		/// An explicit cast operator from a floating point number to a fraction.
		/// </summary>
		/// <param name="fraction">The floating point number.</param>
		/// <returns>The fraction.</returns>
		public static explicit operator FractionBig(double fraction) { return new FractionBig(fraction); }

		/// <summary>
		/// An explicit cast operator from a floating point number to a fraction.
		/// </summary>
		/// <param name="fraction">The floating point number.</param>
		/// <returns>The fraction.</returns>
		public static explicit operator FractionBig(decimal fraction) { return new FractionBig((double)fraction); }

		/// <summary>
		/// An explicit cast operator from a floating point number to a fraction.
		/// </summary>
		/// <param name="fraction">The floating point number.</param>
		/// <returns>The fraction.</returns>
		public static explicit operator FractionBig(float fraction) { return new FractionBig(fraction); }

		/// <summary>
		/// The equal operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator ==(FractionBig left, FractionBig right) { return left.Equals(right); }

		/// <summary>
		/// The not equal operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator !=(FractionBig left, FractionBig right) { return !left.Equals(right); }

		/// <summary>
		/// Whether the fractions are equal.
		/// </summary>
		/// <param name="other">Another fraction.</param>
		/// <returns>Whether the fractions are equal.</returns>
		public bool Equals(FractionBig other) { return this.Numerator == other.Numerator && this.Denominator == other.Denominator; }

		/// <summary>
		/// The greater than operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator >(FractionBig left, FractionBig right) { return left.CompareTo(right) > 0; }

		/// <summary>
		/// The greater than or equal to operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator >=(FractionBig left, FractionBig right) { return left.CompareTo(right) >= 0; }

		/// <summary>
		/// The less than operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator <(FractionBig left, FractionBig right) { return left.CompareTo(right) < 0; }

		/// <summary>
		/// The less than or equal to operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator <=(FractionBig left, FractionBig right) { return left.CompareTo(right) <= 0; }

		/// <summary>
		/// An explicit cast operator from a fraction to a 32-bit fraction.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The 32-bit fraction.</returns>
		public static explicit operator Fraction(FractionBig fraction) { return new Fraction((int)fraction.Numerator, (int)fraction.Denominator); }

		/// <summary>
		/// An implicit cast operator from a fraction to a BigInteger fraction.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The BigInteger fraction.</returns>
		public static explicit operator Fraction64(FractionBig fraction) { return new Fraction64((long)fraction.Numerator, (long)fraction.Denominator); }

		#endregion Operators

		#region Interface Implementations

		/// <summary>
		/// Compare the fraction.
		/// </summary>
		/// <param name="other">Another fraction.</param>
		/// <returns>The result.</returns>
		public int CompareTo(FractionBig other)
		{
			if (this.Denominator == other.Denominator) return this.Numerator.CompareTo(other.Numerator);
			if (this.Numerator == other.Numerator) return -this.Denominator.CompareTo(other.Denominator);
			return (this.Numerator * other.Denominator).CompareTo(other.Numerator * this.Denominator);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the fraction.</returns>
		public override string ToString()
		{
			if (this.Denominator == 1) return this.Numerator.ToString();
			if (this == FractionBig.PositiveInfinity) return NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol;
			if (this == FractionBig.NegativeInfinity) return NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol;
			if (this == FractionBig.NaN) return NumberFormatInfo.CurrentInfo.NaNSymbol;
			return this.Numerator + "/" + this.Denominator;
		}

		/// <summary>
		/// Clones the fraction.
		/// </summary>
		/// <returns>The cloned fraction.</returns>
		public object Clone() { return new FractionBig(this.Numerator, this.Denominator); }

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other object.</param>
		/// <returns>Whether the object are equal.</returns>
		public override bool Equals(object obj) { return obj.GetType() == typeof(FractionBig) && this == (FractionBig)obj; }

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the fraction.</returns>
		public override int GetHashCode() { return this.Numerator.GetHashCode() ^ this.Denominator.GetHashCode(); }

		#endregion Interface Implementations
	}
}