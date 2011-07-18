using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpBag.Networking
{
	public class NetworkServer : NetworkServerServiceHandler
	{
		public class ServerSettings
		{
			private int _MaxConnectionCount = 16;
			private int _ConnectionBacklog = 10;

			public int MaxConnectionCount { get { return this._MaxConnectionCount; } set { this._MaxConnectionCount = value; } }

			public int ConnectionBacklog { get { return this._ConnectionBacklog; } set { this._ConnectionBacklog = value; } }
		}

		public event Action<NetworkServer> OnClose;

		public event Action<NetworkServer> OnOpen;

		public event Action<NetworkServer, TcpNetworkConnection> OnConnectionConnected;

		public event Action<NetworkServer, TcpNetworkConnection> OnConnectionDisconnected;

		private object ConnectionLock = new object();

		private Socket Socket;

		private Dictionary<int, TcpNetworkConnection> Connections = new Dictionary<int, TcpNetworkConnection>();

		private volatile int ConnectionIDCounter = 1;

		public ServerSettings Settings { get; private set; }

		private volatile bool _IsOpen;

		public bool IsOpen { get { return this._IsOpen; } }

		public EndPoint LocalEndPoint { get { return this.Socket == null ? null : this.Socket.LocalEndPoint; } }

		private volatile int _ConnectionCount;

		public int ConnectionCount { get { return this._ConnectionCount; } }

		public NetworkServer(ServerSettings settings = null)
		{
			this.Settings = settings == null ? new ServerSettings() : settings;
		}

		public void Open(IPAddress address, int port)
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsOpen)
				{
					this.Open(new IPEndPoint(address, port));
				}
			}
		}

		public void Open(EndPoint localEndPoint)
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsOpen)
				{
					this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					base.OpenAllServices(this);
					this.Socket.Bind(localEndPoint);
					this.Socket.Listen(this.Settings.ConnectionBacklog);
					this._IsOpen = true;
					if (this.OnOpen != null) this.OnOpen(this);
					this.BeginAcceptConnection();
				}
			}
		}

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
					base.CloseAllServices(this);
				}
			}
		}

		public void Send(NetworkPacket packet)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsOpen)
				{
					if (packet.AllButTargets)
					{
						foreach (var connection in this.Connections)
						{
							if (packet.Targets == null || !packet.Targets.Contains(connection.Key))
							{
								connection.Value.Send(packet);
							}
						}
					}
					else
					{
						foreach (var id in packet.Targets)
						{
							TcpNetworkConnection conn;
							if (this.Connections.TryGetValue(id, out conn))
							{
								conn.Send(packet);
							}
						}
					}
				}
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
								connection.OnPacketReceived += new Action<TcpNetworkConnection, NetworkPacket>(PacketReceived);
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

		private void PacketReceived(TcpNetworkConnection connection, NetworkPacket packet)
		{
			lock (this.ConnectionLock)
			{
				this.DeliverPacket(packet);
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

		public override INetworkServerService GetService(int id)
		{
			lock (this.ConnectionLock)
			{
				return base.GetService(id);
			}
		}

		public override void RegisterService(int id, INetworkServerService service)
		{
			lock (this.ConnectionLock)
			{
				base.RegisterService(id, service);
			}
		}

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