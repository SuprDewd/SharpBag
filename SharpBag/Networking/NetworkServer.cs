using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SharpBag.Networking.Serialization;
using SharpBag.Networking.Services;

namespace SharpBag.Networking
{
    /// <summary>
    /// A network server.
    /// </summary>
    public class NetworkServer : NetworkServerServiceHandler
    {
        /// <summary>
        /// Settings for a network server.
        /// </summary>
        public class ServerSettings
        {
            private int _MaxConnectionCount = 16;
            private int _ConnectionBacklog = 10;
            private INetworkSerializer _Serializer = new DefaultNetworkSerializer();

            /// <summary>
            /// Gets or sets the maximum amount of simultaneous connections.
            /// </summary>
            /// <value>
            /// The maximum amount of simultaneous connections.
            /// </value>
            public int MaxConnectionCount { get { return this._MaxConnectionCount; } set { this._MaxConnectionCount = value; } }

            /// <summary>
            /// Gets or sets the connection backlog.
            /// </summary>
            /// <value>
            /// The connection backlog.
            /// </value>
            public int ConnectionBacklog { get { return this._ConnectionBacklog; } set { this._ConnectionBacklog = value; } }

            /// <summary>
            /// Gets or sets the serializer.
            /// </summary>
            /// <value>
            /// The serializer.
            /// </value>
            public INetworkSerializer Serializer { get { return this._Serializer; } set { this._Serializer = value; } }
        }

        /// <summary>
        /// Occurs when the server is closed.
        /// </summary>
        public event Action<NetworkServer> OnClose;

        /// <summary>
        /// Occurs when the server is opened.
        /// </summary>
        public event Action<NetworkServer> OnOpen;

        /// <summary>
        /// Occurs when a connection connects to the server.
        /// </summary>
        public event Action<NetworkServer, TcpNetworkConnection> OnConnectionConnected;

        /// <summary>
        /// Occurs when a connection disconnects from the server.
        /// </summary>
        public event Action<NetworkServer, TcpNetworkConnection> OnConnectionDisconnected;

        private object ConnectionLock = new object();

        private Socket Socket;

        private Dictionary<int, TcpNetworkConnection> Connections = new Dictionary<int, TcpNetworkConnection>();

        private volatile int ConnectionIDCounter = 1;

        /// <summary>
        /// Gets the server settings.
        /// </summary>
        public ServerSettings Settings { get; private set; }

        private volatile bool _IsOpen;

        /// <summary>
        /// Gets a value indicating whether this instance is open.
        /// </summary>
        /// <value>
        /// Whether this instance is open.
        /// </value>
        public bool IsOpen { get { return this._IsOpen; } }

        /// <summary>
        /// Gets the local end point of the server.
        /// </summary>
        public EndPoint LocalEndPoint { get { return this.Socket == null ? null : this.Socket.LocalEndPoint; } }

        private volatile int _ConnectionCount;

