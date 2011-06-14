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
	}
}