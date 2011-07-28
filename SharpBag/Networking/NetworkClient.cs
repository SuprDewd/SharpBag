using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpBag.Networking
{
	/// <summary>
	/// A network client.
	/// </summary>
	public class NetworkClient : NetworkClientServiceHandler
	{
		/// <summary>
		/// Occurs when the client connects.
		/// </summary>
		public event Action<NetworkClient> OnConnect;

		/// <summary>
		/// Occurs when the client disconnects.
		/// </summary>
		public event Action<NetworkClient> OnDisconnect;

		internal object ConnectionLock = new object();

		/// <summary>
		/// Gets the underlying connection.
		/// </summary>
		public TcpNetworkConnection Connection { get; private set; }

		private volatile bool _IsConnected;

		/// <summary>
		/// Gets a value indicating whether this instance is connected.
		/// </summary>
		/// <value>
		/// Whether this instance is connected.
		/// </value>
		public bool IsConnected { get { return this._IsConnected; } }

		/// <summary>
		/// Gets the ID of the client.
		/// </summary>
		public int ID { get { return this.Connection.ID; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkClient"/> class.
		/// </summary>
		public NetworkClient() { }

		/// <summary>
		/// Connects the client to the specified remote end point.
		/// </summary>
		/// <param name="remoteEndPoint">The remote end point.</param>
		public void Connect(EndPoint remoteEndPoint)
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsConnected)
				{
					Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socket.Connect(remoteEndPoint);
					this.Connect(socket);
				}
				else throw new InvalidOperationException("Client was already connected");
			}
		}

		/// <summary>
		/// Connects the client to the specified address and port.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="port">The port.</param>
		public void Connect(IPAddress address, int port)
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsConnected)
				{
					Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socket.Connect(address, port);
					this.Connect(socket);
				}
				else throw new InvalidOperationException("Client was already connected");
			}
		}

		/// <summary>
		/// Connects the client to the specified host and port.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="port">The port.</param>
		public void Connect(string host, int port)
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsConnected)
				{
					Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socket.Connect(host, port);
					this.Connect(socket);
				}
				else throw new InvalidOperationException("Client was already connected");
			}
		}

		private void Connect(Socket socket)
		{
			byte[] rawid = new byte[4];
			SocketError e;
			socket.Receive(rawid, 0, 4, SocketFlags.None, out e);

			if (e == SocketError.Success)
			{
				int id = BitConverter.ToInt32(rawid, 0);
				if (id == 0) throw new Exception("Server denied connection");
				else
				{
					this.Connection = new TcpNetworkConnection(id, socket, ref this.ConnectionLock);
					this.Connection.OnDisconnect += new Action<TcpNetworkConnection>(ConnectionDisconnected);
					this.Connection.OnPacketReceived += new Action<TcpNetworkConnection, NetworkPacket>(PacketReceived);
					this.Connection.Connect();
					this._IsConnected = true;
					if (this.OnConnect != null) this.OnConnect(this);
					base.StartAllServices(this);
				}
			}
			else
			{
				throw new Exception("Could not receive connection ID");
			}
		}

		private void PacketReceived(TcpNetworkConnection conn, NetworkPacket packet)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					this.DeliverPacket(packet);
				}
			}
		}

		private void ConnectionDisconnected(TcpNetworkConnection conn)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					this.Disconnect();
				}
			}
		}

		/// <summary>
		/// Disconnects this instance.
		/// </summary>
		public void Disconnect()
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					if (this.Connection.IsConnected)
					{
						this.Connection.Disconnect();
					}
					else
					{
						this._IsConnected = false;
						if (this.OnDisconnect != null) this.OnDisconnect(this);
						base.StopAllServices(this);
					}
				}
				else throw new InvalidOperationException("Client was already disconnected");
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
				if (this._IsConnected)
				{
					packet.Sender = this.Connection.ID;
					this.Connection.Send(packet);
				}
				else throw new InvalidOperationException("Client must be connected");
			}
		}

		#region Service handler

		/// <summary>
		/// Gets the service with the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns>The service with the specified id.</returns>
		public override INetworkClientService GetService(int id)
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
		public override void RegisterService(int id, INetworkClientService service)
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