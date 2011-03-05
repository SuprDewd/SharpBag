using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SharpBag.Networking
{
    /// <summary>
    /// A network host.
    /// </summary>
    public class Host : PacketEventHandler, IConnection, IDisposable
    {
        private TcpListener Connection { get; set; }
        /// <summary>
        /// Whether the connection has been opened.
        /// </summary>
        public bool Opened { get; private set; }
        private int Port { get; set; }
        private event Action<ConnectionPacket> OnSendPacket;
        /// <summary>
        /// An event that is fired when a client connects.
        /// </summary>
        public event Action<ConnectionHandler> OnClientConnected;

        /// <summary>
        /// Create a new host.
        /// </summary>
        /// <param name="port">The port to listen to.</param>
        public Host(int port)
        {
            this.Port = port;
            this.Opened = false;
            this.Connection = new TcpListener(IPAddress.Any, port);
        }

        /// <summary>
        /// Start listening.
        /// </summary>
        public void Open()
        {
            if (this.Opened) return;
            this.Opened = true;
            this.Connection.Start();
            ThreadPool.QueueUserWorkItem(o => Listen());
        }

        /// <summary>
        /// Send the specified packet to all clients.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        internal void SendAll(ConnectionPacket packet)
        {
            if (this.OnSendPacket != null) this.OnSendPacket(packet);
        }

        private void Listen()
        {
            while (this.Opened)
            {
                if (!this.Connection.Pending()) { Thread.Sleep(50); continue; }
                ThreadPool.QueueUserWorkItem(o => HandleClient(this.Connection.AcceptTcpClient()));
            }
        }

        private void HandleClient(TcpClient tcpClient)
        {
            ConnectionHandler client = new ConnectionHandler(tcpClient);
            Action<ConnectionPacket> onPacketSend = p => { if (!client.SendPacket(p)) this.Close(); };
            this.OnSendPacket += onPacketSend;
            if (this.OnClientConnected != null) this.OnClientConnected(client);

            int i = 0;
            while (this.Opened)
            {
                if (i == 9 && !client.Ping()) break;
                if (client.Available == 0) { Thread.Sleep(50); i++; i %= 10; continue; }

                ConnectionPacket packet = client.ReceivePacket();
                try { if (packet.Data.GetType() == typeof(Ping)) continue; }
                catch { }

                Action<object, PacketEventArgs> action;
                if (this.Events.TryGetValue(packet.EventID, out action)) action(packet.Data, new PacketEventArgs(packet, client));
                if (client.Events.TryGetValue(packet.EventID, out action)) action(packet.Data, new PacketEventArgs(packet, client));
            }

            this.OnSendPacket -= onPacketSend;
        }

        /// <summary>
        /// Stop listening.
        /// </summary>
        public void Close()
        {
            if (!this.Opened) return;
            this.Opened = false;
            this.Connection.Stop();
        }

        /// <see cref="IDisposable.Dispose()"/>
        public void Dispose()
        {
            this.Close();
        }

        /// <summary>
        /// Fire the specified event on the client.
        /// </summary>
        /// <typeparam name="T">The type of data to send.</typeparam>
        /// <param name="eventID">The ID of the event.</param>
        /// <param name="obj">The data to send.</param>
        /// <returns>The current instance (for chaining).</returns>
        public override PacketEventHandler Fire<T>(int eventID, T obj)
        {
            this.SendAll(new ConnectionPacket { EventID = eventID, Data = obj });
            return this;
        }

        /// <summary>
        /// Fire the specified event on the client.
        /// </summary>
        /// <param name="eventID">The ID of the event.</param>
        /// <returns>The current instance (for chaining).</returns>
        public override PacketEventHandler Fire(int eventID)
        {
            this.SendAll(new ConnectionPacket { EventID = eventID });
            return this;
        }
    }
}