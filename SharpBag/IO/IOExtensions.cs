using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SharpBag.IO
{
    /// <summary>
    /// A static class with IO extensions.
    /// </summary>
    public static class IOExtensions
    {
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
        /// Converts a byte array to a string, using its byte order mark to convert it to the right encoding.
        /// </summary>
        /// <param name="buffer">The current instance.</param>
        /// <returns>The bytes as a string.</returns>
        /// <remarks>http://www.west-wind.com/WebLog/posts/197245.aspx</remarks>
        public static string GetString(this byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0) return "";

            Encoding encoding = Encoding.Default;

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf) encoding = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff) encoding = Encoding.Unicode;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff) encoding = Encoding.BigEndianUnicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff) encoding = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76) encoding = Encoding.UTF7;

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}