// Copyright 2008 Adrian Akison
// Distributed under license terms of CPOL http://www.codeproject.com/info/cpol10.aspx

namespace SharpBag.Combinatorics
{
    /// <summary>
    /// Indicates whether a Permutation, Combination or Variation meta-collections
    /// generate repetition sets.
    /// </summary>
    internal enum GenerateOption
    {
        /// <summary>
        /// Do not generate additional sets, typical implementation.
        /// </summary>
        WithoutRepetition,
        /// <summary>
        /// Generate additional sets even if repetition is required.
        /// </summary>
        WithRepetition
    }
}