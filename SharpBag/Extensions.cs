using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading;

namespace SharpBag
{
    /// <summary>
    /// A static class containing static extension methods for various types.
    /// </summary>
    public static class Extensions
    {
        #region Igor Ostrovsky

        /// <summary>
        /// Writes lines from the current instance to the specified TextWriter.
        /// </summary>
        /// <typeparam name="T">The type of the lines that will be written.</typeparam>
        /// <param name="lines">The current instance.</param>
        /// <param name="writer">The TextWriter to write to.</param>
        public static void WriteLinesTo<T>(this IEnumerable<T> lines, TextWriter writer)
        {
            if (lines == null) throw new ArgumentNullException("lines");
            if (writer == null) throw new ArgumentNullException("writer");

            lines.ForEach((line) => writer.WriteLine(line.ToString()));
        }

        /// <summary>
        /// Writes lines from the current instance to the console.
        /// </summary>
        /// <typeparam name="T">The type of the lines that will be written.</typeparam>
        /// <param name="lines">The current instance.</param>
        public static void WriteLinesToConsole<T>(this IEnumerable<T> lines)
        {
            lines.WriteLinesTo(Console.Out);
        }

        /// <summary>
        /// Writes lines from the current instance to the specified file.
        /// </summary>
        /// <typeparam name="T">The type of the lines that will be written.</typeparam>
        /// <param name="lines">The current instance.</param>
        /// <param name="path">The location of the file to write to.</param>
        public static void WriteLinesToFile<T>(this IEnumerable<T> lines, string path)
        {
            if (path == null) throw new ArgumentNullException("path");

            using (TextWriter file = new StreamWriter(path))
            {
                lines.WriteLinesTo(file);
            }
        }

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

        /// <summary>
        /// Outputs the enumerable as a pretty string.
        /// </summary>
        /// <typeparam name="T">The type of elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <returns>The current instance as a pretty string.</returns>
        public static string ToStringPretty<T>(this IEnumerable<T> source)
        {
            return ToStringPretty(source, ",");
        }

        /// <summary>
        /// Outputs the enumerable as a pretty string.
        /// </summary>
        /// <typeparam name="T">The type of elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="delimiter">A string to insert in between the elements.</param>
        /// <returns>The current instance as a pretty string.</returns>
        public static string ToStringPretty<T>(this IEnumerable<T> source, string delimiter)
        {
            return ToStringPretty(source, "", delimiter, "");
        }

        /// <summary>
        /// Outputs the enumerable as a pretty string.
        /// </summary>
        /// <typeparam name="T">The type of elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="before">A string to prepend to the output.</param>
        /// <param name="delimiter">A string to insert in between the elements.</param>
        /// <param name="after">A string to append to the output.</param>
        /// <returns>The current instance as a pretty string.</returns>
        public static string ToStringPretty<T>(this IEnumerable<T> source, string before, string delimiter, string after)
        {
            if (source == null) throw new ArgumentNullException("source");

            StringBuilder result = new StringBuilder();
            result.Append(before);

            bool firstElement = true;
            foreach (T elem in source)
            {
                if (firstElement) firstElement = false;
                else result.Append(delimiter);

                result.Append(elem.ToString());
            }

            result.Append(after);
            return result.ToString();
        }

        /// <summary>
        /// Combines the current instance with another enumerable using the specified function.
        /// </summary>
        /// <typeparam name="TIn1">The type of elements in the current instance.</typeparam>
        /// <typeparam name="TIn2">The type of elements in the enumerable to combine with the current instance.</typeparam>
        /// <typeparam name="TOut">The type of elements to return.</typeparam>
        /// <param name="in1">The current instance.</param>
        /// <param name="in2">The enumerable to combine with the current instance.</param>
        /// <param name="func">The function used to combine the two enumerables.</param>
        /// <returns>The current instance combined with the specified enumerable using the specified function.</returns>
        public static IEnumerable<TOut> Combine<TIn1, TIn2, TOut>(this IEnumerable<TIn1> in1, IEnumerable<TIn2> in2, Func<TIn1, TIn2, TOut> func)
        {
            if (in1 == null) throw new ArgumentNullException("in1");
            if (in2 == null) throw new ArgumentNullException("in2");
            if (func == null) throw new ArgumentNullException("func");

            using (var e1 = in1.GetEnumerator())
            using (var e2 = in2.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    yield return func(e1.Current, e2.Current);
                }
            }
        }

