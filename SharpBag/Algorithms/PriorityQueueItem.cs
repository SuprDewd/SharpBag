using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Algorithms
{
    /// <summary>
    /// An item in a priority queue.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public class PriorityQueueItem<T>
    {
        /// <summary>
        /// The priority of the item.
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// The item.
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="priority">The priority. Defaults to 0.</param>
        public PriorityQueueItem(T item, int priority = 0)
        {
            this.Item = item;
            this.Priority = priority;
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Item.ToString();
        }
    }
}