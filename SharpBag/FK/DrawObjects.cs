using System;
using SharpBag.Strings;
using System.Text;

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
        /// <param name="TT">Týpan af þríhyrningi</param>
        /// <param name="s">Strengurinn sem er notaður inní þríhyrningnum</param>
        /// <param name="p">Strengurinn sem er notaður fyrir utan þríhyrninginn</param>
        /// <returns>Þríhyrningurinn</returns>
        /// <example>Triangle(5, TriangleType.Top, "*", " ")</example>
        public static string Triangle(int height, TriangleType TT, string s = "#", string p = " ")
        {
            if (height == 0) return "";
            if (height == 1) return s;

            StringBuilder t = new StringBuilder();

            switch (TT)
            {
                case TriangleType.TopLeft:
                    {
                        for (int i = height; i >= 0; i--)
                        {
                            for (int a = 0; a < i; a++)
                            {
                                t.Append(s);
                            }

                            t.AppendLine();
                        }

                        return t.ToString().TrimEnd();
                    }
                case TriangleType.BottomLeft:
                    {
                        for (int i = 1; i <= height; i++)
                        {
                            for (int a = 0; a < i; a++)
                            {
                                t.Append(s);
                            }

                            t.AppendLine();
                        }

                        return t.ToString().TrimEnd();
                    }
                case TriangleType.TopRight:
                    {
                        for (int i = height - 1; i >= 0; i--)
                        {
                            for (int a = height - 1; a >= 0; a--)
                            {
                                if (a <= i)
                                {
                                    t.Append(s);
                                }
                                else
                                {
                                    t.Append(p);
                                }
                            }

                            t.AppendLine();
                        }

                        return t.ToString().TrimEnd();
                    }
                case TriangleType.BottomRight:
                    {
                        for (int i = 0; i < height; i++)
                        {
                            for (int a = height - 1; a >= 0; a--)
                            {
                                if (a > i)
                                {
                                    t.Append(p);
                                }
                                else
                                {
                                    t.Append(s);
                                }
                            }

                            t.AppendLine();
                        }

                        return t.ToString().TrimEnd();
                    }
                case TriangleType.Left:
                    {
                        t.AppendLine(Triangle(height - 1, TriangleType.BottomLeft, s, p));

                        for (int i = 0; i < height; i++)
                        {
                            t.Append(s);
                        }

                        t.AppendLine().Append(Triangle(height - 1, TriangleType.TopLeft, s, p));
                        return t.ToString().TrimEnd();
                    }
                case TriangleType.Top:
                    {
                        t.Append(Triangle(height - 1, TriangleType.TopRight, s, p));
                        StringBuilder l = new StringBuilder();

                        for (int i = 0; i < height; i++)
                        {
                            l.AppendLine(s);
                        }

                        return Tools.MergeStrings(Tools.MergeStrings(t.ToString(), l.ToString().TrimEnd()), Triangle(height - 1, TriangleType.TopLeft, s, p)).TrimEnd();
                    }
                case TriangleType.Bottom:
                    {
                        t.Append(Triangle(height - 1, TriangleType.BottomRight, s, p));
                        StringBuilder l = new StringBuilder();

                        for (int i = 0; i < height; i++)
                        {
                            l.AppendLine(s);
                        }

                        return Tools.MergeStrings(Tools.MergeStrings(t.Insert(0, "\n").ToString(), l.ToString().TrimEnd()), "\n" + Triangle(height - 1, TriangleType.BottomLeft, s, p)).TrimEnd();
                    }
                case TriangleType.Right:
                    {
                        string[] BR = Triangle(height - 1, TriangleType.BottomRight, s, p).Lines();
                        string[] TR = Triangle(height - 1, TriangleType.TopRight, s, p).Lines();

                        for (int i = 0; i < BR.Length; i++)
                        {
                            BR[i] = " " + BR[i];
                            TR[i] = " " + TR[i];
                        }

                        t.AppendLine(String.Join("\n", BR));

                        for (int i = 0; i < height; i++)
                        {
                            t.Append(s);
                        }

                        return t.Append("\n" + String.Join("\n", TR)).ToString().TrimEnd();
                    }
            }

            return "";
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
        public static string Square(int height, int width, string s = "#", string p = " ", bool filled = true)
        {
            if (height == 0 || width == 0) return "";
            if (height == width && width == 1) return s;

            StringBuilder u = new StringBuilder();
            for (int i = 0; i < height; i++)
            {
                for (int a = 0; a < width; a++)
                {
                    if (filled || i == 0 || a == 0 || i == height - 1 || a == width - 1)
                    {
                        u.Append(s);
                    }
                    else
                    {
                        u.Append(p);
                    }
                }

                u.AppendLine();
            }

            return u.ToString().TrimEnd();
        }
    }
}
