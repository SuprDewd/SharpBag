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

		private Queue<Tuple<NetworkPacket, int>> OutgoingPackets = new Queue<Tuple<NetworkPacket, int>>();

		public void Open(int id, NetworkServer server)
		{
			this.ID = id;
			this.Server = server;
			this.Server.OnOpen += new Action<NetworkServer>(Server_OnOpen);
			if (this.Server.IsOpen) this.Server_OnOpen(this.Server);
			this.Open();
		}

		private void Server_OnOpen(NetworkServer obj)
		{
			int count = this.OutgoingPackets.Count;
			for (int i = 0; i < count; i++)
			{
				var next = this.OutgoingPackets.Dequeue();
				this.Send(next.Item1, next.Item2);
			}
		}

		public virtual void Open() { }

		public virtual void Close(int id, NetworkServer server) { }

		protected void Send(NetworkPacket packet) { this.Send(packet, Int32.MinValue); }

		protected void Send(NetworkPacket packet, int serviceID)
		{
			if (this.Server != null && this.Server.IsOpen)
			{
				packet.Service = serviceID == Int32.MinValue ? this.ID : serviceID;
				this.Server.Send(packet);
			}
			else
			{
				this.OutgoingPackets.Enqueue(new Tuple<NetworkPacket, int>(packet, serviceID));
			}
		}

		public abstract void Receive(NetworkPacket packet);
	}
}