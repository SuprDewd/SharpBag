using System;
using System.Text;

namespace SharpBag.BagTorrent
{
    /// <summary>
    /// A bencode integer.
    /// </summary>
    public class BInteger : BElement, IComparable<BInteger>
    {
        /// <summary>
        /// Allows you to set an integer to a BInteger.
        /// </summary>
        /// <param name="i">The integer.</param>
        /// <returns>The BInteger.</returns>
        public static implicit operator BInteger(int i)
        {
            return new BInteger(i);
        }

        //public int Value { get; set; }
        /// <summary>
        /// The value of the bencoded integer.
        /// </summary>
        public long Value { get; set; }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="value">The value of the bencoded integer.</param>
        public BInteger(long value /*int value*/)
        {
            this.Value = value;
        }

        /// <summary>
        /// Generates the bencoded equivalent of the integer.
        /// </summary>
        /// <returns>The bencoded equivalent of the integer.</returns>
        public string ToBencodedString()
        {
            return this.ToBencodedString(new StringBuilder()).ToString();
        }

        /// <summary>
        /// Generates the bencoded equivalent of the integer.
        /// </summary>
        /// <returns>The bencoded equivalent of the integer.</returns>
        public StringBuilder ToBencodedString(StringBuilder u)
        {
            return u.Append("i").Append(Value.ToString()).Append("e");
        }

        /// <see cref="Object.GetHashCode()"/>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        /// <see cref="int.Equals(object)"/>
        public override bool Equals(object obj)
        {
            try
            {
                return this.Value.Equals(((BInteger)obj).Value);
            }
            catch { return false; }
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        /// <see cref="IComparable<BInteger>.CompareTo(object)"/>
        public int CompareTo(BInteger other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}