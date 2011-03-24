using System.Collections.Generic;

#if DOTNET4

using System.Diagnostics.Contracts;

#endif

using System.Linq;

namespace SharpBag.Algorithms
{
    /// <summary>
    /// A priority queue.
    /// </summary>
    /// <typeparam name="T">The type of items in the priority queue.</typeparam>
    public class PriorityQueue<T> : IEnumerable<T>
    {
        private readonly List<PriorityQueueItem<T>> Items = new List<PriorityQueueItem<T>>();

        /// <summary>
        /// Adds an item to the queue.
        /// </summary>
        /// <param name="item">An item.</param>
        /// <param name="priority">The priority of the item. Defaults to 0.</param>
        public void Add(T item, int priority = 0)
        {
            this.Enqueue(item, priority);
        }

        /// <summary>
        /// The number of items in the priority queue.
        /// </summary>
        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }

        /// <summary>
        /// Adds an item to the queue.
        /// </summary>
        /// <param name="item">An item.</param>
        /// <param name="priority">The priority of the item. Defaults to 0.</param>
        public void Enqueue(T item, int priority = 0)
        {
            this.Items.Add(new PriorityQueueItem<T>(item, priority));
        }

        /// <summary>
        /// Gets an enumerator sorted by the priority.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerable<T> GetPriorityEnumerator()
        {
            return this.Items.ToArray().OrderByDescending(i => i.Priority).Select(i => i.Item);
        }

        /// <summary>
        /// Returns the next item in the queue, and then removes it from queue.
        /// </summary>
        /// <returns>The next item in the queue.</returns>
        public T Dequeue()
        {
#if DOTNET4
            Contract.Requires(this.Count > 0);
#endif
            return this.Dequeue(true);
        }

        /// <summary>
        /// Returns the next item in the queue.
        /// </summary>
        /// <param name="remove">Whether to remove the item from the queue.</param>
        /// <returns>The next item in the queue.</returns>
        private T Dequeue(bool remove)
        {
#if DOTNET4
            Contract.Requires(this.Count > 0);
#endif
            PriorityQueueItem<T>[] array = this.Items.ToArray();
            if (array.Length == 0) return default(T);

            int maxPriority = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[maxPriority].Priority < array[i].Priority)
                {
                    maxPriority = i;
                }
            }

            PriorityQueueItem<T> nextItem = array[maxPriority];
            if (remove) this.Items.RemoveAt(maxPriority);

            return nextItem.Item;
        }

        /// <summary>
        /// Returns the next item in the queue, without removing it.
        /// </summary>
        /// <returns>The next item in the queue.</returns>
        public T Peek()
        {
#if DOTNET4
            Contract.Requires(this.Count > 0);
#endif
            return this.Dequeue(false);
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">The specified index.</param>
        /// <returns>The item.</returns>
        public T this[int index]
        {
            get
            {
#if DOTNET4
                Contract.Requires(index >= 0);
                Contract.Requires(this.Count > index);
#endif
                return this.Items[index].Item;
            }
            set
            {
#if DOTNET4
                Contract.Requires(index >= 0);
                Contract.Requires(this.Count > index);
#endif
                this.Items[index] = new PriorityQueueItem<T>(value);
            }
        }

        /// <summary>
        /// IEnumerable{T}.GetEnumerator()
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.OrderByDescending(i => i.Priority).Select(i => i.Item).GetEnumerator();
        }

        /// <summary>
        /// IEnumerable{T}.GetEnumerator()
        /// </summary>
        /// <param name="remove">Whether to remove the items that have been returned from the enumerator.</param>
        public IEnumerator<T> GetEnumerator(bool remove)
        {
            PriorityQueueItem<T>[] array = this.Items.OrderByDescending(i => i.Priority).ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                if (remove) this.Items.Remove(array[i]);
                yield return array[i].Item;
            }
        }

        /// <summary>
        /// IEnumerable{T}.GetEnumerator()
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Items.OrderByDescending(i => i.Priority).Select(i => i.Item).GetEnumerator();
        }
    }
}