using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Collections
{
	/// <summary>
	/// A ternary string-search tree.
	/// </summary>
	public class TernaryStringSearchTree : TernarySearchTree<char>, IEnumerable<string>
	{
		/// <summary>
		/// Gets a value indicating whether the search tree is case sensitive.
		/// </summary>
		/// <value>
		/// Whether the search tree is case sensitive.
		/// </value>
		public bool CaseSensitive { get; private set; }

		/// <summary>
		/// Gets the items.
		/// </summary>
		public new IEnumerable<string> Items
		{
			get
			{
				foreach (var item in base.Items)
				{
					yield return new String(item);
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TernaryStringSearchTree"/> class.
		/// </summary>
		/// <param name="caseSensitive">Whether the search tree is case sensitive.</param>
		public TernaryStringSearchTree(bool caseSensitive = true)
		{
			this.CaseSensitive = caseSensitive;
		}

		private char[] ToUpper(char[] sequence)
		{
			char[] upperSequence = new char[sequence.Length];
			for (int i = 0; i < sequence.Length; i++) upperSequence[i] = Char.ToUpper(sequence[i]);
			return upperSequence;
		}

		/// <summary>
		/// Adds the specified sequence.
		/// </summary>
		/// <param name="sequence">The sequence.</param>
		/// <returns>Whether the sequence was new.</returns>
		public bool Add(string sequence)
		{
			return base.Add(this.CaseSensitive ? sequence.ToCharArray() : this.ToUpper(sequence.ToCharArray()));
		}

		/// <summary>
		/// Adds the specified sequence.
		/// </summary>
		/// <param name="sequence">The sequence.</param>
		/// <returns>Whether the sequence was new.</returns>
		public override bool Add(char[] sequence)
		{
			return base.Add(this.CaseSensitive ? sequence : this.ToUpper(sequence));
		}

		/// <summary>
		/// Determines whether the current instance contains the specified sequence.
		/// </summary>
		/// <param name="sequence">The sequence.</param>
		/// <returns>
		///   <c>true</c> if the current instance contains the specified sequence; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(string sequence)
		{
			return base.Contains(this.CaseSensitive ? sequence.ToCharArray() : this.ToUpper(sequence.ToCharArray()));
		}

		/// <summary>
		/// Determines whether the current instance contains the specified sequence.
		/// </summary>
		/// <param name="sequence">The sequence.</param>
		/// <returns>
		///   <c>true</c> if the current instance contains the specified sequence; otherwise, <c>false</c>.
		/// </returns>
		public override bool Contains(char[] sequence)
		{
			return base.Contains(this.CaseSensitive ? sequence : this.ToUpper(sequence));
		}

		/// <summary>
		/// Gets all the sequences starting with the specified sequence.
		/// </summary>
		/// <param name="sequence">The sequence of starting elements.</param>
		/// <returns>The sequences starting with the specified sequence.</returns>
		public IEnumerable<string> StartingWith(string sequence)
		{
			return base.StartingWith(this.CaseSensitive ? sequence.ToCharArray() : this.ToUpper(sequence.ToCharArray())).Select(i => new String(i));
		}

		/// <summary>
		/// Gets all the sequences starting with the specified sequence.
		/// </summary>
		/// <param name="sequence">The sequence of starting elements.</param>
		/// <returns>The sequences starting with the specified sequence.</returns>
		public new IEnumerable<string> StartingWith(char[] sequence)
		{
			return base.StartingWith(this.CaseSensitive ? sequence : this.ToUpper(sequence)).Select(i => new String(i));
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public new IEnumerator<string> GetEnumerator()
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