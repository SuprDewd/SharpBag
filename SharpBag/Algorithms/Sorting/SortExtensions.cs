using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Algorithms.Sorting
{
    public static class SortExtensions
    {
        /// <summary>
        /// BubbleSorts the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the items in the current instance.</typeparam>
        /// <param name="collection">The current instance.</param>
        /// <param name="order">The order to sort in.</param>
        /// <returns>The sorted collection.</returns>
        public static IEnumerable<T> BubbleSort<T>(this IEnumerable<T> collection, SortOrder order = SortOrder.Ascending) where T : IComparable<T>
        {
            T[] array = collection.ToArray();
            T temp;

            for (int outer = array.Length - 1; outer >= 1; outer--)
            {
                for (int inner = 0; inner <= outer - 1; inner++)
                {
                    if (array[inner].CompareTo(array[inner + 1]) == (order == SortOrder.Ascending ? 1 : -1))
                    {
                        temp = array[inner];
                        array[inner] = array[inner + 1];
                        array[inner + 1] = temp;
                    }
                }
            }

            return array;
        }

        /// <summary>
        /// SelectionSorts the current instance.
        /// </summary>
        /// <param name="collection">The current instance.</param>
        /// <param name="order">The order to sort in.</param>
        /// <returns>The sorted collection.</returns>
        public static IEnumerable<int> SelectionSort(this IEnumerable<int> collection, SortOrder order = SortOrder.Ascending)
        {
            int[] array = collection.ToArray();
            int min, temp;

            for (int outer = 0; outer < array.Length; outer++)
            {
                min = outer;

                for (int inner = outer + 1; inner < array.Length; inner++)
                {
                    if ((order == SortOrder.Ascending && array[inner] < array[min]) || (order != SortOrder.Ascending && array[inner] > array[min]))
                    {
                        min = inner;
                    }
                }

                temp = array[outer];
                array[outer] = array[min];
                array[min] = temp;
            }

            return array;
        }
    }
}
