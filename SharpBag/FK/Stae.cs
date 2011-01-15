#if DOTNET4
using System.Diagnostics.Contracts;
#endif

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
        public static decimal PI(int rounds = 1000000)
        {
#if DOTNET4
            Contract.Requires(rounds > 0);
#endif
            decimal pi = 1;
            bool minus = true;

            for (decimal i = 3; i < rounds * 2 + 3; i += 2)
            {
                if (minus) pi -= 1M / i;
                else pi += 1M / i;

                minus = !minus;
            }

            return pi * 4;
        }
    }
}