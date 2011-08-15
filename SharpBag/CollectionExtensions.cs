using System;
using System.Collections.Generic;
using System.Linq;



using System.Diagnostics.Contracts;
using System.Collections;
using SharpBag.Collections;



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

			Contract.Requires(array != null);
			Contract.Requires(min >= 0);

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

			Contract.Requires(array != null);
			Contract.Requires(min >= 0);
			Contract.Requires(max >= min);

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

			Contract.Requires(g != null);
			Contract.Requires(!(xDelta == 0 && yDelta == 0));

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

			Contract.Requires(g != null);
			Contract.Requires(!(xDelta == 0 && yDelta == 0));

			int xl = g.GetLength(0), yl = g.GetLength(1);
			while (x >= 0 && y >= 0 && x < xl && y < yl)
			{
				yield return selector(g[x, y], x, y);
				x += xDelta;
				y += yDelta;
			}
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

			Contract.Requires(source != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

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

			Contract.Requires(enumeration != null);
			Contract.Requires(start >= 0 && start < enumeration.Count());
			Contract.Requires(step > 0);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

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

		#region Random overloads

		/// <summary>
		/// Returns a random item from the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the items.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <returns>A random item from  the current instance.</returns>
		public static T Random<T>(this IEnumerable<T> collection)
		{

			Contract.Requires(collection != null);

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

			Contract.Requires(collection != null);
			Contract.Requires(rand != null);

			T[] array = collection.ToArray();
			return array.Length == 0 ? default(T) : array[rand.Next(0, array.Length)];
		}

		#endregion Random overloads

		/// <summary>
		/// Gets a subarray of the current instance.
		/// </summary>
		/// <param name="a">The current instance.</param>
		/// <param name="x0">X-coordinate 1.</param>
		/// <param name="y0">Y-coordinate 1.</param>
		/// <param name="x1">X-coordinate 2.</param>
		/// <param name="y1">Y-coordinate 2.</param>
		/// <returns>The subarray.</returns>
		public static T[,] Subarray<T>(this T[,] a, int x0, int y0, int x1, int y1)
		{

			Contract.Requires(a != null);
			Contract.Requires(x0 <= x1);
			Contract.Requires(y0 <= y1);
			Contract.Requires(x0 >= 0);
			Contract.Requires(x1 >= 0);
			Contract.Requires(y0 >= 0);
			Contract.Requires(y1 >= 0);
			Contract.Requires(x0 < a.GetLength(0));
			Contract.Requires(x1 < a.GetLength(0));
			Contract.Requires(y0 < a.GetLength(1));
			Contract.Requires(y1 < a.GetLength(1));
			Contract.Ensures(Contract.Result<T[,]>() != null);

			T[,] sub = new T[(x1 - x0) + 1, (y1 - y0) + 1];

			for (int x = 0; x < sub.GetLength(0); x++)
			{
				for (int y = 0; y < sub.GetLength(1); y++)
				{
					sub[x, y] = a[x0 + x, y0 + y];
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

			Contract.Requires(e != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

			while (e.MoveNext()) yield return e.Current;
		}



		/// <summary>
		/// Converts the current instance into an enumerable.
		/// </summary>
		/// <param name="e">The current instance.</param>
		/// <returns>An enumerable.</returns>
		public static IEnumerable<object> AsEnumerable(this IEnumerator e)
		{
			Contract.Requires(e != null);
			Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);

			while (e.MoveNext()) yield return e.Current;
		}



		/// <summary>
		/// Gets a subarray of an array.
		/// </summary>
		/// <typeparam name="T">The type of the enumerable.</typeparam>
		/// <param name="c">The array.</param>
		/// <param name="start">The first index.</param>
		/// <param name="end">The last index.</param>
		/// <returns>A subarray of the array.</returns>
		public static IEnumerable<T> Range<T>(this IEnumerable<T> c, int start, int end)
		{

			Contract.Requires(c != null);
			Contract.Requires(start <= end);
			Contract.Requires(start >= 0 && start < c.Count());
			Contract.Requires(end >= 0 && end < c.Count());
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

			using (IEnumerator<T> e = c.GetEnumerator())
			{
				for (int i = 0; i <= end; i++)
				{
					e.MoveNext();
					if (i > start) yield return e.Current;
				}
			}
		}

		/// <summary>
		/// Immediately executes the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx </remarks>
		public static void Execute<T>(this IEnumerable<T> collection)
		{

			Contract.Requires(collection != null);

			foreach (T item in collection) { }
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

			Contract.Requires(multiDArray != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

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

			Contract.Requires(multiDArray != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

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

			Contract.Requires(jagged != null);

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

			Contract.Requires(mArr != null);
			Contract.Requires(func != null);

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

			Contract.Requires(cols > 0);
			Contract.Requires(collection.Count() % cols == 0);

			T[] arr = collection.ToArray();
			int count = arr.Length,
				rows = count / cols,
				row = 0,
				col = 0;

			T[,] outArr = new T[rows, cols];

			for (int i = 0; i < arr.Length; i++)
			{
				outArr[row, col] = arr[i];
				if (++col == cols)
				{
					col = 0;
					row++;
				}
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

			Contract.Requires(arrFirst.GetLength(0) == arrSecond.GetLength(0));
			Contract.Requires(arrFirst.GetLength(1) == arrSecond.GetLength(1));

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

			Contract.Requires(a != null);

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

			Contract.Requires(collection != null);

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

			Contract.Requires(collection != null);

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

			Contract.Requires(collection != null);

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

			Contract.Requires(collection != null);

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

			Contract.Requires(collection != null);

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

			Contract.Requires(collection != null);

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

			Contract.Requires(items != null);

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

			Contract.Requires(items != null);

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

			Contract.Requires(sequence != null);
			Contract.Requires(selector != null);

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

			Contract.Requires(sequence != null);
			Contract.Requires(selector != null);

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

			Contract.Requires(collection != null);

			List<T> c = cache ? new List<T>() : null;

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
		/// Split the current instance into groups of the specified size.
		/// </summary>
		/// <typeparam name="T">The type of elements in the current instance.</typeparam>
		/// <param name="collection">The current instance.</param>
		/// <param name="size">The size of each group.</param>
		/// <returns>The current instance split into groups.</returns>
		public static IEnumerable<IEnumerable<T>> GroupsOf<T>(this IEnumerable<T> collection, int size)
		{

			Contract.Requires(collection != null);
			Contract.Requires(size > 0);

			T[] buffer = new T[size];
			using (IEnumerator<T> enumerator = collection.GetEnumerator())
			{
				int i = 0;

				while (true)
				{
					for (i = 0; i < size && enumerator.MoveNext(); i++)
					{
						buffer[i] = enumerator.Current;
					}

					if (i == 0) break;
					yield return buffer.Take(i);
				}
			}
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

			Contract.Requires(collection != null);
			Contract.Requires(predicate != null);

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

		#region HasIndex

		/// <summary>
		/// Whether the current instance has the specified index.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="array">The current instance.</param>
		/// <param name="i">The index.</param>
		/// <returns>Whether the current instance has the specified index.</returns>
		public static bool HasIndex<T>(this T[] array, int i)
		{
			return i >= 0 && i < array.Length;
		}

		/// <summary>
		/// Whether the current instance has the specified index.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="array">The current instance.</param>
		/// <param name="i">The index.</param>
		/// <param name="j">The index.</param>
		/// <returns>Whether the current instance has the specified index.</returns>
		public static bool HasIndex<T>(this T[,] array, int i, int j)
		{
			return i >= 0 && j >= 0 && i < array.GetLength(0) && j < array.GetLength(1);
		}

		/// <summary>
		/// Whether the current instance has the specified index.
		/// </summary>
		/// <typeparam name="T">The type of items in the current instance.</typeparam>
		/// <param name="array">The current instance.</param>
		/// <param name="i">The index.</param>
		/// <param name="j">The index.</param>
		/// <param name="k">The index.</param>
		/// <returns>Whether the current instance has the specified index.</returns>
		public static bool HasIndex<T>(this T[,] array, int i, int j, int k)
		{
			return i >= 0 && j >= 0 && k >= 0 && i < array.GetLength(0) && j < array.GetLength(1) && k < array.GetLength(2);
		}

		#endregion HasIndex
	}
}