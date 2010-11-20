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
            if (q != null)
            {
                Console.Write(q);
                Console.Write(separator);
            }

            return Console.ReadLine().As<T>();
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