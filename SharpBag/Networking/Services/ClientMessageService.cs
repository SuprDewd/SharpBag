using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Services
{
    /// <summary>
    /// A client message service.
    /// </summary>
    public class ClientMessageService : INetworkClientService
    {
        /// <summary>
        /// Occurs when a message is received.
        /// </summary>
        public event Action<string, int> OnMessageReceived;

        /// <summary>
        /// Gets or sets a value indicating whether messages should be resent to the sender.
        /// </summary>
        /// <value>
        /// Whether the message should be resent to the sender.
        /// </value>
        public bool ResendToSender { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientMessageService"/> class.
        /// </summary>
        /// <param name="resendToSender">if set to <c>true</c> messages will be resent to the sender.</param>
        public ClientMessageService(bool resendToSender)
        {
            this.ResendToSender = resendToSender;
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(string message)
        {
            NetworkPacket packet = new NetworkPacket();
            packet.AllButTargets = true;
            packet.Targets = this.ResendToSender ? null : new int[] { this.Client.ID };
            packet.DataWriter.Write(message);
            this.Send(packet);
        }

        /// <summary>
        /// Sends the message to the specified target.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        public void SendMessage(string message, int target)
        {
            this.SendMessage(message, new int[] { target });
        }

        /// <summary>
        /// Sends the message to the specified targets.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="targets">The targets.</param>
        public void SendMessage(string message, int[] targets)
        {
            Contract.Requires(targets != null);
            NetworkPacket packet = new NetworkPacket();
            packet.AllButTargets = false;
            packet.Targets = targets;
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
        }
    }
}