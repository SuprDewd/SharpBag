using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBag;
using System.IO;

namespace SharpBag.FK.MVC
{
    /// <summary>
    /// An MVC model.
    /// </summary>
    public class FKModel
    {
        /// <summary>
        /// Read an object from the console.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="q">A description of the object.</param>
        /// <param name="separator">A separator between the description and the object.</param>
        /// <returns>The object that was read.</returns>
        public T Read<T>(string q = null, string separator = ": ")
        {
            this.WriteQuestion(q, separator);
            return Console.ReadLine().As<T>();
        }

        /// <summary>
        /// Read a string from the console.
        /// </summary>
        /// <param name="q">A description of the string.</param>
        /// <param name="separator">A separator between the description and the string.</param>
        /// <returns>The string that was read.</returns>
        public string Read(string q = null, string separator = ": ")
        {
            this.WriteQuestion(q, separator);
            return Console.ReadLine();
        }

        /// <summary>
        /// Read an integer from the console.
        /// </summary>
        /// <param name="q">A description of the integer.</param>
        /// <param name="separator">A separator between the description and the integer.</param>
        /// <returns>The integer that was read.</returns>
        public int ReadInt(string q = null, string separator = ": ")
        {
            this.WriteQuestion(q, separator);
            return Convert.ToInt32(Console.ReadLine());
        }

        /// <summary>
        /// Read an integer from the console.
        /// </summary>
        /// <param name="q">A description of the integer.</param>
        /// <param name="separator">A separator between the description and the integer.</param>
        /// <returns>The integer that was read.</returns>
        public double ReadDouble(string q = null, string separator = ": ")
        {
            this.WriteQuestion(q, separator);
            return Convert.ToDouble(Console.ReadLine());
        }

        private void WriteQuestion(string q = null, string separator = ": ")
        {
            if (q != null)
            {
                Console.Write(q);
                Console.Write(separator);
            }
        }

        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <param name="encoding">An encoding.</param>
        /// <returns>The content of the file.</returns>
        public string ReadFile(string fileName, Encoding encoding = null)
        {
            using (StreamReader sr = new StreamReader(fileName, encoding: encoding ?? Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <param name="encoding">An encoding.</param>
        /// <returns>The lines of the file.</returns>
        public IEnumerable<string> ReadFileLines(string fileName, Encoding encoding = null)
        {
            using (StreamReader sr = new StreamReader(fileName, encoding: encoding ?? Encoding.Default))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}