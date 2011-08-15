using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Collections;
using SharpBag.Strings;

namespace SharpBag.Tests.Collections
{
    [TestClass]
    public class PriorityQueueTest
    {
        [TestMethod]
        public void AddRemove()
        {
            PriorityQueue<string, int> pq = new PriorityQueue<string, int>(10);
            Assert.AreEqual<int>(10, pq.Capacity);
            Assert.AreEqual<int>(0, pq.Count);

            pq.Enqueue("A", 1);
            Assert.AreEqual<int>(1, pq.Count);
            Assert.AreEqual<string>("A", pq.Dequeue(false));

            pq.Enqueue("B", 2);
            Assert.AreEqual<int>(2, pq.Count);
            Assert.AreEqual<string>("B", pq.Dequeue(false));

            pq.Enqueue("C", 1);
            Assert.AreEqual<int>(3, pq.Count);
            Assert.AreEqual<string>("B", pq.Dequeue(false));

            Assert.AreEqual<string>("B", pq.Dequeue());
            Assert.AreEqual<string>("A", pq.Dequeue(true));
            Assert.AreEqual<string>("C", pq.Dequeue());

            Assert.AreEqual<int>(0, pq.Count);
            Assert.AreEqual<int>(10, pq.Capacity);

            pq.Enqueue("A", 1);
            pq.Enqueue("B", 2);
            pq.Enqueue("C", 3);

            pq.Clear();

            Assert.AreEqual<int>(0, pq.Count);
            Assert.AreEqual<int>(10, pq.Capacity);
        }

        [TestMethod]
        public void Contains()
        {
            MinHeap<int> heap1 = new MinHeap<int>(new int[] { 1, 2, 3, 4, 5 }),
                         heap2 = new MinHeap<int>(new int[] { 5, 4, 3, 2, 1 }),
                         heap3 = new MinHeap<int>(new int[] { 2, 4, 5, 1, 3 }),
                         heap4 = new MinHeap<int>(new int[] { 1, 2, 4, 5 }),
                         heap5 = new MinHeap<int>(new int[] { 5, 4, 2, 1 }),
                         heap6 = new MinHeap<int>(new int[] { 2, 4, 5, 1 });

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