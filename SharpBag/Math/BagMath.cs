using System.Collections.Generic;
using System.Threading.Tasks;
using S = System;

using System;

#if DOTNET4

using System.Numerics;
using System.Diagnostics.Contracts;

#endif

namespace SharpBag.Math
{
	/// <summary>
	/// A static class containing methods for math.
	/// </summary>
	public static class BagMath
	{
		#region Gcd & Lcm

		/// <summary>
		/// Finds the greatest common divisor (gcd) of the specified integers.
		/// </summary>
		/// <param name="a">An integer.</param>
		/// <param name="b">An integer.</param>
		/// <returns>The greatest common divisor (gcd) of the specified integers.</returns>
		public static int Gcd(int a, int b)
		{
			if (a == 1 || b == 1) return 1;
			int c;
			if (a < 0) a = -a;
			if (b < 0) b = -b;

			while (b > 1)
			{
				c = b;
				b = a % b;
				a = c;
			}

			return b == 1 ? 1 : a;
		}

		/// <summary>
		/// Finds the least common multiple (lcm) of the specified integers.
		/// </summary>
		/// <param name="a">An integer.</param>
		/// <param name="b">An integer.</param>
		/// <returns>The least common multiple (lcm) of the specified integers.</returns>
		public static int Lcm(int a, int b)
		{
			return (a / Gcd(a, b)) * b;
		}

		/// <summary>
		/// Finds the greatest common divisor (gcd) of the specified integers.
		/// </summary>
		/// <param name="a">An integer.</param>
		/// <param name="b">An integer.</param>
		/// <returns>The greatest common divisor (gcd) of the specified integers.</returns>
		public static long Gcd(long a, long b)
		{
			if (a == 1 || b == 1) return 1;
			long c;
			if (a < 0) a = -a;
			if (b < 0) b = -b;

			while (b > 1)
			{
				c = b;
				b = a % b;
				a = c;
			}

			return b == 1 ? 1 : a;
		}

		/// <summary>
		/// Finds the least common multiple (lcm) of the specified integers.
		/// </summary>
		/// <param name="a">An integer.</param>
		/// <param name="b">An integer.</param>
		/// <returns>The least common multiple (lcm) of the specified integers.</returns>
		public static long Lcm(long a, long b)
		{
			return (a / Gcd(a, b)) * b;
		}

#if DOTNET4

		/// <summary>
		/// Finds the greatest common divisor (gcd) of the specified integers.
		/// </summary>
		/// <param name="a">An integer.</param>
		/// <param name="b">An integer.</param>
		/// <returns>The greatest common divisor (gcd) of the specified integers.</returns>
		public static BigInteger Gcd(BigInteger a, BigInteger b)
		{
			if (a == 1 || b == 1) return 1;
			BigInteger c;
			if (a < 0) a = -a;
			if (b < 0) b = -b;

			while (b > 1)
			{
				c = b;
				b = a % b;
				a = c;
			}

			return b == 1 ? 1 : a;
		}

		/// <summary>
		/// Finds the least common multiple (lcm) of the specified integers.
		/// </summary>
		/// <param name="a">An integer.</param>
		/// <param name="b">An integer.</param>
		/// <returns>The least common multiple (lcm) of the specified integers.</returns>
		public static BigInteger Lcm(BigInteger a, BigInteger b)
		{
			return (a / Gcd(a, b)) * b;
		}

#endif

		#endregion Gcd & Lcm

		#region IsPrime

		/// <summary>
		/// Checks whether a number is a prime number or not.
		/// </summary>
		/// <param name="n">The number to test.</param>
		/// <param name="checkCache">Whether to check cache for primality.</param>
		/// <returns>Whether the number is prime.</returns>
		public static bool IsPrime(int n, bool checkCache = true)
		{
			if (n <= 1) return false;
			if (checkCache && n <= BagMath.LargestSmallPrime) return Array.BinarySearch<int>(BagMath.SmallPrimes, n) >= 0;
			if (n < 4) return true;
			if (n % 2 == 0) return false;
			if (n < 9) return true;
			if (n % 3 == 0) return false;

			uint r = (uint)System.Math.Sqrt(n);
			uint f = 5;
			while (f <= r)
			{
				if (n % f == 0) return false;
				if (n % (f + 2) == 0) return false;
				f += 6;
			}

			return true;
		}

