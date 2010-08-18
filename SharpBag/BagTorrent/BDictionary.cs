using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.BagTorrent
{
    /// <summary>
    /// A bencode dictionary.
    /// </summary>
    public class BDictionary : SortedDictionary<BString, BElement>, BElement
    {
        /// <summary>
        /// Generates the bencoded equivalent of the dictionary.
        /// </summary>
        /// <returns>The bencoded equivalent of the dictionary.</returns>
        public string ToBencodedString()
        {
            return this.ToBencodedString(new StringBuilder()).ToString();
        }

        /// <summary>
        /// Generates the bencoded equivalent of the dictionary.
        /// </summary>
        /// <param name="u">The StringBuilder to append to.</param>
        /// <returns>The bencoded equivalent of the dictionary.</returns>
        public StringBuilder ToBencodedString(StringBuilder u)
        {
            u.Append("d");
            for (int i = 0; i < base.Count; i++)
            {
                base.Keys.ElementAt(i).ToBencodedString(u);
                base.Values.ElementAt(i).ToBencodedString(u);
            }
            return u.Append("e");
        }

        /// <summary>
        /// Adds the specified key-value pair to the dictionary.
        /// </summary>
        /// <param name="key">The specified key.</param>
        /// <param name="value">The specified value.</param>
        public void Add(string key, BElement value)
        {
            base.Add(new BString(key), value);
        }

        /// <summary>
        /// Adds the specified key-value pair to the dictionary.
        /// </summary>
        /// <param name="key">The specified key.</param>
        /// <param name="value">The specified value.</param>
        public void Add(string key, string value)
        {
            base.Add(new BString(key), new BString(value));
        }

        /// <summary>
        /// Adds the specified key-value pair to the dictionary.
        /// </summary>
        /// <param name="key">The specified key.</param>
        /// <param name="value">The specified value.</param>
        public void Add(string key, int value)
        {
            base.Add(new BString(key), new BInteger(value));
        }

        /// <summary>
        /// Gets or sets the value assosiated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value assosiated with the specified key.</returns>
        public BElement this[string key]
        {
            get
            {
                return this[new BString(key)];
            }
            set
            {
                this[new BString(key)] = value;
            }
        }
    }
}