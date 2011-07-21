using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking
{
	public abstract class INetworkClientService
	{
		public int ID { get; private set; }

		protected NetworkClient Client { get; private set; }

		private Queue<Tuple<NetworkPacket, int>> OutgoingPackets = new Queue<Tuple<NetworkPacket, int>>();

		public void Open(int id, NetworkClient client)
		{
			this.ID = id;
			this.Client = client;
			this.Client.OnConnect += new Action<NetworkClient>(Client_OnConnect);
			if (this.Client.IsConnected) this.Client_OnConnect(this.Client);
			this.Open();
		}

		private void Client_OnConnect(NetworkClient obj)
		{
			int count = this.OutgoingPackets.Count;
			for (int i = 0; i < count; i++)
			{
				var next = this.OutgoingPackets.Dequeue();
				this.Send(next.Item1, next.Item2 == Int32.MinValue ? this.ID : next.Item2);
			}
		}

		public virtual void Open() { }

		public virtual void Close(int id, NetworkClient client) { }

		protected void Send(NetworkPacket packet) { this.Send(packet, Int32.MinValue); }

		protected void Send(NetworkPacket packet, int serviceID)
		{
			if (this.Client != null && this.Client.IsConnected)
			{
				packet.Service = serviceID == Int32.MinValue ? this.ID : serviceID;
				this.Client.Send(packet);
			}
			else
			{
				this.OutgoingPackets.Enqueue(new Tuple<NetworkPacket, int>(packet, serviceID));
			}
		}

		public abstract void Receive(NetworkPacket packet);
	}
}