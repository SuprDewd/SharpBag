using System.Collections.Generic;
using System.Linq;

namespace SharpBag.FK
{
    /// <summary>
    /// Klasi sem finnur stærstu runu-summu.
    /// </summary>
    public class StærstaRunuSumma
    {
        /// <summary>
        /// Runan.
        /// </summary>
        public List<int> Runa;
        /// <summary>
        /// Stærsta runu-summan.
        /// </summary>
        public int StærstaSumma;
        /// <summary>
        /// Allar stærstu runurnar.
        /// </summary>
        public List<List<int>> StærstuRunur;

        /// <summary>
        /// Smiður fyrir StærstaRunuSumma-klasann.
        /// </summary>
        /// <param name="runa">Array af tölum.</param>
        public StærstaRunuSumma(int[] runa)
        {
            Runa = runa.ToList();
            FinnaStærstuSummu();
        }

        /// <summary>
        /// Smiður fyrir StærstaRunuSumma-klasann.
        /// </summary>
        /// <param name="runa">Listi af tölum.</param>
        public StærstaRunuSumma(List<int> runa)
        {
            Runa = runa;
            FinnaStærstuSummu();
        }

        /// <summary>
        /// Finnur stærstu summuna.
        /// </summary>
        public void FinnaStærstuSummu()
        {
            if (Runa.Count == 0)
            {
                StærstuRunur = new List<List<int>>() {
                    Runa
                };
                StærstaSumma = 0;
            }
            else if (Runa.Count == 1)
            {
                StærstuRunur = new List<List<int>>() {
                    Runa
                };
                StærstaSumma = Runa[0];
            }

            List<List<int>> mo = Moguleikar();

            var max = from m in mo
                      orderby m.Sum() descending
                      select m.Sum();

            StærstaSumma = max.ToArray()[0];

            StærstuRunur = (from m in mo
                            where m.Sum() == StærstaSumma
                            select m).ToList();
        }

        /// <summary>
        /// Finnur allar mögulegar runur.
        /// </summary>
        /// <returns>Allar mögulegar runur.</returns>
        public List<List<int>> Moguleikar()
        {
            List<List<int>> ut = new List<List<int>>();
            for (int i = 1; i <= Runa.Count; i++)
            {
                for (int a = 0; a <= Runa.Count - i; a++)
                {
                    ut.Add(Runa.GetRange(a, i));
                }
            }

            return ut;
        }
    }
}
