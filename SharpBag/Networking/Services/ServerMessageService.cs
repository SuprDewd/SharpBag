using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Services
{
	/// <summary>
	/// A server message service.
	/// </summary>
	public class ServerMessageService : INetworkServerService
	{
		/// <summary>
		/// Occurs when a message is received.
		/// </summary>
		public event Action<string, int> OnMessageReceived;

		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void SendMessage(string message)
		{
			NetworkPacket packet = new NetworkPacket();
			packet.AllButTargets = true;
			packet.DataWriter.Write(message);
			this.Send(packet);
		}

		/// <summary>
		/// Receive the specified packet.
		/// </summary>
		/// <param name="packet">The packet.</param>
		public override void Receive(NetworkPacket packet)
		{
			if (this.OnMessageReceived != null) this.OnMessageReceived(packet.DataReader.ReadString(), packet.Sender);
			this.Send(packet);
		}
	}
}