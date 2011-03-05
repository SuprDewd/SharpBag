using System;
using System.Net.Sockets;
using System.Threading;

namespace SharpBag.Networking
{
    /// <summary>
    /// A network client.
    /// </summary>
    public class Client : PacketEventHandler, IConnection
    {
        private TcpClient Connection { get; set; }
        private string Hostname;
        private int Port;
        /// <summary>
        /// Whether the client is connected.
        /// </summary>
        public bool Connected { get; private set; }
        /// <summary>
        /// An event that is fired when the client is disconnected.
        /// </summary>
        public event Action OnDisconnected;
        private ConnectionHandler ConnectionHandler { get; set; }

        /// <summary>
        /// Create a new client.
        /// </summary>
        /// <param name="hostname">The hostname, or IP address, of the destination.</param>
        /// <param name="port">The port to connect to.</param>
        public Client(string hostname, int port)
        {
            this.Connected = false;
            this.Hostname = hostname;
            this.Port = port;
            this.Connection = new TcpClient();
        }

        /// <summary>
        /// Open the connection.
        /// </summary>
        public void Open()
        {
            if (this.Connected) return;
            this.Connected = true;
            this.Connection.Connect(this.Hostname, this.Port);
            this.ConnectionHandler = new ConnectionHandler(this.Connection);
            ThreadPool.QueueUserWorkItem(i => Listen());
        }

        private void Listen()
        {
            int i = 0;
            while (this.Connected)
            {
                if (i == 9 && !this.ConnectionHandler.Ping()) break;
                if (this.Connection.Available == 0) { Thread.Sleep(50); i++; i %= 10; continue; }

                ConnectionPacket packet = this.ConnectionHandler.ReceivePacket();
                try { if (packet.Data.GetType() == typeof(Ping)) continue; }
                catch { }

                Action<object, PacketEventArgs> action;
                if (this.Events.TryGetValue(packet.EventID, out action)) action(packet.Data, new PacketEventArgs(packet, this.ConnectionHandler));
                if (this.ConnectionHandler.Events.TryGetValue(packet.EventID, out action)) action(packet.Data, new PacketEventArgs(packet, this.ConnectionHandler));
            }

            if (this.OnDisconnected != null) this.OnDisconnected();
        }

        /// <summary>
        /// Close the connection.
        /// </summary>
        public void Close()
        {
            if (!this.Connected) return;
            this.Connection.Close();
            this.Connected = false;
        }

        /// <see cref="IDisposable.Dispose()"/>
        public void Dispose()
        {
            this.Close();
        }

        /// <summary>
        /// Fire the specified event on the server.
        /// </summary>
        /// <typeparam name="T">The type of data to send.</typeparam>
        /// <param name="eventID">The ID of the event.</param>
        /// <param name="obj">The data to send.</param>
        /// <returns>The current instance (for chaining).</returns>
        public override PacketEventHandler Fire<T>(int eventID, T obj)
        {
            this.ConnectionHandler.Fire(eventID, obj);
            return this;
        }

        /// <summary>
        /// Fire the specified event on the server.
        /// </summary>
        /// <param name="eventID">The ID of the event.</param>
        /// <returns>The current instance (for chaining).</returns>
        public override PacketEventHandler Fire(int eventID)
        {
            this.ConnectionHandler.Fire(eventID);
            return this;
        }
    }
}