		/// <summary>
		/// Checks whether a number is a prime number or not.
		/// </summary>
		/// <param name="n">The number to test.</param>
		/// <param name="checkCache">Whether to check cache for primality.</param>
		/// <returns>Whether the number is prime.</returns>
		public static bool IsPrime(long n, bool checkCache = true)
		{
			if (n <= 1) return false;
			if (checkCache && n <= BagMath.LargestSmallPrime) return Array.BinarySearch<int>(BagMath.SmallPrimes, (int)n) >= 0;
			if (n < 4) return true;
			if (n % 2 == 0) return false;
			if (n < 9) return true;
			if (n % 3 == 0) return false;

			uint r = (uint)S.Math.Sqrt(n);
			uint f = 5;
			while (f <= r)
			{
				if (n % f == 0) return false;
				if (n % (f + 2) == 0) return false;
				f += 6;
			}

			return true;
		}

#if DOTNET4

		/// <summary>
		/// Checks whether a number is a prime number or not.
		/// </summary>
		/// <param name="n">The number to test.</param>
		/// <param name="checkCache">Whether to check cache for primality.</param>
		/// <returns>Whether the number is prime.</returns>
		public static bool IsPrime(BigInteger n, bool checkCache = true)
		{
			if (n <= 1) return false;
			if (checkCache && n <= BagMath.LargestSmallPrime) return Array.BinarySearch<int>(BagMath.SmallPrimes, (int)n) >= 0;
			if (n < 4) return true;
			if (n % 2 == 0) return false;
			if (n < 9) return true;
			if (n % 3 == 0) return false;

			BigInteger r = n.Sqrt(),
					   f = 5;

			while (f <= r)
			{
				if (n % f == 0) return false;
				if (n % (f + 2) == 0) return false;
				f += 6;
			}

			return true;
		}

#endif

		#endregion IsPrime

		#region SieveOfEratosthenes

		#region MathBlog - http://www.mathblog.dk/2010/sum-of-all-primes-below-2000000-problem-10/

		/// <summary>
		/// Performs an Eratosthenes sieve to find primes.
		/// </summary>
		/// <param name="upperLimit">The highest number to check.</param>
		/// <returns>The primes.</returns>
		public static IEnumerable<int> SieveOfEratosthenes(int upperLimit)
		{
			int sieveBound = (upperLimit - 1) / 2,
				upperSqrt = ((int)S.Math.Sqrt(upperLimit) - 1) / 2;

			bool[] PrimeBits = new bool[sieveBound + 1];

			yield return 2;

			for (int i = 1; i <= upperSqrt; i++)
			{
				if (!PrimeBits[i])
				{
					yield return 2 * i + 1;

					for (int j = i * 2 * (i + 1); j <= sieveBound; j += 2 * i + 1)
					{
						PrimeBits[j] = true;
					}
				}
			}

			for (int i = upperSqrt + 1; i <= sieveBound; i++)
			{
				if (!PrimeBits[i])
				{
					yield return 2 * i + 1;
				}
			}
		}

		/// <summary>
		/// Performs an Eratosthenes sieve to find primes.
		/// </summary>
		/// <param name="upperLimit">The highest number to check.</param>
		/// <returns>The primes.</returns>
		public static IEnumerable<ulong> SieveOfEratosthenes(ulong upperLimit)
		{
			ulong sieveBound = (upperLimit - 1) / 2,
				  upperSqrt = ((ulong)S.Math.Sqrt(upperLimit) - 1) / 2;

			bool[] PrimeBits = new bool[sieveBound + 1];

			yield return 2;

			for (ulong i = 1; i <= upperSqrt; i++)
			{
				if (!PrimeBits[i])
				{
					yield return 2 * i + 1;

					for (ulong j = i * 2 * (i + 1); j <= sieveBound; j += 2 * i + 1)
					{
						PrimeBits[j] = true;
					}
				}
			}

			for (ulong i = upperSqrt + 1; i <= sieveBound; i++)
			{
				if (!PrimeBits[i])
				{
					yield return 2 * i + 1;
				}
			}
		}

		#endregion MathBlog - http://www.mathblog.dk/2010/sum-of-all-primes-below-2000000-problem-10/

		#endregion SieveOfEratosthenes

		#region SieveOfAtkin

		#region Geekality - http://www.geekality.net/2009/10/19/the-sieve-of-atkin-in-c/

		/// <summary>
		/// Performs an Atkin sieve to find primes.
		/// </summary>
		/// <param name="max">The highest number to check.</param>
		/// <returns>The primes.</returns>
		public static IEnumerable<ulong> SieveOfAtkin(ulong max)
		{
			var isPrime = new bool[max + 1];
			var sqrt = System.Math.Sqrt(max);

			for (ulong x = 1; x <= sqrt; x++)
				for (ulong y = 1; y <= sqrt; y++)
				{
					var n = 4 * x * x + y * y;
					if (n <= max && (n % 12 == 1 || n % 12 == 5))
						isPrime[n] ^= true;

					n = 3 * x * x + y * y;
					if (n <= max && n % 12 == 7)
						isPrime[n] ^= true;

					n = 3 * x * x - y * y;
					if (x > y && n <= max && n % 12 == 11)
						isPrime[n] ^= true;
				}

			for (ulong n = 5; n <= sqrt; n++)
				if (isPrime[n])
				{
					var s = n * n;
					for (ulong k = s; k <= max; k += s)
						isPrime[k] = false;
				}

			yield return 2;
			yield return 3;
			for (ulong n = 5; n <= max; n += 2)
				if (isPrime[n]) yield return n;
		}

