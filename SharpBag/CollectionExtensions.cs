﻿using System;
using System.Collections.Generic;
using System.Linq;

#if DOTNET4

using System.Diagnostics.Contracts;
using System.Collections;

#endif

namespace SharpBag
{
	/// <summary>
	/// Extension methods for collections.
	/// </summary>
	public static class CollectionExtensions
	{
		/// <summary>
		/// Returns all the subsets of the current instance with the specified minimum length.
		/// </summary>
		/// <typeparam name="T">The type of values the current instance holds.</typeparam>
		/// <param name="array">The current instance.</param>
		/// <param name="min">The minimum length of a subset.</param>
		/// <returns>The subsets.</returns>
		public static IEnumerable<T[]> Subsets<T>(this T[] array, int min = 1)
		{
#if DOTNET4
			Contract.Requires(array != null);
			Contract.Requires(min >= 0);
#endif
			for (int i = 0; i <= array.Length - min; i++)
			{
				for (int j = min - 1; j < array.Length - i; j++)
				{
					yield return array.Range(i, i + j).ToArray();
				}
			}
		}

		/// <summary>
		/// Returns all the subsets of the current instance with the specified minimum length and the specified maximum length.
		/// </summary>
		/// <typeparam name="T">The type of values the current instance holds.</typeparam>
		/// <param name="array">The current instance.</param>
		/// <param name="min">The minimum length of a subset.</param>
		/// <param name="max">The maximum length of a subset.</param>
		/// <returns>The subsets.</returns>
		public static IEnumerable<T[]> Subsets<T>(this T[] array, int min, int max)
		{
#if DOTNET4
			Contract.Requires(array != null);
			Contract.Requires(min >= 0);
			Contract.Requires(max >= min);
#endif
			for (int i = 0; i <= array.Length - min; i++)
			{
				for (int j = min - 1; j < array.Length - i && j < max; j++)
				{
					yield return array.Range(i, i + j).ToArray();
				}
			}
		}

		/// <summary>
		/// Returns a line in the current instance.
		/// </summary>
		/// <param name="g">The current instance.</param>
		/// <param name="x">The first x coordinate.</param>
		/// <param name="y">The first y coordinate.</param>
		/// <param name="xDelta">The x delta.</param>
		/// <param name="yDelta">The y delta.</param>
		/// <returns>The line.</returns>
		public static IEnumerable<T> Line<T>(this T[,] g, int x, int y, int xDelta, int yDelta)
		{
#if DOTNET4
			Contract.Requires(g != null);
			Contract.Requires(!(xDelta == 0 && yDelta == 0));
#endif
			int xl = g.GetLength(0), yl = g.GetLength(1);
			while (x >= 0 && y >= 0 && x < xl && y < yl)
			{
				yield return g[x, y];
				x += xDelta;
				y += yDelta;
			}
		}

		/// <summary>
		/// Returns a line in the current instance.
		/// </summary>
		/// <param name="g">The current instance.</param>
		/// <param name="x">The first x coordinate.</param>
		/// <param name="y">The first y coordinate.</param>
		/// <param name="xDelta">The x delta.</param>
		/// <param name="yDelta">The y delta.</param>
		/// <param name="selector">The result selector.</param>
		/// <returns>The line.</returns>
		public static IEnumerable<TOut> Line<TIn, TOut>(this TIn[,] g, int x, int y, int xDelta, int yDelta, Func<TIn, int, int, TOut> selector)
		{
#if DOTNET4
			Contract.Requires(g != null);
			Contract.Requires(!(xDelta == 0 && yDelta == 0));
#endif
			int xl = g.GetLength(0), yl = g.GetLength(1);
			while (x >= 0 && y >= 0 && x < xl && y < yl)
			{
				yield return selector(g[x, y], x, y);
				x += xDelta;
				y += yDelta;
			}
		}

