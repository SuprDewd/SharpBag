using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Services
{
	public class ServerMessageService : INetworkServerService
	{
		public event Action<string, int> OnMessageReceived;

		public void SendMessage(string message)
		{
			NetworkPacket packet = new NetworkPacket();
			packet.AllButTargets = true;
			packet.DataWriter.Write(message);
			this.Send(packet);
		}

		public override void Receive(NetworkPacket packet)
		{
			if (this.OnMessageReceived != null) this.OnMessageReceived(packet.DataReader.ReadString(), packet.Sender);
			this.Send(packet);
		}
	}
}