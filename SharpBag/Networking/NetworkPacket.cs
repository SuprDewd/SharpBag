using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SharpBag.Networking
{
	public class NetworkPacket
	{
		public int Service { get; set; }

		public int Sender { get; set; }

		public int[] Targets { get; set; }

		public bool AllButTargets { get; set; }

		public BinaryReader DataReader { get; private set; }

		public BinaryWriter DataWriter { get; private set; }

		public MemoryStream Stream;

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

		public NetworkPacket() : this(new MemoryStream()) { }

		public byte[] Serialize(bool prependLength = true)
		{
			int targetCount = this.Targets == null ? 0 : this.Targets.Length;
			int packetSize = 4 * (targetCount + 3) + 1 + (int)this.Stream.Length;
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

			return mem.ToArray();
		}

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

		public void WriteData<T>(T data)
		{
			new BinaryFormatter().Serialize(this.Stream, data);
		}

		public T ReadData<T>()
		{
			return (T)new BinaryFormatter().Deserialize(this.Stream);
		}

		/*#region Writing

		public void Write(byte[] bytes, int offset, int count)
		{
			this.Stream.Write(bytes, offset, count);
		}

		public void Write(char[] chars, int offset, int count)
		{
			this.Write(chars, offset, count, Encoding.UTF8);
		}

		public void Write(char[] chars, int offset, int count, Encoding encoding)
		{
			this.Write(encoding.GetBytes(chars, offset, count));
		}

		public void Write(bool value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(byte value)
		{
			this.Stream.WriteByte(value);
		}

		public void Write(byte[] value)
		{
			this.Stream.Write(value, 0, value.Length);
		}

		public void Write(char value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(char[] value)
		{
			this.Write(value, Encoding.UTF8);
		}

		public void Write(char[] value, Encoding encoding)
		{
			this.Write(encoding.GetBytes(value));
		}

		public void Write(double value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(ushort value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(uint value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(ulong value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(short value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(int value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(long value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(sbyte value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(Single value)
		{
			this.Write(BitConverter.GetBytes(value));
		}

		public void Write(string value)
		{
			this.Write(value, Encoding.UTF8);
		}

		public void Write(string value, Encoding encoding)
		{
			this.Write(encoding.GetBytes(value));
		}

		#endregion Writing

		#region Reading

		public void Read(byte[] buffer, int offset, int count)
		{
			this.Stream.Read(buffer, offset, count);
		}

		public bool ReadBoolean()
		{
			BinaryReader br = new BinaryReader()
			return BitConverter.ToBoolean(this, )
		}

		#endregion Reading*/
	}
}