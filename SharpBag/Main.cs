using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Collections;

namespace SharpBag
{
    /// <summary>
    /// A static class containing static extension methods for various types.
    /// </summary>
    public static class BagExtensions
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
        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
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
            return (((d - .5) * contrast) + .5).Bound(0, 1);
        }

        /// <summary>
        /// Rounds the current instance.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <returns>The current instance rounded.</returns>
        public static int Round(this double d)
        {
            return (int)Math.Round(d);
        }

        /// <summary>
        /// Rounds the current instance.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="digits">Number of digits to keep after the comma.</param>
        /// <returns>The current instance rounded.</returns>
        public static double Round(this double d, int digits)
        {
            return Math.Round(d, digits);
        }

        /// <summary>
        /// Gets the current instance inside the specified boundaries.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="lower">The lower boundary.</param>
        /// <param name="upper">The upper boundary.</param>
        /// <returns>The current instance inside the spcified boundaries.</returns>
        public static int Bound(this int d, int lower, int upper)
        {
            if (d < lower) return lower;
            if (d > upper) return upper;
            return d;
        }

        /// <summary>
        /// Gets the current instance inside the specified boundaries.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="lower">The lower boundary.</param>
        /// <param name="upper">The upper boundary.</param>
        /// <returns>The current instance inside the spcified boundaries.</returns>
        public static double Bound(this double d, double lower, double upper)
        {
            if (d < lower) return lower;
            if (d > upper) return upper;
            return d;
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
            return array[((array.Length - 1) * percent).Round()];
        }

        #endregion

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
        /// <typeparam name="T">The type of elements in the current instance.</typeparam>
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

                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
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
        /// Makes the current instance HTML safe.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>An HTML safe string.</returns>
        public static string HtmlSafe(this string s)
        {
            return s.HtmlSafe(false, false);
        }

        /// <summary>
        /// Makes the current instance HTML safe.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="all">Whether to make all characters entities or just those needed.</param>
        /// <returns>An HTML safe string.</returns>
        public static string HtmlSafe(this string s, bool all)
        {
            return s.HtmlSafe(all, false);
        }

        /// <summary>
        /// Makes the current instance HTML safe.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="all">Whether to make all characters entities or just those needed.</param>
        /// <param name="replace">Whether or not to encode spaces and line breaks.</param>
        /// <returns>An HTML safe string.</returns>
        public static string HtmlSafe(this string s, bool all, bool replace)
        {
            int[] entities = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 28, 29, 30, 31, 34, 39, 38, 60, 62, 123, 124, 125, 126, 127, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 215, 247, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 8704, 8706, 8707, 8709, 8711, 8712, 8713, 8715, 8719, 8721, 8722, 8727, 8730, 8733, 8734, 8736, 8743, 8744, 8745, 8746, 8747, 8756, 8764, 8773, 8776, 8800, 8801, 8804, 8805, 8834, 8835, 8836, 8838, 8839, 8853, 8855, 8869, 8901, 913, 914, 915, 916, 917, 918, 919, 920, 921, 922, 923, 924, 925, 926, 927, 928, 929, 931, 932, 933, 934, 935, 936, 937, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955, 956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968, 969, 977, 978, 982, 338, 339, 352, 353, 376, 402, 710, 732, 8194, 8195, 8201, 8204, 8205, 8206, 8207, 8211, 8212, 8216, 8217, 8218, 8220, 8221, 8222, 8224, 8225, 8226, 8230, 8240, 8242, 8243, 8249, 8250, 8254, 8364, 8482, 8592, 8593, 8594, 8595, 8596, 8629, 8968, 8969, 8970, 8971, 9674, 9824, 9827, 9829, 9830 };
            string ut = "";
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (all || entities.Contains(c))
                {
                    ut += "&#" + ((int)c).ToString() + ";";
                }
                else
                {
                    ut += c.ToString();
                }
            }
            return (replace ? ut.Replace("\r\n", "<br />").Replace("\n", "<br />").Replace(" ", "&nbsp;") : ut);
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
        /// Downloads the source of the Uri and returns it as a string.
        /// </summary>
        /// <param name="page">The source of the Uri.</param>
        /// <returns></returns>
        public static string Download(this Uri page)
        {
            WebRequest request = HttpWebRequest.Create(page);
            string results = null;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    results = sr.ReadToEnd();
                    sr.Close();
                }
                response.Close();
            }
            return results;
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
        /// Checks if the current instance is between, but not equal to, two integers.
        /// </summary>
        /// <param name="n">The current integers.</param>
        /// <param name="min">The lower boundary.</param>
        /// <param name="max">The upper boundary.</param>
        /// <returns>True if the current instance is between, but not equal to, the two integers; otherwise false.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static bool IsBetween(this int n, int min, int max)
        {
            if (min > max) throw new ArgumentException("min must not be greater than max.");
            return (n > min && n < max);
        }

        /// <summary>
        /// Checks if the current instance is between or equal to two integers.
        /// </summary>
        /// <param name="n">The current integers.</param>
        /// <param name="min">The minimum integer.</param>
        /// <param name="max">The maximum integer.</param>
        /// <returns>True if the current instance is between or equal to the two integers; otherwise false.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static bool IsBetweenOrEqualTo(this int n, int min, int max)
        {
            if (min > max) throw new ArgumentException("min must not be greater than max");
            return (n >= min && n <= max);
        }

        /*
        /// <summary>
        /// Writes the string into the specified file.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="filename">The location of the file to be written to.</param>
        public static void WriteIntoFile(this string s, string filename)
        {
            s.WriteIntoFile(filename, false);
        }

        /// <summary>
        /// Writes the string into the specified file.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="filename">The location of the file to be written to.</param>
        /// <param name="append">Wheter or not the string will be appended to the file.</param>
        public static void WriteIntoFile(this string s, string filename, bool append)
        {
            if (append)
            {
                File.AppendAllText(filename, s, Encoding.UTF8);
            }
            else
            {
                File.WriteAllText(filename, s, Encoding.UTF8);
            }
        }
         * */

        /// <summary>
        /// Converts a DateTime object into a SQL compatible string.
        /// </summary>
        /// <param name="dt">The current instance.</param>
        /// <returns>An SQL formatted string.</returns>
        public static string ToSQLDateTime(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Generates a SQL insert query for the current DataTable instance.
        /// </summary>
        /// <param name="dt">The current instance.</param>
        /// <param name="schema">The schema to insert into.</param>
        /// <returns>An SQL string.</returns>
        public static string ToSQL(this DataTable dt, string schema)
        {
            if (dt.Rows.Count == 0) return null;
            string into = schema != null ? schema + "." + dt.TableName : dt.TableName;

            StringBuilder columns = new StringBuilder();
            List<string> nonPrimaryCols = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                if (columns.ToString() != "") columns.Append(",");
                if (!dt.PrimaryKey.Contains(col)) nonPrimaryCols.Add(col.ColumnName);

                columns.Append(col.ColumnName);
            }

            StringBuilder values = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                values.Append(values.ToString() == "" ? "(" : ",(");
                foreach (DataColumn col in dt.Columns)
                {
                    if (dt.Columns.IndexOf(col) != 0) values.Append(",");
                    object o = row[col.ColumnName];
                    if (col.DataType == typeof(DBNull))
                    {
                        values.Append("NULL");
                    }
                    else if (col.DataType == typeof(DateTime))
                    {
                        values.Append("'" + ((DateTime)o).ToSQLDateTime() + "'");
                    }
                    else if (new Type[] { typeof(int), typeof(double), typeof(float), typeof(decimal), typeof(Single) }.Contains(col.DataType))
                    {
                        values.Append(o.ToString());
                    }
                    else
                    {
                        values.Append("'" + o.ToString().SQLEscape() + "'");
                    }
                }
                values.Append(")");
            }

            StringBuilder updCols = new StringBuilder();
            foreach (string nCol in nonPrimaryCols)
            {
                if (updCols.ToString() != "") updCols.Append(",");
                updCols.Append(nCol + "=VALUES(" + nCol + ")");
            }

            return "INSERT INTO " + into + " (" + columns.ToString() + ") VALUES " + values.ToString() + (dt.PrimaryKey.Length == 0 && dt.Columns.Count - dt.PrimaryKey.Length > 0 ? "" : " ON DUPLICATE KEY UPDATE " + updCols.ToString()) + ";";
        }

        /// <summary>
        /// Inserts the current DataTable instance into the specified MySQL database.
        /// </summary>
        /// <param name="dt">The current instance.</param>
        /// <param name="db">The MySQL database to insert into.</param>
        /// <returns>How many rows were affected.</returns>
        public static int InsertInto(this DataTable dt, BagDatabase.BagDB db)
        {
            return db.Execute(dt.ToSQL(db.Schema));
        }

        /// <summary>
        /// Escapes the string for SQL insertion.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The escaped string.</returns>
        public static string SQLEscape(this string s)
        {
            return MySqlHelper.EscapeString(s);
            //return s.Replace(@"\", @"\\").Replace(@"\'", "'").Replace(@"'", @"\'");
        }
    }

    /// <summary>
    /// A static class containing settings and other related data.
    /// </summary>
    public static class BagS
    {
        /// <summary>
        /// Gets the newline string defined for this environment.
        /// </summary>
        public static string NL = Environment.NewLine;

        /// <summary>
        /// BitTorrent tacker error codes and what they mean.
        /// </summary>
        public static Dictionary<int, string> BitTorrentTrackerErrorCodes = new Dictionary<int, string>()
        {
            {100, "Invalid request type: client request was not a HTTP GET."},
            {101, "Missing info_hash."},
            {102, "Missing peer_id."},
            {103, "Missing port."},
            {150, "Invalid infohash: infohash is not 20 bytes long."},
            {151, "Invalid peer_id: peer_id is not 20 bytes long."},
            {152, "Invalid numwant. Client requested more peers than allowed by tracker."},
            {200, "info_hash not found in the database."},
            {500, "Client sent an eventless request before the specified time."},
            {900, "Generic error."}
        };

        /// <summary>
        /// Http status codes and what they mean.
        /// </summary>
        public static Dictionary<int, string> HttpStatusCodes = new Dictionary<int, string>()
        {
            {100, "Continue"},
            {101, "Switching Protocols"},
            {102, "Processing"},

            {200, "OK"},
            {201, "Created"},
            {202, "Accepted"},
            {203, "Non-Authoritative Information"},
            {204, "No Content"},
            {205, "Reset Content"},
            {206, "Partial Content"},
            {207, "Multi-Status"},

            {300, "Multiple Choices"},
            {301, "Moved Permanently"},
            {302, "Found"},
            {303, "See Other"},
            {304, "Not Modified"},
            {305, "Use Proxy"},
            {306, "Switch Proxy"},
            {307, "Temporary Redirect"},

            {400, "Bad Request"},
            {401, "Unauthorized"},
            {402, "Payment Required"},
            {403, "Forbidden"},
            {404, "Not Found"},
            {405, "Method Not Allowed"},
            {406, "Not Acceptable"},
            {407, "Proxy Authentication Required"},
            {408, "Request Timeout"},
            {409, "Conflict"},
            {410, "Gone"},
            {411, "Length Required"},
            {412, "Precondition Failed"},
            {413, "Request Entity Too Large"},
            {414, "Request-URI Too Long"},
            {415, "Unsupported Media Type"},
            {416, "Requested Range Not Satisfiable"},
            {417, "Expectation Failed"},
            {418, "I'm a teapot"}, // FTW!
            {422, "Unprocessable Entity"},
            {423, "Locked"},
            {424, "Failed Dependency"},
            {425, "Unordered Collection"},
            {426, "Upgrade Required"},
            {449, "Retry With"},
            {450, "Blocked by Windows Parental Controls"},

            {500, "Internal Server Error"},
            {501, "Not Implemented"},
            {502, "Bad Gateway"},
            {503, "Service Unavailable"},
            {504, "Gateway Timeout"},
            {505, "HTTP Version Not Supported"},
            {506, "Variant Also Negotiates"},
            {507, "Insufficient Storage"},
            {509, "Bandwidth Limit Exceeded"},
            {510, "Not Extended"}
        };
    }

    /// <summary>
    /// A static class with utility methods.
    /// </summary>
    public static class BagUtils
    {
        /// <summary>
        /// Calculates the execution time of the specified action.
        /// </summary>
        /// <param name="a">The action.</param>
        /// <returns>The execution time in milliseconds.</returns>
        public static long ExecutionTime(Action a)
        {
            return ExecutionTime(a, s => s.ElapsedMilliseconds);
        }

        /// <summary>
        /// Calculates the execution time of the specified action.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="a">The action.</param>
        /// <param name="result">What to return.</param>
        /// <returns>The execution time in milliseconds.</returns>
        public static TResult ExecutionTime<TResult>(Action a, Func<Stopwatch, TResult> result)
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            a();

            s.Stop();
            return result(s);
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
            if (generator == null) throw new ArgumentNullException("generator");

            T t;
            while ((t = generator()) != null)
            {
                yield return t;
            }
        }

        /// <summary>
        /// Returns values that are generated from the generator.
        /// </summary>
        /// <typeparam name="T">The type of what is being generated.</typeparam>
        /// <param name="generator">The main generator.</param>
        /// <returns>Values that are generated from the generator.</returns>
        public static IEnumerable<T> Generate<T>(Func<Nullable<T>> generator) where T : struct
        {
            if (generator == null) throw new ArgumentNullException("generator");

            Nullable<T> t;
            while ((t = generator()).HasValue)
            {
                yield return t.Value;
            }
        }

        /// <summary>
        /// Converts an enumerator to an enumerable.
        /// </summary>
        /// <typeparam name="T">The type of what is being enumered.</typeparam>
        /// <param name="enumerator">The enumerator to convert.</param>
        /// <returns>The enumerator as an enumerable.</returns>
        public static IEnumerable<T> FromEnumerator<T>(IEnumerator<T> enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException("enumerator");

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
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
            if (path == null) throw new ArgumentNullException("path");
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
            if (reader == null) throw new ArgumentNullException("reader");

            return Generate(() => reader.ReadLine());
        }

        #endregion
    }

    /// <summary>
    /// Makes an action disposable.
    /// </summary>
    public class ActionDisposable : IDisposable
    {
        Action action;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="action">The action to execute when the current instance is disposed.</param>
        public ActionDisposable(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// The disposer which executes the dispose action.
        /// </summary>
        void IDisposable.Dispose()
        {
            action();
        }
    }

    namespace BagMath
    {
        /// <summary>
        /// A static class containing methods for doing calculations. 
        /// </summary>
        public static class BagMath
        {
            #region PointsInCircleF & PointsInCircle & all overloads
            /// <summary>
            /// Find points in a circle with a specified radius.
            /// </summary>
            /// <param name="Radius">The radius of the circle.</param>
            /// <returns>An array containing points.</returns>
            /// <exception cref="System.ArgumentException"></exception>
            public static PointF[] PointsInCircleF(int Radius)
            {
                if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
                return PointsInCircleF(Radius, new PointF(Radius, Radius), 360);
            }

            /// <summary>
            /// Find points in a circle with a specified radius.
            /// </summary>
            /// <param name="Radius">The radius of the circle.</param>
            /// <param name="Center">The center-point of the circle.</param>
            /// <returns>An array containing points.</returns>
            /// <exception cref="System.ArgumentNullException"></exception>
            /// <exception cref="System.ArgumentException"></exception>
            public static PointF[] PointsInCircleF(int Radius, PointF Center)
            {
                if (Center == null) throw new ArgumentNullException("Center", "Center must not be null.");
                if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
                return PointsInCircleF(Radius, Center, 360);
            }

            /// <summary>
            /// Find points in a circle with a specified radius.
            /// </summary>
            /// <param name="Radius">The radius of the circle.</param>
            /// <param name="Points">Number of points to return.</param>
            /// <returns>An array containing points.</returns>
            /// <exception cref="System.ArgumentException"></exception>
            public static PointF[] PointsInCircleF(int Radius, int Points)
            {
                if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
                if (Points < 0) throw new ArgumentException("Points must be greater than or equal to 0.");
                return PointsInCircleF(Radius, new PointF(Radius, Radius), Points);
            }

            /// <summary>
            /// Find points in a circle with a specified radius.
            /// </summary>
            /// <param name="Radius">The radius of the circle.</param>
            /// <param name="Center">The center-point of the circle.</param>
            /// <param name="Points">Number of points to return.</param>
            /// <returns>An array containing points.</returns>
            /// <exception cref="System.ArgumentException"></exception>
            public static PointF[] PointsInCircleF(int Radius, PointF Center, int Points)
            {
                if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
                if (Points < 0) throw new ArgumentException("Points must be greater than or equal to 0.");
                PointF[] PointArray = new PointF[Points];
                for (int i = 0; i < Points; i++)
                {
                    PointArray[i] = new PointF((float)(Center.X + Radius * Math.Cos(2 * Math.PI * i / Points)), (float)(Center.Y + Radius * Math.Sin(2 * Math.PI * i / Points)));
                }
                return PointArray;
            }

            /// <summary>
            /// Find points in a circle with a specified radius.
            /// </summary>
            /// <param name="Radius">The radius of the circle.</param>
            /// <returns>An array containing points.</returns>
            /// <exception cref="System.ArgumentException"></exception>
            public static Point[] PointsInCircle(int Radius)
            {
                if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
                return PointsInCircle(Radius, new Point(Radius, Radius), 360);
            }

            /// <summary>
            /// Find points in a circle with a specified radius.
            /// </summary>
            /// <param name="Radius">The radius of the circle.</param>
            /// <param name="Center">The center-point of the circle.</param>
            /// <returns>An array containing points.</returns>
            /// <exception cref="System.ArgumentNullException"></exception>
            /// <exception cref="System.ArgumentException"></exception>
            public static Point[] PointsInCircle(int Radius, Point Center)
            {
                if (Center == null) throw new ArgumentNullException("Center", "Center must not be null.");
                if (Radius < 0) throw new ArgumentException("Radius must be greater than 0.");
                return PointsInCircle(Radius, Center, 360);
            }

            /// <summary>
            /// Find points in a circle with a specified radius.
            /// </summary>
            /// <param name="Radius">The radius of the circle.</param>
            /// <param name="Points">Number of points to return.</param>
            /// <returns>An array containing points.</returns>
            /// <exception cref="System.ArgumentException"></exception>
            public static Point[] PointsInCircle(int Radius, int Points)
            {
                if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
                if (Points < 0) throw new ArgumentException("Points must be greater than or equal to 0.");
                return PointsInCircle(Radius, new Point(Radius, Radius), Points);
            }

            /// <summary>
            /// Find points in a circle with a specified radius.
            /// </summary>
            /// <param name="Radius">The radius of the circle.</param>
            /// <param name="Center">The center-point of the circle.</param>
            /// <param name="Points">Number of points to return.</param>
            /// <returns>An array containing points.</returns>
            /// <exception cref="System.ArgumentNullException"></exception>
            /// <exception cref="System.ArgumentException"></exception>
            public static Point[] PointsInCircle(int Radius, Point Center, int Points)
            {
                if (Center == null) throw new ArgumentNullException("Center", "Center must not be null.");
                if (Radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
                if (Points < 0) throw new ArgumentException("Points must be greater than or equal to 0.");
                Point[] PointArray = new Point[Points];
                for (int i = 0; i < Points; i++)
                {
                    PointArray[i] = new Point((int)Math.Round(Center.X + Radius * Math.Cos(2 * Math.PI * i / Points)), (int)Math.Round(Center.Y + Radius * Math.Sin(2 * Math.PI * i / Points)));
                }
                return PointArray;
            }
            #endregion

            /// <summary>
            /// Calculates wave length from the average fluctuation time.
            /// </summary>
            /// <param name="Tz">The average fluctuation time (Tz).</param>
            /// <returns>The length of the wave.</returns>
            public static double WaveLength(double Tz)
            {
                return (Tz * Tz) * 1.56;
            }

            /// <summary>
            /// Finds the greatest common divisor (gcd) of two integers.
            /// </summary>
            /// <param name="a">An integer.</param>
            /// <param name="b">An integer.</param>
            /// <returns>The greatest commond divisor (gcd) of the two integers.</returns>
            public static int Gcd(int a, int b)
            {
                return GreatestCommonDivisor(a, b);
            }

            /// <summary>
            /// Finds the greatest common divisor (gcd) of two integers.
            /// </summary>
            /// <param name="a">An integer.</param>
            /// <param name="b">An integer.</param>
            /// <returns>The greatest commond divisor (gcd) of the two integers.</returns>
            private static int GreatestCommonDivisor(int a, int b)
            {
                while (true)
                {
                    if (a % b == 0) return b;

                    int c = a % b;
                    a = b;
                    b = c;
                }
            }

            /// <summary>
            /// Checks whether a number is a prime number or not.
            /// </summary>
            /// <param name="candidate">The number to test.</param>
            /// <returns>Whether the number is a prime number or not.</returns>
            public static bool IsPrime(int candidate)
            {
                if ((candidate & 1) == 0)
                {
                    if (candidate == 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                for (int i = 3; (i * i) <= candidate; i += 2)
                {
                    if ((candidate % i) == 0)
                    {
                        return false;
                    }
                }
                return candidate != 1;
            }
        }

        /// <summary>
        /// A class for computing fibonacci numbers.
        /// </summary>
        public class Fibonacci
        {
            /// <summary>
            /// An endless source that will return fibonacci numbers.
            /// </summary>
            public IEnumerable<int> Numbers
            {
                get
                {
                    int a = 0;
                    int b = 1;
                    int t = 0;

                    yield return a;
                    yield return b;

                    while (true)
                    {
                        yield return t = a + b;
                        a = b;
                        b = t;
                    }
                }
            }

            /// <summary>
            /// An endless source that will return fibonacci numbers.
            /// </summary>
            public IEnumerable<long> LongNumbers
            {
                get
                {
                    long a = 0;
                    long b = 1;
                    long t = 0;

                    yield return a;
                    yield return b;

                    while (true)
                    {
                        yield return t = a + b;
                        a = b;
                        b = t;
                    }
                }
            }
        }

        namespace BagConverters
        {
            /// <summary>
            /// Converts numbers between bases.
            /// </summary>
            public static class BaseConverter
            {
                /// <summary>
                /// Converts the specified number from the specified start base to the specified target base.
                /// </summary>
                /// <param name="number">The number as a string.</param>
                /// <param name="start_base">The base of the number.</param>
                /// <param name="target_base">The base to convert to.</param>
                /// <returns>The specified number in the specified target base.</returns>
                public static string ToBase(string number, int start_base, int target_base)
                {

                    int base10 = ToBase10(number, start_base);
                    string rtn = FromBase10(base10, target_base);
                    return rtn;

                }

                /// <summary>
                /// Converts the specified number from the specified start base to base 10.
                /// </summary>
                /// <param name="number">The number as a string.</param>
                /// <param name="start_base">The base of the number.</param>
                /// <returns>The number in base 10.</returns>
                public static int ToBase10(string number, int start_base)
                {

                    if (start_base < 2 || start_base > 36) return 0;
                    if (start_base == 10) return Convert.ToInt32(number);

                    char[] chrs = number.ToCharArray();
                    int m = chrs.Length - 1;
                    int n = start_base;
                    int x;
                    int rtn = 0;

                    foreach (char c in chrs)
                    {

                        if (char.IsNumber(c))
                            x = int.Parse(c.ToString());
                        else
                            x = Convert.ToInt32(c) - 55;

                        rtn += x * (Convert.ToInt32(Math.Pow(n, m)));

                        m--;

                    }

                    return rtn;
                }

                /// <summary>
                /// Converts the specified base 10 number to the specified target base.
                /// </summary>
                /// <param name="number">The base 10 number.</param>
                /// <param name="target_base">The target base.</param>
                /// <returns>The target in the target base.</returns>
                public static string FromBase10(int number, int target_base)
                {

                    if (target_base < 2 || target_base > 36) return "";
                    if (target_base == 10) return number.ToString();

                    int n = target_base;
                    int q = number;
                    int r;
                    string rtn = "";

                    while (q >= n)
                    {

                        r = q % n;
                        q = q / n;

                        if (r < 10)
                            rtn = r.ToString() + rtn;
                        else
                            rtn = Convert.ToChar(r + 55).ToString() + rtn;

                    }

                    if (q < 10)
                        rtn = q.ToString() + rtn;
                    else
                        rtn = Convert.ToChar(q + 55).ToString() + rtn;

                    return rtn;
                }
            }

            // TODO: Finna leið til að minnka kóðann.

            #region Temperature

            /// <summary>
            /// An interface for temperatures.
            /// </summary>
            public interface Temperature
            {
                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                string ToString(bool unit);
                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                string ToString(bool unit, Func<double, string> result);
            }

            /// <summary>
            /// The Celsius temperature unit.
            /// </summary>
            public class Celsius : Temperature
            {
                /// <summary>
                /// The Celsius unit.
                /// </summary>
                public const string Unit = "°C";
                private double Value { get; set; }

                /// <summary>
                /// The Celsius temperature unit.
                /// </summary>
                /// <param name="v">The value.</param>
                public Celsius(double v)
                {
                    this.Value = v;
                }

                /// <summary>
                /// An implicit conversion from double to Celsius.
                /// </summary>
                /// <param name="v">The double value.</param>
                /// <returns>The double value as Celsius.</returns>
                public static implicit operator Celsius(double v)
                {
                    return new Celsius(v);
                }

                /// <summary>
                /// An implicit conversion from Celsius to double.
                /// </summary>
                /// <param name="v">The Celsius value.</param>
                /// <returns>The Celsius value as a double.</returns>
                public static implicit operator double(Celsius v)
                {
                    return v.Value;
                }

                /// <summary>
                /// An explicit conversion from Kelvin to Celsius.
                /// </summary>
                /// <param name="v">The Kelvin value.</param>
                /// <returns>The Kelvin value as Celsius.</returns>
                public static explicit operator Celsius(Kelvin v)
                {
                    return new Celsius(v - 273.15D);
                }

                /// <summary>
                /// An explicit conversion from Fahrenheit to Celsius.
                /// </summary>
                /// <param name="v">The Fahrenheit value.</param>
                /// <returns>The Fahrenheit value as Celsius.</returns>
                public static explicit operator Celsius(Fahrenheit v)
                {
                    return new Celsius((v - 32D) * (5D / 9D));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Celsius.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Celsius.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Celsius.Unit : "");
                }
            }

            /// <summary>
            /// The Fahrenheit temperature unit.
            /// </summary>
            public class Fahrenheit : Temperature
            {
                /// <summary>
                /// The Fahrenheit unit.
                /// </summary>
                public const string Unit = "°F";
                private double Value { get; set; }

                /// <summary>
                /// The Fahrenheit temperature unit.
                /// </summary>
                /// <param name="v">The value.</param>
                public Fahrenheit(double v)
                {
                    this.Value = v;
                }

                /// <summary>
                /// An implicit conversion from double to Fahrenheit.
                /// </summary>
                /// <param name="v">The double value.</param>
                /// <returns>The double value as Fahrenheit.</returns>
                public static implicit operator Fahrenheit(double v)
                {
                    return new Fahrenheit(v);
                }

                /// <summary>
                /// An implicit conversion from Fahrenheit to double.
                /// </summary>
                /// <param name="v">The Fahrenheit value.</param>
                /// <returns>The Fahrenheit value as a double.</returns>
                public static implicit operator double(Fahrenheit v)
                {
                    return v.Value;
                }

                /// <summary>
                /// An explicit conversion from Kelvin to Fahrenheit.
                /// </summary>
                /// <param name="v">The Kelvin value.</param>
                /// <returns>The Kelvin value as Fahrenheit.</returns>
                public static explicit operator Fahrenheit(Kelvin v)
                {
                    return new Fahrenheit(v * (9D / 5D) - 459.67D);
                }

                /// <summary>
                /// An explicit conversion from Celsius to Fahrenheit.
                /// </summary>
                /// <param name="v">The Celsius value.</param>
                /// <returns>The Celsius value as Fahrenheit.</returns>
                public static explicit operator Fahrenheit(Celsius v)
                {
                    return new Fahrenheit(v * (9D / 5D) + 32D);
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Fahrenheit.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Fahrenheit.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Fahrenheit.Unit : "");
                }
            }

            /// <summary>
            /// The Kelvin temperature unit.
            /// </summary>
            public class Kelvin : Temperature
            {
                /// <summary>
                /// The Kelvin unit.
                /// </summary>
                public const string Unit = "K";
                private double Value { get; set; }

                /// <summary>
                /// The Kelvin temperature unit.
                /// </summary>
                /// <param name="v">The value.</param>
                public Kelvin(double v)
                {
                    this.Value = v;
                }

                /// <summary>
                /// An implicit conversion from double to Kelvin.
                /// </summary>
                /// <param name="v">The double value.</param>
                /// <returns>The double value as Kelvin.</returns>
                public static implicit operator Kelvin(double v)
                {
                    return new Kelvin(v);
                }

                /// <summary>
                /// An implicit conversion from Kelvin to double.
                /// </summary>
                /// <param name="v">The Kelvin value.</param>
                /// <returns>The Kelvin value as a double.</returns>
                public static implicit operator double(Kelvin v)
                {
                    return v.Value;
                }

                /// <summary>
                /// An explicit conversion from Fahrenheit to Kelvin.
                /// </summary>
                /// <param name="v">The Fahrenheit value.</param>
                /// <returns>The Fahrenheit value as Kelvin.</returns>
                public static explicit operator Kelvin(Fahrenheit v)
                {
                    return new Kelvin((v + 459.67D) * (5D / 9D));
                }

                /// <summary>
                /// An explicit conversion from Celsius to Kelvin.
                /// </summary>
                /// <param name="v">The Celsius value.</param>
                /// <returns>The Celsius value as Kelvin.</returns>
                public static explicit operator Kelvin(Celsius v)
                {
                    return new Kelvin(v + 273.15D);
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Kelvin.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Kelvin.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Kelvin.Unit : "");
                }
            }

            #endregion

            #region Length

            /// <summary>
            /// An interface for length.
            /// </summary>
            public interface Length
            {
                string ToString(bool unit);
                string ToString(bool unit, Func<double, string> result);
            }

            public class Millimeter : Length
            {
                public const string Unit = "mm";
                public const double BaseValue = 1D;
                private double Value { get; set; }

                public Millimeter(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Millimeter(double v)
                {
                    return new Millimeter(v);
                }

                public static implicit operator double(Millimeter v)
                {
                    return v.Value;
                }

                public static explicit operator Millimeter(Centimeter v)
                {
                    return new Millimeter(v * (Centimeter.BaseValue / Millimeter.BaseValue));
                }

                public static explicit operator Millimeter(Meter v)
                {
                    return new Millimeter(v * (Meter.BaseValue / Millimeter.BaseValue));
                }

                public static explicit operator Millimeter(Kilometer v)
                {
                    return new Millimeter(v * (Kilometer.BaseValue / Millimeter.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Millimeter.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Millimeter.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Millimeter.Unit : "");
                }
            }

            public class Centimeter : Length
            {
                public const string Unit = "cm";
                public const double BaseValue = 10D;
                private double Value { get; set; }

                public Centimeter(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Centimeter(double v)
                {
                    return new Centimeter(v);
                }

                public static implicit operator double(Centimeter v)
                {
                    return v.Value;
                }

                public static explicit operator Centimeter(Millimeter v)
                {
                    return new Centimeter(v * (Millimeter.BaseValue / Centimeter.BaseValue));
                }

                public static explicit operator Centimeter(Meter v)
                {
                    return new Centimeter(v * (Meter.BaseValue / Centimeter.BaseValue));
                }

                public static explicit operator Centimeter(Kilometer v)
                {
                    return new Centimeter(v * (Kilometer.BaseValue / Centimeter.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Centimeter.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Centimeter.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Centimeter.Unit : "");
                }
            }

            public class Meter : Length
            {
                public const string Unit = "m";
                public const double BaseValue = 1000D;
                private double Value { get; set; }

                public Meter(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Meter(double v)
                {
                    return new Meter(v);
                }

                public static implicit operator double(Meter v)
                {
                    return v.Value;
                }

                public static explicit operator Meter(Millimeter v)
                {
                    return new Meter(v * (Millimeter.BaseValue / Meter.BaseValue));
                }

                public static explicit operator Meter(Centimeter v)
                {
                    return new Meter(v * (Centimeter.BaseValue / Meter.BaseValue));
                }

                public static explicit operator Meter(Kilometer v)
                {
                    return new Meter(v * (Kilometer.BaseValue / Meter.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Meter.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Meter.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Meter.Unit : "");
                }
            }

            public class Kilometer : Length
            {
                public const string Unit = "km";
                public const double BaseValue = 1000000D;
                private double Value { get; set; }

                public Kilometer(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Kilometer(double v)
                {
                    return new Kilometer(v);
                }

                public static implicit operator double(Kilometer v)
                {
                    return v.Value;
                }

                public static explicit operator Kilometer(Millimeter v)
                {
                    return new Kilometer(v * (Millimeter.BaseValue / Kilometer.BaseValue));
                }

                public static explicit operator Kilometer(Centimeter v)
                {
                    return new Kilometer(v * (Centimeter.BaseValue / Kilometer.BaseValue));
                }

                public static explicit operator Kilometer(Meter v)
                {
                    return new Kilometer(v * (Meter.BaseValue / Kilometer.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Kilometer.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Kilometer.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Kilometer.Unit : "");
                }
            }

            #endregion

            #region Weight

            /// <summary>
            /// An interface for weight.
            /// </summary>
            public interface Weight
            {
                string ToString(bool unit);
                string ToString(bool unit, Func<double, string> result);
            }

            public class Milligram : Weight
            {
                public const string Unit = "mg";
                public const double BaseValue = 1D;
                private double Value { get; set; }

                public Milligram(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Milligram(double v)
                {
                    return new Milligram(v);
                }

                public static implicit operator double(Milligram v)
                {
                    return v.Value;
                }

                public static explicit operator Milligram(Gram v)
                {
                    return new Milligram(v * (Gram.BaseValue / Milligram.BaseValue));
                }

                public static explicit operator Milligram(Kilogram v)
                {
                    return new Milligram(v * (Kilogram.BaseValue / Milligram.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Milligram.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Milligram.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Milligram.Unit : "");
                }
            }

            public class Gram : Weight
            {
                public const string Unit = "g";
                public const double BaseValue = 1000D;
                private double Value { get; set; }

                public Gram(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Gram(double v)
                {
                    return new Gram(v);
                }

                public static implicit operator double(Gram v)
                {
                    return v.Value;
                }

                public static explicit operator Gram(Milligram v)
                {
                    return new Gram(v * (Milligram.BaseValue / Gram.BaseValue));
                }

                public static explicit operator Gram(Kilogram v)
                {
                    return new Gram(v * (Kilogram.BaseValue / Gram.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Gram.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Gram.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Gram.Unit : "");
                }
            }

            public class Kilogram : Weight
            {
                public const string Unit = "kg";
                public const double BaseValue = 1000000D;
                private double Value { get; set; }

                public Kilogram(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Kilogram(double v)
                {
                    return new Kilogram(v);
                }

                public static implicit operator double(Kilogram v)
                {
                    return v.Value;
                }

                public static explicit operator Kilogram(Milligram v)
                {
                    return new Kilogram(v * (Milligram.BaseValue / Kilogram.BaseValue));
                }

                public static explicit operator Kilogram(Gram v)
                {
                    return new Kilogram(v * (Gram.BaseValue / Kilogram.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Kilogram.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Kilogram.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return result(this.Value) + (unit ? " " + Kilogram.Unit : "");
                }
            }

            #endregion

            #region Bit & Byte

            /// <summary>
            /// An interface for bit and byte size.
            /// </summary>
            public interface BitSize
            {
                /// <see cref="Object.ToString()"/>
                string ToString(bool unit);
                /// <see cref="Object.ToString()"/>
                string ToString(bool unit, Func<double, string> result);
            }

            /// <summary>
            /// A class representing a bit.
            /// </summary>
            public class Bit : BitSize
            {
                public const string Unit = "b";
                public const double BaseValue = 1D;
                private double Value { get; set; }

                public Bit(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Bit(double v)
                {
                    return new Bit(v);
                }

                public static implicit operator double(Bit v)
                {
                    return v.Value;
                }

                public static explicit operator Bit(Kilobit v)
                {
                    return new Bit(v * (Kilobit.BaseValue / Bit.BaseValue));
                }

                public static explicit operator Bit(Megabit v)
                {
                    return new Bit(v * (Megabit.BaseValue / Bit.BaseValue));
                }

                public static explicit operator Bit(Gigabit v)
                {
                    return new Bit(v * (Gigabit.BaseValue / Bit.BaseValue));
                }

                public static explicit operator Bit(Terabit v)
                {
                    return new Bit(v * (Terabit.BaseValue / Bit.BaseValue));
                }

                public static explicit operator Bit(Byte v)
                {
                    return new Bit(v * (Byte.BaseValue / Bit.BaseValue));
                }

                public static explicit operator Bit(Kilobyte v)
                {
                    return new Bit(v * (Kilobyte.BaseValue / Bit.BaseValue));
                }

                public static explicit operator Bit(Megabyte v)
                {
                    return new Bit(v * (Megabyte.BaseValue / Bit.BaseValue));
                }

                public static explicit operator Bit(Gigabyte v)
                {
                    return new Bit(v * (Gigabyte.BaseValue / Bit.BaseValue));
                }

                public static explicit operator Bit(Terabyte v)
                {
                    return new Bit(v * (Terabyte.BaseValue / Bit.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Bit.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Bit.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Bit.Unit : "");
                }
            }

            public class Kilobit : BitSize
            {
                public const string Unit = "Kb";
                public const double BaseValue = 1024D;
                private double Value { get; set; }

                public Kilobit(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Kilobit(double v)
                {
                    return new Kilobit(v);
                }

                public static implicit operator double(Kilobit v)
                {
                    return v.Value;
                }

                public static explicit operator Kilobit(Bit v)
                {
                    return new Kilobit(v * (Bit.BaseValue / Kilobit.BaseValue));
                }

                public static explicit operator Kilobit(Megabit v)
                {
                    return new Kilobit(v * (Megabit.BaseValue / Kilobit.BaseValue));
                }

                public static explicit operator Kilobit(Gigabit v)
                {
                    return new Kilobit(v * (Gigabit.BaseValue / Kilobit.BaseValue));
                }

                public static explicit operator Kilobit(Terabit v)
                {
                    return new Kilobit(v * (Terabit.BaseValue / Kilobit.BaseValue));
                }

                public static explicit operator Kilobit(Byte v)
                {
                    return new Kilobit(v * (Byte.BaseValue / Kilobit.BaseValue));
                }

                public static explicit operator Kilobit(Kilobyte v)
                {
                    return new Kilobit(v * (Kilobyte.BaseValue / Kilobit.BaseValue));
                }

                public static explicit operator Kilobit(Megabyte v)
                {
                    return new Kilobit(v * (Megabyte.BaseValue / Kilobit.BaseValue));
                }

                public static explicit operator Kilobit(Gigabyte v)
                {
                    return new Kilobit(v * (Gigabyte.BaseValue / Kilobit.BaseValue));
                }

                public static explicit operator Kilobit(Terabyte v)
                {
                    return new Kilobit(v * (Terabyte.BaseValue / Kilobit.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Kilobit.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Kilobit.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Kilobit.Unit : "");
                }
            }

            public class Megabit : BitSize
            {
                public const string Unit = "Mb";
                public const double BaseValue = 1048576D;
                private double Value { get; set; }

                public Megabit(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Megabit(double v)
                {
                    return new Megabit(v);
                }

                public static implicit operator double(Megabit v)
                {
                    return v.Value;
                }

                public static explicit operator Megabit(Bit v)
                {
                    return new Megabit(v * (Bit.BaseValue / Megabit.BaseValue));
                }

                public static explicit operator Megabit(Kilobit v)
                {
                    return new Megabit(v * (Kilobit.BaseValue / Megabit.BaseValue));
                }

                public static explicit operator Megabit(Gigabit v)
                {
                    return new Megabit(v * (Gigabit.BaseValue / Megabit.BaseValue));
                }

                public static explicit operator Megabit(Terabit v)
                {
                    return new Megabit(v * (Terabit.BaseValue / Megabit.BaseValue));
                }

                public static explicit operator Megabit(Byte v)
                {
                    return new Megabit(v * (Byte.BaseValue / Megabit.BaseValue));
                }

                public static explicit operator Megabit(Kilobyte v)
                {
                    return new Megabit(v * (Kilobyte.BaseValue / Megabit.BaseValue));
                }

                public static explicit operator Megabit(Megabyte v)
                {
                    return new Megabit(v * (Megabyte.BaseValue / Megabit.BaseValue));
                }

                public static explicit operator Megabit(Gigabyte v)
                {
                    return new Megabit(v * (Gigabyte.BaseValue / Megabit.BaseValue));
                }

                public static explicit operator Megabit(Terabyte v)
                {
                    return new Megabit(v * (Terabyte.BaseValue / Megabit.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Megabit.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Megabit.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Megabit.Unit : "");
                }
            }

            public class Gigabit : BitSize
            {
                public const string Unit = "Gb";
                public const double BaseValue = 1073741824D;
                private double Value { get; set; }

                public Gigabit(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Gigabit(double v)
                {
                    return new Gigabit(v);
                }

                public static implicit operator double(Gigabit v)
                {
                    return v.Value;
                }

                public static explicit operator Gigabit(Bit v)
                {
                    return new Gigabit(v * (Bit.BaseValue / Gigabit.BaseValue));
                }

                public static explicit operator Gigabit(Kilobit v)
                {
                    return new Gigabit(v * (Kilobit.BaseValue / Gigabit.BaseValue));
                }

                public static explicit operator Gigabit(Megabit v)
                {
                    return new Gigabit(v * (Megabit.BaseValue / Gigabit.BaseValue));
                }

                public static explicit operator Gigabit(Terabit v)
                {
                    return new Gigabit(v * (Terabit.BaseValue / Gigabit.BaseValue));
                }

                public static explicit operator Gigabit(Byte v)
                {
                    return new Gigabit(v * (Byte.BaseValue / Gigabit.BaseValue));
                }

                public static explicit operator Gigabit(Kilobyte v)
                {
                    return new Gigabit(v * (Kilobyte.BaseValue / Gigabit.BaseValue));
                }

                public static explicit operator Gigabit(Megabyte v)
                {
                    return new Gigabit(v * (Megabyte.BaseValue / Gigabit.BaseValue));
                }

                public static explicit operator Gigabit(Gigabyte v)
                {
                    return new Gigabit(v * (Gigabyte.BaseValue / Gigabit.BaseValue));
                }

                public static explicit operator Gigabit(Terabyte v)
                {
                    return new Gigabit(v * (Terabyte.BaseValue / Gigabit.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Gigabit.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Gigabit.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Gigabit.Unit : "");
                }
            }

            public class Terabit : BitSize
            {
                public const string Unit = "Tb";
                public const double BaseValue = 1099511627776D;
                private double Value { get; set; }

                public Terabit(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Terabit(double v)
                {
                    return new Terabit(v);
                }

                public static implicit operator double(Terabit v)
                {
                    return v.Value;
                }

                public static explicit operator Terabit(Bit v)
                {
                    return new Terabit(v * (Bit.BaseValue / Terabit.BaseValue));
                }

                public static explicit operator Terabit(Kilobit v)
                {
                    return new Terabit(v * (Kilobit.BaseValue / Terabit.BaseValue));
                }

                public static explicit operator Terabit(Megabit v)
                {
                    return new Terabit(v * (Megabit.BaseValue / Terabit.BaseValue));
                }

                public static explicit operator Terabit(Gigabit v)
                {
                    return new Terabit(v * (Gigabit.BaseValue / Terabit.BaseValue));
                }

                public static explicit operator Terabit(Byte v)
                {
                    return new Terabit(v * (Byte.BaseValue / Terabit.BaseValue));
                }

                public static explicit operator Terabit(Kilobyte v)
                {
                    return new Terabit(v * (Kilobyte.BaseValue / Terabit.BaseValue));
                }

                public static explicit operator Terabit(Megabyte v)
                {
                    return new Terabit(v * (Megabyte.BaseValue / Terabit.BaseValue));
                }

                public static explicit operator Terabit(Gigabyte v)
                {
                    return new Terabit(v * (Gigabyte.BaseValue / Terabit.BaseValue));
                }

                public static explicit operator Terabit(Terabyte v)
                {
                    return new Terabit(v * (Terabyte.BaseValue / Terabit.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Terabit.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Terabit.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Terabit.Unit : "");
                }
            }

            public class Byte : BitSize
            {
                public const string Unit = "B";
                public const double BaseValue = 8D;
                private double Value { get; set; }

                public Byte(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Byte(double v)
                {
                    return new Byte(v);
                }

                public static implicit operator double(Byte v)
                {
                    return v.Value;
                }

                public static explicit operator Byte(Bit v)
                {
                    return new Byte(v * (Bit.BaseValue / Byte.BaseValue));
                }

                public static explicit operator Byte(Kilobit v)
                {
                    return new Byte(v * (Kilobit.BaseValue / Byte.BaseValue));
                }

                public static explicit operator Byte(Megabit v)
                {
                    return new Byte(v * (Megabit.BaseValue / Byte.BaseValue));
                }

                public static explicit operator Byte(Gigabit v)
                {
                    return new Byte(v * (Gigabit.BaseValue / Byte.BaseValue));
                }

                public static explicit operator Byte(Terabit v)
                {
                    return new Byte(v * (Terabit.BaseValue / Byte.BaseValue));
                }

                public static explicit operator Byte(Kilobyte v)
                {
                    return new Byte(v * (Kilobyte.BaseValue / Byte.BaseValue));
                }

                public static explicit operator Byte(Megabyte v)
                {
                    return new Byte(v * (Megabyte.BaseValue / Byte.BaseValue));
                }

                public static explicit operator Byte(Gigabyte v)
                {
                    return new Byte(v * (Gigabyte.BaseValue / Byte.BaseValue));
                }

                public static explicit operator Byte(Terabyte v)
                {
                    return new Byte(v * (Terabyte.BaseValue / Byte.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Byte.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Byte.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Byte.Unit : "");
                }
            }

            public class Kilobyte : BitSize
            {
                public const string Unit = "KB";
                public const double BaseValue = 8192D;
                private double Value { get; set; }

                public Kilobyte(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Kilobyte(double v)
                {
                    return new Kilobyte(v);
                }

                public static implicit operator double(Kilobyte v)
                {
                    return v.Value;
                }

                public static explicit operator Kilobyte(Bit v)
                {
                    return new Kilobyte(v * (Bit.BaseValue / Kilobyte.BaseValue));
                }

                public static explicit operator Kilobyte(Kilobit v)
                {
                    return new Kilobyte(v * (Kilobit.BaseValue / Kilobyte.BaseValue));
                }

                public static explicit operator Kilobyte(Megabit v)
                {
                    return new Kilobyte(v * (Megabit.BaseValue / Kilobyte.BaseValue));
                }

                public static explicit operator Kilobyte(Gigabit v)
                {
                    return new Kilobyte(v * (Gigabit.BaseValue / Kilobyte.BaseValue));
                }

                public static explicit operator Kilobyte(Terabit v)
                {
                    return new Kilobyte(v * (Terabit.BaseValue / Kilobyte.BaseValue));
                }

                public static explicit operator Kilobyte(Byte v)
                {
                    return new Kilobyte(v * (Byte.BaseValue / Kilobyte.BaseValue));
                }

                public static explicit operator Kilobyte(Megabyte v)
                {
                    return new Kilobyte(v * (Megabyte.BaseValue / Kilobyte.BaseValue));
                }

                public static explicit operator Kilobyte(Gigabyte v)
                {
                    return new Kilobyte(v * (Gigabyte.BaseValue / Kilobyte.BaseValue));
                }

                public static explicit operator Kilobyte(Terabyte v)
                {
                    return new Kilobyte(v * (Terabyte.BaseValue / Kilobyte.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Kilobyte.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Kilobyte.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Kilobyte.Unit : "");
                }
            }

            public class Megabyte : BitSize
            {
                public const string Unit = "MB";
                public const double BaseValue = 8388608D;
                private double Value { get; set; }

                public Megabyte(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Megabyte(double v)
                {
                    return new Megabyte(v);
                }

                public static implicit operator double(Megabyte v)
                {
                    return v.Value;
                }

                public static explicit operator Megabyte(Bit v)
                {
                    return new Megabyte(v * (Bit.BaseValue / Megabyte.BaseValue));
                }

                public static explicit operator Megabyte(Kilobit v)
                {
                    return new Megabyte(v * (Kilobit.BaseValue / Megabyte.BaseValue));
                }

                public static explicit operator Megabyte(Megabit v)
                {
                    return new Megabyte(v * (Megabit.BaseValue / Megabyte.BaseValue));
                }

                public static explicit operator Megabyte(Gigabit v)
                {
                    return new Megabyte(v * (Gigabit.BaseValue / Megabyte.BaseValue));
                }

                public static explicit operator Megabyte(Terabit v)
                {
                    return new Megabyte(v * (Terabit.BaseValue / Megabyte.BaseValue));
                }

                public static explicit operator Megabyte(Byte v)
                {
                    return new Megabyte(v * (Byte.BaseValue / Megabyte.BaseValue));
                }

                public static explicit operator Megabyte(Kilobyte v)
                {
                    return new Megabyte(v * (Kilobyte.BaseValue / Megabyte.BaseValue));
                }

                public static explicit operator Megabyte(Gigabyte v)
                {
                    return new Megabyte(v * (Gigabyte.BaseValue / Megabyte.BaseValue));
                }

                public static explicit operator Megabyte(Terabyte v)
                {
                    return new Megabyte(v * (Terabyte.BaseValue / Megabyte.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Megabyte.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Megabyte.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Megabyte.Unit : "");
                }
            }

            public class Gigabyte : BitSize
            {
                public const string Unit = "GB";
                public const double BaseValue = 8589934592D;
                private double Value { get; set; }

                public Gigabyte(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Gigabyte(double v)
                {
                    return new Gigabyte(v);
                }

                public static implicit operator double(Gigabyte v)
                {
                    return v.Value;
                }

                public static explicit operator Gigabyte(Bit v)
                {
                    return new Gigabyte(v * (Bit.BaseValue / Gigabyte.BaseValue));
                }

                public static explicit operator Gigabyte(Kilobit v)
                {
                    return new Gigabyte(v * (Kilobit.BaseValue / Gigabyte.BaseValue));
                }

                public static explicit operator Gigabyte(Megabit v)
                {
                    return new Gigabyte(v * (Megabit.BaseValue / Gigabyte.BaseValue));
                }

                public static explicit operator Gigabyte(Gigabit v)
                {
                    return new Gigabyte(v * (Gigabit.BaseValue / Gigabyte.BaseValue));
                }

                public static explicit operator Gigabyte(Terabit v)
                {
                    return new Gigabyte(v * (Terabit.BaseValue / Gigabyte.BaseValue));
                }

                public static explicit operator Gigabyte(Byte v)
                {
                    return new Gigabyte(v * (Byte.BaseValue / Gigabyte.BaseValue));
                }

                public static explicit operator Gigabyte(Kilobyte v)
                {
                    return new Gigabyte(v * (Kilobyte.BaseValue / Gigabyte.BaseValue));
                }

                public static explicit operator Gigabyte(Megabyte v)
                {
                    return new Gigabyte(v * (Megabyte.BaseValue / Gigabyte.BaseValue));
                }

                public static explicit operator Gigabyte(Terabyte v)
                {
                    return new Gigabyte(v * (Terabyte.BaseValue / Gigabyte.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Gigabyte.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Gigabyte.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Gigabyte.Unit : "");
                }
            }

            public class Terabyte : BitSize
            {
                public const string Unit = "TB";
                public const double BaseValue = 8796093020000D;
                private double Value { get; set; }

                public Terabyte(double v)
                {
                    this.Value = v;
                }

                public static implicit operator Terabyte(double v)
                {
                    return new Terabyte(v);
                }

                public static implicit operator double(Terabyte v)
                {
                    return v.Value;
                }

                public static explicit operator Terabyte(Bit v)
                {
                    return new Terabyte(v * (Bit.BaseValue / Terabyte.BaseValue));
                }

                public static explicit operator Terabyte(Kilobit v)
                {
                    return new Terabyte(v * (Kilobit.BaseValue / Terabyte.BaseValue));
                }

                public static explicit operator Terabyte(Megabit v)
                {
                    return new Terabyte(v * (Megabit.BaseValue / Terabyte.BaseValue));
                }

                public static explicit operator Terabyte(Gigabit v)
                {
                    return new Terabyte(v * (Gigabit.BaseValue / Terabyte.BaseValue));
                }

                public static explicit operator Terabyte(Terabit v)
                {
                    return new Terabyte(v * (Terabit.BaseValue / Terabyte.BaseValue));
                }

                public static explicit operator Terabyte(Byte v)
                {
                    return new Terabyte(v * (Byte.BaseValue / Terabyte.BaseValue));
                }

                public static explicit operator Terabyte(Kilobyte v)
                {
                    return new Terabyte(v * (Kilobyte.BaseValue / Terabyte.BaseValue));
                }

                public static explicit operator Terabyte(Megabyte v)
                {
                    return new Terabyte(v * (Megabyte.BaseValue / Terabyte.BaseValue));
                }

                public static explicit operator Terabyte(Gigabyte v)
                {
                    return new Terabyte(v * (Gigabyte.BaseValue / Terabyte.BaseValue));
                }

                /// <see cref="Object.ToString()"/>
                public override string ToString()
                {
                    return this.Value.ToString() + " " + Terabyte.Unit;
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                public string ToString(bool unit)
                {
                    return this.Value.ToString() + (unit ? " " + Terabyte.Unit : "");
                }

                /// <see cref="Object.ToString()"/>
                /// <param name="unit">Whether or not to append the measurement unit.</param>
                /// <param name="result">The result.</param>
                public string ToString(bool unit, Func<double, string> result)
                {
                    return this.Value.ToString() + (unit ? " " + Terabyte.Unit : "");
                }
            }

            #endregion
        }
    }

    namespace BagLogging
    {
        /// <summary>
        /// A class used for application logging.
        /// </summary>
        public sealed class BagLogger
        {
            /// <summary>
            /// A function used for logging.
            /// </summary>
            /// <param name="s">The output string that needs to be handled.</param>
            public delegate void LogFunction(string s);

            /// <summary>
            /// The logger.
            /// </summary>
            public LogFunction Logger { get; set; }
            /// <summary>
            /// Whether to prepend a timestamp on the output string or not.
            /// </summary>
            public bool Timestamp { get; set; }
            /// <summary>
            /// The format of the timestamp. See DateTime.ToString().
            /// </summary>
            public string Timeformat { get; set; }
            /// <summary>
            /// Whether the logger is enabled or not.
            /// </summary>
            public bool Enabled { get; set; }

            /// <summary>
            /// A class used for application logging.
            /// </summary>
            /// <param name="logger"></param>
            /// <param name="timestamp"></param>
            public BagLogger(LogFunction logger, bool timestamp)
            {
                Logger = logger;
                Timestamp = timestamp;
                Timeformat = "dd.MM.yyyy HH:mm:ss: ";
                Enabled = true;
            }

            /// <summary>
            /// Log the specified string.
            /// </summary>
            /// <param name="s">The string to be logged.</param>
            public void Log(string s)
            {
                if (Enabled) Logger((Timestamp ? DateTime.Now.ToString(Timeformat) : "") + s);
            }

            /// <summary>
            /// Log the specified string if expression is true.
            /// </summary>
            /// <param name="expression">An expression.</param>
            /// <param name="s">The string to be logged.</param>
            public void LogIf(bool expression, string s)
            {
                if (expression) Log(s);
            }

            /// <summary>
            /// Log the specified string if expression is false.
            /// </summary>
            /// <param name="expression">An expression.</param>
            /// <param name="s">The string to be logged.</param>
            public void LogIfNot(bool expression, string s)
            {
                if (!expression) Log(s);
            }
        }

        /// <summary>
        /// A class used for managing the console window, so that the program can both read from the console and write into the console at the samt time.
        /// </summary>
        public sealed class BagConsole
        {
            /// <summary>
            /// A delegate that can be called when a command is entered.
            /// </summary>
            /// <param name="command">The command that was entered.</param>
            public delegate void CommandEnteredEvent(string command);
            /// <summary>
            /// An event that will be fired when a user enters a command into the console window.
            /// </summary>
            public event CommandEnteredEvent OnCommandEntered;
            /// <summary>
            /// The start of the command line.
            /// </summary>
            public string CommandStart { get; set; }
            /// <summary>
            /// Can be set to true to shut down the main reading thread and all readers.
            /// </summary>
            private bool Exit = false;
            /// <summary>
            /// Whether or not there is a reading going on.
            /// </summary>
            private bool Reading = false;
            /// <summary>
            /// The command that is currently being written into the console.
            /// </summary>
            private string Command = "";
            /// <summary>
            /// The main reader thread.
            /// </summary>
            private Thread ReaderThread = null;
            /// <summary>
            /// An array of keys that will not be treated as input.
            /// </summary>
            public ConsoleKey[] NotInput = new ConsoleKey[] { ConsoleKey.LeftArrow, ConsoleKey.UpArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.Tab, ConsoleKey.Insert, ConsoleKey.Home, ConsoleKey.PageUp, ConsoleKey.PageDown, ConsoleKey.End, ConsoleKey.Delete, ConsoleKey.F1, ConsoleKey.F2, ConsoleKey.F3, ConsoleKey.F4, ConsoleKey.F5, ConsoleKey.F6, ConsoleKey.F7, ConsoleKey.F8, ConsoleKey.F9, ConsoleKey.F10, ConsoleKey.F11, ConsoleKey.F12 };

            /// <summary>
            /// The main constructor.
            /// </summary>
            public BagConsole() : this("> ") { }
            /// <summary>
            /// The main constructor.
            /// </summary>
            /// <param name="commandStart">The start of the command line.</param>
            public BagConsole(string commandStart)
            {
                this.CommandStart = commandStart;
                for (int i = 0; i < Console.BufferHeight; i++)
                {
                    Console.WriteLine();
                }
            }

            /// <summary>
            /// The main deconstructor.
            /// </summary>
            ~BagConsole()
            {
                this.Stop();
            }

            /// <summary>
            /// Stop all readers and reader threads.
            /// Note: This should always be called before application shutdown to make sure all threads are terminated.
            /// </summary>
            public void Stop()
            {
                this.Exit = true;

                if (this.ReaderThread != null)
                {
                    this.ReaderThread.Abort();
                }
            }

            /// <summary>
            /// Reads commands asynchronously and fires the OnCommandEntered event when a command has been entered.
            /// </summary>
            public void ReadCommandAsync()
            {
                this.Exit = false;
                if (!this.Reading)
                {
                    this.ReaderThread = new Thread(new ThreadStart(this._ReadCommandAsync));
                    this.ReaderThread.Start();
                }
            }

            /// <summary>
            /// Reads commands asynchronously and fires the OnCommandEntered event when a command has been entered.
            /// </summary>
            private void _ReadCommandAsync()
            {
                while (!this.Exit)
                {
                    this.OnCommandEntered(this.ReadCommand());
                }
            }

            /// <summary>
            /// Reads a command and returns it as a string.
            /// Note: This method will not return anything until the command is entered. Use ReadCommandAsync to read commands asynchronously.
            /// </summary>
            /// <returns></returns>
            public string ReadCommand()
            {
                if (this.Reading) return null;

                this.Exit = false;
                this.Reading = true;
                string command = "";
                while (true)
                {
                    if (this.Exit)
                    {
                        this.Reading = false;
                        return null;
                    }

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo c = Console.ReadKey();
                        if (c.Key == ConsoleKey.Enter)
                        {
                            command = this.Command;
                            this.Command = "";
                            this.WriteLine(null);
                            break;
                        }
                        else if (NotInput.Contains(c.Key))
                        {

                        }
                        else
                        {
                            if (c.Key == ConsoleKey.Backspace)
                            {
                                try
                                {
                                    this.Command = this.Command.Substring(0, this.Command.Length - 1);
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                this.Command += c.KeyChar;
                            }

                            this.WriteLine(null);
                        }
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }

                this.Reading = false;
                return command;
            }

            /// <summary>
            /// Writes a string to the console.
            /// Note: This should be used instead of Console.WriteLine and Console.Write.
            /// </summary>
            /// <param name="o"></param>
            public void WriteLine(object o)
            {
                string s = (o == null ? null : o.ToString());
                Console.CursorLeft = 0;
                if (s != null)
                {
                    Console.Write(s);
                    for (int i = 1; i < Console.BufferWidth - s.Length; i++)
                    {
                        Console.Write(" ");
                    }
                }

                Console.Write((s == null ? "" : "\n") + this.CommandStart + this.Command);
                int k = Console.BufferWidth - this.Command.Length - 3;
                for (int i = 0; i < k; i++)
                {
                    Console.Write(" ");
                }

                try
                {
                    Console.CursorLeft -= k;
                }
                catch { }
            }
        }
    }

    namespace BagDatabase
    {
        /// <summary>
        /// A class for working with MySQL databases.
        /// </summary>
        public class BagDB
        {
            /// <summary>
            /// The server to connect to.
            /// </summary>
            public string Server { get; set; }
            /// <summary>
            /// The default schema.
            /// </summary>
            public string Schema { get; set; }
            /// <summary>
            /// The username used to connect.
            /// </summary>
            public string Username { get; set; }
            /// <summary>
            /// The password used to connect.
            /// </summary>
            public string Password { get; set; }
            /// <summary>
            /// The time before the MySQL connection times out.
            /// </summary>
            public int CommandTimeout { get; set; }
            /// <summary>
            /// The state of the MySQL connection.
            /// </summary>
            public ConnectionState State { get { return this.Connection.State; } }

            /// <summary>
            /// The MySQL connection.
            /// </summary>
            public MySqlConnection Connection { get; set; }

            /// <summary>
            /// The main BagDB constructor.
            /// </summary>
            /// <param name="server">The server to connect to.</param>
            /// <param name="schema">The default schema.</param>
            /// <param name="username">The username used to connect.</param>
            /// <param name="password">The password used to connect.</param>
            public BagDB(string server, string schema, string username, string password)
            {
                this.CommandTimeout = 120;
                this.Server = server;
                this.Schema = schema;
                this.Username = username;
                this.Password = password;
                this.Connect();
            }

            /// <summary>
            /// Connects, or reconnects, to the MySQL database.
            /// </summary>
            public void Connect()
            {
                if (this.Connection == null) this.Connection = new MySqlConnection();
                if (this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.ConnectionString = "SERVER=" + this.Server + ";DATABASE=" + this.Schema + ";UID=" + this.Username + ";PWD=" + this.Password + ";";
                    this.Connection.Open();
                }
            }

            /// <summary>
            /// Send a query to the MySQL database.
            /// </summary>
            /// <param name="q">The query string.</param>
            /// <returns>A DataTable object with the results from the query.</returns>
            public DataTable Query(string q)
            {
                Monitor.Enter(this.Connection);
                this.Connect();
                DataTable dt = null;
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(q, this.Connection))
                    {
                        cmd.CommandTimeout = this.CommandTimeout;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt = new DataTable();
                            dt.Load(reader);
                            reader.Close();
                            reader.Dispose();
                        }
                        cmd.Dispose();
                    }
                }
                finally
                {
                    Monitor.Exit(this.Connection);
                }
                return dt;
            }

            /// <summary>
            /// Send a query to the MySQL database and only return the first column of the first row.
            /// </summary>
            /// <param name="q">The query string.</param>
            /// <returns>The first column of the first row.</returns>
            public object QuerySingle(string q)
            {
                this.Connect();
                Monitor.Enter(this.Connection);
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(q, this.Connection))
                    {
                        cmd.CommandTimeout = this.CommandTimeout;
                        return cmd.ExecuteScalar();
                    }
                }
                finally
                {
                    Monitor.Exit(this.Connection);
                }
            }

            /// <summary>
            /// Send a query to the MySQL database and only return the first column of the first row casted to T.
            /// </summary>
            /// <typeparam name="T">The type of the first column.</typeparam>
            /// <param name="q">The query string.</param>
            /// <returns>The first column of the first row casted to T.</returns>
            public T QuerySingle<T>(string q)
            {
                try
                {
                    object o = this.QuerySingle(q);
                    if (o.GetType() == typeof(DBNull)) return default(T);
                    else return (T)o;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            /// <summary>
            /// Executes a query on the MySQL database and returns how many rows were affected.
            /// </summary>
            /// <param name="q">The query to execute.</param>
            /// <returns>How many rows were affected.</returns>
            public int Execute(string q)
            {
                this.Connect();
                Monitor.Enter(this.Connection);
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(q, this.Connection))
                    {
                        cmd.CommandTimeout = this.CommandTimeout;
                        return cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    Monitor.Exit(this.Connection);
                }
            }

            /// <summary>
            /// Kills the current thread and then closes the MySQL connection.
            /// </summary>
            public void Close()
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        this.Execute("KILL " + this.Connection.ServerThread + ";");
                    }
                    catch { }
                    this.Connection.Close();
                }
            }

            /// <summary>
            /// Converts a DateTime object into an SQL compatible string.
            /// </summary>
            /// <param name="dt">The DateTime object.</param>
            /// <returns>An SQL formatted string.</returns>
            public static string DateTimeToSQL(DateTime dt)
            {
                return dt.ToSQLDateTime();
            }
        }
    }

    namespace BagTorrent
    {
        /// <summary>
        /// A class used for decoding Bencoding.
        /// </summary>
        public class BencodeDecoder
        {
            /// <summary>
            /// A bencoding exception.
            /// </summary>
            public class BencodingException : FormatException
            {
                /// <summary>
                /// Creates a new BencodingException.
                /// </summary>
                public BencodingException() { }
                /// <summary>
                /// Creates a new BencodingException.
                /// </summary>
                /// <param name="message">The message.</param>
                public BencodingException(string message) : base(message) { }
                /// <summary>
                /// Creates a new BencodingException.
                /// </summary>
                /// <param name="message">The message.</param>
                /// <param name="inner">The inner exception.</param>
                public BencodingException(string message, Exception inner) : base(message, inner) { }
            }

            /// <summary>
            /// The main constructor.
            /// </summary>
            /// <param name="s">The bencoded string to decode.</param>
            public BencodeDecoder(string s)
            {
                BencodedString = s;
            }

            /// <summary>
            /// Where the reader will start reading next.
            /// </summary>
            private int Index = 0;
            /// <summary>
            /// The bencoded string.
            /// </summary>
            public string BencodedString = null;

            /// <summary>
            /// Decodes the string.
            /// </summary>
            /// <returns>An array of root elements.</returns>
            public BElement[] Decode()
            {
                try
                {
                    List<BElement> rootElements = new List<BElement>();
                    while (BencodedString.Length > Index)
                    {
                        rootElements.Add(ReadElement());
                    }
                    return rootElements.ToArray();
                }
                catch (BencodingException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw Error(e);
                }
            }

            /// <summary>
            /// Reads and element.
            /// </summary>
            /// <returns>The element that was read.</returns>
            private BElement ReadElement()
            {
                switch (BencodedString[Index])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9': return ReadString();
                    case 'i': return ReadInteger();
                    case 'l': return ReadList();
                    case 'd': return ReadDictionary();
                    default: throw Error(new Exception());
                }
            }

            /// <summary>
            /// Reads a dictionary.
            /// </summary>
            /// <returns>The dictionary that was read.</returns>
            private BDictionary ReadDictionary()
            {
                Index++;
                BDictionary dict = new BDictionary();
                try
                {
                    while (BencodedString[Index] != 'e')
                    {
                        BString K = ReadString();
                        BElement V = ReadElement();
                        dict.Add(K, V);
                    }
                }
                catch (BencodingException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw Error(e);
                }
                Index++;
                return dict;
            }

            /// <summary>
            /// Reads a list.
            /// </summary>
            /// <returns>The list that was read.</returns>
            private BList ReadList()
            {
                Index++;
                BList lst = new BList();
                try
                {
                    while (BencodedString[Index] != 'e')
                    {
                        lst.Add(ReadElement());
                    }
                }
                catch (BencodingException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw Error(e);
                }
                Index++;
                return lst;
            }

            /// <summary>
            /// Reads an integer.
            /// </summary>
            /// <returns>The integer that was read.</returns>
            private BInteger ReadInteger()
            {
                Index++;
                int end = BencodedString.IndexOf('e', Index);
                if (end == -1) throw Error(new Exception());
                long Integer = 0;
                try
                {
                    Integer = Convert.ToInt64(BencodedString.Substring(Index, end - Index));
                    Index = end + 1;
                }
                catch (Exception e)
                {
                    throw Error(e);
                }
                return new BInteger(Integer);
            }

            /// <summary>
            /// Reads a string.
            /// </summary>
            /// <returns>The string that was read.</returns>
            private BString ReadString()
            {
                int length = 0;
                int semicolon = 0;
                try
                {
                    semicolon = BencodedString.IndexOf(':', Index);
                    if (semicolon == -1) throw Error(new Exception());
                    length = Convert.ToInt32(BencodedString.Substring(Index, semicolon - Index));
                }
                catch (Exception e)
                {
                    throw Error(e);
                }

                Index = semicolon + 1;
                int tmpIndex = Index;
                Index += length;
                try
                {
                    return new BString(BencodedString.Substring(tmpIndex, length));
                }
                catch (Exception e)
                {
                    throw Error(e);
                }
            }

            /// <summary>
            /// Generates an error.
            /// </summary>
            /// <param name="e">The inner exception.</param>
            /// <returns>An exception that can then be thrown.</returns>
            private Exception Error(Exception e)
            {
                return new BencodingException("Bencoded string invalid", e);
            }
        }

        /// <summary>
        /// An interface for bencoded elements.
        /// </summary>
        public interface BElement
        {
            /// <summary>
            /// Generates the bencoded equivalent of the element.
            /// </summary>
            /// <returns>The bencoded equivalent of the element.</returns>
            string ToBencodedString();
        }

        /// <summary>
        /// A bencode integer.
        /// </summary>
        public class BInteger : BElement
        {
            /// <summary>
            /// Allows you to set an integer to a BInteger.
            /// </summary>
            /// <param name="i">The integer.</param>
            /// <returns>The BInteger.</returns>
            public static implicit operator BInteger(int i)
            {
                return new BInteger(i);
            }

            //public int Value { get; set; }
            /// <summary>
            /// The value of the bencoded integer.
            /// </summary>
            public long Value { get; set; }

            /// <summary>
            /// The main constructor.
            /// </summary>
            /// <param name="value">The value of the bencoded integer.</param>
            public BInteger(long value /*int value*/)
            {
                this.Value = value;
            }

            /// <summary>
            /// Generates the bencoded equivalent of the integer.
            /// </summary>
            /// <returns>The bencoded equivalent of the integer.</returns>
            public string ToBencodedString()
            {
                return "i" + Value.ToString() + "e";
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// <see cref="Object.GetHashCode()"/>
            public override int GetHashCode()
            {
                return this.Value.GetHashCode();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            /// <see cref="int.Equals(object)"/>
            public override bool Equals(object obj)
            {
                try
                {
                    return this.Value.Equals(((BInteger)obj).Value);
                }
                catch { return false; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// <see cref="Object.ToString()"/>
            public override string ToString()
            {
                return this.Value.ToString();
            }
        }

        /// <summary>
        /// A bencode string.
        /// </summary>
        public class BString : BElement
        {
            /// <summary>
            /// Allows you to set a string to a BString.
            /// </summary>
            /// <param name="s">The string.</param>
            /// <returns>The BString.</returns>
            public static implicit operator BString(string s)
            {
                return new BString(s);
            }

            /// <summary>
            /// The value of the bencoded integer.
            /// </summary>
            public string Value { get; set; }

            /// <summary>
            /// The main constructor.
            /// </summary>
            /// <param name="value"></param>
            public BString(string value)
            {
                this.Value = value;
            }

            /// <summary>
            /// Generates the bencoded equivalent of the string.
            /// </summary>
            /// <returns>The bencoded equivalent of the string.</returns>
            public string ToBencodedString()
            {
                return this.Value.Length + ":" + this.Value;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// <see cref="Object.GetHashCode()"/>
            public override int GetHashCode()
            {
                return this.Value.GetHashCode();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            /// <see cref="string.Equals(object)"/>
            public override bool Equals(object obj)
            {
                try
                {
                    return this.Value.Equals(((BString)obj).Value);
                }
                catch { return false; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            /// <see cref="Object.ToString()"/>
            public override string ToString()
            {
                return this.Value.ToString();
            }
        }

        /// <summary>
        /// A bencode list.
        /// </summary>
        public class BList : List<BElement>, BElement
        {
            /// <summary>
            /// Generates the bencoded equivalent of the list.
            /// </summary>
            /// <returns>The bencoded equivalent of the list.</returns>
            public string ToBencodedString()
            {
                string u = "l";
                foreach (BElement element in base.ToArray())
                {
                    u += element.ToBencodedString();
                }
                return u + "e";
            }

            /// <summary>
            /// Adds the specified value to the list.
            /// </summary>
            /// <param name="value">The specified value.</param>
            public void Add(string value)
            {
                base.Add(new BString(value));
            }

            /// <summary>
            /// Adds the specified value to the list.
            /// </summary>
            /// <param name="value">The specified value.</param>
            public void Add(int value)
            {
                base.Add(new BInteger(value));
            }
        }

        /// <summary>
        /// A bencode dictionary.
        /// </summary>
        public class BDictionary : SortedDictionary<BString, BElement>, BElement
        {
            /// <summary>
            /// Generates the bencoded equivalent of the dictionary.
            /// </summary>
            /// <returns>The bencoded equivalent of the dictionary.</returns>
            public string ToBencodedString()
            {
                string u = "d";
                for (int i = 0; i < base.Count; i++)
                {
                    u += base.Keys.ElementAt(i).ToBencodedString();
                    u += base.Values.ElementAt(i).ToBencodedString();
                }
                return u + "e";
            }

            /// <summary>
            /// Adds the specified key-value pair to the dictionary.
            /// </summary>
            /// <param name="key">The specified key.</param>
            /// <param name="value">The specified value.</param>
            public void Add(string key, BElement value)
            {
                base.Add(new BString(key), value);
            }

            /// <summary>
            /// Adds the specified key-value pair to the dictionary.
            /// </summary>
            /// <param name="key">The specified key.</param>
            /// <param name="value">The specified value.</param>
            public void Add(string key, string value)
            {
                base.Add(new BString(key), new BString(value));
            }

            /// <summary>
            /// Adds the specified key-value pair to the dictionary.
            /// </summary>
            /// <param name="key">The specified key.</param>
            /// <param name="value">The specified value.</param>
            public void Add(string key, int value)
            {
                base.Add(new BString(key), new BInteger(value));
            }

            /// <summary>
            /// Gets or sets the value assosiated with the specified key.
            /// </summary>
            /// <param name="key">The key of the value to get or set.</param>
            /// <returns>The value assosiated with the specified key.</returns>
            public BElement this[string key]
            {
                get
                {
                    return this[new BString(key)];
                }
                set
                {
                    this[new BString(key)] = value;
                }
            }
        }
    }

    namespace BagNet
    {
        /// <summary>
        /// A static class for working with internet communications and such.
        /// </summary>
        public static class BagNet
        {
            /// <summary>
            /// Gets a response object from the specified page. The object can the be used to read data from the page.
            /// </summary>
            /// <param name="page">The page to get response from.</param>
            /// <returns>The response object of from the page.</returns>
            public static string DownloadPage(Uri page)
            {
                return page.Download();
            }
        }
    }

    namespace BagFK
    {
        /// <summary>
        /// Klasi fyrir kennitölur.
        /// </summary>
        public class Kennitala
        {
            private string Upprunalegt;
            /// <summary>
            /// Fyrir hvaða dag er kennitalan.
            /// </summary>
            public int Dagur = -1;
            /// <summary>
            /// Fyrir hvaða mánuð er kennitalan.
            /// </summary>
            public int Manudur = -1;
            /// <summary>
            /// Fyrir hvaða ár er kennitalan.
            /// </summary>
            public int Ar = -1;
            /// <summary>
            /// 4 stafa lokatala kennitölunar.
            /// </summary>
            public int Lokatala = -1;
            /// <summary>
            /// Hvort kennitalan sé gild.
            /// </summary>
            public bool ErILagi = false;
            /// <summary>
            /// Stjörnumerki kennitölunar.
            /// </summary>
            public string Stjornumerki = "";

            /// <summary>
            /// Smiður kennitölu-klasans.
            /// </summary>
            /// <param name="kt">Kennitalan sem strengur (ddmmyy-nnnn).</param>
            public Kennitala(string kt)
            {
                Upprunalegt = kt;
                ErILagi = ErRett(kt);

                if (ErILagi)
                {
                    Dagur = Int32.Parse(kt.Substring(0, 2));
                    Manudur = Int32.Parse(kt.Substring(2, 2));
                    Ar = Int32.Parse(kt.Substring(4, 2));
                    Lokatala = Int32.Parse(kt.Substring(kt.Length - 4));
                    Stjornumerki = StjMrki();
                }
            }

            /// <summary>
            /// Smiður kennitölu-klasans.
            /// </summary>
            /// <param name="ar">Ár kennitölunnar.</param>
            /// <param name="manudur">Mánuður kennitölunnar.</param>
            /// <param name="dagur">Dagur kennitölunnar.</param>
            /// <param name="lokatala">4 stafa lokatala kennitölunnar.</param>
            public Kennitala(int ar, int manudur, int dagur, int lokatala)
            {
                Ar = ar;
                Manudur = manudur;
                Dagur = dagur;
                Lokatala = lokatala;
                Stjornumerki = StjMrki();
                ErILagi = lokatala <= 9999;
            }

            private string StjMrki()
            {
                string[] Merki = new string[] { "Hrútur", "Naut", "Tvíburi", "Krabbi", "Ljón", "Meyja", "Vog", "Sporðdreki", "Bogmaður", "Steingeit", "Vatnsberi", "Fiskur" };
                DateTime[][] Timar = new DateTime[][] {
                new DateTime[]{new DateTime(Ar, 3,  21), new DateTime(Ar, 4,  20)},
                new DateTime[]{new DateTime(Ar, 4,  21), new DateTime(Ar, 5,  21)},
                new DateTime[]{new DateTime(Ar, 5,  22), new DateTime(Ar, 6,  20)},
                new DateTime[]{new DateTime(Ar, 6,  21), new DateTime(Ar, 7,  23)},
                new DateTime[]{new DateTime(Ar, 7,  24), new DateTime(Ar, 8,  23)},
                new DateTime[]{new DateTime(Ar, 8,  24), new DateTime(Ar, 9,  23)},
                new DateTime[]{new DateTime(Ar, 9,  24), new DateTime(Ar, 10, 22)},
                new DateTime[]{new DateTime(Ar, 10, 23), new DateTime(Ar, 11, 21)},
                new DateTime[]{new DateTime(Ar, 11, 22), new DateTime(Ar, 12, 21)},
                new DateTime[]{new DateTime(Ar, 12, 22), new DateTime(Ar, 1,  20)},
                new DateTime[]{new DateTime(Ar, 1,  21), new DateTime(Ar, 2,  19)},
                new DateTime[]{new DateTime(Ar, 2,  20), new DateTime(Ar, 3,  02)}
            };

                DateTime kt = new DateTime(Ar, Manudur, Dagur);

                for (int i = 0; i < Timar.Length; i++)
                {
                    if (kt >= Timar[i][0] && kt <= Timar[i][1])
                    {
                        return Merki[i];
                    }
                }

                return null;
            }

            /// <summary>
            /// Tekur inn streng sem inniheldur hugsanlega kennitölu og skilar true ef það er kennitala; annars false.
            /// </summary>
            /// <param name="kt">Strengur sem inniheldur hugsanlega kennitölu.</param>
            /// <returns>True ef strengurinn er kennitala; annars false.</returns>
            public static bool ErRett(string kt)
            {
                if (!Regex.IsMatch(kt, "[0-9]{6}-?[0-9]{4}")) return false;

                try
                {
                    int d = Int32.Parse(kt.Substring(0, 2));
                    int m = Int32.Parse(kt.Substring(2, 2));
                    int y = Int32.Parse(kt.Substring(4, 2));
                    int l = Int32.Parse(kt.Substring(kt.Length - 4));

                    return (d >= 1 && d <= 31 && m >= 1 && m <= 12 && y >= 0 && y <= 99 && l >= 0 && l <= 9999);
                }
                catch { return false; };
            }

            /// <summary>
            /// Skilar kennitölunni með eða án - sem streng.
            /// </summary>
            /// <param name="skiptir">Hvort nota eigi - eða ekki.</param>
            /// <returns>Kennitalan sem strengur.</returns>
            public string ToString(bool skiptir)
            {
                if (!ErILagi) return "";

                return Dagur.ToString() + Manudur.ToString() + Ar.ToString() + (skiptir ? "-" : "") + Lokatala.ToString();
            }

            /// <summary>
            /// Skilar kennitölunni með - sem streng.
            /// </summary>
            /// <returns>Kennitalan sem strengur.</returns>
            public override string ToString()
            {
                return ToString(true);
            }
        }

        /// <summary>
        /// Klasi sem finnur stærstu runu-summu.
        /// </summary>
        public class StærstaRunuSumma
        {
            /// <summary>
            /// Runan.
            /// </summary>
            public List<int> Runa;
            /// <summary>
            /// Stærsta runu-summan.
            /// </summary>
            public int StærstaSumma;
            /// <summary>
            /// Allar stærstu runurnar.
            /// </summary>
            public List<List<int>> StærstuRunur;

            /// <summary>
            /// Smiður fyrir StærstaRunuSumma-klasann.
            /// </summary>
            /// <param name="runa">Array af tölum.</param>
            public StærstaRunuSumma(int[] runa)
            {
                Runa = runa.ToList();
                FinnaStærstuSummu();
            }

            /// <summary>
            /// Smiður fyrir StærstaRunuSumma-klasann.
            /// </summary>
            /// <param name="runa">Listi af tölum.</param>
            public StærstaRunuSumma(List<int> runa)
            {
                Runa = runa;
                FinnaStærstuSummu();
            }

            /// <summary>
            /// Finnur stærstu summuna.
            /// </summary>
            public void FinnaStærstuSummu()
            {
                if (Runa.Count == 0)
                {
                    StærstuRunur = new List<List<int>>() {
                    Runa
                };
                    StærstaSumma = 0;
                }
                else if (Runa.Count == 1)
                {
                    StærstuRunur = new List<List<int>>() {
                    Runa
                };
                    StærstaSumma = Runa[0];
                }

                List<List<int>> mo = Moguleikar();

                var max = from m in mo
                          orderby m.Sum() descending
                          select m.Sum();

                StærstaSumma = max.ToArray()[0];

                StærstuRunur = (from m in mo
                                where m.Sum() == StærstaSumma
                                select m).ToList();
            }

            /// <summary>
            /// Finnur allar mögulegar runur.
            /// </summary>
            /// <returns>Allar mögulegar runur.</returns>
            public List<List<int>> Moguleikar()
            {
                List<List<int>> ut = new List<List<int>>();
                for (int i = 1; i <= Runa.Count; i++)
                {
                    for (int a = 0; a <= Runa.Count - i; a++)
                    {
                        ut.Add(Runa.GetRange(a, i));
                    }
                }

                return ut;
            }
        }

        /// <summary>
        /// Method sem gætu verið notuð fyrir Minesweeper leikinn
        /// </summary>
        public static class Minesweeper
        {
            /// <summary>
            /// Gáir hvað margar sprengjur eru í kringum reit
            /// </summary>
            /// <param name="field">Spilaborðið</param>
            /// <param name="bomb">Hvernig sprengjan er</param>
            /// <param name="x">X-hnitin á reitnum</param>
            /// <param name="y">Y-hnitin á reitnum</param>
            /// <returns>Hversu margar sprengjur eru í kringum reitinn</returns>
            public static int ManyBombsAround(string[,] field, string bomb, int x, int y)
            {
                int bombsAround = 0;
                string[] ps = new string[] { "-1,-1", "-1,0", "-1,1", "0,-1", "0,1", "1,-1", "1,0", "1,1" };

                foreach (string p in ps)
                {
                    int[] t = p.SplitIntoInts(',');
                    int cX = x + t[0];
                    int cY = y + t[1];
                    if (OnField(field.GetLength(0), field.GetLength(1), cX, cY) && field[cX, cY] == bomb)
                    {
                        bombsAround++;
                    }
                }

                return bombsAround;
            }

            /// <summary>
            /// Athugar hvort hnit séu inná borðinu
            /// </summary>
            /// <param name="w">Breydd borðsins</param>
            /// <param name="h">Hæð borðsins</param>
            /// <param name="x">X-hnitið</param>
            /// <param name="y">Y-hnitið</param>
            /// <returns>Hvort hnitin séu inná borðinu</returns>
            public static bool OnField(int w, int h, int x, int y)
            {
                return (x.IsBetweenOrEqualTo(0, w - 1) && y.IsBetweenOrEqualTo(0, h - 1));
            }
        }

        /// <summary>
        /// Allskonar method til að teikna hluti
        /// </summary>
        public static class DrawObjects
        {
            /// <summary>
            /// Gerðir af þríhyrningum. Heitir eftir því í hvaða horni eða brún á kassa þríhyrningurinn væri.
            /// </summary>
            public enum TriangleType
            {
                /// <summary>
                /// Þríhyrningur sem er með langhliðina á toppnum.
                /// </summary>
                Top,
                /// <summary>
                /// Þríhyrningur sem er með langhliðina til vinstri.
                /// </summary>
                Left,
                /// <summary>
                /// Þríhyrningur sem er með langhliðina til hægri.
                /// </summary>
                Right,
                /// <summary>
                /// Þríhyrningur sem er með langhliðina á botninum.
                /// </summary>
                Bottom,
                /// <summary>
                /// Þríhyrningur sem er með bendir upp í efra vinstra hornið.
                /// </summary>
                TopLeft,
                /// <summary>
                /// Þríhyrningur sem er með bendir upp í efra hægra hornið.
                /// </summary>
                TopRight,
                /// <summary>
                /// Þríhyrningur sem er með bendir upp í neðra vinstra hornið.
                /// </summary>
                BottomLeft,
                /// <summary>
                /// Þríhyrningur sem er með bendir upp í neðra hægra hornið.
                /// </summary>
                BottomRight
            }

            /// <summary>
            /// Teiknar þríhyrninga
            /// </summary>
            /// <param name="height">Hæð þríhyrningsins</param>
            /// <param name="s">Strengurinn sem er notaður inní þríhyrningnum</param>
            /// <param name="p">Strengurinn sem er notaður fyrir utan þríhyrninginn</param>
            /// <param name="TT">Týpan af þríhyrningi</param>
            /// <returns>Þríhyrningurinn</returns>
            /// <example>Triangle(5, "*", " ", TriangleType.Top)</example>
            public static string Triangle(int height, string s, string p, TriangleType TT)
            {
                if (height == 0) return "";

                if (height == 1) return s;

                string t = "";
                string br = "\n";

                switch (TT)
                {
                    case TriangleType.TopLeft:
                        {
                            for (int i = height; i >= 0; i--)
                            {
                                for (int a = 0; a < i; a++)
                                {
                                    t += s;
                                }
                                t += br;
                            }
                            t = t.Trim();
                        }
                        break;
                    case TriangleType.BottomLeft:
                        {
                            for (int i = 1; i <= height; i++)
                            {
                                for (int a = 0; a < i; a++)
                                {
                                    t += s;
                                }
                                t += br;
                            }
                            t = t.Trim();
                        }
                        break;
                    case TriangleType.TopRight:
                        {
                            for (int i = height - 1; i >= 0; i--)
                            {
                                for (int a = height - 1; a >= 0; a--)
                                {
                                    if (a <= i)
                                    {
                                        t += s;
                                    }
                                    else
                                    {
                                        t += p;
                                    }
                                }
                                t += br;
                            }
                            t = t.Trim();
                        }
                        break;
                    case TriangleType.BottomRight:
                        {
                            for (int i = 0; i < height; i++)
                            {
                                for (int a = height - 1; a >= 0; a--)
                                {
                                    if (a > i)
                                    {
                                        t += p;
                                    }
                                    else
                                    {
                                        t += s;
                                    }
                                }
                                t += br;
                            }
                            t = t.TrimEnd();
                        }
                        break;
                    case TriangleType.Left:
                        {
                            t += Triangle(height - 1, s, p, TriangleType.BottomLeft) + br;
                            for (int i = 0; i < height; i++)
                            {
                                t += s;
                            }
                            t += br + Triangle(height - 1, s, p, TriangleType.TopLeft);
                            t = t.Trim();
                        }
                        break;
                    case TriangleType.Top:
                        {
                            t += Triangle(height - 1, s, p, TriangleType.TopRight);
                            string l = "";
                            for (int i = 0; i < height; i++)
                            {
                                l += s + br;
                            }
                            t = Tools.MergeStrings(t, l.Trim());
                            t = Tools.MergeStrings(t, Triangle(height - 1, s, p, TriangleType.TopLeft));
                            t = t.Trim();
                        }
                        break;
                    case TriangleType.Bottom:
                        {
                            t += Triangle(height - 1, s, p, TriangleType.BottomRight);
                            string l = "";
                            for (int i = 0; i < height; i++)
                            {
                                l += s + br;
                            }
                            t = Tools.MergeStrings(br + t, l.Trim());
                            t = Tools.MergeStrings(t, br + Triangle(height - 1, s, p, TriangleType.BottomLeft));
                            t = t.TrimEnd();
                        }
                        break;
                    case TriangleType.Right:
                        {
                            string[] BR = Triangle(height - 1, s, p, TriangleType.BottomRight).Lines();
                            string[] TR = Triangle(height - 1, s, p, TriangleType.TopRight).Lines();

                            for (int i = 0; i < BR.Length; i++)
                            {
                                BR[i] = " " + BR[i];
                                TR[i] = " " + TR[i];
                            }

                            t += String.Join("\n", BR) + br;
                            for (int i = 0; i < height; i++)
                            {
                                t += s;
                            }
                            t += br + String.Join("\n", TR);
                        }
                        break;
                }

                return t;
            }

            /// <summary>
            /// Teiknar kassa
            /// </summary>
            /// <param name="height">Hæðin á kassanum</param>
            /// <param name="width">Breyddin á kassanum</param>
            /// <param name="s">Strengurinn sem er inní kassanum</param>
            /// <param name="p">Strengurinn sem er innæi kassanum ef hann er ekki fylltur</param>
            /// <param name="filled">Bool um hvort hann sé fylltur eða ekki</param>
            /// <returns>Kassinn</returns>
            /// <example>Square(5, 4, "*", " ", false)</example>
            public static string Square(int height, int width, string s, string p, bool filled)
            {
                if (height == 0 || width == 0) return "";
                if (height == width && width == 1) return s;

                string u = "";
                for (int i = 0; i < height; i++)
                {
                    for (int a = 0; a < width; a++)
                    {
                        if (i == 0 || a == 0 || i == height - 1 || a == width - 1 || filled)
                        {
                            u += s;
                        }
                        else
                        {
                            u += p;
                        }
                    }
                    u += "\n";
                }

                return u.Trim();
            }
        }

        /// <summary>
        /// Allskonar verkfæri
        /// </summary>
        public static class Tools
        {
            /// <summary>
            /// Mergar tvo strengi saman (mega vera margra lína)
            /// </summary>
            /// <param name="a">Fyrri strengurinn</param>
            /// <param name="b">Seinni strengurinn</param>
            /// <returns>Strengirnir saman</returns>
            public static string MergeStrings(string a, string b)
            {
                List<string> aS = a.Split(new char[] { '\n' }).ToList();
                List<string> bS = b.Split(new char[] { '\n' }).ToList();
                int aLen = aS.OrderByDescending(n => n.Length).ToArray()[0].Length;
                int bLen = bS.OrderByDescending(n => n.Length).ToArray()[0].Length;

                if (aS.Count > bS.Count)
                {
                    for (int i = 0; i < aS.Count - bS.Count; i++)
                    {
                        bS.Add("");
                    }
                }
                else if (bS.Count > aS.Count)
                {
                    for (int i = 0; i < bS.Count - aS.Count; i++)
                    {
                        aS.Add("");
                    }
                }

                for (int i = 0; i < aS.Count; i++)
                {
                    for (int k = aS[i].Length; k < aLen; k++)
                    {
                        aS[i] += " ";
                    }

                    aS[i] += bS[i];
                }

                return String.Join("\n", aS.ToArray());
            }

            /// <summary>
            /// Lætur strenginn verða ákveðið langann hvort sem hann er lengri eða styttri fyrir
            /// </summary>
            /// <param name="s"></param>
            /// <param name="length">Lengdin sem strengurinn verður</param>
            /// <param name="fill">Stafur sem notaður er til að fylla upp í ef að strengurinn er of stuttur</param>
            /// <returns>Strengurinn með rétta lengd</returns>
            public static string MakeLength(string s, int length, char fill)
            {
                if (s.Length < length)
                {
                    for (int i = s.Length; i < length; i++)
                    {
                        s += fill;
                    }
                }
                else if (s.Length > length)
                {
                    s = s.Substring(0, length);
                }

                return s;
            }

            /// <summary>
            /// Tekur inn 2D array og skilar því sem streng
            /// </summary>
            /// <param name="a">2D array</param>
            /// <returns>Arrayið sem strengur</returns>
            /// <example>{
            /// {"a","b","c"},
            /// {"a","b","c"},
            /// {"a","b","c"}
            /// }
            /// verður:
            /// abc
            /// abc
            /// abc</example>
            public static string TwoDArrayOutput(string[,] a)
            {
                string s = "";
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    if (i != 0)
                    {
                        s += "\n";
                    }

                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        s += a[i, j];
                    }
                }

                return s;
            }
        }

        /// <summary>
        /// Stærðfræði-tengd method
        /// </summary>
        public static class Stæ
        {
            /// <summary>
            /// Hefur N í veldið veldi
            /// </summary>
            /// <param name="N">Talan</param>
            /// <param name="veldi">Veldið sem talan á að fara í</param>
            /// <returns></returns>
            public static double HefjaIVeldi(double N, int veldi)
            {
                if (veldi < 0) return 0;
                if (veldi == 0) return 1;

                double tala = N;
                for (int i = 1; i < veldi; i++)
                {
                    tala *= N;
                }

                return tala;
            }

            /// <summary>
            /// PI
            /// </summary>
            /// <returns>PI</returns>
            private static decimal PI()
            {
                throw new NotImplementedException();
            }
        }
    }
}