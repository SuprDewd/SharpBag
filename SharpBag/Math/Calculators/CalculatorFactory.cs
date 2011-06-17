using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math.Calculators
{
	/// <summary>
	/// A calculator factory.
	/// </summary>
	public static class CalculatorFactory
	{
		private static Int32Calculator _Int32Calculator = new Int32Calculator();
		private static Int64Calculator _Int64Calculator = new Int64Calculator();
		private static BigIntegerCalculator _BigIntegerCalculator = new BigIntegerCalculator();
		private static DoubleCalculator _DoubleCalculator = new DoubleCalculator();
		private static FractionCalculator<int> _FractionCalculatorInt32 = new FractionCalculator<int>();
		private static FractionCalculator<long> _FractionCalculatorInt64 = new FractionCalculator<long>();
		private static FractionCalculator<BigInteger> _FractionCalculatorBigInteger = new FractionCalculator<BigInteger>();

		/// <summary>
		/// Get a calculator instance for the specified type.
		/// </summary>
		/// <typeparam name="T">The specified type.</typeparam>
		/// <returns>The calculator instance.</returns>
		public static Calculator<T> GetInstanceFor<T>()
		{
			if (typeof(T) == typeof(int)) return (Calculator<T>)(object)_Int32Calculator;
			else if (typeof(T) == typeof(long)) return (Calculator<T>)(object)_Int64Calculator;
			else if (typeof(T) == typeof(BigInteger)) return (Calculator<T>)(object)_BigIntegerCalculator;
			else if (typeof(T) == typeof(double)) return (Calculator<T>)(object)_DoubleCalculator;
			else if (typeof(T) == typeof(Fraction<int>)) return (Calculator<T>)(object)_FractionCalculatorInt32;
			else if (typeof(T) == typeof(Fraction<long>)) return (Calculator<T>)(object)_FractionCalculatorInt64;
			else if (typeof(T) == typeof(Fraction<BigInteger>)) return (Calculator<T>)(object)_FractionCalculatorBigInteger;

			return null;
		}

		/// <summary>
		/// Get a checked calculator instance for the specified type.
		/// </summary>
		/// <typeparam name="T">The specified type.</typeparam>
		/// <returns>The checked calculator instance.</returns>
		public static Calculator<T> GetCheckedInstanceFor<T>()
		{
			Calculator<T> calc = GetInstanceFor<T>();
			if (calc == null) return null;
			return new CheckedCalculator<T>(calc);
		}
	}
}