using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag;
using SharpBag.Collections;
using SharpBag.Strings;

namespace SharpBag.Tests.Collections
{
    [TestClass]
    public class MaxHeapTest
    {
        [TestMethod]
        public void AddRemove()
        {
            MaxHeap<int> heap = new MaxHeap<int>(10);

            Assert.AreEqual<int>(10, heap.Capacity);
            Assert.AreEqual<int>(0, heap.Count);

            heap.Push(1);

            Assert.AreEqual<int>(1, heap.Peek());
            Assert.AreEqual<int>(10, heap.Capacity);
            Assert.AreEqual<int>(1, heap.Count);

            heap.Push(6);

            Assert.AreEqual<int>(6, heap.Peek());
            Assert.AreEqual<int>(10, heap.Capacity);
            Assert.AreEqual<int>(2, heap.Count);

            heap.Push(4);

            Assert.AreEqual<int>(6, heap.Peek());
            Assert.AreEqual<int>(10, heap.Capacity);
            Assert.AreEqual<int>(3, heap.Count);

            heap.Push(5);
            heap.Push(1);
            heap.Push(2);
            heap.Push(7);
            heap.Push(4);
            heap.Push(6);

            Assert.AreEqual<int>(7, heap.Peek());
            Assert.AreEqual<int>(10, heap.Capacity);
            Assert.AreEqual<int>(9, heap.Count);

            heap.Push(9);

            Assert.AreEqual<int>(9, heap.Peek());
            Assert.AreEqual<int>(10, heap.Capacity);
            Assert.AreEqual<int>(10, heap.Count);

            heap.Push(11);

            Assert.AreEqual<int>(11, heap.Peek());
            Assert.AreEqual<int>(20, heap.Capacity);
            Assert.AreEqual<int>(11, heap.Count);

            Assert.AreEqual<int>(11, heap.Pop());
            Assert.AreEqual<int>(10, heap.Count);

            Assert.AreEqual<int>(9, heap.Pop());
            Assert.AreEqual<int>(9, heap.Count);

            Assert.AreEqual<int>(7, heap.Pop());
            Assert.AreEqual<int>(8, heap.Count);

            Assert.AreEqual<int>(6, heap.Pop());
            Assert.AreEqual<int>(7, heap.Count);

            Assert.AreEqual<int>(6, heap.Pop());
            Assert.AreEqual<int>(6, heap.Count);

            heap.Clear();

            Assert.AreEqual<int>(0, heap.Count);
            Assert.AreEqual<int>(20, heap.Capacity);
        }

        private class HardCoreAddRemoveTest : IComparable<HardCoreAddRemoveTest>
        {
            public int Value { get; set; }

            public HardCoreAddRemoveTest(int value)
            {
                this.Value = value;
            }

            public int CompareTo(HardCoreAddRemoveTest other)
            {
                return this.Value.CompareTo(other.Value);
            }

            public override bool Equals(object obj)
            {
                return Object.ReferenceEquals(this, obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        [TestMethod]
        public void HardCoreAddRemove()
        {
            MaxHeap<HardCoreAddRemoveTest> heap = new MaxHeap<HardCoreAddRemoveTest>();
            HashSet<HardCoreAddRemoveTest> set = new HashSet<HardCoreAddRemoveTest>();

            const int count = 1000;
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                HardCoreAddRemoveTest instance = new HardCoreAddRemoveTest(rand.Next(0, Int32.MaxValue));
                heap.Push(instance);
                set.Add(instance);

                Assert.AreSame(set.Max(), heap.Peek());
            }

            while (set.Count > 0)
            {
                HardCoreAddRemoveTest next = heap.Pop(),
                                      max = set.Max(),
                                      r = set.Random(rand);

                set.Remove(max);
                Assert.AreEqual(heap.Count, set.Count);
                Assert.AreSame(max, next);

                r.Value = rand.Next(0, Int32.MaxValue);
                heap.Invalidate(r);
            }

            Assert.AreEqual(0, heap.Count);
        }

        [TestMethod]
        public void Sort()
        {
            int[] arr1 = { 1, 2, 3, 4, 5 };
            int[] arr2 = { 5, 4, 3, 2, 1 };
            int[] arr3 = { 2, 4, 5, 1, 3 };

            Assert.AreEqual(arr1.ToStringPretty(), MaxHeap<int>.Sort((int[])arr1.Clone()).ToStringPretty());
            Assert.AreEqual(arr1.ToStringPretty(), MaxHeap<int>.Sort((int[])arr2.Clone()).ToStringPretty());
            Assert.AreEqual(arr1.ToStringPretty(), MaxHeap<int>.Sort((int[])arr3.Clone()).ToStringPretty());

            Assert.AreEqual(arr2.ToStringPretty(), MaxHeap<int>.SortDescending((int[])arr1.Clone()).ToStringPretty());
            Assert.AreEqual(arr2.ToStringPretty(), MaxHeap<int>.SortDescending((int[])arr2.Clone()).ToStringPretty());
            Assert.AreEqual(arr2.ToStringPretty(), MaxHeap<int>.SortDescending((int[])arr3.Clone()).ToStringPretty());
        }

        [TestMethod]
        public void Contains()
        {
            MaxHeap<int> heap1 = new MaxHeap<int>(new int[] { 1, 2, 3, 4, 5 }),
                         heap2 = new MaxHeap<int>(new int[] { 5, 4, 3, 2, 1 }),
                         heap3 = new MaxHeap<int>(new int[] { 2, 4, 5, 1, 3 }),
                         heap4 = new MaxHeap<int>(new int[] { 1, 2, 4, 5 }),
                         heap5 = new MaxHeap<int>(new int[] { 5, 4, 2, 1 }),
                         heap6 = new MaxHeap<int>(new int[] { 2, 4, 5, 1 });

            for (int i = 1; i <= 5; i++)
            {
                Assert.IsTrue(heap1.Contains(i));
                Assert.IsTrue(heap2.Contains(i));
                Assert.IsTrue(heap3.Contains(i));

                if (i != 3)
                {
                    Assert.IsTrue(heap4.Contains(i));
                    Assert.IsTrue(heap5.Contains(i));
                    Assert.IsTrue(heap6.Contains(i));
                }
                else
                {
                    Assert.IsFalse(heap4.Contains(i));
                    Assert.IsFalse(heap5.Contains(i));
                    Assert.IsFalse(heap6.Contains(i));
                }
            }
        }
    }
}