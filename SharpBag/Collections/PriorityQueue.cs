using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Collections
{
	public class PriorityQueue<T, C> : IEnumerable<T> where C : IComparable<C>
	{
		protected class Node : IComparable<Node>
		{
			public C Key;

			public T Value;

			public uint Number;

			public Node(C key, T value, uint number)
			{
				this.Key = key;
				this.Value = value;
				this.Number = number;
			}

			public int CompareTo(Node other)
			{
				int keyCmp = this.Key.CompareTo(other.Key);
				if (keyCmp != 0) return keyCmp;
				return -this.Number.CompareTo(other.Number);
			}
		}

		protected MaxHeap<Node> InternalHeap;

		public int Count { get { return this.InternalHeap.Count; } }

		public int Capacity { get { return this.InternalHeap.Capacity; } }

		private uint Number = 0;

		public PriorityQueue()
		{
			this.InternalHeap = new MaxHeap<Node>();
		}

		public PriorityQueue(int capacity)
		{
			this.InternalHeap = new MaxHeap<Node>(capacity);
		}

		public void Enqueue(T item, C priority)
		{
			this.InternalHeap.Push(new Node(priority, item, this.Number++));
		}

		public T Dequeue(bool remove = true)
		{
			if (remove) return this.InternalHeap.Pop().Value;
			else return this.InternalHeap.Peek().Value;
		}

		public void Clear()
		{
			this.InternalHeap.Clear();
		}

		public IEnumerator<T> GetEnumerator()
		{
			while (this.Count > 0)
			{
				yield return this.Dequeue();
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}