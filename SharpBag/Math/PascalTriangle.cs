using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math
{
    /// <summary>
    /// A static class with methods concerning the Pascal triangle.
    /// </summary>
    public static class PascalTriangle
    {
        /// <summary>
        /// Gets an entry at the specified row and column.
        /// </summary>
        /// <param name="row">The specified row.</param>
        /// <param name="column">The specified column.</param>
        /// <returns>The value at the specified row and column.</returns>
        public static long GetEntry(int row, int column)
        {
            long current = 1;

            for (int i = 1; i <= column; i++)
            {
                current = (current * (row + 1 - i)) / i;
            }

            return current;
        }
    }
}
