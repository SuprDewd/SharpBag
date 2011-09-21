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
    public class TernaryStringSearchTree<V> : TernarySearchTree<char, V>, IEnumerable<KeyValuePair<string, V>>
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
        public new IEnumerable<KeyValuePair<string, V>> Items
        {
            get
            {
                foreach (var item in base.Items)
                {
                    yield return new KeyValuePair<string, V>(new String(item.Key), item.Value);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the TernaryStringSearchTree class.
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
        /// Adds the specified key and value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Whether the key was new.</returns>
        public bool Add(string key, V value)
        {
            return this.Add(key.ToCharArray(), value);
        }

        /// <summary>
        /// Adds the specified key and value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Whether the key was new.</returns>
        public override bool Add(char[] key, V value)
        {
            return base.Add(this.CaseSensitive ? key : this.ToUpper(key), value);
        }

        /// <summary>
        /// Determines whether the current instance contains the specified sequence.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the current instance contains the specified sequence; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            return this.Contains(key.ToCharArray());
        }

        /// <summary>
        /// Determines whether the current instance contains the specified sequence.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the current instance contains the specified sequence; otherwise, <c>false</c>.
        /// </returns>
        public override bool Contains(char[] key)
        {
            return base.Contains(this.CaseSensitive ? key : this.ToUpper(key));
        }

        /// <summary>
        /// Gets all the sequences starting with the specified sequence.
        /// </summary>
        /// <param name="key">The sequence of starting elements.</param>
        /// <returns>The sequences starting with the specified sequence.</returns>
        public IEnumerable<KeyValuePair<string, V>> StartingWith(string key)
        {
            return this.StartingWith(key.ToCharArray());
        }

        /// <summary>
        /// Gets all the sequences starting with the specified sequence.
        /// </summary>
        /// <param name="key">The sequence of starting elements.</param>
        /// <returns>The sequences starting with the specified sequence.</returns>
        public new IEnumerable<KeyValuePair<string, V>> StartingWith(char[] key)
        {
            return base.StartingWith(this.CaseSensitive ? key : this.ToUpper(key)).Select(i => new KeyValuePair<string, V>(new String(i.Key), i.Value));
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public new IEnumerator<KeyValuePair<string, V>> GetEnumerator()
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