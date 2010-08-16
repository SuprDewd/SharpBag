using System.Collections.Generic;

namespace SharpBag.BagMath
{
    /// <summary>
    /// A class for computing fibonacci numbers.
    /// </summary>
    public class Fibonacci
    {
        /// <summary>
        /// An endless source that will return fibonacci numbers.
        /// </summary>
        public IEnumerable<int> Numbers
        {
            get
            {
                int a = 0;
                int b = 1;
                int t = 0;

                yield return a;
                yield return b;

                while (true)
                {
                    yield return t = a + b;
                    a = b;
                    b = t;
                }
            }
        }

        /// <summary>
        /// An endless source that will return fibonacci numbers.
        /// </summary>
        public IEnumerable<long> LongNumbers
        {
            get
            {
                long a = 0;
                long b = 1;
                long t = 0;

                yield return a;
                yield return b;

                while (true)
                {
                    yield return t = a + b;
                    a = b;
                    b = t;
                }
            }
        }
    }
}