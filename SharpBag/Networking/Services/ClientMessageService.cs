using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Services
{
	public class ClientMessageService : INetworkClientService
	{
		public Action<string, int> OnMessageReceived;

		public bool ResendToSender { get; set; }

		public ClientMessageService(bool resendToSender)
		{
			this.ResendToSender = resendToSender;
		}

		public void SendMessage(string message)
		{
			NetworkPacket packet = new NetworkPacket();
			packet.AllButTargets = true;
			packet.Targets = this.ResendToSender ? null : new int[] { this.Client.ID };
			packet.DataWriter.Write(message);
			this.Send(packet);
		}

		public void SendMessage(string message, int target)
		{
			this.SendMessage(message, new int[] { target });
		}

		public void SendMessage(string message, int[] targets)
		{
			Contract.Requires(targets != null);
			NetworkPacket packet = new NetworkPacket();
			packet.AllButTargets = false;
			packet.Targets = targets;
			packet.DataWriter.Write(message);
			this.Send(packet);
		}

		public override void Receive(NetworkPacket packet)
		{
			if (this.OnMessageReceived != null) this.OnMessageReceived(packet.DataReader.ReadString(), packet.Sender);
		}
	}
}