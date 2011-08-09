using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Serialization
{
	/// <summary>
	/// A network serializer.
	/// </summary>
	public class DefaultNetworkSerializer : INetworkSerializer
	{
		/// <summary>
		/// Deserializes the specified packet.
		/// </summary>
		/// <param name="packet">The packet.</param>
		/// <returns>The deserialized packet.</returns>
		public NetworkPacket Deserialize(byte[] packet)
		{
			return NetworkPacket.Deserialize(packet);
		}

		/// <summary>
		/// Serializes the specified packet.
		/// </summary>
		/// <param name="packet">The packet.</param>
		/// <returns>The serialized packet.</returns>
		public byte[] Serialize(NetworkPacket packet)
		{
			return packet.Serialize();
		}
	}
}