		/// <summary>
		/// Returns a line in the current instance.
		/// </summary>
		/// <param name="g">The current instance.</param>
		/// <param name="x">The first x coordinate.</param>
		/// <param name="y">The first y coordinate.</param>
		/// <param name="xDelta">The x delta.</param>
		/// <param name="yDelta">The y delta.</param>
		/// <param name="selector">The result selector.</param>
		/// <returns>The line.</returns>
		public static IEnumerable<TOut> Line<TIn, TOut>(this TIn[,] g, int x, int y, int xDelta, int yDelta, Func<TIn, TOut> selector)
		{
#if DOTNET4
			Contract.Requires(g != null);
			Contract.Requires(!(xDelta == 0 && yDelta == 0));
#endif
			int xl = g.GetLength(0), yl = g.GetLength(1);
			while (x >= 0 && y >= 0 && x < xl && y < yl)
			{
				yield return selector(g[x, y]);
				x += xDelta;
				y += yDelta;
			}
		}

		/// <summary>
		/// Returns the items in the current instance with the specified indexes.
		/// </summary>
		/// <typeparam name="T">The type of elements in the current instance.</typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="indices">The indices.</param>
		/// <returns>The items in the current instance with the specified indexes.</returns>
		public static IEnumerable<T> Take<T>(this IEnumerable<T> collection, IEnumerable<int> indices)
		{
#if DOTNET4
			Contract.Requires(collection != null);
			Contract.Requires(indices != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			indices = indices.Where(i => i >= 0);
			if (!indices.Any()) return Enumerable.Empty<T>();

			T[] array = collection.Take(indices.Max() + 1).ToArray();

			return indices.Select(i => array[i]);
		}

		/// <summary>
		/// Returns the items in the current instance with the specified indexes.
		/// </summary>
		/// <typeparam name="T">The type of elements in the current instance.</typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="indices">The indices.</param>
		/// <returns>The items in the current instance with the specified indexes.</returns>
		public static IEnumerable<T> Take<T>(this IEnumerable<T> collection, params int[] indices)
		{
#if DOTNET4
			Contract.Requires(collection != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			return collection.Take(indices.AsEnumerable());
		}

		/// <summary>
		/// Shuffle the collection.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="source">The current instance.</param>
		/// <param name="rand">A random number generator.</param>
		/// <returns>The shuffled collection.</returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rand = null)
		{
#if DOTNET4
			Contract.Requires(source != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			rand = rand ?? new Random();

			T[] array = source.ToArray();
			for (int n = array.Length; n > 1; )
			{
				int k = rand.Next(n--);

				if (n != k)
				{
					T tmp = array[k];
					array[k] = array[n];
					array[n] = tmp;
				}
			}

			return array;
		}

		/// <summary>
		/// Takes every n-th item of the collection.
		/// </summary>
		/// <typeparam name="T">The type of the items.</typeparam>
		/// <param name="enumeration">The current instance.</param>
		/// <param name="step">The step to take.</param>
		/// <param name="start">The location to start at.</param>
		/// <returns>Every n-th item of the collection.</returns>
		public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> enumeration, int step, int start = 0)
		{
#if DOTNET4
			Contract.Requires(enumeration != null);
			Contract.Requires(start >= 0 && start < enumeration.Count());
			Contract.Requires(step > 0);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			int first = 0;
			int count = 0;

			foreach (T item in enumeration)
			{
				if (first < start) first++;
				else if (first == start)
				{
					yield return item;

					first++;
				}
				else
				{
					count++;

					if (count == step)
					{
						yield return item;

						count = 0;
					}
				}
			}
		}

		/// <summary>
		/// Returns an empty enumerable if the current instance is null.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="pSeq">The current instance.</param>
		/// <returns>An empty enumerable if the current instance is null.</returns>
		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> pSeq)
		{
#if DOTNET4
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			return pSeq ?? Enumerable.Empty<T>();
		}

		#region IsIn overloads

		/// <summary>
		/// Whether the current instance is in the specified collection.
		/// </summary>
		/// <typeparam name="T">The type of the current instance.</typeparam>
		/// <param name="item">The current instance.</param>
		/// <param name="collection">The collection.</param>
		/// <returns>Whether the current instance is in the specified collection.</returns>
		public static bool IsIn<T>(this T item, IEnumerable<T> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			return collection.Contains(item);
		}

		/// <summary>
		/// Whether the current instance is in the specified collection.
		/// </summary>
		/// <typeparam name="T">The type of the current instance.</typeparam>
		/// <param name="item">The current instance.</param>
		/// <param name="collection">The collection.</param>
		/// <returns>Whether the current instance is in the specified collection.</returns>
		public static bool IsIn<T>(this T item, params T[] collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			return collection.Contains(item);
		}

		#endregion IsIn overloads

		#region Random overloads

		/// <summary>
		/// Returns a random item from the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the items.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>A random item from  the current instance.</returns>
		public static T Random<T>(this IEnumerable<T> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			return collection.Random(new Random());
		}

		/// <summary>
		/// Returns a random item from the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the items.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <param name="rand">A random number generator.</param>
		/// <returns>A random item from  the current instance.</returns>
		public static T Random<T>(this IEnumerable<T> collection, Random rand)
		{
#if DOTNET4
			Contract.Requires(collection != null);
			Contract.Requires(rand != null);
#endif
			T[] array = collection.ToArray();
			return array.Length == 0 ? default(T) : array[rand.Next(0, array.Length)];
		}

		#endregion Random overloads

		/// <summary>
		/// Gets a subarray of the current instance.
		/// </summary>
		/// <param name="a">The current instance.</param>
		/// <param name="x1">X-coordinate 1.</param>
		/// <param name="y1">Y-coordinate 1.</param>
		/// <param name="x2">X-coordinate 2.</param>
		/// <param name="y2">Y-coordinate 2.</param>
		/// <returns>The subarray.</returns>
		public static int[,] Subarray(this int[,] a, int x1, int y1, int x2, int y2)
		{
#if DOTNET4
			Contract.Requires(a != null);
			Contract.Requires(x1 <= x2);
			Contract.Requires(y1 <= y2);
			Contract.Requires(x1 >= 0);
			Contract.Requires(x2 >= 0);
			Contract.Requires(y1 >= 0);
			Contract.Requires(y2 >= 0);
			Contract.Requires(x1 < a.GetLength(0));
			Contract.Requires(x2 < a.GetLength(0));
			Contract.Requires(y1 < a.GetLength(1));
			Contract.Requires(y2 < a.GetLength(1));
			Contract.Ensures(Contract.Result<int[,]>() != null);
#endif
			int[,] sub = new int[(x2 - x1) + 1, (y2 - y1) + 1];

			for (int x = 0; x < sub.GetLength(0); x++)
			{
				for (int y = 0; y < sub.GetLength(1); y++)
				{
					sub[x, y] = a[x1 + x, y1 + y];
				}
			}

			return sub;
		}

		/// <summary>
		/// Converts the current instance into an enumerable.
		/// </summary>
		/// <typeparam name="T">The type of elements in the current instance.</typeparam>
		/// <param name="e">The current instance.</param>
		/// <returns>An enumerable.</returns>
		public static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> e)
		{
#if DOTNET4
			Contract.Requires(e != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			while (e.MoveNext()) yield return e.Current;
		}

		/// <summary>
		/// Converts the current instance into an enumerable.
		/// </summary>
		/// <param name="e">The current instance.</param>
		/// <returns>An enumerable.</returns>
		public static IEnumerable<object> AsEnumerable(this IEnumerator e)
		{
#if DOTNET4
			Contract.Requires(e != null);
			Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);
#endif
			while (e.MoveNext()) yield return e.Current;
		}

