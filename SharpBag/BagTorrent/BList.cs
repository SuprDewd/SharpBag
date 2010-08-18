using System.Collections.Generic;
using System.Text;

namespace SharpBag.BagTorrent
{
    /// <summary>
    /// A bencode list.
    /// </summary>
    public class BList : List<BElement>, BElement
    {
        /// <summary>
        /// Generates the bencoded equivalent of the list.
        /// </summary>
        /// <returns>The bencoded equivalent of the list.</returns>
        public string ToBencodedString()
        {
            return this.ToBencodedString(new StringBuilder()).ToString();
        }

        /// <summary>
        /// Generates the bencoded equivalent of the list.
        /// </summary>
        /// <param name="u">The StringBuilder to append to.</param>
        /// <returns>The bencoded equivalent of the list.</returns>
        public StringBuilder ToBencodedString(StringBuilder u)
        {
            u.Append("l");
            foreach (BElement element in base.ToArray())
            {
                element.ToBencodedString(u);
            }
            return u.Append("e");
        }

        /// <summary>
        /// Adds the specified value to the list.
        /// </summary>
        /// <param name="value">The specified value.</param>
        public void Add(string value)
        {
            base.Add(new BString(value));
        }

        /// <summary>
        /// Adds the specified value to the list.
        /// </summary>
        /// <param name="value">The specified value.</param>
        public void Add(int value)
        {
            base.Add(new BInteger(value));
        }
    }
}