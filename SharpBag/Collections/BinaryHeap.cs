using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Collections
{
    /// <summary>
    /// A binary heap.
    /// </summary>
    /// <typeparam name="T">The type of items in the heap.</typeparam>
    public abstract class BinaryHeap<T>
    {
        #region Fields

        /// <summary>
        /// The number of items in the heap.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// The maximum number of items that can be stored in the heap.
        /// </summary>
        public int Capacity { get; protected set; }

        /// <summary>
        /// The internal array.
        /// </summary>
        protected T[] InternalArray;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// The constructor.
        /// </summary>
        public BinaryHeap() : this(16) { }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="capacity">The maximum number of items that can be stored in the heap.</param>
        public BinaryHeap(int capacity)
        {
            Contract.Requires(capacity > 0);

            this.Count = 0;
            this.Capacity = capacity;
            this.InternalArray = new T[this.Capacity];
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="array">An array of items.</param>
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

        /// <summary>
        /// Invalidates the position of the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Invalidate(T item)
        {
            bool found = false;
            int i = 0;

            for (; i < this.Count; i++)
            {
                if (Object.ReferenceEquals(item, this.InternalArray[i]))
                {
                    found = true;
                    break;
                }
            }

            if (!found) return;

            if (i == 0) this.MaintainHeap(0);
            else
            {
                int parent = this.Parent(i);
                int cmp = this.Compare(parent, i);

                if (cmp < 0) this.BubbleUp(i);
                else if (cmp > 0) this.MaintainHeap(i);
            }
        }

        /// <summary>
        /// Swap the values at the specified indices.
        /// </summary>
        /// <param name="i">The first index.</param>
        /// <param name="j">The second index.</param>
        protected void Swap(int i, int j)
        {
            T temp = this.InternalArray[i];
            this.InternalArray[i] = this.InternalArray[j];
            this.InternalArray[j] = temp;
        }

        /// <summary>
        /// Maintain the heap.
        /// </summary>
        /// <param name="i">The highest item to maintain.</param>
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

                if (max == i) break;

                this.Swap(i, max);
                i = max;
            }
        }

        /// <summary>
        /// Peek at the next item in the heap.
        /// </summary>
        /// <returns>The item.</returns>
        public T Peek()
        {
            return this.InternalArray[0];
        }

        /// <summary>
        /// Pop the next item from the heap.
        /// </summary>
        /// <returns>The item.</returns>
        public T Pop()
        {
            T ret = this.InternalArray[0];
            this.Count--;
            this.InternalArray[0] = this.InternalArray[this.Count];
            this.MaintainHeap(0);
            return ret;
        }

        private void BubbleUp(int i)
        {
            if (i == 0) return;
            int parent = this.Parent(i);

            while (this.Compare(i, parent) > 0)
            {
                this.Swap(i, parent);
                i = parent;
                if (i == 0) return;
                parent = this.Parent(i);
            }
        }

        /// <summary>
        /// Push an item into the heap.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Push(T item)
        {
            if (this.Count == this.Capacity)
            {
                if (this.Capacity < 1) this.Capacity = 1;
                else this.Capacity <<= 1;
                Array.Resize<T>(ref this.InternalArray, this.Capacity);
            }

            int i = this.Count;
            this.InternalArray[i] = item;
            this.Count++;

            this.BubbleUp(i);
        }

        /// <summary>
        /// Clears the heap.
        /// </summary>
        public void Clear()
        {
            this.Count = 0;
        }

        #endregion Data Structuring

        #region Ordering

        /// <summary>
        /// The item comparer.
        /// </summary>
        /// <param name="firstIndex">The index of the first item.</param>
        /// <param name="secondIndex">The index of the second item.</param>
        /// <returns>The order of the items.</returns>
        protected abstract int Compare(int firstIndex, int secondIndex);

        #endregion Ordering

        #region Traversing

        /// <summary>
        /// Gets the left child of the specified index.
        /// </summary>
        /// <param name="i">The specified index.</param>
        /// <returns>The left child.</returns>
        protected int Left(int i)
        {
            return ((i + 1) << 1) - 1;
        }

        /// <summary>
        /// Gets the right child of the specified index.
        /// </summary>
        /// <param name="i">The specified index.</param>
        /// <returns>The right child.</returns>
        protected int Right(int i)
        {
            return (i + 1) << 1;
        }

        /// <summary>
        /// Gets the parent of the specified index.
        /// </summary>
        /// <param name="i">The specified index.</param>
        /// <returns>The parent.</returns>
        protected int Parent(int i)
        {
            return (i - 1) >> 1;
        }

        #endregion Traversing

        #region Heapsort

        /// <summary>
        /// The internal heap sort.
        /// </summary>
        /// <returns>The sorted items.</returns>
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

        /// <summary>
        /// The reverse internal heap sort.
        /// </summary>
        /// <returns>The reverse sorted items.</returns>
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