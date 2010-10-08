using System.Collections.Generic;

namespace SharpBag.BagMath
{
    /// <summary>
    /// A class for computing fibonacci numbers.
    /// </summary>
    public static class Sources
    {
        /// <summary>
        /// An endless source that will return fibonacci numbers.
        /// </summary>
        public static IEnumerable<int> Fibonacci
        {
            get
            {
                int a = 0;
                int b = 1;
                int t = 0;

                yield return a;
                yield return b;

                while (true)
                {
                    yield return t = a + b;
                    a = b;
                    b = t;
                }
            }
        }

        /// <summary>
        /// An endless source that will return fibonacci numbers.
        /// </summary>
        public static IEnumerable<long> Fibonacci64
        {
            get
            {
                long a = 0;
                long b = 1;
                long t = 0;

                yield return a;
                yield return b;

                while (true)
                {
                    yield return t = a + b;
                    a = b;
                    b = t;
                }
            }
        }

        /// <summary>
        /// An endless source that will return prime numbers.
        /// </summary>
        public static IEnumerable<int> Primes
        {
            get
            {
                for (int i = 2; ; i++)
                {
                    if (BagMath.IsPrime(i)) yield return i;
                }
            }
        }

        /// <summary>
        /// An endless source that will return prime numbers.
        /// </summary>
        public static IEnumerable<long> Primes64
        {
            get
            {
                for (long i = 2; ; i++)
                {
                    if (BagMath.IsPrime(i)) yield return i;
                }
            }
        }
    }
}