using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Services
{
    /// <summary>
    /// A network client service.
    /// </summary>
    public abstract class INetworkClientService
    {
        /// <summary>
        /// The ID of the service.
        /// </summary>
        public int ID { get; internal set; }

        /// <summary>
        /// An internal setter for the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        internal NetworkClient ClientSetter
        {
            set
            {
                this._Client = value;
                if (this._Client != null)
                {
                    this._Client.OnConnect += c => this.ClientConnected();
                    if (this._Client.IsConnected) this.ClientConnected();
                }
            }
        }

        /// <summary>
        /// Gets the network client.
        /// </summary>
        protected NetworkClient Client { get { return this._Client; } }

        private NetworkClient _Client;
        private Queue<Tuple<NetworkPacket, int>> OutgoingPackets = new Queue<Tuple<NetworkPacket, int>>();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public virtual void Stop() { }

        private void ClientConnected()
        {
            int count = this.OutgoingPackets.Count;
            for (int i = 0; i < count && this.Client.IsConnected; i++)
            {
                var next = this.OutgoingPackets.Dequeue();
                this.Send(next.Item1, next.Item2 == Int32.MinValue ? this.ID : next.Item2);
            }
        }

        /// <summary>
        /// Send the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        protected void Send(NetworkPacket packet) { this.Send(packet, Int32.MinValue); }

        /// <summary>
        /// Send the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="serviceID">The service ID.</param>
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

        /// <summary>
        /// Receive the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public abstract void Receive(NetworkPacket packet);
    }
}