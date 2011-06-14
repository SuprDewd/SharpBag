using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Collections
{
	public abstract class BinaryHeap<T>
	{
		#region Fields

		public int Count { get; protected set; }

		public int Capacity { get; protected set; }

		protected T[] InternalArray;

		#endregion Fields

		#region Constructors

		public BinaryHeap() : this(16) { }

		public BinaryHeap(int capacity)
		{
			this.Count = 0;
			this.Capacity = capacity;
			this.InternalArray = new T[this.Capacity];
		}

		public BinaryHeap(T[] array)
		{
			this.InternalArray = array;
			this.Count = this.Capacity = array.Length;

			for (int i = this.Count / 2; i >= 0; i--)
			{
				this.MaintainHeap(i);
			}
		}

		#endregion Constructors

		#region Data Structuring

		protected void MaintainHeap(int i)
		{
			while (true)
			{
				int max = i,
				left = this.Left(i),
				right = this.Right(i);

				if (left < this.Count)
				{
					if (this.Compare(max, left) < 0)
					{
						max = left;
					}

					if (right < this.Count && this.Compare(max, right) < 0)
					{
						max = right;
					}
				}

				if (max != i)
				{
					T temp = this.InternalArray[i];
					this.InternalArray[i] = this.InternalArray[max];
					this.InternalArray[max] = temp;
					i = max;
				}
				else break;
			}
		}

		public T Peek()
		{
			return this.InternalArray[0];
		}

		public T Pop()
		{
			T ret = this.InternalArray[0];
			this.Count--;
			this.InternalArray[0] = this.InternalArray[this.Count];
			this.MaintainHeap(0);
			return ret;
		}

		public void Push(T item)
		{
			if (this.Count == this.Capacity)
			{
				if (this.Capacity < 1) this.Capacity = 1;
				else this.Capacity <<= 1;
				Array.Resize<T>(ref this.InternalArray, this.Capacity);
			}

			int i = this.Count, parent = this.Parent(i);
			this.InternalArray[i] = item;
			this.Count++;

			while (this.Compare(i, parent) > 0)
			{
				T temp = this.InternalArray[i];
				this.InternalArray[i] = this.InternalArray[parent];
				this.InternalArray[parent] = temp;
				i = parent;
				parent = this.Parent(i);
			}
		}

		public void Clear()
		{
			this.Count = 0;
		}

		#endregion Data Structuring

		#region Ordering

		protected abstract int Compare(int firstIndex, int secondIndex);

		#endregion Ordering

		#region Traversing

		protected int Left(int i)
		{
			return ((i + 1) << 1) - 1;
		}

		protected int Right(int i)
		{
			return (i + 1) << 1;
		}

		protected int Parent(int i)
		{
			return i >> 1;
		}

		#endregion Traversing

		#region Heapsort

		protected IEnumerable<T> InternalSort()
		{
			while (this.Count > 0)
			{
				yield return this.InternalArray[0];
				this.Count--;
				if (this.Count > 0)
				{
					this.InternalArray[0] = this.InternalArray[this.Count];
					this.MaintainHeap(0);
				}
			}
		}

		protected IEnumerable<T> InternalSortReversed()
		{
			int count = this.Count;

			while (this.Count > 1)
			{
				this.Count--;
				T temp = this.InternalArray[0];
				this.InternalArray[0] = this.InternalArray[this.Count];
				this.InternalArray[this.Count] = temp;
				this.MaintainHeap(0);
			}

			for (int i = 0; i < count; i++) yield return this.InternalArray[i];
		}

		#endregion Heapsort
	}
}