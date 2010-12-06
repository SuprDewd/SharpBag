﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Windows.Threading;

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
            Contract.Requires(source != null);
            Contract.Requires(action != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

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
            Contract.Requires(source != null);
            Contract.Requires(action != null);

            foreach (T elem in source)
            {
                action(elem);
            }
        }

        #endregion Igor Ostrovsky

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
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<int>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<int>>().Any());

            if (start < end)
            {
                for (int i = start; i <= end; i += step)
                {
                    yield return i;
                }
            }
            else if (start > end)
            {
                for (int i = start; i >= end; i -= step)
                {
                    yield return i;
                }
            }
            else yield return end;
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
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<long>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<long>>().Any());

            if (start < end)
            {
                for (long i = start; i <= end; i += step)
                {
                    yield return i;
                }
            }
            else if (start > end)
            {
                for (long i = start; i >= end; i -= step)
                {
                    yield return i;
                }
            }
            else yield return end;
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<BigInteger> To(this BigInteger start, BigInteger end)
        {
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>().Any());

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
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<BigInteger>>().Any());

            if (start < end)
            {
                for (BigInteger i = start; i <= end; i += step)
                {
                    yield return i;
                }
            }
            else if (start > end)
            {
                for (BigInteger i = start; i >= end; i -= step)
                {
                    yield return i;
                }
            }
            else yield return end;
        }

        /// <summary>
        /// Generates chars that range from the current instance to end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The char to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<char> To(this char start, char end, int step = 1)
        {
            Contract.Requires(step > 0);
            Contract.Ensures(Contract.Result<IEnumerable<char>>() != null);
            Contract.Ensures(Contract.Result<IEnumerable<char>>().Any());

            if (start < end)
            {
                for (int i = start; i <= end; i += step)
                {
                    yield return (char)i;
                }
            }
            else if (start > end)
            {
                for (int i = start; i >= end; i -= step)
                {
                    yield return (char)i;
                }
            }
            else yield return end;
        }

        #endregion To overloads

        /// <summary>
        /// Executes the specified function N times where N is the value of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the value returned from the function.</typeparam>
        /// <param name="i">The current instance.</param>
        /// <param name="f">The function to execute.</param>
        /// <returns>An enumerable with the returned values of the function.</returns>
        public static IEnumerable<T> Times<T>(this int i, Func<T> f)
        {
            Contract.Requires(i >= 0);
            Contract.Requires(f != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            for (int j = 0; j < i; j++) yield return f();
        }

        /// <summary>
        /// Immediately executes the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
        /// <param name="sequence">The current instance.</param>
        public static void Execute<T>(this IEnumerable<T> sequence)
        {
            Contract.Requires(sequence != null);
            foreach (T item in sequence) { }
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
            Contract.Requires(array != null);
            Contract.Requires(array.Length > 0);
            Contract.Requires(percent >= 0 && percent <= 1);

            return array[Math.MathExtensions.Round((array.Length - 1) * percent)];
        }

        #endregion Yet Another Language Geek

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
            Contract.Requires(multiDArray != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

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

        #endregion Multidimensional Arrays

        /// <summary>
        /// Checks whether the array contains the specified object.
        /// </summary>
        /// <param name="a">The array.</param>
        /// <param name="o">The object.</param>
        /// <returns>Whether the array contains the specified object.</returns>
        public static bool ContainsArray(this Array a, object o)
        {
            Contract.Requires(a != null);

            return a.Cast<object>().Any(item => item == o);
        }

        /// <summary>
        /// Invokes the specified action if the current object is not null.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>(this T obj, Action<T> action) where T : class
        {
            Contract.Requires(action != null);

            if (obj != null) action(obj);
        }

        /// <summary>
        /// Invokes the specified action if the current object is not null.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>(this T obj, Action action) where T : class
        {
            Contract.Requires(action != null);

            if (obj != null) action();
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
            Contract.Requires(e != null);
            Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);

            while (e.MoveNext())
            {
                yield return e.Current;
            }
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
            Contract.Requires(d != null);

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
            Contract.Requires(array != null);

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
            Contract.Requires(array != null);

            for (int i = 0; i < array.Count(); i++)
            {
                array[i] = value;
            }
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
            Contract.Requires(array != null);
            Contract.Requires(start <= end);
            Contract.Requires(start >= 0 && start < array.Count());
            Contract.Requires(end >= 0 && end < array.Count());
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

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
            Contract.Requires(control != null);
            Contract.Requires(methodcall != null);

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
            Contract.Requires(control != null);
            Contract.Requires(methodcall != null);

            if (control.Dispatcher.Thread != Thread.CurrentThread) control.Dispatcher.Invoke(priorityForCall, methodcall);
            else methodcall();
        }

        #endregion InvokeIfRequired overloads

        #region UnionAll overloads

        /// <summary>
        /// Unions all elements in the current instance and the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the collections.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="other">The collection to union.</param>
        /// <returns>The current instance and the specified collection unioned.</returns>
        public static IEnumerable<T> UnionAll<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            Contract.Requires(source != null);
            Contract.Requires(other != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            foreach (T item in source)
            {
                yield return item;
            }

            foreach (T item in other)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Adds the specified item to the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="newItem">The new item.</param>
        /// <returns>The current instance with the new item.</returns>
        public static IEnumerable<T> UnionAll<T>(this IEnumerable<T> source, T newItem)
        {
            Contract.Requires(source != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

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
        public static IEnumerable<T> UnionAll<T>(this IEnumerable<T> source, params T[] newItems)
        {
            Contract.Requires(source != null);
            Contract.Requires(newItems != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            foreach (T item in source)
            {
                yield return item;
            }

            foreach (T item in newItems)
            {
                yield return item;
            }
        }

        #endregion UnionAll overloads

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
        public static bool IsIn<T>(this T item, IEnumerable<T> collection)
        {
            Contract.Requires(collection != null);
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
            Contract.Requires(collection != null);
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
        /// Executes the specified action on the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the current instance.</typeparam>
        /// <param name="obj">The current instance.</param>
        /// <param name="act">An action.</param>
        public static void With<T>(this T obj, Action<T> act) { Contract.Requires(act != null); act(obj); }

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
            Contract.Requires(selector != null);
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
        public static T CastAs<T>(this object item) where T : class
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
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
            return pSeq ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Converts an the current instance to a dictionary, with it's properties as the keys.
        /// </summary>
        /// <param name="o">The current instance.</param>
        /// <returns>The current instance as a dictionary.</returns>
        public static Dictionary<string, object> ToDictionary(this object o)
        {
            Contract.Requires(o != null);
            Contract.Ensures(Contract.Result<Dictionary<string, object>>() != null);

            return o.GetType().GetProperties().Where(propertyInfo => propertyInfo.GetIndexParameters().Length == 0).ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.GetValue(o, null));
        }

        /// <summary>
        /// Converts the current instance to the specified type.
        /// </summary>
        /// <typeparam name="TOut">Type the current instance will be converted to.</typeparam>
        /// <param name="original">The current instance.</param>
        /// <param name="defaultValue">The default value to use in case the current instance can't be converted.</param>
        /// <returns>The converted value.</returns>
        public static TOut As<TOut>(this object original, TOut defaultValue = default(TOut))
        {
            return original.As(CultureInfo.CurrentCulture, defaultValue);
        }

        /// <summary>
        /// Converts the current instance to the specified type.
        /// </summary>
        /// <typeparam name="TOut">Type the current instance will be converted to.</typeparam>
        /// <param name="original">The current instance.</param>
        /// <param name="provider">An IFormatProvider.</param>
        /// <param name="defaultValue">The default value to use in case the current instance can't be converted.</param>
        /// <returns>The converted value.</returns>
        public static TOut As<TOut>(this object original, IFormatProvider provider, TOut defaultValue = default(TOut))
        {
            Contract.Requires(provider != null);

            Type type = typeof(TOut);
            if (type.IsNullableType()) type = Nullable.GetUnderlyingType(type);

            try
            {
                return type.IsEnum && original.Is<string>() ? (TOut)Enum.Parse(type, original.As<string>(), true) : (TOut)Convert.ChangeType(original, type, provider);
            }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Returns whether or not the specified type is <see cref="Nullable{T}"/>.
        /// </summary>
        /// <param name="type">A <see cref="Type"/>.</param>
        /// <returns>True if the specified type is <see cref="Nullable{T}"/>; otherwise, false.</returns>
        /// <remarks>Use <see cref="Nullable.GetUnderlyingType"/> to access the underlying type.</remarks>
        public static bool IsNullableType(this Type type)
        {
            Contract.Requires(type != null);
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
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
            return ShuffleIterator(source, rand);
        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rand = null)
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

        /// <summary>
        /// Returns the current instance if the specified expression is true, else returns the specified default value.
        /// </summary>
        /// <typeparam name="T">The type of the current instance.</typeparam>
        /// <param name="obj">The current instance.</param>
        /// <param name="expression">An expression.</param>
        /// <param name="def">The default value.</param>
        /// <returns>The current instance if the specified expression is true, else returns the specified default value.</returns>
        public static T If<T>(this T obj, bool expression, T def = default(T))
        {
            return expression ? obj : def;
        }
    }
}