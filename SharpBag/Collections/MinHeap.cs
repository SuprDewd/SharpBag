using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Collections
{
	public class MinHeap<T> : BinaryHeap<T> where T : IComparable<T>
	{
		public MinHeap() : base() { }

		public MinHeap(int capacity) : base(capacity) { }

		public MinHeap(T[] array) : base(array) { }

		protected override int Compare(int firstIndex, int secondIndex)
		{
			return -this.InternalArray[firstIndex].CompareTo(this.InternalArray[secondIndex]);
		}

		public static IEnumerable<T> Sort(T[] array)
		{
			MinHeap<T> heap = new MinHeap<T>(array);
			return heap.InternalSort();
		}

		public static IEnumerable<T> SortDescending(T[] array)
		{
			MinHeap<T> heap = new MinHeap<T>(array);
			return heap.InternalSortReversed();
		}
	}
}