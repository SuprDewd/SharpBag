using System;

using System.Diagnostics.Contracts;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SharpBag.IO
{
    /// <summary>
    /// A static class used for object serialization.
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// Serializes an object using a binary serializer.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="fileName">The file to serialize to.</param>
        public static void Serialize<T>(T obj, string fileName)
        {
            Contract.Requires(!String.IsNullOrEmpty(fileName));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// Deserializes an object using a binary deserializer.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="fileName">The file to deserialize from.</param>
        /// <returns>The object.</returns>
        public static T Deserialize<T>(string fileName)
        {
            Contract.Requires(!String.IsNullOrEmpty(fileName));

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(fs);
            }
        }

        /// <summary>
        /// Serializes an object using an xml serializer.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="fileName">The file to serialize to.</param>
        public static void XmlSerialize<T>(T obj, string fileName)
        {
            Contract.Requires(!String.IsNullOrEmpty(fileName));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// Deserializes an object using an xml deserializer.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="fileName">The file to deserialize from.</param>
        /// <returns>The object.</returns>
        public static T XmlDeserialize<T>(string fileName)
        {
            Contract.Requires(!String.IsNullOrEmpty(fileName));
            Contract.Requires(File.Exists(fileName));

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(fs);
            }
        }
    }
}