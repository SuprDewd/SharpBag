using System.Collections.Generic;
using System.Linq;

namespace SharpBag.Combinatorics
{
    /// <summary>
    /// A class with combinatoric extension methods.
    /// </summary>
    public static class CombinatoricExtensions
    {
        /// <summary>
        /// Returns all the permutations of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of items in the current instance.</typeparam>
        /// <param name="collection">The current instance.</param>
        /// <param name="distinct">Whether the items should be distinct.</param>
        /// <returns>All the permutations of the current instance.</returns>
        public static IEnumerable<T[]> Permutations<T>(this T[] collection, bool distinct = false)
        {
            return new Permutations<T>(collection, distinct ? GenerateOption.WithoutRepetition : GenerateOption.WithRepetition).Select(c => c.ToArray());
        }

        /// <summary>
        /// Returns all the combinations of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of items in the current instance.</typeparam>
        /// <param name="collection">The current instance.</param>
        /// <param name="size">The size of each combination.</param>
        /// <param name="distinct">Whether the items should be distinct.</param>
        /// <returns>All the combinations of the current instance.</returns>
        public static IEnumerable<T[]> Combinations<T>(this T[] collection, int size, bool distinct = false)
        {
            return new Combinations<T>(collection, size, distinct ? GenerateOption.WithoutRepetition : GenerateOption.WithRepetition).Select(c => c.ToArray());
        }

        /// <summary>
        /// Returns all the variations of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of items in the current instance.</typeparam>
        /// <param name="collection">The current instance.</param>
        /// <param name="size">The size of each variation.</param>
        /// <param name="distinct">Whether the items should be distinct.</param>
        /// <returns>All the variations of the current instance.</returns>
        public static IEnumerable<T[]> Variations<T>(this T[] collection, int size, bool distinct = false)
        {
            return new Variations<T>(collection, size, distinct ? GenerateOption.WithoutRepetition : GenerateOption.WithRepetition).Select(c => c.ToArray());
        }
    }
}