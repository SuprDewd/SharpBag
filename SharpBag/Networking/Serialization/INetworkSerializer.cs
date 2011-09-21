using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Serialization
{
    /// <summary>
    /// A network serializer.
    /// </summary>
    public interface INetworkSerializer
    {
        /// <summary>
        /// Deserializes the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>The deserialized packet.</returns>
        NetworkPacket Deserialize(byte[] packet);

        /// <summary>
        /// Serializes the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>The serialized packet.</returns>
        byte[] Serialize(NetworkPacket packet);
    }
}