		#endregion Geekality - http://www.geekality.net/2009/10/19/the-sieve-of-atkin-in-c/

		#region http://alicebobandmallory.com/articles/2010/01/14/prime-factorization-in-parallel

		/// <summary>
		/// Performs an Atkin sieve to find primes, in parallel.
		/// </summary>
		/// <param name="max">The highest number to check.</param>
		/// <returns>The primes.</returns>
		/// <remarks>Parallel is faster for finding primes bigger than 1000.</remarks>
		public static IEnumerable<uint> SieveOfAtkinParallel(int max)
		{
			var isPrime = new bool[max + 1];
			var sqrt = (int)S.Math.Sqrt(max);

			Parallel.For(1, sqrt, x =>
			{
				uint xx = (uint)(x * x);
				for (uint y = 1; y <= sqrt; y++)
				{
					var yy = y * y;
					var n = 4 * xx + yy;
					if (n <= max && (n % 12 == 1 || n % 12 == 5))
						isPrime[n] ^= true;

					n = 3 * xx + yy;
					if (n <= max && n % 12 == 7)
						isPrime[n] ^= true;

					n = 3 * xx - yy;
					if (x > y && n <= max && n % 12 == 11)
						isPrime[n] ^= true;
				}
			});

			yield return 2;
			yield return 3;
			for (uint n = 5; n <= sqrt; n++)
			{
				if (isPrime[n])
				{
					yield return n;
					uint nn = n * n;
					for (uint k = nn; k <= max; k += nn)
						isPrime[k] = false;
				}
			}

			for (uint n = (uint)sqrt + 1; n <= max; n++)
				if (isPrime[n]) yield return n;
		}

		#endregion http://alicebobandmallory.com/articles/2010/01/14/prime-factorization-in-parallel

		#endregion SieveOfAtkin

		#region CollatzCount

		/// <summary>
		/// Returns the Collatz count.
		/// </summary>
		/// <param name="n">The current instance.</param>
		/// <returns>The Collatz count.</returns>
		public static int CollatzCount(int n)
		{
			if (n == 0) return 0;

			int i = 0, c = n;

			while (c != 1)
			{
				i++;

				if (c % 2 == 0) c = c / 2;
				else c = c * 3 + 1;
			}

			return i;
		}

		/// <summary>
		/// Returns the Collatz count.
		/// </summary>
		/// <param name="n">The current instance.</param>
		/// <returns>The Collatz count.</returns>
		public static long CollatzCount(long n)
		{
			if (n == 0) return 0;

			long i = 0, c = n;

			while (c != 1)
			{
				i++;

				if (c % 2 == 0) c = c / 2;
				else c = c * 3 + 1;
			}

			return i;
		}

#if DOTNET4

		/// <summary>
		/// Returns the Collatz count.
		/// </summary>
		/// <param name="n">The current instance.</param>
		/// <returns>The Collatz count.</returns>
		public static BigInteger CollatzCount(BigInteger n)
		{
			if (n == 0) return 0;

			BigInteger i = 0, c = n;

			while (c != 1)
			{
				i++;

				if (c % 2 == 0) c = c / 2;
				else c = c * 3 + 1;
			}

			return i;
		}

#endif

		#endregion CollatzCount

		#region SmallPrimes

		private static readonly int[] _SmallPrimes = {
			2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
			101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,
			211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293,
			307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397,
			401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
			503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599,
			601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691,
			701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797,
			809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887,
			907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997,
			1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097,
			1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 1187, 1193,
			1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297,
			1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373, 1381, 1399,
			1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499,
			1511, 1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597,
			1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657, 1663, 1667, 1669, 1693, 1697, 1699,
			1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789,
			1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871, 1873, 1877, 1879, 1889,
			1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987, 1993, 1997, 1999 };

		/// <summary>
		/// All primes below or equal to 1999, sorted in ascending order.
		/// </summary>
		public static int[] SmallPrimes { get { return _SmallPrimes; } }

		/// <summary>
		/// The largest value in the SmallPrime array, which is 1999.
		/// </summary>
		public static int LargestSmallPrime { get { return 1999; } }

		#endregion SmallPrimes
	}
}