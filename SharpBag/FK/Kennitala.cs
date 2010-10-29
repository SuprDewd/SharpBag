using System;
using System.Text.RegularExpressions;

namespace SharpBag.FK
{
    /// <summary>
    /// Klasi fyrir kennitölur.
    /// </summary>
    public class Kennitala
    {
        private string Upprunalegt;
        /// <summary>
        /// Fyrir hvaða dag er kennitalan.
        /// </summary>
        public int Dagur = -1;
        /// <summary>
        /// Fyrir hvaða mánuð er kennitalan.
        /// </summary>
        public int Manudur = -1;
        /// <summary>
        /// Fyrir hvaða ár er kennitalan.
        /// </summary>
        public int Ar = -1;
        /// <summary>
        /// 4 stafa lokatala kennitölunar.
        /// </summary>
        public int Lokatala = -1;
        /// <summary>
        /// Hvort kennitalan sé gild.
        /// </summary>
        public bool ErILagi = false;
        /// <summary>
        /// Stjörnumerki kennitölunar.
        /// </summary>
        public string Stjornumerki = "";

        /// <summary>
        /// Smiður kennitölu-klasans.
        /// </summary>
        /// <param name="kt">Kennitalan sem strengur (ddmmyy-nnnn).</param>
        public Kennitala(string kt)
        {
            Upprunalegt = kt;
            ErILagi = ErRett(kt);

            if (ErILagi)
            {
                Dagur = Int32.Parse(kt.Substring(0, 2));
                Manudur = Int32.Parse(kt.Substring(2, 2));
                Ar = Int32.Parse(kt.Substring(4, 2));
                Lokatala = Int32.Parse(kt.Substring(kt.Length - 4));
                Stjornumerki = StjMrki();
            }
        }

        /// <summary>
        /// Smiður kennitölu-klasans.
        /// </summary>
        /// <param name="ar">Ár kennitölunnar.</param>
        /// <param name="manudur">Mánuður kennitölunnar.</param>
        /// <param name="dagur">Dagur kennitölunnar.</param>
        /// <param name="lokatala">4 stafa lokatala kennitölunnar.</param>
        public Kennitala(int ar, int manudur, int dagur, int lokatala)
        {
            Ar = ar;
            Manudur = manudur;
            Dagur = dagur;
            Lokatala = lokatala;
            Stjornumerki = StjMrki();
            ErILagi = lokatala <= 9999;
        }

        private string StjMrki()
        {
            string[] Merki = new string[] { "Hrútur", "Naut", "Tvíburi", "Krabbi", "Ljón", "Meyja", "Vog", "Sporðdreki", "Bogmaður", "Steingeit", "Vatnsberi", "Fiskur" };
            DateTime[][] Timar = new DateTime[][] {
                new DateTime[]{new DateTime(Ar, 3,  21), new DateTime(Ar, 4,  20)},
                new DateTime[]{new DateTime(Ar, 4,  21), new DateTime(Ar, 5,  21)},
                new DateTime[]{new DateTime(Ar, 5,  22), new DateTime(Ar, 6,  20)},
                new DateTime[]{new DateTime(Ar, 6,  21), new DateTime(Ar, 7,  23)},
                new DateTime[]{new DateTime(Ar, 7,  24), new DateTime(Ar, 8,  23)},
                new DateTime[]{new DateTime(Ar, 8,  24), new DateTime(Ar, 9,  23)},
                new DateTime[]{new DateTime(Ar, 9,  24), new DateTime(Ar, 10, 22)},
                new DateTime[]{new DateTime(Ar, 10, 23), new DateTime(Ar, 11, 21)},
                new DateTime[]{new DateTime(Ar, 11, 22), new DateTime(Ar, 12, 21)},
                new DateTime[]{new DateTime(Ar, 12, 22), new DateTime(Ar, 1,  20)},
                new DateTime[]{new DateTime(Ar, 1,  21), new DateTime(Ar, 2,  19)},
                new DateTime[]{new DateTime(Ar, 2,  20), new DateTime(Ar, 3,  02)}
            };

            DateTime kt = new DateTime(Ar, Manudur, Dagur);

            for (int i = 0; i < Timar.Length; i++)
            {
                if (kt >= Timar[i][0] && kt <= Timar[i][1])
                {
                    return Merki[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Tekur inn streng sem inniheldur hugsanlega kennitölu og skilar true ef það er kennitala; annars false.
        /// </summary>
        /// <param name="kt">Strengur sem inniheldur hugsanlega kennitölu.</param>
        /// <returns>True ef strengurinn er kennitala; annars false.</returns>
        public static bool ErRett(string kt)
        {
            if (!Regex.IsMatch(kt, "[0-9]{6}-?[0-9]{4}")) return false;

            try
            {
                int d = Int32.Parse(kt.Substring(0, 2));
                int m = Int32.Parse(kt.Substring(2, 2));
                int y = Int32.Parse(kt.Substring(4, 2));
                int l = Int32.Parse(kt.Substring(kt.Length - 4));

                return (d >= 1 && d <= 31 && m >= 1 && m <= 12 && y >= 0 && y <= 99 && l >= 0 && l <= 9999);
            }
            catch { return false; };
        }

        /// <summary>
        /// Skilar kennitölunni með eða án - sem streng.
        /// </summary>
        /// <param name="skiptir">Hvort nota eigi - eða ekki.</param>
        /// <returns>Kennitalan sem strengur.</returns>
        public string ToString(bool skiptir)
        {
            if (!ErILagi) return "";

            return Dagur.ToString() + Manudur.ToString() + Ar.ToString() + (skiptir ? "-" : "") + Lokatala.ToString();
        }

        /// <summary>
        /// Skilar kennitölunni með - sem streng.
        /// </summary>
        /// <returns>Kennitalan sem strengur.</returns>
        public override string ToString()
        {
            return ToString(true);
        }
    }
}
