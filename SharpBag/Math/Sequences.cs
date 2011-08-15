using System;
using System.Collections.Generic;
using System.Linq;



using System.Numerics;
using System.Diagnostics.Contracts;



namespace SharpBag.Math
{
	/// <summary>
	/// A class for sequences.
	/// </summary>
	public static class Sequences
	{
		#region Fibonacci numbers

		/// <summary>
		/// An endless source of fibonacci numbers.
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
		/// An endless source of fibonacci numbers.
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



		/// <summary>
		/// An endless source of fibonacci numbers.
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



		#endregion Fibonacci numbers

		#region Factorial numbers

		/// <summary>
		/// An endless source of factorial numbers.
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
		/// An endless source of factorial numbers.
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
		/// An endless source of factorial numbers.
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
		/// An endless source of prime numbers.
		/// </summary>
		public static IEnumerable<int> Primes
		{
			get
			{
				yield return 2;
				yield return 3;

				for (int i = 5; ; i += 6)
				{
					if (BagMath.IsPrime(i)) yield return i;
					if (BagMath.IsPrime(i + 2)) yield return i + 2;
				}
			}
		}

		/// <summary>
		/// An endless source of prime numbers.
		/// </summary>
		public static IEnumerable<long> Primes64
		{
			get
			{
				yield return 2;
				yield return 3;

				for (long i = 5; ; i += 6)
				{
					if (BagMath.IsPrime(i)) yield return i;
					if (BagMath.IsPrime(i + 2)) yield return i + 2;
				}
			}
		}



		/// <summary>
		/// An endless source of prime numbers.
		/// </summary>
		public static IEnumerable<BigInteger> PrimesBig
		{
			get
			{
				yield return 2;
				yield return 3;

				for (BigInteger i = 5; ; i += 6)
				{
					if (BagMath.IsPrime(i)) yield return i;
					if (BagMath.IsPrime(i + 2)) yield return i + 2;
				}
			}
		}



		#endregion Prime numbers

		#region Perfect numbers

		/// <summary>
		/// An endless source of perfect numbers.
		/// </summary>
		public static IEnumerable<int> PerfectNumbers
		{
			get
			{
				return from i in 1.ToInfinity()
					   where i.DivisorSum() - i == i
					   select i;
			}
		}

		/// <summary>
		/// An endless source of perfect numbers.
		/// </summary>
		public static IEnumerable<long> PerfectNumbers64
		{
			get
			{
				return from i in 1L.ToInfinity()
					   where i.DivisorSum() - i == i
					   select i;
			}
		}



		/// <summary>
		/// An endless source of perfect numbers.
		/// </summary>
		public static IEnumerable<BigInteger> PerfectNumbersBig
		{
			get
			{
				return from i in BigInteger.One.ToInfinity()
					   where i.DivisorSum() - i == i
					   select i;
			}
		}



		#endregion Perfect numbers

		#region Deficient numbers

		/// <summary>
		/// An endless source of deficient numbers.
		/// </summary>
		public static IEnumerable<int> DeficientNumbers
		{
			get
			{
				return from i in 1.ToInfinity()
					   where i.DivisorSum() - i < i
					   select i;
			}
		}

		/// <summary>
		/// An endless source of deficient numbers.
		/// </summary>
		public static IEnumerable<long> DeficientNumbers64
		{
			get
			{
				return from i in 1L.ToInfinity()
					   where i.DivisorSum() - i < i
					   select i;
			}
		}



		/// <summary>
		/// An endless source of deficient numbers.
		/// </summary>
		public static IEnumerable<BigInteger> DeficientNumbersBig
		{
			get
			{
				return from i in BigInteger.One.ToInfinity()
					   where i.DivisorSum() - i < i
					   select i;
			}
		}



		#endregion Deficient numbers

		#region Abundant number

		/// <summary>
		/// An endless source of abundant number.
		/// </summary>
		public static IEnumerable<int> AbundantNumbers
		{
			get
			{
				return from i in 1.ToInfinity()
					   where i.DivisorSum() - i > i
					   select i;
			}
		}

		/// <summary>
		/// An endless source of abundant number.
		/// </summary>
		public static IEnumerable<long> AbundantNumbers64
		{
			get
			{
				return from i in 1L.ToInfinity()
					   where i.DivisorSum() - i > i
					   select i;
			}
		}



		/// <summary>
		/// An endless source of abundant number.
		/// </summary>
		public static IEnumerable<BigInteger> AbundantNumbersBig
		{
			get
			{
				return from i in BigInteger.One.ToInfinity()
					   where i.DivisorSum() - i > i
					   select i;
			}
		}



		#endregion Abundant number

		#region Pythagorean Triplets

		/// <summary>
		/// An endless source of Pythagorean Triplets.
		/// </summary>
		public static IEnumerable<Tuple<int, int, int>> PythagoreanTriplets
		{
			get
			{
				for (int m = 2; ; m++)
				{
					int msq = m * m;
					for (int n = 1; n < m; n++)
					{
						int nsq = n * n;
						yield return new Tuple<int, int, int>(msq - nsq, 2 * m * n, msq + nsq);
					}
				}
			}
		}

		/// <summary>
		/// An endless source of Pythagorean Triplets.
		/// </summary>
		public static IEnumerable<Tuple<long, long, long>> PythagoreanTriplets64
		{
			get
			{
				for (long m = 2; ; m++)
				{
					long msq = m * m;
					for (long n = 1; n < m; n++)
					{
						long nsq = n * n;
						yield return new Tuple<long, long, long>(msq - nsq, 2 * m * n, msq + nsq);
					}
				}
			}
		}



		/// <summary>
		/// An endless source of Pythagorean Triplets.
		/// </summary>
		public static IEnumerable<Tuple<BigInteger, BigInteger, BigInteger>> PythagoreanTripletsBig
		{
			get
			{
				for (BigInteger m = 2; ; m++)
				{
					BigInteger msq = m * m;
					for (BigInteger n = 1; n < m; n++)
					{
						BigInteger nsq = n * n;
						yield return new Tuple<BigInteger, BigInteger, BigInteger>(msq - nsq, 2 * m * n, msq + nsq);
					}
				}
			}
		}



		#endregion Pythagorean Triplets

		#region Figurate Numbers

		/// <summary>
		/// Numbers which are n-figurate.
		/// </summary>
		/// <param name="n">The n in n-figurate.</param>
		/// <returns>The numbers.</returns>
		public static IEnumerable<int> FigurateNumbers(int n)
		{

			Contract.Requires(n >= 0);

			int cur = 0;

			for (int i = 1; ; i += n)
			{
				cur += i;
				yield return cur;
			}
		}

		/// <summary>
		/// Numbers which are n-figurate.
		/// </summary>
		/// <param name="n">The n in n-figurate.</param>
		/// <returns>The numbers.</returns>
		public static IEnumerable<long> FigurateNumbers(long n)
		{

			Contract.Requires(n >= 0);

			long cur = 0;

			for (long i = 1; ; i += n)
			{
				cur += i;
				yield return cur;
			}
		}



		/// <summary>
		/// Numbers which are n-figurate.
		/// </summary>
		/// <param name="n">The n in n-figurate.</param>
		/// <returns>The numbers.</returns>
		public static IEnumerable<BigInteger> FigurateNumbers(BigInteger n)
		{
			Contract.Requires(n >= 0);
			BigInteger cur = 0;

			for (BigInteger i = 1; ; i += n)
			{
				cur += i;
				yield return cur;
			}
		}



		#endregion Figurate Numbers
	}
}