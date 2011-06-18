using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Collections
{
	/// <summary>
	/// A priority queue.
	/// </summary>
	/// <typeparam name="T">The type of values in the queue.</typeparam>
	/// <typeparam name="C">The type of keys in the queue.</typeparam>
	public class PriorityQueue<T, C> : IEnumerable<T> where C : IComparable<C>
	{
		/// <summary>
		/// A priority queue node.
		/// </summary>
		protected class Node : IComparable<Node>
		{
			/// <summary>
			/// The key.
			/// </summary>
			public C Key;

			/// <summary>
			/// The value.
			/// </summary>
			public T Value;

			/// <summary>
			/// The number.
			/// </summary>
			public uint Number;

			/// <summary>
			/// The constructor.
			/// </summary>
			/// <param name="key">The key of the node.</param>
			/// <param name="value">The value of the node.</param>
			/// <param name="number">The number of the node.</param>
			public Node(C key, T value, uint number)
			{
				this.Key = key;
				this.Value = value;
				this.Number = number;
			}

			/// <summary>
			/// The comparer.
			/// </summary>
			/// <param name="other">The node to compare to.</param>
			/// <returns>The order.</returns>
			public int CompareTo(Node other)
			{
				int keyCmp = this.Key.CompareTo(other.Key);
				if (keyCmp != 0) return keyCmp;
				return -this.Number.CompareTo(other.Number);
			}
		}

		/// <summary>
		/// The internal heap.
		/// </summary>
		protected MaxHeap<Node> InternalHeap;

		/// <summary>
		/// The number of items in the queue.
		/// </summary>
		public int Count { get { return this.InternalHeap.Count; } }

		/// <summary>
		/// The maximum number of items in the queue.
		/// </summary>
		public int Capacity { get { return this.InternalHeap.Capacity; } }

		private uint Number = 0;

		/// <summary>
		/// The constructor.
		/// </summary>
		public PriorityQueue()
		{
			this.InternalHeap = new MaxHeap<Node>();
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="capacity">The initial capacity.</param>
		public PriorityQueue(int capacity)
		{
			this.InternalHeap = new MaxHeap<Node>(capacity);
		}

		/// <summary>
		/// Enqueue the specified item with the specified priority.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="priority">The priority.</param>
		public void Enqueue(T item, C priority)
		{
			this.InternalHeap.Push(new Node(priority, item, this.Number++));
		}

		/// <summary>
		/// Dequeue the next item from the queue.
		/// </summary>
		/// <param name="remove">Whether to remove the item.</param>
		/// <returns>The next item from the queue.</returns>
		public T Dequeue(bool remove = true)
		{
			if (remove) return this.InternalHeap.Pop().Value;
			else return this.InternalHeap.Peek().Value;
		}

		/// <summary>
		/// Clear the items from the queue.
		/// </summary>
		public void Clear()
		{
			this.InternalHeap.Clear();
		}

		/// <summary>
		/// Get an enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			while (this.Count > 0)
			{
				yield return this.Dequeue();
			}
		}

		/// <summary>
		/// Get an enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}