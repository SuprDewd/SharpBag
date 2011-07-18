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

		private Queue<NetworkPacket> OutgoingPackets = new Queue<NetworkPacket>();

		public void Open(int id, NetworkClient client)
		{
			this.ID = id;
			this.Client = client;
			this.Client.OnConnect += new Action<NetworkClient>(Client_OnConnect);
			this.Open();
		}

		private void Client_OnConnect(NetworkClient obj)
		{
			while (this.OutgoingPackets.Count > 0)
			{
				this.Client.Send(this.OutgoingPackets.Dequeue());
			}
		}

		public virtual void Open() { }

		public virtual void Close(int id, NetworkClient client) { }

		protected void Send(NetworkPacket packet) { this.Send(packet, this.ID); }

		protected void Send(NetworkPacket packet, int serviceID)
		{
			packet.Service = serviceID;

			if (this.Client.IsConnected)
			{
				this.Client.Send(packet);
			}
			else
			{
				this.OutgoingPackets.Enqueue(packet);
			}
		}

		public abstract void Receive(NetworkPacket packet);
	}
}