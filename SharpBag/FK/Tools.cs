using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpBag.FK
{
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
}