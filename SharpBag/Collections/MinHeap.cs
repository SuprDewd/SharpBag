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

		public bool Contains(T item)
		{
			return this.Contains(item, 0);
		}

		private bool Contains(T item, int i)
		{
			if (item.CompareTo(this.InternalArray[i]) == 0) return true;

			int left = this.Left(i);
			if (left < this.Count)
			{
				if (item.CompareTo(this.InternalArray[left]) >= 0 && this.Contains(item, left)) return true;

				int right = this.Right(i);
				if (right < this.Count && item.CompareTo(this.InternalArray[right]) >= 0 && this.Contains(item, right)) return true;
			}

			return false;
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