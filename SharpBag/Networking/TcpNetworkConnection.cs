using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpBag.Networking
{
	public class TcpNetworkConnection
	{
		public event Action<TcpNetworkConnection> OnDisconnect;

		public event Action<TcpNetworkConnection, NetworkPacket> OnPacketReceived;

		public int ID { get; internal set; }

		private volatile bool _IsConnected;

		private volatile bool _IsReceiving;

		public bool IsConnected { get { return this._IsConnected; } }

		public EndPoint LocalEndPoint { get { return this.Socket == null ? null : this.Socket.LocalEndPoint; } }

		public EndPoint RemoteEndPoint { get { return this.Socket == null ? null : this.Socket.RemoteEndPoint; } }

		private Socket Socket;

		private object ConnectionLock;

		internal TcpNetworkConnection(Socket socket, ref object connectionLock) : this(0, socket, ref connectionLock) { }

		internal TcpNetworkConnection(int id, Socket socket, ref object connectionLock)
		{
			Contract.Requires(socket.Connected);
			this.ID = id;
			this.Socket = socket;
			this._IsConnected = true;
			this.ConnectionLock = connectionLock;
		}

		public void Connect()
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsReceiving)
				{
					this._IsReceiving = true;
					this.BeginReceive();
				}
			}
		}

		public void Disconnect()
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					try
					{
						this.Socket.Shutdown(SocketShutdown.Both);
						this.Socket.Close();
					}
					catch { }

					this._IsConnected = false;
					this.OnDisconnect(this);
				}
			}
		}

		public void Send(NetworkPacket packet)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					try
					{
						byte[] bytes = packet.SerializeWithLength();
						SocketError e;
						this.Socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, out e, this.EndSend, null);
						if (e != SocketError.Success) this.Error(e);
					}
					catch (SocketException e)
					{
						this.Error(e.SocketErrorCode);
					}
				}
			}
		}

		private void EndSend(IAsyncResult result)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					try
					{
						SocketError e;
						this.Socket.EndSend(result, out e);
						if (e != SocketError.Success) this.Error(e);
					}
					catch (SocketException e)
					{
						this.Error(e.SocketErrorCode);
					}
				}
			}
		}

		private void BeginReceive()
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					try
					{
						byte[] rawLength = new byte[4];
						SocketError e;
						this.Socket.BeginReceive(rawLength, 0, 4, SocketFlags.None, out e, this.EndReceive, rawLength);
						if (e != SocketError.Success) this.Error(e);
					}
					catch (SocketException e)
					{
						this.Error(e.SocketErrorCode);
					}
				}
			}
		}

		private void EndReceive(IAsyncResult result)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					try
					{
						SocketError e;
						this.Socket.EndReceive(result, out e);
						if (e != SocketError.Success) this.Error(e);
						else this.BeginReceivePacket(BitConverter.ToInt32((byte[])result.AsyncState, 0));
					}
					catch (SocketException e)
					{
						this.Error(e.SocketErrorCode);
					}
				}
			}
		}

		private void BeginReceivePacket(int length)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					try
					{
						SocketError e;
						byte[] buffer = new byte[length];
						this.Socket.BeginReceive(buffer, 0, length, SocketFlags.None, out e, this.EndReceivePacket, buffer);
						if (e != SocketError.Success) this.Error(e);
					}
					catch (SocketException e)
					{
						this.Error(e.SocketErrorCode);
					}
				}
			}
		}

		private void EndReceivePacket(IAsyncResult result)
		{
			lock (this.ConnectionLock)
			{
				if (this._IsConnected)
				{
					try
					{
						SocketError e;
						this.Socket.EndReceive(result, out e);
						if (e != SocketError.Success) this.Error(e);
						else
						{
							byte[] bytes = (byte[])result.AsyncState;
							if (bytes != null && bytes.Length > 0)
							{
								if (this.OnPacketReceived != null) this.OnPacketReceived(this, NetworkPacket.Deserialize(bytes, 0));
								this.BeginReceive();
							}
							else this.Disconnect();
						}
					}
					catch (SocketException e)
					{
						this.Error(e.SocketErrorCode);
					}
				}
			}
		}

		private void Error(SocketError e)
		{
			lock (this.ConnectionLock)
			{
				this.Disconnect();
			}
		}
	}
}