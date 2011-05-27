using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using SharpBag.Math.Calculators;

namespace SharpBag.Math
{
	/// <summary>
	/// A fraction.
	/// </summary>
	/// <remarks>http://www.codeproject.com/KB/recipes/fractiion.aspx</remarks>
	public struct Fraction<T> : IComparable<Fraction<T>>, IEquatable<Fraction<T>>, ICloneable
	{
		static Fraction()
		{
			if (Calculator == null)
			{
				if (typeof(T) == typeof(int)) Calculator = (Calculator<T>)(object)new Int32Calculator();
				else if (typeof(T) == typeof(long)) Calculator = (Calculator<T>)(object)new Int64Calculator();
				else if (typeof(T) == typeof(BigInteger)) Calculator = (Calculator<T>)(object)new BigIntegerCalculator();
			}
		}

		private static Calculator<T> _Calculator;

		public static Calculator<T> Calculator
		{
			get { return _Calculator; }
			set
			{
				_Calculator = value;
				PositiveInfinity = new Fraction<T>(Calculator.One, Calculator.Zero);
				NegativeInfinity = new Fraction<T>(Calculator.NegativeOne, Calculator.Zero);
				NaN = new Fraction<T>(Calculator.Zero, Calculator.Zero);
				One = new Fraction<T>(Calculator.One, Calculator.One);
				Zero = new Fraction<T>(Calculator.Zero, Calculator.One);
				NegativeOne = new Fraction<T>(Calculator.NegativeOne, Calculator.One);
			}
		}

		#region Properties

		private T _Numerator;

		/// <summary>
		/// The numerator.
		/// </summary>
		public T Numerator { get { return _Numerator; } private set { _Numerator = value; } }

		private T _Denominator;

		/// <summary>
		/// The denominator.
		/// </summary>
		public T Denominator { get { return _Denominator; } private set { _Denominator = value; } }

		/// <summary>
		/// Returns the convergents of the fraction.
		/// </summary>
		public IEnumerable<T> Convergents
		{
			get
			{
				Fraction<T> fract = this;

				while (Calculator.GreaterThan(fract.Denominator, Calculator.One))
				{
					T wholes = Calculator.Floor(Calculator.Divide(fract.Numerator, fract.Denominator));
					yield return wholes;
					fract = new Fraction<T>(fract.Denominator, Calculator.Subtract(fract.Numerator, Calculator.Multiply(wholes, fract.Denominator)));
				}

				if (!Calculator.Equal(fract.Denominator, Calculator.Zero)) yield return fract.Numerator;
			}
		}

		/// <summary>
		/// Returns the reciprocal of the fraction.
		/// </summary>
		public Fraction<T> Reciprocal { get { return new Fraction<T>(this.Denominator, this.Numerator); } }

		/// <summary>
		/// Returns the number of wholes in the fraction.
		/// </summary>
		public T Wholes
		{
			get
			{
				return Calculator.Divide(this.Numerator, this.Denominator);
			}
		}

		/// <summary>
		/// Returns the remainder of the fraction.
		/// </summary>
		public T Remainder
		{
			get
			{
				return Calculator.Modulo(this.Numerator, this.Denominator);
			}
		}

		public Fraction<T> Floor
		{
			get
			{
				return new Fraction<T>(Calculator.Multiply(Calculator.Floor(Calculator.Divide(this.Numerator, this.Denominator)), this.Denominator), this.Denominator);
			}
		}

		public Fraction<T> Ceiling
		{
			get
			{
				return new Fraction<T>(Calculator.Multiply(Calculator.Ceiling(Calculator.Divide(this.Numerator, this.Denominator)), this.Denominator), this.Denominator);
			}
		}

		#endregion Properties

		#region Static Instances

		/// <summary>
		/// A fraction that represents positive infinity.
		/// </summary>
		public static Fraction<T> PositiveInfinity { get; private set; }

		/// <summary>
		/// A fraction that represents negative infinity.
		/// </summary>
		public static Fraction<T> NegativeInfinity { get; private set; }

		public static Fraction<T> One { get; private set; }

		public static Fraction<T> NegativeOne { get; private set; }

		public static Fraction<T> Zero { get; private set; }

		/// <summary>
		/// A fraction that represents NaN (not a number).
		/// </summary>
		public static Fraction<T> NaN { get; private set; }

		#endregion Static Instances

