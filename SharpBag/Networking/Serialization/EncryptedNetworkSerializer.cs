using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace SharpBag.Networking.Serialization
{
    /// <summary>
    /// An RSA encrypted network serializer.
    /// </summary>
    public class RSAEncryptedNetworkSerializer : INetworkSerializer
    {
        private RSACryptoServiceProvider Encrypter { get; set; }

        private RSACryptoServiceProvider Decrypter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RSAEncryptedNetworkSerializer"/> class.
        /// </summary>
        /// <param name="encrypter">The encrypter.</param>
        /// <param name="decrypter">The decrypter.</param>
        public RSAEncryptedNetworkSerializer(RSACryptoServiceProvider encrypter, RSACryptoServiceProvider decrypter)
        {
            this.Encrypter = encrypter;
            this.Decrypter = decrypter;
        }

        /// <summary>
        /// Deserializes the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>The deserialized packet.</returns>
        public NetworkPacket Deserialize(byte[] packet)
        {
            return NetworkPacket.Deserialize(this.Decrypter.Decrypt(packet, true));
        }

        /// <summary>
        /// Serializes the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>The serialized packet.</returns>
        public byte[] Serialize(NetworkPacket packet)
        {
            byte[] buffer = packet.Serialize();
            return this.Encrypter.Encrypt(buffer, true);
        }
    }
}