using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SharpBag.Networking
{
	/// <summary>
	/// A network packet.
	/// </summary>
	public class NetworkPacket
	{
		/// <summary>
		/// Gets or sets the service this packet belongs to.
		/// </summary>
		/// <value>
		/// The service.
		/// </value>
		public int Service { get; set; }

		/// <summary>
		/// Gets or sets the sender of this packet.
		/// </summary>
		/// <value>
		/// The sender.
		/// </value>
		public int Sender { get; set; }

		/// <summary>
		/// Gets or sets the targets.
		/// </summary>
		/// <value>
		/// The targets.
		/// </value>
		public int[] Targets { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the packet should be sent to everyone, except the specified targets.
		/// </summary>
		/// <value>
		/// Whether the packet should be sent to everyone, except the specified targets.
		/// </value>
		public bool AllButTargets { get; set; }

		/// <summary>
		/// Gets the data reader.
		/// </summary>
		public BinaryReader DataReader { get; private set; }

		/// <summary>
		/// Gets the data writer.
		/// </summary>
		public BinaryWriter DataWriter { get; private set; }

		private MemoryStream Stream;

		/// <summary>
		/// Gets or sets the position of the datastream.
		/// </summary>
		/// <value>
		/// The position of the datastream.
		/// </value>
		public long Position { get { return this.Stream.Position; } set { this.Stream.Position = value; } }

		private NetworkPacket(MemoryStream stream)
		{
			this.Stream = stream;
			this.DataReader = new BinaryReader(this.Stream);
			this.DataWriter = new BinaryWriter(this.Stream);
		}

		private NetworkPacket(byte[] bytes)
			: this(new MemoryStream(bytes.Length))
		{
			this.Stream.Write(bytes, 0, bytes.Length);
			this.Stream.Position = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkPacket"/> class.
		/// </summary>
		public NetworkPacket() : this(new MemoryStream()) { }

		/// <summary>
		/// Serializes the packet.
		/// </summary>
		/// <param name="prependLength">if set to <c>true</c> the length of the packet will be prepended.</param>
		/// <returns>The serialized packet.</returns>
		public byte[] Serialize(bool prependLength = true)
		{
			long beforePosition = this.Stream.Position;
			this.Stream.Position = 0;
			int targetCount = this.Targets == null ? 0 : this.Targets.Length,
				packetSize = 4 * (targetCount + 3) + 1 + (int)this.Stream.Length;
			MemoryStream mem = new MemoryStream(packetSize);

			if (prependLength) mem.Write(BitConverter.GetBytes(packetSize), 0, 4);
			mem.Write(BitConverter.GetBytes(this.Service), 0, 4);
			mem.Write(BitConverter.GetBytes(this.Sender), 0, 4);
			mem.Write(BitConverter.GetBytes(targetCount), 0, 4);

			if (this.Targets != null)
			{
				for (int i = 0; i < this.Targets.Length; i++)
				{
					mem.Write(BitConverter.GetBytes(this.Targets[i]), 0, 4);
				}
			}

			mem.Write(BitConverter.GetBytes(this.AllButTargets), 0, 1);
			this.Stream.WriteTo(mem);
			this.Stream.Position = beforePosition;

			return mem.ToArray();
		}

		/// <summary>
		/// Deserializes the serialized packet.
		/// </summary>
		/// <param name="bytes">The serialized packet.</param>
		/// <returns>The packet.</returns>
		public static NetworkPacket Deserialize(byte[] bytes)
		{
			int service = BitConverter.ToInt32(bytes, 0),
				sender = BitConverter.ToInt32(bytes, 4),
				targetCount = BitConverter.ToInt32(bytes, 8),
				position = 12;

			int[] targets = targetCount == 0 ? null : new int[targetCount];

			for (int i = 0; i < targetCount; i++)
			{
				targets[i] = BitConverter.ToInt32(bytes, position);
				position += 4;
			}

			bool allButTargets = BitConverter.ToBoolean(bytes, position);
			position += 1;

			byte[] data = new byte[bytes.Length - position];
			Buffer.BlockCopy(bytes, position, data, 0, data.Length);

			return new NetworkPacket(data)
			{
				Service = service,
				Sender = sender,
				Targets = targets,
				AllButTargets = allButTargets
			};
		}

		/// <summary>
		/// Serializes and writes the data to the datastream.
		/// </summary>
		/// <typeparam name="T">The type of data.</typeparam>
		/// <param name="data">The data.</param>
		public void WriteData<T>(T data)
		{
			new BinaryFormatter().Serialize(this.Stream, data);
		}

		/// <summary>
		/// Reads and deserializes the data from the datastream.
		/// </summary>
		/// <typeparam name="T">The type of data to read.</typeparam>
		/// <returns>The deserialized data.</returns>
		public T ReadData<T>()
		{
			return (T)new BinaryFormatter().Deserialize(this.Stream);
		}
	}
}