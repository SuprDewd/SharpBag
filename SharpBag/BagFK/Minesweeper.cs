
namespace SharpBag.BagFK
{
    /// <summary>
    /// Method sem gætu verið notuð fyrir Minesweeper leikinn
    /// </summary>
    public static class Minesweeper
    {
        /// <summary>
        /// Gáir hvað margar sprengjur eru í kringum reit
        /// </summary>
        /// <param name="field">Spilaborðið</param>
        /// <param name="bomb">Hvernig sprengjan er</param>
        /// <param name="x">X-hnitin á reitnum</param>
        /// <param name="y">Y-hnitin á reitnum</param>
        /// <returns>Hversu margar sprengjur eru í kringum reitinn</returns>
        public static int ManyBombsAround(string[,] field, string bomb, int x, int y)
        {
            int bombsAround = 0;
            string[] ps = new string[] { "-1,-1", "-1,0", "-1,1", "0,-1", "0,1", "1,-1", "1,0", "1,1" };

            foreach (string p in ps)
            {
                int[] t = p.SplitIntoInts(',');
                int cX = x + t[0];
                int cY = y + t[1];
                if (OnField(field.GetLength(0), field.GetLength(1), cX, cY) && field[cX, cY] == bomb)
                {
                    bombsAround++;
                }
            }

            return bombsAround;
        }

        /// <summary>
        /// Athugar hvort hnit séu inná borðinu
        /// </summary>
        /// <param name="w">Breydd borðsins</param>
        /// <param name="h">Hæð borðsins</param>
        /// <param name="x">X-hnitið</param>
        /// <param name="y">Y-hnitið</param>
        /// <returns>Hvort hnitin séu inná borðinu</returns>
        public static bool OnField(int w, int h, int x, int y)
        {
            return (x.IsBetweenOrEqualTo(0, w - 1) && y.IsBetweenOrEqualTo(0, h - 1));
        }
    }
}
