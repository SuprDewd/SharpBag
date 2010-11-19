using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading;
using System.Numerics;
using SharpBag.Math;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SharpBag
{
    /// <summary>
    /// A static class containing static extension methods for various types.
    /// </summary>
    public static class Extensions
    {
        #region Igor Ostrovsky

        /// <summary>
        /// Performs an action on each element of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="action">The action to perform on each element.</param>
        /// <returns>The current instance.</returns>
        public static IEnumerable<T> Iter<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (T elem in source)
            {
                action(elem);
                yield return elem;
            }
        }

        /// <summary>
        /// Performs an action on each element of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="action">The action to perform on each element.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (T elem in source)
            {
                action(elem);
            }
        }

        #endregion

        #region Yet Another Language Geek

        #region To overloads

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<int> To(this int start, int end, int step = 1)
        {
            var diff = end - start > 0 ? System.Math.Abs(step) : -System.Math.Abs(step);
            for (var current = start; current != end; current += diff)
                yield return current;
            yield return end;
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<long> To(this long start, long end, long step = 1)
        {
            var diff = end - start > 0 ? System.Math.Abs(step) : -System.Math.Abs(step);
            for (var current = start; current != end; current += diff)
                yield return current;
            yield return end;
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<BigInteger> To(this BigInteger start, BigInteger end)
        {
            return start.To(end, BigInteger.One);
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<BigInteger> To(this BigInteger start, BigInteger end, BigInteger step)
        {
            var diff = end - start > 0 ? BigInteger.Abs(step) : -BigInteger.Abs(step);
            for (var current = start; current != end; current += diff)
                yield return current;
            yield return end;
        }

        /// <summary>
        /// Generates chars that range from the current instance to end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The char to end at.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<char> To(this char start, char end)
        {
            var iStart = (int)start;
            var iEnd = (int)end;
            var diff = iEnd - iStart > 0 ? 1 : -1;
            for (var current = iStart; current != iEnd; current += diff)
                yield return (char)current;
        }

        #endregion

        /// <summary>
        /// Executes the specified function N times where N is the value of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the value returned from the function.</typeparam>
        /// <param name="i">The current instance.</param>
        /// <param name="f">The function to execute.</param>
        /// <returns>An enumerable with the returned values of the function.</returns>
        public static IEnumerable<T> Times<T>(this int i, Func<T> f)
        {
            for (int j = 0; j < i; ++j)
                yield return f();
        }

        /// <summary>
        /// Immediately executes the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
        /// <param name="sequence">The current instance.</param>
        public static void Execute<T>(this IEnumerable<T> sequence)
        {
            foreach (var item in sequence) ;
        }

        /// <summary>
        /// Gets the element in the array located at the specified percent.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The current instance.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The element in the array located at the specified percent.</returns>
        public static T GetByPercent<T>(this T[] array, double percent)
        {
            return array[Math.MathExtensions.Round((array.Length - 1) * percent)];
        }

        #endregion

        #region Multidimensional Arrays

        /// <summary>
        /// Returns an enumerable of all the items in the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="multiDArray">The current instance.</param>
        /// <returns>An enumerable of all the items in the current instance.</returns>
        public static IEnumerable<T> AsEnumerable<T>(this T[,] multiDArray)
        {
            for (int i1 = 0; i1 < multiDArray.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < multiDArray.GetLength(1); i2++)
                {
                    yield return multiDArray[i1, i2];
                }
            }
        }

        /// <summary>
        /// Returns an enumerable of all the items in the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="multiDArray">The current instance.</param>
        /// <returns>An enumerable of all the items in the current instance.</returns>
        public static IEnumerable<T> AsEnumerable<T>(this T[, ,] multiDArray)
        {
            for (int i1 = 0; i1 < multiDArray.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < multiDArray.GetLength(1); i2++)
                {
                    for (int i3 = 0; i3 < multiDArray.GetLength(2); i3++)
                    {
                        yield return multiDArray[i1, i2, i3];
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Checks whether the array contains the specified object.
        /// </summary>
        /// <param name="a">The array.</param>
        /// <param name="o">The object.</param>
        /// <returns>Whether the array contains the specified object.</returns>
        public static bool Contains(this Array a, object o)
        {
            foreach (var item in a)
            {
                if (item == o) return true;
            }

            return false;
        }

        /// <summary>
        /// Invokes the specified action if the current object is not null.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>(this T obj, Action<T> action) where T : class
        {
            if (obj != null)
            {
                action(obj);
            }
        }

        /// <summary>
        /// Invokes the specified action if the current object is not null.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>(this T obj, Action action) where T : class
        {
            if (obj != null)
            {
                action();
            }
        }

        /// <summary>
        /// Converts the current instance into an enumerable.
        /// </summary>
        /// <typeparam name="T">The type of elements in the current instance.</typeparam>
        /// <param name="e">The current instance.</param>
        /// <returns>An enumerable.</returns>
        public static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> e)
        {
            while (e.MoveNext())
            {
                yield return e.Current;
            }
        }

        /// <summary>
        /// Converts the current instance into an enumerable.
        /// </summary>
        /// <param name="e">The current instance.</param>
        /// <returns>An enumerable.</returns>
        public static IEnumerable<object> AsEnumerable(this IEnumerator e)
        {
            while (e.MoveNext())
            {
                yield return e.Current;
            }
        }

        /// <summary>
        /// Converts all elements in the current instance using the specified action.
        /// </summary>
        /// <typeparam name="TInput">The type of the input elements.</typeparam>
        /// <typeparam name="TOutput">The type of the output elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="action">The action to perform on each element.</param>
        /// <returns>A new enumerable with the output elements.</returns>
        public static IEnumerable<TOutput> ConvertElements<TInput, TOutput>(this IEnumerable<TInput> source, Func<TInput, TOutput> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (TInput elem in source)
            {
                yield return action(elem);
            }
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// If overwrite is true and the dictionary contains the specified key, the key's value will be overwritten.
        /// If overwrite is false and the dictionary contains the specified key, an exception won't be thrown.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="D">The dictionary.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        /// <param name="overwrite">true if key's value should be overwritten; otherwise false.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> D, TKey key, TValue value, bool overwrite)
        {
            if (D == null) throw new ArgumentNullException("D", "The current instance must not be null.");
            if (key == null) throw new ArgumentNullException("key", "key must not be null.");
            if (D.ContainsKey(key))
            {
                if (overwrite)
                {
                    D[key] = value;
                }
            }
            else
            {
                D.Add(key, value);
            }
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
            for (int i = 0; i < array.Count(); i++)
            {
                array[i] = value;
            }
        }

        /// <summary>
        /// Fills the current array with the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The current instance.</param>
        /// <param name="value">The value to fill the array with.</param>
        public static void Fill<T>(this List<T> array, T value)
        {
            for (int i = 0; i < array.Count(); i++)
            {
                array[i] = value;
            }
        }

        #endregion

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
            for (int i = start; i <= end; i++)
            {
                yield return array.ElementAt(i);
            }
        }

        #region InvokeIfRequired overloads

        /// <summary>
        /// Simple helper extension method to marshall to correct thread if its required.
        /// </summary>
        /// <param name="control">The source control.</param>
        /// <param name="methodcall">The method to call.</param>
        public static void InvokeIfRequired(this DispatcherObject control, Action methodcall)
        {
            control.InvokeIfRequired(methodcall, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Simple helper extension method to marshall to correct thread if its required.
        /// </summary>
        /// <param name="control">The source control.</param>
        /// <param name="methodcall">The method to call.</param>
        /// <param name="priorityForCall">The thread priority.</param>
        public static void InvokeIfRequired(this DispatcherObject control, Action methodcall, DispatcherPriority priorityForCall)
        {
            if (control.Dispatcher.Thread != Thread.CurrentThread)
            {
                control.Dispatcher.Invoke(priorityForCall, methodcall);
            }
            else
            {
                methodcall();
            }
        }

        #endregion

        /// <summary>
        /// Unions all elements in the current instance and the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the collections.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="other">The collection to union.</param>
        /// <returns>The current instance and the specified collection unioned.</returns>
        public static IEnumerable<T> UnionAll<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            foreach (T item in source)
            {
                yield return item;
            }

            foreach (T item in other)
            {
                yield return item;
            }
        }

        #region Add overloads

        /// <summary>
        /// Adds the specified item to the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="newItem">The new item.</param>
        /// <returns>The current instance with the new item.</returns>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> source, T newItem)
        {
            foreach (T item in source)
            {
                yield return item;
            }

            yield return newItem;
        }

        /// <summary>
        /// Adds the specified items to the current instance.
        /// </summary>
        /// <typeparam name="T">The type of items in the current instance.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="newItems">The items to add.</param>
        /// <returns>The current instance and the new items.</returns>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> source, params T[] newItems)
        {
            foreach (T item in source)
            {
                yield return item;
            }

            foreach (T item in newItems)
            {
                yield return item;
            }
        }

        #endregion

        /// <summary>
        /// Gets a subarray from the current instance.
        /// </summary>
        /// <param name="a">The current instance.</param>
        /// <param name="x1">X-coordinate 1.</param>
        /// <param name="y1">Y-coordinate 1.</param>
        /// <param name="x2">X-coordinate 2.</param>
        /// <param name="y2">Y-coordinate 2.</param>
        /// <returns>The subarray.</returns>
        public static int[,] Subarray(this int[,] a, int x1, int y1, int x2, int y2)
        {
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

        #region IsIn overloads

        /// <summary>
        /// Whether the current instance is in the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the current instance.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>Whether the current instance is in the specified collection.</returns>
        public static bool IsIn<T>(T item, IEnumerable<T> collection)
        {
            return collection.Contains(item);
        }

        /// <summary>
        /// Whether the current instance is in the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the current instance.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>Whether the current instance is in the specified collection.</returns>
        public static bool IsIn<T>(T item, params T[] collection)
        {
            return collection.Contains(item);
        }

        #endregion

        #region Random overloads

        /// <summary>
        /// Returns a random item from the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="collection">The current instance.</param>
        /// <param name="rand">A random number generator.</param>
        /// <returns>A random item from  the current instance.</returns>
        public static T Random<T>(this IEnumerable<T> collection)
        {
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
            T[] array = collection.ToArray();

            return array[rand.Next(0, array.Length)];
        }

        #endregion

        /// <summary>
        /// Executes the specified action on the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the current instance.</typeparam>
        /// <param name="obj">The current instance.</param>
        /// <param name="act">An action.</param>
        public static void With<T>(this T obj, Action<T> act) { act(obj); }

        /// <summary>
        /// If the current instance is not null, returns the value returned from selector function, else returns the elseValue.
        /// </summary>
        /// <typeparam name="TIn">The type of the current instance.</typeparam>
        /// <typeparam name="TReturn">The type of the return value.</typeparam>
        /// <param name="obj">The current instance.</param>
        /// <param name="selector">A selector function.</param>
        /// <param name="elseValue">The default value to return.</param>
        /// <returns>If the current instance is not null, returns the value returned from selector function, else returns the elseValue.</returns>
        public static TReturn NullOr<TIn, TReturn>(this TIn obj, Func<TIn, TReturn> selector, TReturn elseValue = default(TReturn)) where TIn : class
        {
            return obj != null ? selector(obj) : elseValue;
        }

        /// <summary>
        /// Whether the current instance is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of items in the current instance.</typeparam>
        /// <param name="collection">The current instance.</param>
        /// <returns>Whether the current instance is null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// Whether the current instance is T.
        /// </summary>
        /// <typeparam name="T">The type to check against.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <returns>Whether the current instance is T.</returns>
        public static bool Is<T>(this object item) where T : class
        {
            return item is T;
        }

        /// <summary>
        /// Whether the current instance is not T.
        /// </summary>
        /// <typeparam name="T">The type to check against.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <returns>Whether the current instance is not T.</returns>
        public static bool IsNot<T>(this object item) where T : class
        {
            return !(item.Is<T>());
        }

        /// <summary>
        /// Returns the current instance as T.
        /// </summary>
        /// <typeparam name="T">The type to return the current instance as.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <returns>The current instance as T.</returns>
        public static T As<T>(this object item) where T : class
        {
            return item as T;
        }

        /// <summary>
        /// Returns an empty enumerable if the current instance is null.
        /// </summary>
        /// <typeparam name="T">The type of items in the current instance.</typeparam>
        /// <param name="pSeq">The current instance.</param>
        /// <returns>An empty enumerable if the current instance is null.</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> pSeq)
        {
            return pSeq ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Converts an the current instance to a dictionary, with it's properties as the keys.
        /// </summary>
        /// <param name="o">The current instance.</param>
        /// <returns>The current instance as a dictionary.</returns>
        public static Dictionary<string, object> ToDictionary(this object o)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var propertyInfo in o.GetType().GetProperties())
            {
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(o, null));
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Converts the current instance to the specified type.
        /// </summary>
        /// <typeparam name="T">Type the current instance will be converted to.</typeparam>
        /// <param name="original">The current instance.</param>
        /// <param name="defaultValue">The default value to use in case the current instance can't be converted.</param>
        /// <returns>The converted value.</returns>
        public static TOut As<TIn, TOut>(this TIn original, TOut defaultValue = default(TOut))
        {
            return As(original, CultureInfo.CurrentCulture, defaultValue);
        }

        /// <summary>
        /// Converts the current instance to the specified type.
        /// </summary>
        /// <typeparam name="T">Type the current instance will be converted to.</typeparam>
        /// <param name="original">The current instance.</param>
        /// <param name="provider">An IFormatProvider.</param>
        /// <param name="defaultValue">The default value to use in case the current instance can't be converted.</param>
        /// <returns>The converted value.</returns>
        public static TOut As<TIn, TOut>(this TIn original, IFormatProvider provider, TOut defaultValue = default(TOut))
        {
            Type type = typeof(TOut);

            if (type.IsNullableType())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            try
            {
                return type.IsEnum && original.Is<string>() ? (TOut)Enum.Parse(type, original.As<string>(), true) : (TOut)Convert.ChangeType(original, type, provider);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns whether or not the specified type is <see cref="Nullable{T}"/>.
        /// </summary>
        /// <param name="type">A <see cref="Type"/>.</param>
        /// <returns>True if the specified type is <see cref="Nullable{T}"/>; otherwise, false.</returns>
        /// <remarks>Use <see cref="Nullable.GetUnderlyingType"/> to access the underlying type.</remarks>
        public static bool IsNullableType(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        static public IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            return ShuffleIterator(source);
        }

        static private IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source)
        {
            T[] array = source.ToArray();
            Random rnd = new Random();
            for (int n = array.Length; n > 1; )
            {
                int k = rnd.Next(n--); // 0 <= k < n

                //Swap items
                if (n != k)
                {
                    T tmp = array[k];
                    array[k] = array[n];
                    array[n] = tmp;
                }
            }

            foreach (var item in array) yield return item;
        }

        public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> enumeration, int step, int start = 0)
        {
            if (enumeration == null) throw new ArgumentNullException("enumeration");

            int first = 0;
            int count = 0;

            foreach (T item in enumeration)
            {
                if (first < start)
                {
                    first++;
                }
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
    }
}