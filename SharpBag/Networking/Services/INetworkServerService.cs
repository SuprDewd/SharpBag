using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Services
{
    /// <summary>
    /// A network server service.
    /// </summary>
    public abstract class INetworkServerService
    {
        /// <summary>
        /// Gets the ID of the service.
        /// </summary>
        public int ID { get; internal set; }

        /// <summary>
        /// An internal setter for the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        internal NetworkServer ServerSetter
        {
            set
            {
                this._Server = value;
                if (this._Server != null)
                {
                    this._Server.OnOpen += o => this.ServerOpened();
                    if (this._Server.IsOpen) this.ServerOpened();
                }
            }
        }

        /// <summary>
        /// Gets the network server.
        /// </summary>
        protected NetworkServer Server { get { return this._Server; } }

        private NetworkServer _Server;
        private Queue<Tuple<NetworkPacket, int>> OutgoingPackets = new Queue<Tuple<NetworkPacket, int>>();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public virtual void Stop() { }

        private void ServerOpened()
        {
            int count = this.OutgoingPackets.Count;
            for (int i = 0; i < count && this._Server.IsOpen; i++)
            {
                var next = this.OutgoingPackets.Dequeue();
                this.Send(next.Item1, next.Item2);
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

        /// <summary>
        /// Receive the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public abstract void Receive(NetworkPacket packet);
    }
}