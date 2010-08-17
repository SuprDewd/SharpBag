using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using MySql.Data.MySqlClient;

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
}
