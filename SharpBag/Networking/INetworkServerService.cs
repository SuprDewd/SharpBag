using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking
{
	public abstract class INetworkServerService
	{
		public int ID { get; private set; }

		protected NetworkServer Server { get; private set; }

		private Queue<NetworkPacket> OutgoingPackets = new Queue<NetworkPacket>();

		public void Open(int id, NetworkServer server)
		{
			this.ID = id;
			this.Server = server;
			this.Server.OnOpen += new Action<NetworkServer>(Server_OnOpen);
			this.Open();
		}

		private void Server_OnOpen(NetworkServer obj)
		{
			while (this.OutgoingPackets.Count > 0)
			{
				this.Server.Send(this.OutgoingPackets.Dequeue());
			}
		}

		public virtual void Open() { }

		public virtual void Close(int id, NetworkServer server) { }

		protected void Send(NetworkPacket packet) { this.Send(packet, this.ID); }

		protected void Send(NetworkPacket packet, int serviceID)
		{
			packet.Service = serviceID;

			if (this.Server.IsOpen)
			{
				this.Server.Send(packet);
			}
			else
			{
				this.OutgoingPackets.Enqueue(packet);
			}
		}

		public abstract void Receive(NetworkPacket packet);
	}
}