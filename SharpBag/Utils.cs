using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;

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
            Contract.Requires(a != null);

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
            Contract.Requires(objects != null);

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

        #region Sort

        /// <summary>
        /// Sort a few variables.
        /// </summary>
        /// <typeparam name="T">The type of the variables.</typeparam>
        /// <param name="value1">A variable.</param>
        /// <param name="value2">A variable.</param>
        public static void Sort<T>(ref T value1, ref T value2) where T : IComparable<T>
        {
            if (value1.CompareTo(value2) > 0) Utils.Swap(ref value1, ref value2);
        }

        /// <summary>
        /// Sort a few variables.
        /// </summary>
        /// <typeparam name="T">The type of the variables.</typeparam>
        /// <param name="value1">A variable.</param>
        /// <param name="value2">A variable.</param>
        /// <param name="value3">A variable.</param>
        public static void Sort<T>(ref T value1, ref T value2, ref T value3) where T : IComparable<T>
        {
            if (value1.CompareTo(value2) > 0) Utils.Swap(ref value1, ref value2);
            if (value1.CompareTo(value3) > 0) Utils.Swap(ref value1, ref value3);
            Utils.Sort(ref value2, ref value3);
        }

        /// <summary>
        /// Sort a few variables.
        /// </summary>
        /// <typeparam name="T">The type of the variables.</typeparam>
        /// <param name="value1">A variable.</param>
        /// <param name="value2">A variable.</param>
        /// <param name="value3">A variable.</param>
        /// <param name="value4">A variable.</param>
        public static void Sort<T>(ref T value1, ref T value2, ref T value3, ref T value4) where T : IComparable<T>
        {
            if (value1.CompareTo(value2) > 0) Utils.Swap(ref value1, ref value2);
            if (value1.CompareTo(value3) > 0) Utils.Swap(ref value1, ref value3);
            if (value1.CompareTo(value4) > 0) Utils.Swap(ref value1, ref value4);
            Utils.Sort(ref value2, ref value3, ref value4);
        }

        /// <summary>
        /// Sort a few variables.
        /// </summary>
        /// <typeparam name="T">The type of the variables.</typeparam>
        /// <param name="value1">A variable.</param>
        /// <param name="value2">A variable.</param>
        /// <param name="value3">A variable.</param>
        /// <param name="value4">A variable.</param>
        /// <param name="value5">A variable.</param>
        public static void Sort<T>(ref T value1, ref T value2, ref T value3, ref T value4, ref T value5) where T : IComparable<T>
        {
            if (value1.CompareTo(value2) > 0) Utils.Swap(ref value1, ref value2);
            if (value1.CompareTo(value3) > 0) Utils.Swap(ref value1, ref value3);
            if (value1.CompareTo(value4) > 0) Utils.Swap(ref value1, ref value4);
            if (value1.CompareTo(value5) > 0) Utils.Swap(ref value1, ref value5);
            Utils.Sort(ref value2, ref value3, ref value4, ref value5);
        }

        /// <summary>
        /// Sort a few variables.
        /// </summary>
        /// <typeparam name="T">The type of the variables.</typeparam>
        /// <param name="value1">A variable.</param>
        /// <param name="value2">A variable.</param>
        /// <param name="value3">A variable.</param>
        /// <param name="value4">A variable.</param>
        /// <param name="value5">A variable.</param>
        /// <param name="value6">A variable.</param>
        public static void Sort<T>(ref T value1, ref T value2, ref T value3, ref T value4, ref T value5, ref T value6) where T : IComparable<T>
        {
            if (value1.CompareTo(value2) > 0) Utils.Swap(ref value1, ref value2);
            if (value1.CompareTo(value3) > 0) Utils.Swap(ref value1, ref value3);
            if (value1.CompareTo(value4) > 0) Utils.Swap(ref value1, ref value4);
            if (value1.CompareTo(value5) > 0) Utils.Swap(ref value1, ref value5);
            if (value1.CompareTo(value6) > 0) Utils.Swap(ref value1, ref value6);
            Utils.Sort(ref value2, ref value3, ref value4, ref value5, ref value6);
        }

        #endregion Sort

        #region Hash

        /// <summary>
        /// Hashes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>The hash.</returns>
        public static int Hash(params object[] items)
        {
            if (items.Length == 0) return 0;
            int hash = 23;
            for (int i = 0; i < items.Length; i++) hash = hash * 31 + items[i].GetHashCode();
            return hash;
        }

        /// <summary>
        /// Hashes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>The hash.</returns>
        public static int Hash(IEnumerable<object> items)
        {
            int hash = 23;
            bool any = false;

            foreach (var item in items)
            {
                any = true;
                hash = hash * 31 + item.GetHashCode();
            }

            return any ? hash : 0;
        }

        /// <summary>
        /// Hashes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>The hash.</returns>
        public static int Hash(IEnumerable items)
        {
            int hash = 23;
            bool any = false;

            foreach (var item in items)
            {
                any = true;
                hash = hash * 31 + item.GetHashCode();
            }

            return any ? hash : 0;
        }

        /// <summary>
        /// Hashes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>The hash.</returns>
        public static int Hash(object[,] items)
        {
            if (items.GetLength(0) == 0 || items.GetLength(1) == 0) return 0;

            int hash = 23;
            for (int i = 0; i < items.GetLength(0); i++)
            {
                for (int j = 0; j < items.GetLength(1); j++)
                {
                    hash = hash * 31 + items[i, j].GetHashCode();
                }
            }

            return hash;
        }

        #endregion Hash
    }
}