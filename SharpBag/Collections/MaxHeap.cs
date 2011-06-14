using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Collections
{
	public class MaxHeap<T> : BinaryHeap<T> where T : IComparable<T>
	{
		public MaxHeap() : base() { }

		public MaxHeap(int capacity) : base(capacity) { }

		public MaxHeap(T[] array) : base(array) { }

		protected override int Compare(int firstIndex, int secondIndex)
		{
			return this.InternalArray[firstIndex].CompareTo(this.InternalArray[secondIndex]);
		}

		public static IEnumerable<T> Sort(T[] array)
		{
			MaxHeap<T> heap = new MaxHeap<T>(array);
			return heap.InternalSortReversed();
		}

		public static IEnumerable<T> SortDescending(T[] array)
		{
			MaxHeap<T> heap = new MaxHeap<T>(array);
			return heap.InternalSort();
		}
	}
}