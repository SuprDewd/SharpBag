using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#if DOTNET4

using System.Diagnostics.Contracts;

#endif

namespace SharpBag
{
	/// <summary>
	/// A static class with utility methods.
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Calculates the execution time of the specified action.
		/// </summary>
		/// <param name="a">The action.</param>
		/// <param name="handleGc">Whether to handle the garbage collector. If true, the GC will be forced to clean up before taking the time.</param>
		/// <returns>The execution time in milliseconds.</returns>
		public static TimeSpan ExecutionTime(Action a, bool handleGc = true)
		{
#if DOTNET4
			Contract.Requires(a != null);
#endif
			if (handleGc)
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			Stopwatch s = new Stopwatch();
			s.Start();

			a();

			s.Stop();
			return s.Elapsed;
		}

		/// <summary>
		/// Creates an IEnumerable from the specified objects.
		/// </summary>
		/// <typeparam name="T">The type of the IEnumerable to create.</typeparam>
		/// <param name="objects">The objects.</param>
		/// <returns>The IEnumerable.</returns>
		public static IEnumerable<T> CreateIEnumerable<T>(params T[] objects)
		{
#if DOTNET4
			Contract.Requires(objects != null);
#endif
			return objects;
		}

		/// <summary>
		/// Converts a single value to an enumerable.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns>The value as an enumerable.</returns>
		/// <remarks>Igor Ostrovsky - http://igoro.com/archive/extended-linq-additional-operators-for-linq-to-objects/</remarks>
		public static IEnumerable<T> Single<T>(T value)
		{
			yield return value;
		}

		/// <summary>
		/// Swap the specified variables.
		/// </summary>
		/// <typeparam name="T">The type of the variables.</typeparam>
		/// <param name="a">The first variable.</param>
		/// <param name="b">The second variable.</param>
		public static void Swap<T>(ref T a, ref T b)
		{
			T temp = a;
			a = b;
			b = temp;
		}
	}
}