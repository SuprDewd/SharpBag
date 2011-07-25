using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpBag.Networking
{
	public class NetworkBroadcastServer
	{
		public event Action<NetworkBroadcastServer> OnOpen;
		public event Action<NetworkBroadcastServer> OnClose;

		private Socket ListenSocket;
		private volatile bool _IsOpen;
		private object ConnectionLock = new object();

		public byte[] Data { get; set; }

		public bool IsOpen { get { return _IsOpen; } }

		public NetworkBroadcastServer(byte[] data)
		{
			this.Data = data;
		}

		public void Open(int port)
		{
			lock (this.ConnectionLock)
			{
				if (!this._IsOpen)
				{
					try
					{
						this._IsOpen = true;
						this.ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
						this.ListenSocket.Bind(new IPEndPoint(IPAddress.Any, port));
						this.BeginReceive();
						this.OnOpen(this);
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
				if (this._IsOpen)
				{
					SocketError e;
					byte[] buffer = new byte[6];
					this.ListenSocket.BeginReceive(buffer, 0, 6, SocketFlags.None, out e, this.EndReceive, buffer);

					if (e != SocketError.Success) this.Error(e);
				}
			}
		}

		private void EndReceive(IAsyncResult result)
		{
			lock (this.ConnectionLock)
			{
				if (this.IsOpen)
				{
					SocketError e;
					this.ListenSocket.EndReceive(result, out e);
					if (e != SocketError.Success)
					{
						this.Error(e);
						return;
					}
				}
				else return;
			}

			try
			{
				this.SendData(Deserialize((byte[])result.AsyncState));
			}
			catch { }

			this.BeginReceive();
		}

		private void SendData(EndPoint client)
		{
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(client);
			socket.Send(BitConverter.GetBytes(this.Data.Length), 0, 4, SocketFlags.None);
			socket.BeginSend(this.Data, 0, this.Data.Length, SocketFlags.None, null, null);
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

		private void Error(SocketError e)
		{
			lock (this.ConnectionLock)
			{
				this.Close();
			}
		}

		internal static byte[] Serialize(IPEndPoint endPoint)
		{
			byte[] buffer = new byte[6];
			Buffer.BlockCopy(endPoint.Address.GetAddressBytes(), 0, buffer, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes((UInt16)endPoint.Port), 0, buffer, 4, 2);
			return buffer;
		}

		internal static IPEndPoint Deserialize(byte[] buffer)
		{
			byte[] ip = new byte[4];
			Buffer.BlockCopy(buffer, 0, ip, 0, 4);
			return new IPEndPoint(new IPAddress(ip), BitConverter.ToUInt16(buffer, 4));
		}
	}
}