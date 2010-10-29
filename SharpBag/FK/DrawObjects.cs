using System;

namespace SharpBag.FK
{
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
}
