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
	}
}