		/// <summary>
		/// Adds the specified key and value to the dictionary.
		/// If overwrite is true and the dictionary contains the specified key, the key's value will be overwritten.
		/// If overwrite is false and the dictionary contains the specified key, an exception won't be thrown.
		/// </summary>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <param name="d">The dictionary.</param>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <param name="overwrite">True if the key's value should be overwritten; otherwise false.</param>
		public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value, bool overwrite)
		{
#if DOTNET4
			Contract.Requires(d != null);
#endif
			if (d.ContainsKey(key))
			{
				if (overwrite) d[key] = value;
			}
			else d.Add(key, value);
		}

		#region Fill overloads

		/// <summary>
		/// Fills the current array with the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the array.</typeparam>
		/// <param name="array">The current instance.</param>
		/// <param name="value">The value to fill the array with.</param>
		public static void Fill<T>(this T[] array, T value)
		{
#if DOTNET4
			Contract.Requires(array != null);
#endif
			for (int i = 0; i < array.Count(); i++) array[i] = value;
		}

		/// <summary>
		/// Fills the current array with the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the array.</typeparam>
		/// <param name="array">The current instance.</param>
		/// <param name="value">The value to fill the array with.</param>
		public static void Fill<T>(this List<T> array, T value)
		{
#if DOTNET4
			Contract.Requires(array != null);
#endif
			for (int i = 0; i < array.Count(); i++) array[i] = value;
		}

