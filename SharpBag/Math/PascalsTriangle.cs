using System.Collections.Generic;
using System.Numerics;

namespace SharpBag.Math
{
	/// <summary>
	/// A static class with methods concerning Pascals triangle.
	/// </summary>
	public static class PascalsTriangle
	{
		/// <summary>
		/// Gets an entry at the specified row and column.
		/// </summary>
		/// <param name="row">The specified row.</param>
		/// <param name="column">The specified column.</param>
		/// <returns>The value at the specified row and column.</returns>
		public static long GetEntry(int row, int column)
		{
			long current = 1;
			for (int i = 1; i <= column; i++) current = (current * (row + 1 - i)) / i;
			return current;
		}

		/// <summary>
		/// Gets an entry at the specified row and column.
		/// </summary>
		/// <param name="row">The specified row.</param>
		/// <param name="column">The specified column.</param>
		/// <returns>The value at the specified row and column.</returns>
		public static BigInteger GetEntryBig(long row, long column)
		{
			BigInteger current = 1;
			for (long i = 1; i <= column; i++) current = (current * (row + 1 - i)) / i;
			return current;
		}

		#region Rows

		/// <summary>
		/// Calculates the rows of Pascal's triangle.
		/// </summary>
		/// <returns>The rows.</returns>
		public static IEnumerable<int[]> Rows()
		{
			yield return new int[] { 1 };
			int[] row = new int[] { 1, 1 };
			int n = 3;

			while (true)
			{
				yield return row;
				int[] next = new int[n];
				next[0] = next[n - 1] = 1;
				for (int i = 1; i < n - 1; i++) next[i] = row[i - 1] + row[i];
				row = next;
				n++;
			}
		}

		/// <summary>
		/// Calculates the rows of Pascal's triangle.
		/// </summary>
		/// <returns>The rows.</returns>
		public static IEnumerable<long[]> Rows64()
		{
			yield return new long[] { 1 };
			long[] row = new long[] { 1, 1 };
			long n = 3;

			while (true)
			{
				yield return row;
				long[] next = new long[n];
				next[0] = next[n - 1] = 1;
				for (long i = 1; i < n - 1; i++) next[i] = row[i - 1] + row[i];
				row = next;
				n++;
			}
		}

		/// <summary>
		/// Calculates the rows of Pascal's triangle.
		/// </summary>
		/// <returns>The rows.</returns>
		public static IEnumerable<BigInteger[]> RowsBig()
		{
			yield return new BigInteger[] { 1 };
			BigInteger[] row = new BigInteger[] { 1, 1 };
			long n = 3;

			while (true)
			{
				yield return row;
				BigInteger[] next = new BigInteger[n];
				next[0] = next[n - 1] = 1;
				for (long i = 1; i < n - 1; i++) next[i] = row[i - 1] + row[i];
				row = next;
				n++;
			}
		}

		#endregion Rows
	}
}