using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SharpBag.Networking
{
	[Serializable]
	public class NetworkPacket
	{
		public int Sender { get; set; }

		public int Service { get; set; }

		public int[] Targets { get; set; }

		public bool AllButTargets { get; set; }

		public object Data { get; set; }

		public byte[] Serialize()
		{
			MemoryStream mem = new MemoryStream();
			new BinaryFormatter().Serialize(mem, this);
			return mem.ToArray();
		}

		public byte[] SerializeWithLength()
		{
			byte[] ser = this.Serialize();
			byte[] result = new byte[ser.Length + 4];
			Buffer.BlockCopy(BitConverter.GetBytes(ser.Length), 0, result, 0, 4);
			Buffer.BlockCopy(ser, 0, result, 4, ser.Length);
			return result;
		}

		public static NetworkPacket Deserialize(byte[] bytes, int position = 0)
		{
			MemoryStream mem = new MemoryStream(bytes);
			mem.Position = position;
			return (NetworkPacket)new BinaryFormatter().Deserialize(mem);
		}
	}
}