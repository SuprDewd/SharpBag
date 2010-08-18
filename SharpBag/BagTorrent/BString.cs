using System;
using System.Text;

namespace SharpBag.BagTorrent
{
    /// <summary>
    /// A bencode string.
    /// </summary>
    public class BString : BElement, IComparable<BString>
    {
        /// <summary>
        /// Allows you to set a string to a BString.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>The BString.</returns>
        public static implicit operator BString(string s)
        {
            return new BString(s);
        }

        /// <summary>
        /// The value of the bencoded integer.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="value"></param>
        public BString(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Generates the bencoded equivalent of the string.
        /// </summary>
        /// <returns>The bencoded equivalent of the string.</returns>
        public string ToBencodedString()
        {
            return this.ToBencodedString(new StringBuilder()).ToString();
        }

        /// <summary>
        /// Generates the bencoded equivalent of the string.
        /// </summary>
        /// <param name="u">The StringBuilder to append to.</param>
        /// <returns>The bencoded equivalent of the string.</returns>
        public StringBuilder ToBencodedString(StringBuilder u)
        {
            return u.Append(this.Value.Length).Append(":").Append(this.Value);
        }

        /// <see cref="Object.GetHashCode()"/>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        /// <see cref="string.Equals(object)"/>
        public override bool Equals(object obj)
        {
            try
            {
                return this.Value.Equals(((BString)obj).Value);
            }
            catch { return false; }
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        /// <see cref="IComparable<BString>.CompareTo(object)"/>
        public int CompareTo(BString other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}