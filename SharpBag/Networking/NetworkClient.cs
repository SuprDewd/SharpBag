using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpBag.Networking
{
	public class NetworkClient : NetworkClientServiceHandler
	{
		public event Action<NetworkClient> OnConnect;

		public event Action<NetworkClient> OnDisconnect;

		internal object ConnectionLock = new object();

		private TcpNetworkConnection Connection;

		private volatile bool _IsConnected;

		public bool IsConnected { get { return this._IsConnected; } }

		public int ID { get { return this.Connection.ID; } }

		public EndPoint LocalEndPoint { get { return this.Connection.LocalEndPoint; } }

		public EndPoint RemoteEndPoint { get { return this.Connection.RemoteEndPoint; } }

		public NetworkClient() { }

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
			}
		}

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
			}
		}

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
				if (id == 0) throw new Exception("Server denied connection.");
				else
				{
					this.Connection = new TcpNetworkConnection(id, socket, ref this.ConnectionLock);
					this.Connection.OnDisconnect += new Action<TcpNetworkConnection>(ConnectionDisconnected);
					this.Connection.OnPacketReceived += new Action<TcpNetworkConnection, NetworkPacket>(PacketReceived);
					this.Connection.Connect();
					this._IsConnected = true;
					if (this.OnConnect != null) this.OnConnect(this);
					base.OpenAllServices(this);
				}
			}
			else
			{
				throw new Exception("Could not receive connection ID.");
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
						base.CloseAllServices(this);
					}
				}
			}
		}

		public void Send(NetworkPacket packet)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					packet.Sender = this.Connection.ID;
					this.Connection.Send(packet);
				}
			}
		}

		#region Service handler

		public override INetworkClientService GetService(int id)
		{
			lock (this.ConnectionLock)
			{
				return base.GetService(id);
			}
		}

		public override void RegisterService(int id, INetworkClientService service)
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