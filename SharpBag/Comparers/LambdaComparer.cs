using System;
using System.Collections.Generic;

using System.Diagnostics.Contracts;

namespace SharpBag
{
    /// <summary>
    /// An IEqualityComparer for use with lambda functions.
    /// </summary>
    /// <typeparam name="T">The type of object to compare.</typeparam>
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> Comparer { get; set; }

        private Func<T, int> Hasher { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="compare">The compare function.</param>
        /// <param name="hash">The hash function.</param>
        public LambdaComparer(Func<T, T, bool> compare, Func<T, int> hash = null)
        {
            Contract.Requires(compare != null);

            this.Comparer = compare;
            this.Hasher = hash;
        }

        /// <summary>
        /// IEqualityComparer{T}.Equals(T, T)
        /// </summary>
        public bool Equals(T x, T y)
        {
            return this.Comparer(x, y);
        }

        /// <summary>
        /// IEqualityComparer{T}.GetHashCode(T)
        /// </summary>
        public int GetHashCode(T obj)
        {
            if (this.Hasher != null) return this.Hasher(obj);
            return obj.GetHashCode();
        }
    }
}