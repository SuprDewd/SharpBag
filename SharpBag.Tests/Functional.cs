using System;
using System.Collections.Generic;
using System.Linq;
using SharpBag;
using NUnit;
using NUnit.Framework;

namespace SharpBag.Tests
{
    [TestFixture]
    public class FunctionalTests
    {
        [Test]
        public void MemoizeWithDefaultMemo()
        {
            Func<int, int> factorial = null;
            factorial = i => i > 2 ? i * factorial(i - 1) : i;
            Func<int, int> memoizedFactorial = factorial.Memoize();

            for (int j = 0; j < 2; j++) for (int i = 0; i < 20; i++)
            {
                Assert.AreEqual(factorial(i), memoizedFactorial(i));
            }
        }

        [Test]
        public void Memoize()
        {
            Func<int, int> factorial = null;
            factorial = i => i > 2 ? i * factorial(i - 1) : i;
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
            Func<int, int> factorial = null;
            factorial = i => i > 2 ? i * factorial(i - 1) : i;
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

        [Test]
        public void Iter()
        {
            int[] i = new[] { 10, 4, 16, 1 };
            List<int> i2 = new List<int>();

            i.Iter(j => i2.Add(j));
            Assert.AreEqual(0, i2.Count());

            i.Iter(j => i2.Add(j)).ToArray();
            Assert.AreEqual(i.Length, i2.Count());

            for (int k = 0; k < 4; k++)
            {
                Assert.AreEqual(i[k], i2[k]);
            }
        }

        [Test]
        public void ForEach()
        {
            int[] i = new[] { 5, 8, 2, 4 };
            List<int> i2 = new List<int>();

            i.ForEach(j => i2.Add(j));
            Assert.AreEqual(i.Length, i2.Count());

            for (int k = 0; k < 4; k++)
            {
                Assert.AreEqual(i[k], i2[k]);
            }
        }

        [Test]
        public void To()
        {
            int[] t1 = 1.To(3).ToArray();
            int[] t2 = 3.To(1).ToArray();
            int[] t3 = 1.To(4, 2).ToArray();
            int[] t4 = 4.To(1, 2).ToArray();
            int[] t5 = 2.To(7, 3).ToArray();
            int[] t6 = 7.To(2, 3).ToArray();

            Assert.AreEqual(3, t1.Length);
            Assert.AreEqual(1, t1[0]);
            Assert.AreEqual(2, t1[1]);
            Assert.AreEqual(3, t1[2]);

            Assert.AreEqual(3, t2.Length);
            Assert.AreEqual(3, t2[0]);
            Assert.AreEqual(2, t2[1]);
            Assert.AreEqual(1, t2[2]);

            Assert.AreEqual(2, t3.Length);
            Assert.AreEqual(1, t3[0]);
            Assert.AreEqual(3, t3[1]);

            Assert.AreEqual(2, t4.Length);
            Assert.AreEqual(4, t4[0]);
            Assert.AreEqual(2, t4[1]);

            Assert.AreEqual(2, t5.Length);
            Assert.AreEqual(2, t5[0]);
            Assert.AreEqual(5, t5[1]);

            Assert.AreEqual(2, t6.Length);
            Assert.AreEqual(7, t6[0]);
            Assert.AreEqual(4, t6[1]);
        }
    }
}