		#endregion Fill overloads

		/// <summary>
		/// Gets a subarray of an array.
		/// </summary>
		/// <typeparam name="T">The type of the enumerable.</typeparam>
		/// <param name="array">The array.</param>
		/// <param name="start">The first index.</param>
		/// <param name="end">The last index.</param>
		/// <returns>A subarray of the array.</returns>
		public static IEnumerable<T> Range<T>(this IEnumerable<T> array, int start, int end)
		{
#if DOTNET4
			Contract.Requires(array != null);
			Contract.Requires(start <= end);
			Contract.Requires(start >= 0 && start < array.Count());
			Contract.Requires(end >= 0 && end < array.Count());
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			for (int i = start; i <= end; i++) yield return array.ElementAt(i);
		}

		/// <summary>
		/// Immediately executes the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
		/// <param name="sequence">The current instance.</param>
		/// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
		public static void Execute<T>(this IEnumerable<T> sequence)
		{
#if DOTNET4
			Contract.Requires(sequence != null);
#endif
			foreach (T item in sequence) { }
		}

		/// <summary>
		/// Gets the element in the array located at the specified percent.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the array.</typeparam>
		/// <param name="array">The current instance.</param>
		/// <param name="percent">The percent.</param>
		/// <returns>The element in the array located at the specified percent.</returns>
		/// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
		public static T GetByPercent<T>(this T[] array, double percent)
		{
#if DOTNET4
			Contract.Requires(array != null);
			Contract.Requires(array.Length > 0);
			Contract.Requires(percent >= 0 && percent <= 1);
#endif
			return array[Math.MathExtensions.Round((array.Length - 1) * percent)];
		}

		#region Multidimensional Arrays

