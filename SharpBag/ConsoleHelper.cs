using System;

namespace SharpBag
{
    /// <summary>
    /// A class for helping with console IO.
    /// </summary>
    public class ConsoleHelper
    {
        private static ConsoleHelper Instance { get; set; }

        private ConsoleHelper() { }

        /// <summary>
        /// Returns an instance of the console helper.
        /// </summary>
        /// <returns>An instance of the console helper.</returns>
        public static ConsoleHelper GetInstance()
        {
            if (Instance == null) Instance = new ConsoleHelper();
            return Instance;
        }

        /// <summary>
        /// Writes the specified object to the console.
        /// </summary>
        /// <param name="o">The specified object.</param>
        /// <returns>The current instance (for chaining).</returns>
        public ConsoleHelper Write<T>(T o)
        {
            Console.Write(o);
            return this;
        }

        /// <summary>
        /// Writes the specified object to the console, and then puts the cursor on a new line.
        /// </summary>
        /// <param name="o">The specified object.</param>
        /// <returns>The current instance (for chaining).</returns>
        public ConsoleHelper WriteLine<T>(T o)
        {
            Console.WriteLine(o);
            return this;
        }

        /// <summary>
        /// Formats the specified string and objects with String.Format and the writes it to the console.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The current instance (for chaining).</returns>
        public ConsoleHelper Write(string format, params object[] args)
        {
            Console.Write(format, args);
            return this;
        }

        /// <summary>
        /// Formats the specified string and objects with String.Format, writes it to the console and the puts the cursor on a new line.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The current instance (for chaining).</returns>
        public ConsoleHelper WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            return this;
        }

        /// <summary>
        /// Reads a string from the console.
        /// </summary>
        /// <returns>The string read from the console.</returns>
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Reads a string from the console and then converts it to the specified type.
        /// </summary>
        /// <returns>The converted string read from the console.</returns>
        public T ReadLine<T>()
        {
            return Console.ReadLine().As<T>();
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return String.Empty;
        }
    }
}