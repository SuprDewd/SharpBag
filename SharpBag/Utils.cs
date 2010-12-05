using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

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
        public static long ExecutionTime(Action a, bool handleGc = true)
        {
            Contract.Requires(a != null);
            return ExecutionTime(a, s => s.ElapsedMilliseconds, handleGc);
        }

        /// <summary>
        /// Calculates the execution time of the specified action.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="a">The action.</param>
        /// <param name="result">What to return.</param>
        /// <param name="handleGc">Whether to handle the garbage collector. If true, the GC will be forced to clean up before taking the time.</param>
        /// <returns>The execution time in milliseconds.</returns>
        public static TResult ExecutionTime<TResult>(Action a, Func<Stopwatch, TResult> result, bool handleGc = true)
        {
            Contract.Requires(a != null);
            Contract.Requires(result != null);

            if (handleGc)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            Stopwatch s = new Stopwatch();
            s.Start();

            a();

            s.Stop();
            return result(s);
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

        #region Igor Ostrovsky

        /// <summary>
        /// Returns values that are generated from the generator.
        /// </summary>
        /// <typeparam name="T">The type of what is being generated.</typeparam>
        /// <param name="generator">The main generator.</param>
        /// <returns>Values that are generated from the generator.</returns>
        public static IEnumerable<T> Generate<T>(Func<T> generator) where T : class
        {
            Contract.Requires(generator != null);

            T t;
            while ((t = generator()) != null) yield return t;
        }

        /// <summary>
        /// Returns values that are generated from the generator.
        /// </summary>
        /// <typeparam name="T">The type of what is being generated.</typeparam>
        /// <param name="generator">The main generator.</param>
        /// <returns>Values that are generated from the generator.</returns>
        public static IEnumerable<T> Generate<T>(Func<Nullable<T>> generator) where T : struct
        {
            Contract.Requires(generator != null);

            Nullable<T> t;
            while ((t = generator()).HasValue) yield return t.Value;
        }

        /// <summary>
        /// Converts an enumerator to an enumerable.
        /// </summary>
        /// <typeparam name="T">The type of what is being enumered.</typeparam>
        /// <param name="enumerator">The enumerator to convert.</param>
        /// <returns>The enumerator as an enumerable.</returns>
        public static IEnumerable<T> FromEnumerator<T>(IEnumerator<T> enumerator)
        {
            Contract.Requires(enumerator != null);

            while (enumerator.MoveNext()) yield return enumerator.Current;
        }

        /// <summary>
        /// Converts a single value to an enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The value as an enumerable.</returns>
        public static IEnumerable<T> Single<T>(T value)
        {
            return Enumerable.Repeat(value, 1);
        }

        /// <summary>
        /// Reads a file and converts all the lines read to an enumerable.
        /// </summary>
        /// <param name="path">The location of the file.</param>
        /// <returns>An enumerable that contains all the lines read.</returns>
        public static IEnumerable<string> ReadLinesFromFile(string path)
        {
            Contract.Requires(path != null);
            Contract.Requires(File.Exists(path));

            using (StreamReader file = new StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null) yield return line;
            }
        }

        /// <summary>
        /// Reads a line from the console and returns it as an enumerable.
        /// </summary>
        /// <returns>The line read as an enumerable.</returns>
        public static IEnumerable<string> ReadLinesFromConsole()
        {
            return ReadLinesFrom(Console.In);
        }

        /// <summary>
        /// Creates an enumerable that reads lines from the specified TextReader.
        /// </summary>
        /// <param name="reader">The TextReader to read from.</param>
        /// <returns>An enumerable that reads lines from the specified TextReader.</returns>
        public static IEnumerable<string> ReadLinesFrom(TextReader reader)
        {
            Contract.Requires(reader != null);

            return Generate(reader.ReadLine);
        }

        #endregion Igor Ostrovsky

        /// <summary>
        /// Generates data with the specified data generator.
        /// </summary>
        /// <typeparam name="T">The type of items returned by the generator.</typeparam>
        /// <param name="generator">A data generator.</param>
        /// <returns>An endless source of data from the generator.</returns>
        public static IEnumerable<T> GenerateEndless<T>(Func<T> generator)
        {
            Contract.Requires(generator != null);

            while (true) yield return generator();
        }
    }
}