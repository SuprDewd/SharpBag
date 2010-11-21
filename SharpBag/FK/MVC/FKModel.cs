using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBag;
using System.IO;

namespace SharpBag.FK.MVC
{
    public class FKModel
    {
        public T Read<T>(string q = null, string separator = ": ")
        {
            this.WriteQuestion(q, separator);
            return Console.ReadLine().As<T>();
        }

        public string Read(string q = null, string separator = ": ")
        {
            this.WriteQuestion(q, separator);
            return Console.ReadLine();
        }

        public int ReadInt(string q = null, string separator = ": ")
        {
            this.WriteQuestion(q, separator);
            return Convert.ToInt32(Console.ReadLine());
        }

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

        public string ReadFile(string fileName, Encoding encoding = null)
        {
            using (StreamReader sr = new StreamReader(fileName, encoding: encoding ?? Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }

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