        /// <summary>
        /// Gets the number of connected connections.
        /// </summary>
        public int ConnectionCount { get { return this._ConnectionCount; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkServer"/> class.
        /// </summary>
        /// <param name="settings">The server settings.</param>
        public NetworkServer(ServerSettings settings = null)
        {
            this.Settings = settings ?? new ServerSettings();
        }

        /// <summary>
        /// Opens the server at the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        public void Open(int port)
        {
            lock (this.ConnectionLock)
            {
                if (!this._IsOpen)
                {
                    this.Open(new IPEndPoint(IPAddress.Any, port));
                }
                else throw new InvalidOperationException("Server is already open");
            }
        }

        /// <summary>
        /// Opens the server at the specified address and port.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="port">The port.</param>
        public void Open(IPAddress address, int port)
        {
            lock (this.ConnectionLock)
            {
                if (!this._IsOpen)
                {
                    this.Open(new IPEndPoint(address, port));
                }
                else throw new InvalidOperationException("Server is already open");
            }
        }

        /// <summary>
        /// Opens the server at the specified local end point.
        /// </summary>
        /// <param name="localEndPoint">The local end point.</param>
        public void Open(EndPoint localEndPoint)
        {
            lock (this.ConnectionLock)
            {
                if (!this._IsOpen)
                {
                    this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    base.StartAllServices(this);
                    this.Socket.Bind(localEndPoint);
                    this.Socket.Listen(this.Settings.ConnectionBacklog);
                    this._IsOpen = true;
                    if (this.OnOpen != null) this.OnOpen(this);
                    this.BeginAcceptConnection();
                }
                else throw new InvalidOperationException("Server is already open");
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            lock (this.ConnectionLock)
            {
                if (this._IsOpen)
                {
                    try
                    {
                        this.Socket.Close();
                        this.Socket = null;
                    }
                    catch { }

                    var conns = this.Connections.ToArray();

                    for (int i = 0; i < conns.Length; i++)
                    {
                        try
                        {
                            conns[i].Value.Disconnect();
                        }
                        catch { }
                    }

                    this._IsOpen = false;
                    this.Connections.Clear();
                    this._ConnectionCount = 0;
                    if (this.OnClose != null) this.OnClose(this);
                    base.StopAllServices(this);
                }
                else throw new InvalidOperationException("Server is already closed");
            }
        }

        /// <summary>
        /// Sends the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public void Send(NetworkPacket packet)
        {
            lock (this.ConnectionLock)
            {
                if (this._IsOpen)
                {
                    if (packet.AllButTargets)
                    {
                        var connections = this.Connections.ToArray();

                        for (int i = 0; i < connections.Length; i++)
                        {
                            if (packet.Targets == null || !packet.Targets.Contains(connections[i].Key))
                            {
                                connections[i].Value.Send(this.Settings.Serializer.Serialize(packet));
                            }
                        }
                    }
                    else
                    {
                        if (packet.Targets != null)
                        {
                            foreach (var id in packet.Targets)
                            {
                                TcpNetworkConnection conn;
                                if (this.Connections.TryGetValue(id, out conn))
                                {
                                    conn.Send(this.Settings.Serializer.Serialize(packet));
                                }
                            }
                        }
                    }
                }
                else throw new InvalidOperationException("Server must be open");
            }
        }

        private void BeginAcceptConnection()
        {
            lock (this.ConnectionLock)
            {
                if (this._IsOpen)
                {
                    this.Socket.BeginAccept(this.EndAcceptConnection, null);
                }
            }
        }

        private void EndAcceptConnection(IAsyncResult result)
        {
            lock (this.ConnectionLock)
            {
                if (this._IsOpen)
                {
                    try
                    {
                        Socket socket = this.Socket.EndAccept(result);
                        if (this.Settings.MaxConnectionCount == 0 || this._ConnectionCount < this.Settings.MaxConnectionCount)
                        {
                            int id = this.ConnectionIDCounter++;
                            if (id == 0) id = this.ConnectionIDCounter++;
                            SocketError e;

                            try
                            {
                                socket.Send(BitConverter.GetBytes(id), 0, 4, SocketFlags.None, out e);
                            }
                            catch
                            {
                                e = SocketError.SocketError;
                            }

                            if (e == SocketError.Success)
                            {
                                TcpNetworkConnection connection = new TcpNetworkConnection(id, socket, ref this.ConnectionLock);
                                connection.OnDisconnect += new Action<TcpNetworkConnection>(ConnectionDisconnected);
                                connection.OnPacketReceived += new Action<TcpNetworkConnection, byte[]>(PacketReceived);
                                connection.Connect();
                                this.Connections.Add(id, connection);
                                this._ConnectionCount++;
                                // connection.Send(new NetworkPacket { Service = -1, Targets = new int[1] { id }, Data = id });
                                if (this.OnConnectionConnected != null) this.OnConnectionConnected(this, connection);
                            }
                        }
                        else
                        {
                            try
                            {
                                socket.BeginSend(new byte[4], 0, 4, SocketFlags.None, null, null);
                            }
                            catch { }
                        }

                        this.BeginAcceptConnection();
                    }
                    catch (SocketException e)
                    {
                        this.Error(e.SocketErrorCode);
                    }
                }
            }
        }

        private void PacketReceived(TcpNetworkConnection connection, byte[] packet)
        {
            lock (this.ConnectionLock)
            {
                this.DeliverPacket(this.Settings.Serializer.Deserialize(packet));
            }
        }

        private void ConnectionDisconnected(TcpNetworkConnection connection)
        {
            lock (this.ConnectionLock)
            {
                this.Connections.Remove(connection.ID);
                if (this.OnConnectionDisconnected != null) this.OnConnectionDisconnected(this, connection);
            }
        }

        private void Error(SocketError e)
        {
            lock (this.ConnectionLock)
            {
                this.Close();
            }
        }

        #region Service handler

        /// <summary>
        /// Gets the service with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The service with the specified id.</returns>
        public override INetworkServerService GetService(int id)
        {
            lock (this.ConnectionLock)
            {
                return base.GetService(id);
            }
        }

        /// <summary>
        /// Registers the service.
        /// </summary>
        /// <param name="id">The id of the service.</param>
        /// <param name="service">The service.</param>
        public override void RegisterService(int id, INetworkServerService service)
        {
            lock (this.ConnectionLock)
            {
                base.RegisterService(id, service);
            }
        }

        /// <summary>
        /// Unregisters the service with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public override void UnregisterService(int id)
        {
            lock (this.ConnectionLock)
            {
                base.UnregisterService(id);
            }
        }

        #endregion Service handler
    }
}