using System;
using System.Collections.Generic;

namespace SharpBag.BagTorrent
{
    /// <summary>
    /// A class used for decoding Bencoding.
    /// </summary>
    public class BencodeDecoder
    {
        /// <summary>
        /// A bencoding exception.
        /// </summary>
        public class BencodingException : FormatException
        {
            /// <summary>
            /// Creates a new BencodingException.
            /// </summary>
            public BencodingException() { }
            /// <summary>
            /// Creates a new BencodingException.
            /// </summary>
            /// <param name="message">The message.</param>
            public BencodingException(string message) : base(message) { }
            /// <summary>
            /// Creates a new BencodingException.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <param name="inner">The inner exception.</param>
            public BencodingException(string message, Exception inner) : base(message, inner) { }
        }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="s">The bencoded string to decode.</param>
        public BencodeDecoder(string s)
        {
            BencodedString = s;
        }

        /// <summary>
        /// Where the reader will start reading next.
        /// </summary>
        private int Index = 0;
        /// <summary>
        /// The bencoded string.
        /// </summary>
        public string BencodedString = null;

        /// <summary>
        /// Decodes the string.
        /// </summary>
        /// <returns>An array of root elements.</returns>
        public BElement[] Decode()
        {
            try
            {
                List<BElement> rootElements = new List<BElement>();
                while (BencodedString.Length > Index)
                {
                    rootElements.Add(ReadElement());
                }
                return rootElements.ToArray();
            }
            catch (BencodingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw Error(e);
            }
        }

        /// <summary>
        /// Reads and element.
        /// </summary>
        /// <returns>The element that was read.</returns>
        private BElement ReadElement()
        {
            switch (BencodedString[Index])
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9': return ReadString();
                case 'i': return ReadInteger();
                case 'l': return ReadList();
                case 'd': return ReadDictionary();
                default: throw Error(new Exception());
            }
        }

        /// <summary>
        /// Reads a dictionary.
        /// </summary>
        /// <returns>The dictionary that was read.</returns>
        private BDictionary ReadDictionary()
        {
            Index++;
            BDictionary dict = new BDictionary();
            try
            {
                while (BencodedString[Index] != 'e')
                {
                    BString K = ReadString();
                    BElement V = ReadElement();
                    dict.Add(K, V);
                }
            }
            catch (BencodingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw Error(e);
            }
            Index++;
            return dict;
        }

        /// <summary>
        /// Reads a list.
        /// </summary>
        /// <returns>The list that was read.</returns>
        private BList ReadList()
        {
            Index++;
            BList lst = new BList();
            try
            {
                while (BencodedString[Index] != 'e')
                {
                    lst.Add(ReadElement());
                }
            }
            catch (BencodingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw Error(e);
            }
            Index++;
            return lst;
        }

        /// <summary>
        /// Reads an integer.
        /// </summary>
        /// <returns>The integer that was read.</returns>
        private BInteger ReadInteger()
        {
            Index++;
            int end = BencodedString.IndexOf('e', Index);
            if (end == -1) throw Error(new Exception());
            long Integer = 0;
            try
            {
                Integer = Convert.ToInt64(BencodedString.Substring(Index, end - Index));
                Index = end + 1;
            }
            catch (Exception e)
            {
                throw Error(e);
            }
            return new BInteger(Integer);
        }

        /// <summary>
        /// Reads a string.
        /// </summary>
        /// <returns>The string that was read.</returns>
        private BString ReadString()
        {
            int length = 0;
            int semicolon = 0;
            try
            {
                semicolon = BencodedString.IndexOf(':', Index);
                if (semicolon == -1) throw Error(new Exception());
                length = Convert.ToInt32(BencodedString.Substring(Index, semicolon - Index));
            }
            catch (Exception e)
            {
                throw Error(e);
            }

            Index = semicolon + 1;
            int tmpIndex = Index;
            Index += length;
            try
            {
                return new BString(BencodedString.Substring(tmpIndex, length));
            }
            catch (Exception e)
            {
                throw Error(e);
            }
        }

        /// <summary>
        /// Generates an error.
        /// </summary>
        /// <param name="e">The inner exception.</param>
        /// <returns>An exception that can then be thrown.</returns>
        private Exception Error(Exception e)
        {
            return new BencodingException("Bencoded string invalid", e);
        }
    }
}