        /// <summary>
        /// Shuffle the current instance.
        /// </summary>
        /// <typeparam name="T">The type of elements in the current instance.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <returns>The current instance shuffled.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return Shuffle(source, new Random());
        }

        /// <summary>
        /// Shuffle the current instance.
        /// </summary>
        /// <typeparam name="T">The type of elements in the current instance.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="random">The randomness generator.</param>
        /// <returns>The current instance shuffled.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (random == null) throw new ArgumentNullException("random");

            T[] array = source.ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                int r = random.Next(i + 1);
                T tmp = array[r];
                array[r] = array[i];
                array[i] = tmp;
            }

            return array;
        }

        #endregion

        #region Yet Another Language Geek

        /// <summary>
        /// Converts the current instance to an Int32.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The current instance as an Int32.</returns>
        public static int ToInt(this string s)
        {
            return Convert.ToInt32(s);
        }

        /// <summary>
        /// Converts the current instance to an Int64.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The current instance as an Int64.</returns>
        public static long ToLong(this string s)
        {
            return Convert.ToInt64(s);
        }

        /// <summary>
        /// Converts the current instance to a double.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The current instance as a double.</returns>
        public static double ToDouble(this string s)
        {
            return Convert.ToDouble(s);
        }

        /// <summary>
        /// Converts the current instance to a bool.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The current instance as a bool.</returns>
        public static bool ToBool(this string s)
        {
            return Convert.ToBoolean(s);
        }

        /// <summary>
        /// Generates numbers that range from the value of the current instance to the value of end.
        /// </summary>
        /// <param name="start">The current instance.</param>
        /// <param name="end">The number to end at.</param>
        /// <returns>An enumerable containing the numbers.</returns>
        public static IEnumerable<int> To(this int start, int end)
        {
            var diff = end - start > 0 ? 1 : -1;
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
        public static IEnumerable<long> To(this long start, long end)
        {
            var diff = end - start > 0 ? 1 : -1;
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
        public static IEnumerable<int> To(this int start, int end, int step)
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
        public static IEnumerable<long> To(this long start, long end, int step)
        {
            var diff = end - start > 0 ? System.Math.Abs(step) : -System.Math.Abs(step);
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
        /// Gets an enumerable containing all pixels that are in the specified rectangle on the current instance.
        /// </summary>
        /// <param name="image">The current instance.</param>
        /// <param name="rect">The rectangle in the current instance.</param>
        /// <returns>An enumerable containing all pixels that are in the specified rectangle on the current instance.</returns>
        public static IEnumerable<Color> GetPixels(this Bitmap image, Rectangle rect)
        {
            return from y in rect.Top.To(rect.Bottom)
                   from x in rect.Left.To(rect.Right)
                   select image.GetPixel(x, y);
        }

        /// <summary>
        /// Gets the luminosity of the specified rectangle in the current instance.
        /// </summary>
        /// <param name="image">The current instance.</param>
        /// <param name="rect">The rectangle in the current instance.</param>
        /// <returns>The luminosity of the specified rectangle in the current instance.</returns>
        public static double GetLuminosity(this Bitmap image, Rectangle rect)
        {
            return image.GetPixels(rect).Average(c => .3 * c.R + .59 * c.G + .11 * c.B) / 255;
        }

        /// <summary>
        /// Gets a new rectangle that has the same with and height as the current instance.
        /// </summary>
        /// <param name="image">The current instance.</param>
        /// <returns>A new rectangle that has the same with and height as the current instance.</returns>
        public static Rectangle GetRectangle(this Image image)
        {
            return new Rectangle(0, 0, image.Width, image.Height);
        }

        /// <summary>
        /// Gets the contrast of the current instance.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="contrast">The contrast.</param>
        /// <returns>The contrast.</returns>
        public static double Contrast(this double d, double contrast)
        {
            return Math.MathExtensions.Bound((((d - .5) * contrast) + .5), 0, 1);
        }

        /// <summary>
        /// Gets a subset of rectangles all with the specified width and height from the current instance.
        /// </summary>
        /// <param name="rect">The current instance.</param>
        /// <param name="width">The width of all sub-rectangles.</param>
        /// <param name="height">The height of all sub-rectangles.</param>
        /// <returns>An enumerable with the sub-rectangles.</returns>
        public static IEnumerable<Rectangle> GetSubRectangles(this Rectangle rect, int width, int height)
        {
            var xSize = rect.Width / (double)width;
            var ySize = rect.Height / (double)height;
            return from y in 0.To(height)
                   from x in 0.To(width)
                   let r = CreateRectangle((int)(x * xSize), (int)(y * ySize), (int)((x + 1) * xSize), (int)((y + 1) * ySize))
                   where r.Height > 0 && r.Width > 0
                   select r;
        }

        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="x">The upper left x coordinate.</param>
        /// <param name="y">The upper left y coordinate.</param>
        /// <param name="nextX">The lower right x coordinate.</param>
        /// <param name="nextY">The lower right y coordinate.</param>
        /// <returns>A rectangle.</returns>
        public static Rectangle CreateRectangle(int x, int y, int nextX, int nextY)
        {
            return new Rectangle(x, y, nextX - x, nextY - y);
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
        /// Calculates the edit distance between the current instance and the specified string.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="t">The string to compare to.</param>
        /// <param name="caseSensitive">Whether or not to perform a case sensitive comparison.</param>
        /// <returns>The edit distance between the current instance and the specified string.</returns>
        public static int DistanceTo(this string s, string t, bool caseSensitive)
        {
            if (!caseSensitive)
            {
                s = s.ToLower();
                t = t.ToLower();
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }

        /// <summary>
        /// Calculates the edit distance between the current instance and the specified string.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="t">The string to compare to.</param>
        /// <returns>The edit distance between the current instance and the specified string.</returns>
        public static int DistanceTo(this string s, string t)
        {
            return s.DistanceTo(t, true);
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
        /// Modifies all elements in the current instance using the specified function.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="func">The function to perform on each element.</param>
        /// <returns>A new enumerable containing the ouput elements.</returns>
        public static IEnumerable<T> Modify<T>(this IEnumerable<T> source, Func<T, T> func)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");

            foreach (T elem in source)
            {
                yield return func(elem);
            }
        }

        /// <summary>
        /// Compares the current instance to another string using the specified char array to determine the results.
        /// </summary>
        /// <param name="a">The current instance.</param>
        /// <param name="b">The string to compare to.</param>
        /// <param name="c">The char array.</param>
        /// <returns>Whether the current instance is less than, equal to or greater than the specified string.</returns>
        public static int CompareTo(this string a, string b, char[] c)
        {
            return a.CompareTo(b, c, false);
        }

        /// <summary>
        /// Compares the current instance to another string using the specified char array to determine the results.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="b">The string to compare to.</param>
        /// <param name="c">The char array.</param>
        /// <param name="caseSensitive">Whether or not the comparison is case-sensitive.</param>
        /// <returns>Whether the current instance is less than, equal to or greater than the specified string.</returns>
        public static int CompareTo(this string s, string b, char[] c, bool caseSensitive)
        {
            string a = s;
            if (!caseSensitive)
            {
                a = a.ToLower();
                b = b.ToLower();

                for (int i = 0; i < c.Length; i++)
                {
                    c[i] = Convert.ToChar(c[i].ToString().ToLower());
                }
            }

            if (a == b) return 0;

            for (int i = 0; i < (a.Length > b.Length ? a.Length : b.Length); i++)
            {
                int r = a[i].CompareTo(b[i]);
                if (r == 0) continue;
                return r;
            }

            return 0;
        }

        /// <summary>
        /// Compares the current instance to another char using the specified char array to determine the results.
        /// </summary>
        /// <param name="a">The current instance.</param>
        /// <param name="b">The char to compare to.</param>
        /// <param name="c">The char array.</param>
        /// <returns>Whether the current instance is less than, equal to or greater than the specified char.</returns>
        public static int CompareTo(this char a, char b, char[] c)
        {
            if (a == b) return 0;

            if (!c.Contains(a) || !c.Contains(b))
            {
                return ((int)a).CompareTo((int)b);
            }
            else
            {
                for (int i = 0; i < c.Length; i++)
                {
                    if (c[i] == a) return -1;
                    if (c[i] == b) return 1;
                }

                return ((int)a).CompareTo((int)b);
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

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string repeatedly until the new string no longer contains the specified string.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of oldValue are repeatedly replaced with newValue until the new string no longer contains oldValue.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static string ReplaceAll(this string s, string oldValue, string newValue)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            if (oldValue == null) throw new ArgumentNullException("oldValue", "oldValue must not be null.");
            if (newValue == null) throw new ArgumentNullException("newValue", "newValue must not be null.");
            if (newValue.Contains(oldValue)) throw new ArgumentException("newValue cannot contain oldValue. This will result in an endless loop.");

            string tS = s;
            while (tS.Contains(oldValue))
            {
                tS = tS.Replace(oldValue, newValue);
            }
            return tS;
        }

        /// <summary>
        /// Returns all the words in the string.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>All the words in the string.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string[] Words(this string s)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            return s.Split(new char[] { ' ', '.', ',', '?' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns all the lines in the string.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>All the lines in the string.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string[] Lines(this string s)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            return s.NoCarriageReturns().Split('\n');
        }

        /// <summary>
        /// Takes the string and removes all carriage returns ('\r').
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The string without carriage returns ('\r').</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string NoCarriageReturns(this string s)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            return s.Replace("\r", "");
        }

        /// <summary>
        /// Takes the string, replaces all line breaks with a space, then replaces all double spaces with a space and finally trims the string.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The string in one line, with no double spaces, trimmed.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string OneLineNoDoubleSpaceTrimmed(this string s)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            return s.NoCarriageReturns().ReplaceAll("\n", " ").ReplaceAll("  ", " ").Trim();
        }

        /// <summary>
        /// Returns a copy of this System.Char converted to uppercase, using the casing rules of the current culture.
        /// </summary>
        /// <param name="c">The current instance.</param>
        /// <returns>A copy of this System.Char converted to uppercase.</returns>
        public static char ToUpper(this char c)
        {
            return Convert.ToChar(c.ToString().ToUpper());
        }

        /// <summary>
        /// Returns a copy of this System.Char converted to lowercase, using the casing rules of the current culture.
        /// </summary>
        /// <param name="c">The current instance.</param>
        /// <returns>A copy of this System.Char converted to lowercase.</returns>
        public static char ToLower(this char c)
        {
            return Convert.ToChar(c.ToLower().ToUpper());
        }

        #region Split overloads.

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by the specified string. A parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="separator">A string that delimits the substrings in this string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited by the separator.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string[] Split(this string s, string separator)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            if (separator == null) throw new ArgumentNullException("separator", "separator must not be null.");
            return s.Split(separator, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by the specified string. A parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="separator">A string that delimits the substrings in this string.</param>
        /// <param name="options">RemoveEmptyEntries to omit empty array elements from the array returned; or None to include empty array elements in the array returned.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited by the separator.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string[] Split(this string s, string separator, StringSplitOptions options)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            if (separator == null) throw new ArgumentNullException("separator", "separator must not be null.");
            return s.Split(new string[] { separator }, options);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by the specified char. A parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="separator">A char that delimits the substrings in this string.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited by the separator.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string[] Split(this string s, char separator)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            return s.Split(separator, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by the specified char. A parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="separator">A char that delimits the substrings in this string.</param>
        /// <param name="options">RemoveEmptyEntries to omit empty array elements from the array returned; or None to include empty array elements in the array returned.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited by the separator.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string[] Split(this string s, char separator, StringSplitOptions options)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            return s.Split(new char[] { separator }, options);
        }

        #endregion

        /// <summary>
        /// Splits the current string into substrings using the separator and then converts each substring into an int.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="separator">The separator used to split the string into ints.</param>
        /// <returns>An array of the ints.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static int[] SplitIntoInts(this string s, string separator)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            if (separator == null) throw new ArgumentNullException("separator", "separator must not be null.");
            List<int> ints = new List<int>();

            foreach (string i in s.Split(separator, StringSplitOptions.None))
            {
                ints.Add(Convert.ToInt32(i.Trim()));
            }

            return ints.ToArray();
        }

        /// <summary>
        /// Splits the current string into substrings using the separator and then converts each substring into an int.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="separator">The separator used to split the string into ints.</param>
        /// <returns>An array of the ints.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static int[] SplitIntoInts(this string s, char separator)
        {
            if (s == null) throw new ArgumentNullException("s", "The current instance must not be null.");
            List<int> ints = new List<int>();

            foreach (string i in s.Split(separator, StringSplitOptions.None))
            {
                ints.Add(Convert.ToInt32(i.Trim()));
            }

            return ints.ToArray();
        }

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

        /// <summary>
        /// Performs a function on each element of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="function">The function to perform on each element.</param>
        /// <returns>The current instance.</returns>
        public static IEnumerable<T> Map<T>(this IEnumerable<T> source, Func<T, T> function)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (function == null) throw new ArgumentNullException("function");

            foreach (T elem in source)
            {
                yield return function(elem);
            }
        }
    }
}