using System;
using SharpBag;
using NUnit;
using NUnit.Framework;
using NUnit.Core;

namespace SharpBag.Tests
{
	[TestFixture]
	public class FunctionalTests
	{
		public delegate TOut Func2<T1, TOut>(T1 i1);

		// [Test]
		public void MemoizeWithDefaultMemo()
		{
			Func<int, int> factorial = i => i > 2 ? i * factorial(i - 1) : i;
			Func<int, int> memoizedFactorial = factorial.Memoize();

			for (int i = 0; i < 20; i++)
			{
				Assert.AreEqual(factorial(i), memoizedFactorial(i));
			}
		}

		[Test]
		public void Blaa()
		{
			Assert.IsTrue(true);
		}

		[Test]
		public void Blaa2()
		{
			Assert.IsFalse(true);
		}
	}
}