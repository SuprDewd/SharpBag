using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpBag.Networking
{
    /// <summary>
    /// A connection handler.
    /// </summary>
    public class ConnectionHandler : PacketEventHandler
    {
        /// <summary>
        /// An event that is fired when the connection is disconnected.
        /// </summary>
        public event Action OnDisconnect;
        private TcpClient Connection;
        private NetworkStream ConnectionStream;
        /// <summary>
        /// Whether the connection is connected.
        /// </summary>
        public bool Connected { get; private set; }

        /// <summary>
        /// Create a connection handler.
        /// </summary>
        /// <param name="connection">The connection to handle.</param>
        public ConnectionHandler(TcpClient connection)
        {
            this.Connected = true;
            this.Connection = connection;
            this.ConnectionStream = this.Connection.GetStream();
        }

        /// <summary>
        /// Fire the specified event on the other side.
        /// </summary>
        /// <param name="eventID">The ID of the event.</param>
        public override PacketEventHandler Fire(int eventID)
        {
            this.SendPacket(new ConnectionPacket() { EventID = eventID });
            return this;
        }

        /// <summary>
        /// Fire the specified event on the other side.
        /// </summary>
        /// <typeparam name="T">The type of data to send.</typeparam>
        /// <param name="eventID">The ID of the event.</param>
        /// <param name="obj">The data to send.</param>
        public override PacketEventHandler Fire<T>(int eventID, T obj)
        {
            this.SendPacket(new ConnectionPacket() { EventID = eventID, Data = obj });
            return this;
        }
        
        /// <summary>
        /// Send the specified packet to the other side.
        /// </summary>
        /// <param name="packet">The specified packet.</param>
        /// <returns>Whether the packet was successfully sent.</returns>
        public bool SendPacket(ConnectionPacket packet)
        {
            try
            {
                lock (this.ConnectionStream)
                {
                    new BinaryFormatter().Serialize(this.ConnectionStream, packet);
                    this.ConnectionStream.Flush();
                    return true;
                }
            }
            catch { this.FireOnDisconnect(); return false; }
        }

        /// <summary>
        /// Receive a packet from the other side.
        /// </summary>
        /// <returns>The packet.</returns>
        public ConnectionPacket ReceivePacket()
        {
            lock (this.ConnectionStream)
            {
                return (ConnectionPacket)new BinaryFormatter().Deserialize(this.ConnectionStream);
            }
        }

        /// <summary>
        /// Fires the OnDisconnect event.
        /// </summary>
        internal void FireOnDisconnect()
        {
            if (this.Connected && this.OnDisconnect != null) this.OnDisconnect();
            this.Connected = false;
        }

        /// <summary>
        /// Pings the other side.
        /// </summary>
        /// <returns>Whether the ping was successful.</returns>
        public bool Ping()
        {
            return this.SendPacket(new ConnectionPacket { Data = new Ping() });
        }

        /// <summary>
        /// The amount of data available from the other side.
        /// </summary>
        public int Available { get { return this.Connection.Available; } }

        /// <summary>
        /// The remote endpoint.
        /// </summary>
        public EndPoint RemoteEndPoint { get { return this.Connection.Client.RemoteEndPoint; } }
        /// <summary>
        /// The local endpoint.
        /// </summary>
        public EndPoint LocalEndPoint { get { return this.Connection.Client.LocalEndPoint; } }
    }
}