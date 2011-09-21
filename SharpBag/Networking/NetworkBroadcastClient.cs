using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpBag.Networking
{
	/// <summary>
	/// A broadcast client.
	/// Useful for server discovery.
	/// </summary>
	public class NetworkBroadcastClient
	{
		/// <summary>
		/// Occurs when data is received.
		/// </summary>
		public event Action<byte[], NetworkBroadcastClient> OnDataReceived;

		/// <summary>
		/// Occurs when the client is opened.
		/// </summary>
		public event Action<NetworkBroadcastClient> OnOpen;

		/// <summary>
		/// Occurs when the client is closed.
		/// </summary>
		public event Action<NetworkBroadcastClient> OnClose;

		private object ConnectionLock = new object();
		private Socket ListenSocket;
		private volatile int LocalPort;
		private volatile bool _IsOpen;

		/// <summary>
		/// Gets a value indicating whether this instance is open.
		/// </summary>
		/// <value>
		/// Whether this instance is open.
		/// </value>
		public bool IsOpen { get { return this._IsOpen; } }

		private class ClientInfo
		{
			public Socket ClientSocket { get; set; }

			public byte[] Data { get; set; }
		}

		/// <summary>
		/// Opens this instance.
		/// </summary>
		public void Open()
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsOpen)
				{
					try
					{
						this._IsOpen = true;
						this.LocalPort = 30000.To(0xFFFF).First(Network.IsPortFree);
						this.ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
						this.ListenSocket.Bind(new IPEndPoint(IPAddress.Any, this.LocalPort));
						this.ListenSocket.Listen(100);
						this.BeginAccept();
						if (this.OnOpen != null) this.OnOpen(this);
					}
					catch (SocketException e)
					{
						this.Error(e.SocketErrorCode);
					}
				}
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
					this._IsOpen = false;

					try
					{
						this.ListenSocket.Close();
					}
					catch { }

					this.ListenSocket = null;
					if (this.OnClose != null) this.OnClose(this);
				}
			}
		}

		/// <summary>
		/// Broadcasts to the specified remote port.
		/// </summary>
		/// <param name="remotePort">The remote port.</param>
		public void Broadcast(int remotePort)
		{
			this.Broadcast(IPAddress.Broadcast, remotePort);
		}

		/// <summary>
		/// Broadcasts to the specified port on the specified address.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="remotePort">The remote port.</param>
		public void Broadcast(IPAddress address, int remotePort)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsOpen)
				{
					Socket broadcast = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					broadcast.Connect(IPAddress.Broadcast, remotePort);

					foreach (IPAddress myAddress in Network.LocalIPAddresses.Where(i => i.AddressFamily == AddressFamily.InterNetwork))
					{
						try
						{
							broadcast.Send(NetworkBroadcastServer.Serialize(new IPEndPoint(myAddress, this.LocalPort)));
						}
						catch { }
					}
				}
				else throw new InvalidOperationException("Broadcast client must be open before broadcasting");
			}
		}

		private void BeginAccept()
		{
			lock (this.ConnectionLock)
			{
				if (this._IsOpen)
				{
					this.ListenSocket.BeginAccept(this.EndAccept, null);
				}
			}
		}

		private void EndAccept(IAsyncResult result)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsOpen)
				{
					try
					{
						Socket client = this.ListenSocket.EndAccept(result);
						this.BeginReceive(client);
					}
					catch { }

					this.BeginAccept();
				}
			}
		}

		private void BeginReceive(Socket client)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsOpen)
				{
					byte[] buffer = new byte[4];
					client.BeginReceive(buffer, 0, 4, SocketFlags.None, this.EndReceive, new ClientInfo { Data = buffer, ClientSocket = client });
				}
			}
		}

		private void EndReceive(IAsyncResult result)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsOpen)
				{
					SocketError e;
					ClientInfo client = (ClientInfo)result.AsyncState;
					client.ClientSocket.EndReceive(result, out e);

					if (e == SocketError.Success)
					{
						this.BeginReceiveData(client, BitConverter.ToInt32(client.Data, 0));
					}
					else
					{
						this.Error(e);
					}
				}
			}
		}

		private void BeginReceiveData(ClientInfo client, int length)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsOpen)
				{
					client.Data = new byte[length];
					client.ClientSocket.BeginReceive(client.Data, 0, length, SocketFlags.None, this.EndReceiveData, client);
				}
			}
		}

		private void EndReceiveData(IAsyncResult result)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsOpen)
				{
					SocketError e;
					ClientInfo client = (ClientInfo)result.AsyncState;
					client.ClientSocket.EndReceive(result, out e);

					if (e == SocketError.Success)
					{
						if (this.OnDataReceived != null) this.OnDataReceived(client.Data, this);

						try
						{
							client.ClientSocket.Shutdown(SocketShutdown.Both);
							client.ClientSocket.Close();
						}
						catch { }
					}
				}
			}
		}

		private void Error(SocketError e)
		{
			lock (this.ConnectionLock)
			{
				this.Close();
			}
		}
	}
}