using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math
{
	/// <summary>
	/// A rational fraction.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface Fraction<T>
	{
		/// <summary>
		/// The numerator.
		/// </summary>
		T Numerator { get; }

		/// <summary>
		/// The denominator.
		/// </summary>
		T Denominator { get; }

		/// <summary>
		/// Whether to automatically reduce the fraction.
		/// </summary>
		bool AutoReduce { get; }

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <param name="digits">The maximum number of digits after the comma.</param>
		/// <returns>The string representation of the fraction.</returns>
		string ToString(int digits);
	}
}