		#region Constructors

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		public Fraction(T numerator, T denominator)
		{
			_Numerator = numerator;
			_Denominator = denominator;
			this.Reduce();
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="wholeNumber">The number of wholes.</param>
		public Fraction(T wholeNumber)
		{
			_Numerator = wholeNumber;
			_Denominator = Calculator.One;
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		public Fraction(Fraction<T> numerator, Fraction<T> denominator)
		{
			this = numerator / denominator;
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="other">Another fraction to copy.</param>
		public Fraction(Fraction<T> other)
		{
			_Numerator = other.Numerator;
			_Denominator = other.Denominator;
		}

		#endregion Constructors

		#region Helpers

		private void Reduce()
		{
			if (Calculator.LessThan(this.Denominator, Calculator.Zero))
			{
				this.Numerator = Calculator.Negate(this.Numerator);
				this.Denominator = Calculator.Negate(this.Denominator);
			}

			if (Calculator.Equal(this.Denominator, Calculator.One)) return;
			if (Calculator.Equal(this.Denominator, Calculator.Zero))
			{
				this.Numerator = Calculator.Equal(this.Numerator, Calculator.Zero) ? Calculator.Zero : Calculator.LessThan(this.Numerator, Calculator.Zero) ? Calculator.NegativeOne : Calculator.One;
				return;
			}

			T gcd = Calculator.Gcd(this.Numerator, this.Denominator);
			if (Calculator.LessThanOrEqual(gcd, Calculator.One)) return;
			this.Numerator = Calculator.Divide(this.Numerator, gcd);
			this.Denominator = Calculator.Divide(this.Denominator, gcd);
		}

		#endregion Helpers

		#region Static Factories

		/// <summary>
		/// Returns the fraction represented by the convergents.
		/// </summary>
		/// <param name="convergents">The convergents.</param>
		/// <returns>The fraction represented by the convergents.</returns>
		public static Fraction<T> FromConvergents(T[] convergents)
		{
			Fraction<T> fract = new Fraction<T>(convergents[convergents.Length - 1], Calculator.One);

			for (int i = convergents.Length - 2; i >= 0; i--)
			{
				fract = new Fraction<T>(Calculator.Add(Calculator.Multiply(convergents[i], fract.Numerator), fract.Denominator), fract.Numerator);
			}

			return fract;
		}

		public static Fraction<T> FromFloatingPoint(double value)
		{
			if (Double.IsPositiveInfinity(value)) return Fraction<T>.PositiveInfinity;
			if (Double.IsNegativeInfinity(value)) return Fraction<T>.NegativeInfinity;
			if (Double.IsNaN(value)) return Fraction<T>.NaN;

			T sign = Calculator.One;
			if (value < 0)
			{
				value = -value;
				sign = Calculator.Negate(sign);
			}

			T fractionNumerator = Calculator.Convert(value);
			int maxIterations = 594;
			double fractionDenominator = 1,
				previousDenominator = 0,
				remainingDigits = value,
				scratch;

			while (remainingDigits != System.Math.Floor(remainingDigits) && System.Math.Abs(value - Calculator.ConvertToDouble(fractionNumerator) / fractionDenominator) > Double.Epsilon)
			{
				remainingDigits = 1.0 / (remainingDigits - System.Math.Floor(remainingDigits));
				scratch = fractionDenominator;
				fractionDenominator = (System.Math.Floor(remainingDigits) * fractionDenominator) + previousDenominator;
				fractionNumerator = Calculator.Convert(value * fractionDenominator + 0.5);
				previousDenominator = scratch;
				if (maxIterations-- < 0) break;
			}

			return new Fraction<T>(Calculator.Multiply(fractionNumerator, sign), Calculator.Convert(fractionDenominator));
		}

		/// <summary>
		/// Parse the specified string.
		/// </summary>
		/// <param name="fraction">The fraction as a string.</param>
		/// <returns>The parsed fraction.</returns>
		public static Fraction<T> Parse(string fraction)
		{
			int slash = fraction.IndexOf('/');
			if (slash == -1) return new Fraction<T>(Calculator.Convert(fraction));
			return new Fraction<T>(Calculator.Convert(fraction.Substring(0, slash)), Calculator.Convert(fraction.Substring(slash + 1)));
		}

		#endregion Static Factories

		#region Operators

		/// <summary>
		/// The addition operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The added fractions.</returns>
		public static Fraction<T> operator +(Fraction<T> left, Fraction<T> right)
		{
			if (Calculator.Equal(left.Denominator, right.Denominator)) return new Fraction<T>(Calculator.Add(left.Numerator, right.Numerator), left.Denominator);
			return new Fraction<T>(Calculator.Add(Calculator.Multiply(left.Numerator, right.Denominator), Calculator.Multiply(right.Numerator, left.Denominator)), Calculator.Multiply(left.Denominator, right.Denominator));
		}

		/// <summary>
		/// The subtraction operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The subtracted fractions.</returns>
		public static Fraction<T> operator -(Fraction<T> left, Fraction<T> right)
		{
			if (Calculator.Equal(left.Denominator, right.Denominator)) return new Fraction<T>(Calculator.Subtract(left.Numerator, right.Numerator), left.Denominator);
			return new Fraction<T>(Calculator.Subtract(Calculator.Multiply(left.Numerator, right.Denominator), Calculator.Multiply(right.Numerator, left.Denominator)), Calculator.Multiply(left.Denominator, right.Denominator));
		}

		public static Fraction<T> operator -(Fraction<T> fraction)
		{
			return new Fraction<T>(Calculator.Negate(fraction.Numerator), fraction.Denominator);
		}

		/// <summary>
		/// The multiplication operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The fractions multiplied.</returns>
		public static Fraction<T> operator *(Fraction<T> left, Fraction<T> right) { return new Fraction<T>(Calculator.Multiply(left.Numerator, right.Numerator), Calculator.Multiply(left.Denominator, right.Denominator)); }

		/// <summary>
		/// The division operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The fractions divided.</returns>
		public static Fraction<T> operator /(Fraction<T> left, Fraction<T> right) { return left * right.Reciprocal; }

		/// <summary>
		/// The modulo operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The left fraction modulo the right fraction.</returns>
		public static Fraction<T> operator %(Fraction<T> left, Fraction<T> right) { return left - new Fraction<T>((left / right).Wholes) * right; }

		/// <summary>
		/// An explicit cast operator from a fraction to a double.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The double.</returns>
		public static explicit operator double(Fraction<T> fraction) { return Calculator.ConvertToDouble(fraction.Numerator) / Calculator.ConvertToDouble(fraction.Denominator); }

		/// <summary>
		/// An explicit cast operator from a fraction to a decimal.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The decimal.</returns>
		public static explicit operator decimal(Fraction<T> fraction) { return Calculator.ConvertToDecimal(fraction.Numerator) / Calculator.ConvertToDecimal(fraction.Denominator); }

		/// <summary>
		/// An explicit cast operator from a fraction to a float.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The float.</returns>
		public static explicit operator float(Fraction<T> fraction) { return Calculator.ConvertToFloat(fraction.Numerator) / Calculator.ConvertToFloat(fraction.Denominator); }

		/// <summary>
		/// An explicit cast operator from a fraction to an integer.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The integer.</returns>
		public static explicit operator int(Fraction<T> fraction) { return Calculator.ConvertToInt(fraction.Numerator) / Calculator.ConvertToInt(fraction.Denominator); }

		/// <summary>
		/// An explicit cast operator from a fraction to a long integer.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The long integer.</returns>
		public static explicit operator long(Fraction<T> fraction) { return Calculator.ConvertToLong(fraction.Numerator) / Calculator.ConvertToLong(fraction.Denominator); }

		/// <summary>
		/// An explicit cast operator from a fraction to a BigInteger.
		/// </summary>
		/// <param name="fraction">The fraction.</param>
		/// <returns>The BigInteger.</returns>
		public static explicit operator BigInteger(Fraction<T> fraction) { return Calculator.ConvertToBigInteger(fraction.Numerator) / Calculator.ConvertToBigInteger(fraction.Denominator); }

		/// <summary>
		/// An implicit cast operator from a whole number to a fraction.
		/// </summary>
		/// <param name="wholeNumber">The whole number.</param>
		/// <returns>The fraction.</returns>
		public static implicit operator Fraction<T>(T wholeNumber) { return new Fraction<T>(wholeNumber); }

		/// <summary>
		/// An explicit cast operator from a floating point number to a fraction.
		/// </summary>
		/// <param name="fraction">The floating point number.</param>
		/// <returns>The fraction.</returns>
		public static explicit operator Fraction<T>(double fraction) { return Fraction<T>.FromFloatingPoint(fraction); }

		/// <summary>
		/// An explicit cast operator from a floating point number to a fraction.
		/// </summary>
		/// <param name="fraction">The floating point number.</param>
		/// <returns>The fraction.</returns>
		public static explicit operator Fraction<T>(decimal fraction) { return Fraction<T>.FromFloatingPoint((double)fraction); }

		/// <summary>
		/// An explicit cast operator from a floating point number to a fraction.
		/// </summary>
		/// <param name="fraction">The floating point number.</param>
		/// <returns>The fraction.</returns>
		public static explicit operator Fraction<T>(float fraction) { return Fraction<T>.FromFloatingPoint(fraction); }

		/// <summary>
		/// The equal operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator ==(Fraction<T> left, Fraction<T> right) { return left.Equals(right); }

		/// <summary>
		/// The not equal operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator !=(Fraction<T> left, Fraction<T> right) { return !left.Equals(right); }

		/// <summary>
		/// Whether the fractions are equal.
		/// </summary>
		/// <param name="other">Another fraction.</param>
		/// <returns>Whether the fractions are equal.</returns>
		public bool Equals(Fraction<T> other) { return Calculator.Equal(this.Numerator, other.Numerator) && Calculator.Equal(this.Denominator, other.Denominator); }

		/// <summary>
		/// The greater than operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator >(Fraction<T> left, Fraction<T> right) { return left.CompareTo(right) > 0; }

		/// <summary>
		/// The greater than or equal to operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator >=(Fraction<T> left, Fraction<T> right) { return left.CompareTo(right) >= 0; }

		/// <summary>
		/// The less than operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator <(Fraction<T> left, Fraction<T> right) { return left.CompareTo(right) < 0; }

		/// <summary>
		/// The less than or equal to operator.
		/// </summary>
		/// <param name="left">The left fraction.</param>
		/// <param name="right">The right fraction.</param>
		/// <returns>The result.</returns>
		public static bool operator <=(Fraction<T> left, Fraction<T> right) { return left.CompareTo(right) <= 0; }

		public Fraction<int> AsIntFraction() { return new Fraction<int>(Calculator.ConvertToInt(this.Numerator), Calculator.ConvertToInt(this.Denominator)); }

		public Fraction<long> AsLongFraction() { return new Fraction<long>(Calculator.ConvertToLong(this.Numerator), Calculator.ConvertToLong(this.Denominator)); }

		public Fraction<BigInteger> AsBigIntegerFraction() { return new Fraction<BigInteger>(Calculator.ConvertToBigInteger(this.Numerator), Calculator.ConvertToBigInteger(this.Denominator)); }

		public Fraction<T> Pow(T power) { return new Fraction<T>(Calculator.Pow(this.Numerator, power), Calculator.Pow(this.Denominator, power)); }

		#endregion Operators

		#region Interface Implementations

		/// <summary>
		/// Compare the fraction.
		/// </summary>
		/// <param name="other">Another fraction.</param>
		/// <returns>The result.</returns>
		public int CompareTo(Fraction<T> other)
		{
			if (Calculator.Equal(this.Denominator, other.Denominator)) return Calculator.Compare(this.Numerator, other.Numerator);
			if (Calculator.Equal(this.Numerator, other.Numerator)) return -Calculator.Compare(this.Denominator, other.Denominator);
			return Calculator.Compare(Calculator.Multiply(this.Numerator, other.Denominator), Calculator.Multiply(other.Numerator, this.Denominator));
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the fraction.</returns>
		public override string ToString()
		{
			if (Calculator.Equal(this.Denominator, Calculator.One)) return this.Numerator.ToString();
			if (this == Fraction<T>.PositiveInfinity) return NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol;
			if (this == Fraction<T>.NegativeInfinity) return NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol;
			if (this == Fraction<T>.NaN) return NumberFormatInfo.CurrentInfo.NaNSymbol;
			return Calculator.ConvertToString(this.Numerator) + "/" + Calculator.ConvertToString(this.Denominator);
		}

		/// <summary>
		/// Clones the fraction.
		/// </summary>
		/// <returns>The cloned fraction.</returns>
		public object Clone() { return new Fraction<T>(this.Numerator, this.Denominator); }

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other object.</param>
		/// <returns>Whether the object are equal.</returns>
		public override bool Equals(object obj) { return obj.GetType() == typeof(Fraction<T>) && this == (Fraction<T>)obj; }

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the fraction.</returns>
		public override int GetHashCode() { return this.Numerator.GetHashCode() ^ this.Denominator.GetHashCode(); }

		#endregion Interface Implementations
	}
}