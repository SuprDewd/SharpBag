using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpBag.BagTorrent
{
    /// <summary>
    /// An interface for bencoded elements.
    /// </summary>
    public interface BElement
    {
        /// <summary>
        /// Generates the bencoded equivalent of the element.
        /// </summary>
        /// <returns>The bencoded equivalent of the element.</returns>
        string ToBencodedString();
    }

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
            return "i" + Value.ToString() + "e";
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
            return this.Value.Length + ":" + this.Value;
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
            string u = "l";
            foreach (BElement element in base.ToArray())
            {
                u += element.ToBencodedString();
            }
            return u + "e";
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
            string u = "d";
            for (int i = 0; i < base.Count; i++)
            {
                u += base.Keys.ElementAt(i).ToBencodedString();
                u += base.Values.ElementAt(i).ToBencodedString();
            }
            return u + "e";
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