		/// <summary>
		/// Returns an enumerable of all the items in the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the items.</typeparam>
		/// <param name="multiDArray">The current instance.</param>
		/// <returns>An enumerable of all the items in the current instance.</returns>
		public static IEnumerable<T> AsEnumerable<T>(this T[,] multiDArray)
		{
#if DOTNET4
			Contract.Requires(multiDArray != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			for (int i1 = 0; i1 < multiDArray.GetLength(0); i1++)
				for (int i2 = 0; i2 < multiDArray.GetLength(1); i2++)
					yield return multiDArray[i1, i2];
		}

		/// <summary>
		/// Returns an enumerable of all the items in the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the items.</typeparam>
		/// <param name="multiDArray">The current instance.</param>
		/// <returns>An enumerable of all the items in the current instance.</returns>
		public static IEnumerable<T> AsEnumerable<T>(this T[, ,] multiDArray)
		{
#if DOTNET4
			Contract.Requires(multiDArray != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
#endif
			for (int i1 = 0; i1 < multiDArray.GetLength(0); i1++)
				for (int i2 = 0; i2 < multiDArray.GetLength(1); i2++)
					for (int i3 = 0; i3 < multiDArray.GetLength(2); i3++)
						yield return multiDArray[i1, i2, i3];
		}

		/// <summary>
		/// Converts the current instance to a multidimensional array.
		/// </summary>
		/// <typeparam name="T">The type of items the current instance holds.</typeparam>
		/// <param name="jagged">The current instance.</param>
		/// <returns>A multidimensional array.</returns>
		public static T[,] ToMultidimensional<T>(this T[][] jagged)
		{
#if DOTNET4
			Contract.Requires(jagged != null);
#endif
			int xlen = jagged.Length;
			if (xlen == 0) return new T[0, 0];
			int ylen = jagged[0].Length;
			T[,] arr = new T[xlen, ylen];

			for (int x = 0; x < xlen; x++)
			{
				for (int y = 0; y < ylen; y++)
				{
					arr[x, y] = jagged[x][y];
				}
			}

			return arr;
		}

		/// <summary>
		/// Projects every element in the current instance using the specified function.
		/// </summary>
		/// <typeparam name="TIn">The types in the current instance.</typeparam>
		/// <typeparam name="TOut">The types in the result.</typeparam>
		/// <param name="mArr">The current instance.</param>
		/// <param name="func">The projector function.</param>
		/// <returns>The new array.</returns>
		public static TOut[,] Select<TIn, TOut>(this TIn[,] mArr, Func<TIn, TOut> func)
		{
#if DOTNET4
			Contract.Requires(mArr != null);
			Contract.Requires(func != null);
#endif
			int xLen = mArr.GetLength(0), yLen = mArr.GetLength(1);
			TOut[,] mArrOut = new TOut[xLen, yLen];

			for (int x = 0; x < xLen; x++)
			{
				for (int y = 0; y < yLen; y++)
				{
					mArrOut[x, y] = func(mArr[x, y]);
				}
			}

			return mArrOut;
		}

		/// <summary>
		/// Turns the current instance into a multidimensional array.
		/// </summary>
		/// <typeparam name="T">The type of elements in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <param name="cols">The number of columns in the result array.</param>
		/// <returns>The multidimensional array.</returns>
		public static T[,] AsMultidimensional<T>(this IEnumerable<T> collection, int cols)
		{
#if DOTNET4
			Contract.Requires(cols > 0);
			Contract.Requires(collection.Count() % cols == 0);
#endif
			int count = collection.Count(),
				rows = count / cols,
				i = 0;

			T[,] outArr = new T[rows, cols];

			foreach (T item in collection)
			{
				outArr[i / cols, i % cols] = item;
				i++;
			}

			return outArr;
		}

		/// <summary>
		/// Combines the current instance with another multidimensional array.
		/// </summary>
		/// <typeparam name="TIn">The type of items in the current instance.</typeparam>
		/// <typeparam name="TOut">The type of items in the result array.</typeparam>
		/// <param name="arrFirst">The current instance.</param>
		/// <param name="arrSecond">The other array.</param>
		/// <param name="func">The function.</param>
		/// <returns>The combined array.</returns>
		public static TOut[,] Zip<TIn, TOut>(this TIn[,] arrFirst, TIn[,] arrSecond, Func<TIn, TIn, TOut> func)
		{
#if DOTNET4
			Contract.Requires(arrFirst.GetLength(0) == arrSecond.GetLength(0));
			Contract.Requires(arrFirst.GetLength(1) == arrSecond.GetLength(1));
#endif
			int xLen = arrFirst.GetLength(0), yLen = arrFirst.GetLength(1);
			TOut[,] resArr = new TOut[xLen, yLen];

			for (int x = 0; x < xLen; x++)
			{
				for (int y = 0; y < yLen; y++)
				{
					resArr[x, y] = func(arrFirst[x, y], arrSecond[x, y]);
				}
			}

			return resArr;
		}

		/// <summary>
		/// Shifts each element in the current instance.
		/// </summary>
		/// <typeparam name="T">The type of elements in the current instance.</typeparam>
		/// <param name="arr">The current instance.</param>
		/// <param name="xDelta">The x delta.</param>
		/// <param name="yDelta">The y delta.</param>
		/// <param name="wrap">Whether to wrap.</param>
		/// <returns>The new array with shifted elements.</returns>
		public static T[,] Shift<T>(this T[,] arr, int xDelta, int yDelta, bool wrap = false)
		{
			int xLen = arr.GetLength(0), yLen = arr.GetLength(1);
			T[,] outArr = new T[xLen, yLen];

			for (int x = 0; x < xLen; x++)
			{
				for (int y = 0; y < yLen; y++)
				{
					int nextX = (x + xDelta),
						nextY = (y + yDelta);

					if (!wrap && (nextX >= xLen || nextX < 0 || nextY >= yLen || nextY < 0)) continue;

					nextX %= xLen;
					nextY %= yLen;

					while (nextX < 0) nextX += xLen;
					while (nextY < 0) nextY += yLen;

					outArr[nextX, nextY] = arr[x, y];
				}
			}

			return outArr;
		}

		#endregion Multidimensional Arrays

		/// <summary>
		/// Checks whether the array contains the specified object.
		/// </summary>
		/// <param name="a">The array.</param>
		/// <param name="o">The object.</param>
		/// <returns>Whether the array contains the specified object.</returns>
		public static bool ArrayContains(this Array a, object o)
		{
#if DOTNET4
			Contract.Requires(a != null);
#endif
			return a.Cast<object>().Any(item => item == o);
		}

		/// <summary>
		/// Adds extended info to the current instance.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>The current instance with extended info.</returns>
		public static IEnumerable<EnumerableEntry.WithInfo<T>> WithInfo<T>(this IEnumerable<T> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			bool first = true, last = false;
			T previous = default(T), next = default(T);
			int index = 0;

			using (IEnumerator<T> en = collection.GetEnumerator())
			{
				if (!en.MoveNext()) yield break;

				while (!last)
				{
					T current = en.Current;
					last = !en.MoveNext();
					next = last ? default(T) : en.Current;
					yield return new EnumerableEntry.WithInfo<T>(current, index++, first, last, previous, next);
					first = false;
					previous = current;
				}
			}
		}

		/// <summary>
		/// Removes extended info from the current instance.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>The current instance without extended info.</returns>
		public static IEnumerable<T> WithoutInfo<T>(this IEnumerable<EnumerableEntry.WithInfo<T>> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			return collection.Select(i => i.Value);
		}

		/// <summary>
		/// Updates extended info of the current instance.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>The current instance with updated info.</returns>
		public static IEnumerable<EnumerableEntry.WithInfo<T>> UpdateInfo<T>(this IEnumerable<EnumerableEntry.WithInfo<T>> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			return collection.WithoutInfo().WithInfo();
		}

		/// <summary>
		/// Adds indices to the current instance.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>The current instance with indices.</returns>
		public static IEnumerable<EnumerableEntry.WithIndex<T>> WithIndex<T>(this IEnumerable<T> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			int index = 0;

			foreach (T value in collection)
			{
				yield return new EnumerableEntry.WithIndex<T>(value, index++);
			}
		}

		/// <summary>
		/// Removes indices from the current instance.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>The current instance without indices.</returns>
		public static IEnumerable<T> WithoutIndex<T>(this IEnumerable<EnumerableEntry.WithIndex<T>> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			return collection.Select(i => i.Value);
		}

		/// <summary>
		/// Updates indices of the current instance.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>The current instance with updated indices.</returns>
		public static IEnumerable<EnumerableEntry.WithIndex<T>> UpdateIndex<T>(this IEnumerable<EnumerableEntry.WithIndex<T>> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			return collection.WithoutIndex().WithIndex();
		}

		/// <summary>
		/// Converts the current instance to a Stack.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="items">The current instance.</param>
		/// <returns>The new Stack.</returns>
		public static Stack<T> ToStack<T>(this IEnumerable<T> items)
		{
#if DOTNET4
			Contract.Requires(items != null);
#endif
			return items.Aggregate(new Stack<T>(), (stack, item) =>
			{
				stack.Push(item);
				return stack;
			});
		}

		/// <summary>
		/// Converts the current instance to a Queue.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="items">The current instance.</param>
		/// <returns>The new Queue.</returns>
		public static Queue<T> ToQueue<T>(this IEnumerable<T> items)
		{
#if DOTNET4
			Contract.Requires(items != null);
#endif
			return items.Aggregate(new Queue<T>(), (stack, item) =>
			{
				stack.Enqueue(item);
				return stack;
			});
		}

		/// <summary>
		/// Returns the item where the selector returns the maximum value.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <typeparam name="TCompare">The type to compare.</typeparam>
		/// <param name="sequence">The current instance.</param>
		/// <param name="selector">The selector.</param>
		/// <returns>The item where the selector returns the maximum value.</returns>
		public static T MaxItem<T, TCompare>(this IEnumerable<T> sequence, Func<T, TCompare> selector) where TCompare : IComparable<TCompare>
		{
#if DOTNET4
			Contract.Requires(sequence != null);
			Contract.Requires(selector != null);
#endif
			using (IEnumerator<T> enumerator = sequence.GetEnumerator())
			{
				enumerator.MoveNext();
				T max = enumerator.Current;
				TCompare maxComparator = selector(max);

				while (enumerator.MoveNext())
				{
					T cur = enumerator.Current;
					TCompare curComparator = selector(cur);

					if (curComparator.CompareTo(maxComparator) > 0)
					{
						max = cur;
						maxComparator = curComparator;
					}
				}

				return max;
			}
		}

		/// <summary>
		/// Returns the item where the selector returns the minimum value.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <typeparam name="TCompare">The type to compare.</typeparam>
		/// <param name="sequence">The current instance.</param>
		/// <param name="selector">The selector.</param>
		/// <returns>The item where the selector returns the minimum value.</returns>
		public static T MinItem<T, TCompare>(this IEnumerable<T> sequence, Func<T, TCompare> selector) where TCompare : IComparable<TCompare>
		{
#if DOTNET4
			Contract.Requires(sequence != null);
			Contract.Requires(selector != null);
#endif
			using (IEnumerator<T> enumerator = sequence.GetEnumerator())
			{
				enumerator.MoveNext();
				T min = enumerator.Current;
				TCompare minComparator = selector(min);

				while (enumerator.MoveNext())
				{
					T cur = enumerator.Current;
					TCompare curComparator = selector(cur);

					if (curComparator.CompareTo(minComparator) < 0)
					{
						min = cur;
						minComparator = curComparator;
					}
				}

				return min;
			}
		}

		/// <summary>
		/// Cycles the items in the current instance.
		/// </summary>
		/// <typeparam name="T">The items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <param name="cache">Whether to cache items returned from the current instance.</param>
		/// <returns>The items in the current instance in a cycle.</returns>
		public static IEnumerable<T> Cycle<T>(this IEnumerable<T> collection, bool cache = true)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			List<T> c = new List<T>();

			foreach (T item in collection)
			{
				if (cache) c.Add(item);
				yield return item;
			}

			while (true)
			{
				if (cache) foreach (T item in c) yield return item;
				else foreach (T item in collection) yield return item;
			}
		}

		/// <summary>
		/// Adds the specified items to the current instance.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <param name="items">The items to add.</param>
		/// <returns>The current instance and the specified items.</returns>
		public static IEnumerable<T> Add<T>(this IEnumerable<T> collection, params T[] items)
		{
#if DOTNET4
			Contract.Requires(collection != null);
			Contract.Requires(items != null);
#endif
			foreach (T item in collection) yield return item;
			foreach (T item in items) yield return item;
		}

		/// <summary>
		/// Split the current instance into groups of the specified size.
		/// </summary>
		/// <typeparam name="T">The type of elements in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <param name="size">The size of each group.</param>
		/// <returns>The current instance split into groups.</returns>
		public static IEnumerable<IEnumerable<T>> GroupsOf<T>(this IEnumerable<T> collection, int size)
		{
#if DOTNET4
			Contract.Requires(collection != null);
			Contract.Requires(size > 0);
#endif
			while (collection.Any())
			{
				yield return collection.Take(size);
				collection = collection.Skip(size);
			}
		}

		/// <summary>
		/// Cast all the items in the current instance to the specified type.
		/// </summary>
		/// <typeparam name="TResult">The type to cast to.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>The current instance with the items cast to the specified type.</returns>
		public static IEnumerable<TResult> CastAll<TResult>(this IEnumerable<object> collection)
		{
#if DOTNET4
			Contract.Requires(collection != null);
#endif
			foreach (var element in collection) yield return (TResult)element;
		}

		/// <summary>
		/// Groups neighbours in the current instance according to the specified predicate.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns>The grouped collection.</returns>
		public static IEnumerable<IEnumerable<T>> GroupNeighbours<T>(this IEnumerable<T> collection, Func<IEnumerable<T>, T, bool> predicate)
		{
#if DOTNET4
			Contract.Requires(collection != null);
			Contract.Requires(predicate != null);
#endif
			using (var e = collection.GetEnumerator())
			{
				if (!e.MoveNext()) yield break;
				List<T> curGroup = new List<T> { e.Current };

				while (e.MoveNext())
				{
					T cur = e.Current;

					if (predicate(curGroup, cur)) curGroup.Add(cur);
					else
					{
						yield return curGroup;
						curGroup = new List<T> { cur };
					}
				}

				yield return curGroup;
			}
		}

		/// <summary>
		/// Flattens the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the items.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>All the items in all the collections in the current instance.</returns>
		public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> collection)
		{
			foreach (IEnumerable<T> coll in collection) foreach (T item in coll) yield return item;
		}
	}
}