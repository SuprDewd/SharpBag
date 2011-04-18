using System.Collections.Generic;
using System.Linq;

#if DOTNET4

using System.Numerics;

#endif

namespace SharpBag.Math
{
    /// <summary>
    /// A class for number sources.
    /// </summary>
    public static class Sequences
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
                int t;

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
                long t;

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

#if DOTNET4

        /// <summary>
        /// An endless source that will return fibonacci numbers.
        /// </summary>
        public static IEnumerable<BigInteger> FibonacciBig
        {
            get
            {
                BigInteger a = 0;
                BigInteger b = 1;
                BigInteger t;

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

#endif

        #endregion Fibonacci numbers

        #region Factorial numbers

        /// <summary>
        /// An endless source that will return factorial numbers.
        /// </summary>
        public static IEnumerable<int> Factorial
        {
            get
            {
                int cur = 1;

                for (int i = 1; ; i++)
                {
                    yield return cur;
                    cur *= i;
                }
            }
        }

        /// <summary>
        /// An endless source that will return factorial numbers.
        /// </summary>
        public static IEnumerable<long> Factorial64
        {
            get
            {
                long cur = 1;

                for (long i = 1; ; i++)
                {
                    yield return cur;
                    cur *= i;
                }
            }
        }

        /// <summary>
        /// An endless source that will return factorial numbers.
        /// </summary>
        public static IEnumerable<BigInteger> FactorialBig
        {
            get
            {
                BigInteger cur = 1;

                for (BigInteger i = 1; ; i++)
                {
                    yield return cur;
                    cur *= i;
                }
            }
        }

        #endregion Factorial numbers

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

#if DOTNET4

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

#endif

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

#if DOTNET4

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

#endif

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
                       where i.Divisors().Sum() == i
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
                       where i.Divisors().Sum() == i
                       select i;
            }
        }

#if DOTNET4

        /// <summary>
        /// An endless source for perfect numbers.
        /// </summary>
        public static IEnumerable<BigInteger> PerfectNumbersBig
        {
            get
            {
                return from i in BigInteger.One.ToInfinity()
                       where i.Divisors().Aggregate(BigInteger.Zero, (a, b) => a + b) == i
                       select i;
            }
        }

#endif

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
                       where i.Divisors().Sum() < i
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
                       where i.Divisors().Sum() < i
                       select i;
            }
        }

#if DOTNET4

        /// <summary>
        /// An endless source for deficient numbers.
        /// </summary>
        public static IEnumerable<BigInteger> DeficientNumbersBig
        {
            get
            {
                return from i in BigInteger.One.ToInfinity()
                       where i.Divisors().Aggregate(BigInteger.Zero, (a, n) => a + n) < i
                       select i;
            }
        }

#endif

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
                       where i.Divisors().Sum() > i
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
                       where i.Divisors().Sum() > i
                       select i;
            }
        }

#if DOTNET4

        /// <summary>
        /// An endless source for abundant number
        /// </summary>
        public static IEnumerable<BigInteger> AbundantNumbersBig
        {
            get
            {
                return from i in BigInteger.One.ToInfinity()
                       where i.Divisors().Aggregate(BigInteger.Zero, (a, n) => a + n) > i
                       select i;
            }
        }

#endif

        #endregion Abundant number
    }
}