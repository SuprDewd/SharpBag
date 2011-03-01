using System;
using System.Collections.Generic;
using System.Linq;
using SharpBag;
using NUnit;
using NUnit.Framework;
using NUnit.Core;

namespace SharpBag.Tests
{
	[TestFixture]
	public class FunctionalTests
	{
		[Test]
		public void MemoizeWithDefaultMemo()
		{
			Func<int, int> factorial = i => i > 2 ? i * factorial(i - 1) : i;
			Func<int, int> memoizedFactorial = factorial.Memoize();

			for (int j = 0; j < 2; j++) for (int i = 0; i < 20; i++)
			{
				Assert.AreEqual(factorial(i), memoizedFactorial(i));
			}
		}

		[Test]
		public void Memoize()
		{
			Func<int, int> factorial = i => i > 2 ? i * factorial(i - 1) : i;
			Func<int, int> memoizedFactorial = factorial.Memoize(new Dictionary<int, int> {
				{0, 0},
				{1, 1},
				{2, 2}
			});

			for (int j = 0; j < 2; j++) for (int i = 0; i < 20; i++)
			{
				Assert.AreEqual(factorial(i), memoizedFactorial(i));
			}
		}

		public void Memoize2()
		{
			Func<int, int> testFunc = i => i;
			Func<int, int> memoizedTestFunc = testFunc.Memoize(new Dictionary<int, int> {
				{0, 10},
				{1, 10}
			});

			Assert.AreEqual(memoizedTestFunc(0), 10);
			Assert.AreEqual(memoizedTestFunc(1), 10);
			Assert.AreEqual(memoizedTestFunc(2), 2);
			Assert.AreEqual(memoizedTestFunc(3), 3);
		}

		[Test]
		public void Memoize3()
		{
			Func<int, int> factorial = i => i > 2 ? i * factorial(i - 1) : i;
			Func<int, int> memoizedFactorial = null;
			Func<int, int> factorial2 = i => i * memoizedFactorial(i - 1);
			memoizedFactorial = factorial2.Memoize(new Dictionary<int, int> {
				{0, 0},
				{1, 1},
				{2, 2}
			});

			for (int j = 0; j < 2; j++) for (int i = 0; i < 20; i++)
			{
				Assert.AreEqual(factorial(i), memoizedFactorial(i));
			}
		}
	}
}