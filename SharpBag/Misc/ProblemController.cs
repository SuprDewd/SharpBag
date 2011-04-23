using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SharpBag.Comparers;
using SharpBag.Strings;

namespace SharpBag.Misc
{
    /// <summary>
    /// A problem controller.
    /// </summary>
    public abstract class ProblemController
    {
        /// <summary>
        /// The problems.
        /// </summary>
        public ProblemMetadata[] Problems { get; private set; }

        /// <summary>
        /// The title of the controller.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Whether to time all problems.
        /// </summary>
        public bool TimeAll { get; set; }

        private const char VerticalChar = '-';
        private const char HorizontalChar = '|';

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="title">The title of the controller.</param>
        /// <param name="timeAll">Whether to time all problems.</param>
        public ProblemController(string title = null, bool timeAll = false)
        {
            this.Output = true;
            this.Title = title;
            this.TimeAll = timeAll;
            this.Problems = (from m in this.GetType().GetMethods()
                             where m.IsPublic && Attribute.IsDefined(m, typeof(ProblemAttribute)) && !m.GetParameters().Any()
                             let attr = Attribute.GetCustomAttribute(m, typeof(ProblemAttribute)) as ProblemAttribute
                             select new ProblemMetadata(attr, m)).OrderBy(m => m.Title, new AlphaNumberComparer()).ToArray();
        }

        private string CurrentTitle = null;

        /// <summary>
        /// Run the controller.
        /// </summary>
        public void Run()
        {
            var f = this.Problems.FirstOrDefault(p => p.Start);
            if (f != null) while (true) this.ExecuteProblem(f);

            while (true)
            {
                Console.Clear();
                if (this.Title != null) this.WriteHeader(this.Title);
                this.ListProblems();
                Console.Write("\nRun: ");
                string run = Console.ReadLine();
                if (run == "") break;

                var m = SearchProblem(run);
                if (m == null) continue;

                this.ExecuteProblem(m);
            }
        }

        /// <summary>
        /// Clears the Console.
        /// </summary>
        public void ClearScreen()
        {
            if (!this.Output) return;
            Console.Clear();
            if (this.CurrentTitle != null) this.WriteHeader(this.CurrentTitle);
        }

        /// <summary>
        /// Whether to write to the console.
        /// </summary>
        public bool Output { get; set; }

        /// <summary>
        /// Displays the object.
        /// </summary>
        /// <param name="o">The object.</param>
        public void WriteLine(object o)
        {
            if (this.Output) Console.WriteLine(o);
        }

        /// <summary>
        /// Displays the object.
        /// </summary>
        /// <param name="o">The object.</param>
        public void Write(object o)
        {
            if (this.Output) Console.Write(o);
        }

        private void ExecuteProblem(ProblemMetadata m, bool time = false)
        {
            Console.Clear();
            if (m.Title != null) this.WriteHeader(this.CurrentTitle = (m.Title + (m.Description != null ? "\n" + m.Description : "")));
            else this.CurrentTitle = null;

            if ((m.Time.HasValue && m.Time.Value) || (!m.Time.HasValue && this.TimeAll) || time)
            {
                long exTime = Utils.ExecutionTime(() =>
                    {
                        m.Method.Invoke(this, new object[0]);
                    });

                Console.WriteLine();
                Console.Write("Time: ");
                Console.Write(exTime);
                Console.WriteLine(" (ms)");
            }
            else
            {
                m.Method.Invoke(this, new object[0]);
            }

            if (m.Pause) Console.ReadLine();
        }

        private ProblemMetadata SearchProblem(string s)
        {
            return this.Problems.OrderBy(p => p.Title.DistanceTo(s)).ThenBy(p => p.Title.StartsWithIgnoreCase(s) ? 0 : 1).ThenBy(p => p.Title.ContainsIgnoreCase(s) ? 0 : 1).FirstOrDefault();
        }

        private void ListProblems()
        {
            int maxTitle = this.Problems.Max(p => p.Title.Length);
            this.Problems.ForEach(p => Console.WriteLine(p.ToString(maxTitle)));
        }

        private void WriteHeader(string header)
        {
            var lines = header.Lines();
            int length = lines.Select(l => l.Length).Max();
            string verticalString = new String(VerticalChar, length + 4);

            Console.WriteLine(verticalString);

            foreach (string line in lines)
            {
                Console.Write(HorizontalChar);
                Console.Write(' ');
                Console.Write(line);
                Console.Write(new String(' ', length - line.Length + 1));
                Console.WriteLine(HorizontalChar);
            }

            Console.WriteLine(verticalString);
            Console.WriteLine();
        }

        /// <summary>
        /// Read an object from the console.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="q">A description of the object.</param>
        /// <param name="separator">A separator between the description and the object.</param>
        /// <param name="validator">A validator function.</param>
        /// <returns>The object that was read.</returns>
        public T Read<T>(string q = null, string separator = ": ", Func<T, bool> validator = null)
        {
            T o;

            do
            {
                this.WriteQuery(q, separator);
                o = Console.ReadLine().As<T>();
            } while (validator != null && !validator(o));

            return o;
        }

        /// <summary>
        /// Read a string from the console.
        /// </summary>
        /// <param name="q">A description of the string.</param>
        /// <param name="separator">A separator between the description and the string.</param>
        /// <param name="validator">A validator function.</param>
        /// <returns>The string that was read.</returns>
        public string Read(string q = null, string separator = ": ", Func<string, bool> validator = null)
        {
            string s;

            do
            {
                this.WriteQuery(q, separator);
                s = Console.ReadLine();
            } while (validator != null && !validator(s));

            return s;
        }

        /// <summary>
        /// Read a double from the console.
        /// </summary>
        /// <param name="q">A description of the string.</param>
        /// <param name="separator">A separator between the description and the string.</param>
        /// <param name="validator">A validator function.</param>
        /// <returns>The string that was read.</returns>
        public double ReadDouble(string q = null, string separator = ": ", Func<double, bool> validator = null)
        {
            double s;

            do
            {
                this.WriteQuery(q, separator);
                s = Console.ReadLine().Replace('.', ',').As<double>();
            } while (validator != null && !validator(s));

            return s;
        }

        private void WriteQuery(string q = null, string separator = ": ")
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
            using (StreamReader sr = new StreamReader(fileName, encoding ?? Encoding.Default))
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
            using (StreamReader sr = new StreamReader(fileName, encoding ?? Encoding.Default))
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