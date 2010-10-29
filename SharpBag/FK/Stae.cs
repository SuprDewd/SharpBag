using System;

namespace SharpBag.FK
{
    /// <summary>
    /// Stærðfræði-tengd method
    /// </summary>
    public static class Stæ
    {
        /// <summary>
        /// Hefur N í veldið veldi
        /// </summary>
        /// <param name="N">Talan</param>
        /// <param name="veldi">Veldið sem talan á að fara í</param>
        /// <returns></returns>
        public static double HefjaIVeldi(double N, int veldi)
        {
            if (veldi < 0) return 0;
            if (veldi == 0) return 1;

            double tala = N;
            for (int i = 1; i < veldi; i++)
            {
                tala *= N;
            }

            return tala;
        }

        /// <summary>
        /// PI
        /// </summary>
        /// <returns>PI</returns>
        private static decimal PI()
        {
            throw new NotImplementedException();
        }
    }
}
