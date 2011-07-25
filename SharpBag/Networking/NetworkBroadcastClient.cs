using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpBag.Networking
{
	public class NetworkBroadcastClient
	{
		public event Action<byte[], NetworkBroadcastClient> OnDataReceived;
		public event Action<NetworkBroadcastClient> OnOpen;
		public event Action<NetworkBroadcastClient> OnClose;

		private object ConnectionLock = new object();
		private Socket ListenSocket;
		private volatile bool _IsOpen;

		public bool IsOpen { get { return this._IsOpen; } }

		private class ClientInfo
		{
			public Socket ClientSocket { get; set; }

			public byte[] Data { get; set; }
		}

		public void Open(int remotePort)
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsOpen)
				{
					try
					{
						this._IsOpen = true;
						int localPort = 30000.To(0xFFFF).First(Network.IsPortFree);
						this.ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
						this.ListenSocket.Bind(new IPEndPoint(IPAddress.Any, localPort));
						this.ListenSocket.Listen(100);
						this.BeginAccept();
						this.OnOpen(this);

						Socket broadcast = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
						broadcast.Connect(IPAddress.Broadcast, remotePort);

						foreach (IPAddress address in Network.LocalIPAddresses.Where(i => i.AddressFamily == AddressFamily.InterNetwork))
						{
							try
							{
								broadcast.Send(NetworkBroadcastServer.Serialize(new IPEndPoint(address, localPort)));
							}
							catch { }
						}
					}
					catch (SocketException e)
					{
						this.Error(e.SocketErrorCode);
					}
				}
			}
		}

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
					this.OnClose(this);
				}
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
						this.OnDataReceived(client.Data, this);

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