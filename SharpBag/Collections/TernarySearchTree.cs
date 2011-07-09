using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Collections
{
	/// <summary>
	/// A ternary search tree.
	/// </summary>
	/// <typeparam name="T">The type of items in the tree.</typeparam>
	public class TernarySearchTree<T> : IEnumerable<T[]> where T : IComparable<T>
	{
		private class Node
		{
			public T Value { get; private set; }

			public bool IsEnd { get; set; }

			public Node Below { get; set; }

			public Node Left { get; set; }

			public Node Right { get; set; }

			public Node(T value)
			{
				this.Value = value;
				this.IsEnd = false;
			}

			public Node GetNextFor(T value)
			{
				return this.GetNextFor(this.Value.CompareTo(value));
			}

			public Node GetNextFor(int cmp)
			{
				if (cmp == 0) return this.Below;
				if (cmp > 0) return this.Left;
				return this.Right;
			}

			public void Add(Node node)
			{
				this.Add(node, this.Value.CompareTo(node.Value));
			}

			public void Add(Node node, int cmp)
			{
				if (cmp == 0) this.Below = node;
				else if (cmp > 0) this.Left = node;
				else this.Right = node;
			}
		}

		/// <summary>
		/// Gets the item count.
		/// </summary>
		public int Count { get; private set; }

		/// <summary>
		/// Gets the items.
		/// </summary>
		public IEnumerable<T[]> Items
		{
			get
			{
				foreach (var item in this.StartingWith(new T[0], this.Root))
				{
					yield return item;
				}
			}
		}

		private Node Root { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TernarySearchTree&lt;T&gt;"/> class.
		/// </summary>
		public TernarySearchTree() { Count = 0; }

		/// <summary>
		/// Adds the specified sequence.
		/// </summary>
		/// <param name="sequence">The sequence.</param>
		/// <returns>Whether the sequence was new.</returns>
		public virtual bool Add(T[] sequence)
		{
			Contract.Requires(sequence.Length > 0);
			bool isNew = false;

			if (this.Root == null)
			{
				this.Root = new Node(sequence[0]);
				isNew = true;
			}

			Node cur = this.Root;
			for (int i = 0; i < sequence.Length; i++)
			{
				int cmp = cur.Value.CompareTo(sequence[i]);

				if (cmp == 0)
				{
					if (i < sequence.Length - 1)
					{
						if (cur.Below == null)
						{
							cur.Below = new Node(sequence[i + 1]);
							isNew = true;
						}

						cur = cur.Below;
					}
				}
				else if (cmp > 0)
				{
					if (cur.Left == null)
					{
						cur.Left = new Node(sequence[i]);
						isNew = true;
					}

					cur = cur.Left;
					i--;
				}
				else
				{
					if (cur.Right == null)
					{
						cur.Right = new Node(sequence[i]);
						isNew = true;
					}

					cur = cur.Right;
					i--;
				}

				if (i + 1 == sequence.Length)
				{
					if (!cur.IsEnd) isNew = true;
					cur.IsEnd = true;
				}
			}

			if (isNew) this.Count++;
			return isNew;
		}

		/// <summary>
		/// Determines whether the current instance contains the specified sequence.
		/// </summary>
		/// <param name="sequence">The sequence.</param>
		/// <returns>
		///   <c>true</c> if the current instance contains the specified sequence; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool Contains(T[] sequence)
		{
			Contract.Requires(sequence.Length > 0);
			if (this.Root == null) return false;

			Node cur = this.Root;
			for (int i = 0; i < sequence.Length; i++)
			{
				int cmp = cur.Value.CompareTo(sequence[i]);
				if (i == sequence.Length - 1 && cmp == 0) return cur.IsEnd;
				cur = cur.GetNextFor(cmp);
				if (cmp != 0) i--;
				if (cur == null) return false;
			}

			return false;
		}

		/// <summary>
		/// Gets all the sequences starting with the specified elements.
		/// </summary>
		/// <param name="sequence">The sequence of starting elements.</param>
		/// <returns>The sequences starting with the specified elements.</returns>
		public virtual IEnumerable<T[]> StartingWith(T[] sequence)
		{
			Contract.Requires(sequence.Length > 0);
			if (this.Root == null) yield break;

			Node cur = this.Root;
			for (int i = 0; i < sequence.Length; i++)
			{
				int cmp = cur.Value.CompareTo(sequence[i]);
				if (i == sequence.Length - 1 && cmp == 0) break;
				cur = cur.GetNextFor(cmp);
				if (cmp != 0) i--;
				if (cur == null) yield break;
			}

			if (cur.IsEnd) yield return sequence;
			if (cur.Below != null) foreach (T[] seq in this.StartingWith(sequence, cur.Below)) yield return seq;
		}

		private IEnumerable<T[]> StartingWith(T[] sequence, Node cur)
		{
			T[] seq = new T[sequence.Length + 1];
			Array.Copy(sequence, seq, sequence.Length);
			seq[sequence.Length] = cur.Value;

			if (cur.IsEnd) yield return seq;
			if (cur.Left != null) foreach (T[] s in this.StartingWith(sequence, cur.Left)) yield return s;
			if (cur.Below != null) foreach (T[] s in this.StartingWith(seq, cur.Below)) yield return s;
			if (cur.Right != null) foreach (T[] s in this.StartingWith(sequence, cur.Right)) yield return s;
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T[]> GetEnumerator()
		{
			foreach (var item in this.Items)
			{
				yield return item;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}