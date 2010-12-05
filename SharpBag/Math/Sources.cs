using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SharpBag.Math
{
    /// <summary>
    /// A class for number sources.
    /// </summary>
    public static class Sources
    {
        #region Fibonacci numbers

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
        /// An endless source that will return fibonacci numbers.
        /// </summary>
        public static IEnumerable<BigInteger> FibonacciBig
        {
            get
            {
                BigInteger a = 0;
                BigInteger b = 1;
                BigInteger t = 0;

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

        #endregion Fibonacci numbers

        #region Prime numbers

        /// <summary>
        /// A possibly endless source that will return prime numbers.
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
        /// A possibly endless source that will return prime numbers.
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

        /// <summary>
        /// A possibly endless source that will return prime numbers.
        /// </summary>
        public static IEnumerable<BigInteger> PrimesBig
        {
            get
            {
                for (BigInteger i = 2; ; i++)
                {
                    if (BagMath.IsPrime(i)) yield return i;
                }
            }
        }

        #endregion Prime numbers

        #region Triangle numbers

        /// <summary>
        /// An endless source for triangle numbers.
        /// </summary>
        public static IEnumerable<int> TriangleNumbers
        {
            get
            {
                int triangle = 0;
                for (int i = 1; ; i++)
                {
                    triangle += i;
                    yield return triangle;
                }
            }
        }

        /// <summary>
        /// An endless source for triangle numbers.
        /// </summary>
        public static IEnumerable<long> TriangleNumbers64
        {
            get
            {
                long triangle = 0;
                for (long i = 1; ; i++)
                {
                    triangle += i;
                    yield return triangle;
                }
            }
        }

        /// <summary>
        /// An endless source for triangle numbers.
        /// </summary>
        public static IEnumerable<BigInteger> TriangleNumbersBig
        {
            get
            {
                BigInteger triangle = 0;
                for (BigInteger i = 1; ; i++)
                {
                    triangle += i;
                    yield return triangle;
                }
            }
        }

        #endregion Triangle numbers

        #region Perfect numbers

        /// <summary>
        /// An endless source for perfect numbers.
        /// </summary>
        public static IEnumerable<int> PerfectNumbers
        {
            get
            {
                return from i in 1.ToInfinity()
                       where i.FactorsButSelf().Sum() == i
                       select i;
            }
        }

        /// <summary>
        /// An endless source for perfect numbers.
        /// </summary>
        public static IEnumerable<long> PerfectNumbers64
        {
            get
            {
                return from i in 1L.ToInfinity()
                       where i.FactorsButSelf().Sum() == i
                       select i;
            }
        }

        /// <summary>
        /// An endless source for perfect numbers.
        /// </summary>
        public static IEnumerable<BigInteger> PerfectNumbersBig
        {
            get
            {
                return from i in BigInteger.One.ToInfinity()
                       where i.FactorsButSelf().Aggregate(BigInteger.Zero, (a, b) => a + b) == i
                       select i;
            }
        }

        #endregion Perfect numbers

        #region Deficient numbers

        /// <summary>
        /// An endless source for deficient numbers.
        /// </summary>
        public static IEnumerable<int> DeficientNumbers
        {
            get
            {
                return from i in 1.ToInfinity()
                       where i.FactorsButSelf().Sum() < i
                       select i;
            }
        }

        /// <summary>
        /// An endless source for deficient numbers.
        /// </summary>
        public static IEnumerable<long> DeficientNumbers64
        {
            get
            {
                return from i in 1L.ToInfinity()
                       where i.FactorsButSelf().Sum() < i
                       select i;
            }
        }

        /// <summary>
        /// An endless source for deficient numbers.
        /// </summary>
        public static IEnumerable<BigInteger> DeficientNumbersBig
        {
            get
            {
                return from i in BigInteger.One.ToInfinity()
                       where i.FactorsButSelf().Aggregate(BigInteger.Zero, (a, n) => a + n) < i
                       select i;
            }
        }

        #endregion Deficient numbers

        #region Abundant number

        /// <summary>
        /// An endless source for abundant number
        /// </summary>
        public static IEnumerable<int> AbundantNumbers
        {
            get
            {
                return from i in 1.ToInfinity()
                       where i.FactorsButSelf().Sum() > i
                       select i;
            }
        }

        /// <summary>
        /// An endless source for abundant number
        /// </summary>
        public static IEnumerable<long> AbundantNumbers64
        {
            get
            {
                return from i in 1L.ToInfinity()
                       where i.FactorsButSelf().Sum() > i
                       select i;
            }
        }

        /// <summary>
        /// An endless source for abundant number
        /// </summary>
        public static IEnumerable<BigInteger> AbundantNumbersBig
        {
            get
            {
                return from i in BigInteger.One.ToInfinity()
                       where i.FactorsButSelf().Aggregate(BigInteger.Zero, (a, n) => a + n) > i
                       select i;
            }
        }

        #endregion Abundant number
    }
}