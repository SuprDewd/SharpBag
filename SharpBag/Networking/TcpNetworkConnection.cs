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
    /// <summary>
    /// A TCP network connection.
    /// </summary>
    public class TcpNetworkConnection
    {
        /// <summary>
        /// Occurs when the connection disconnects.
        /// </summary>
        public event Action<TcpNetworkConnection> OnDisconnect;

        /// <summary>
        /// Occurs when a packet is received.
        /// </summary>
        public event Action<TcpNetworkConnection, byte[]> OnPacketReceived;

        /// <summary>
        /// Gets the ID of the connection.
        /// </summary>
        public int ID { get; internal set; }

        private volatile bool _IsConnected;

        private volatile bool _IsReceiving;

        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// Whether this instance is connected.
        /// </value>
        public bool IsConnected { get { return this._IsConnected; } }

        /// <summary>
        /// Gets the local end point of the connection.
        /// </summary>
        public EndPoint LocalEndPoint { get { return this.Socket == null ? null : this.Socket.LocalEndPoint; } }

        /// <summary>
        /// Gets the remote end point of the connection.
        /// </summary>
        public EndPoint RemoteEndPoint { get { return this.Socket == null ? null : this.Socket.RemoteEndPoint; } }

        private Socket Socket;

        private object ConnectionLock;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpNetworkConnection"/> class.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="connectionLock">The connection lock.</param>
        internal TcpNetworkConnection(Socket socket, ref object connectionLock) : this(0, socket, ref connectionLock) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpNetworkConnection"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="socket">The socket.</param>
        /// <param name="connectionLock">The connection lock.</param>
        internal TcpNetworkConnection(int id, Socket socket, ref object connectionLock)
        {
            Contract.Requires(socket.Connected);
            this.ID = id;
            this.Socket = socket;
            this._IsConnected = true;
            this.ConnectionLock = connectionLock;
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        public void Connect()
        {
            lock (this.ConnectionLock)
            {
                if (!this._IsReceiving)
                {
                    this._IsReceiving = true;
                    this.BeginReceive();
                }
                else throw new InvalidOperationException("Connection is already connected");
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
                    try
                    {
                        this.Socket.Shutdown(SocketShutdown.Both);
                        this.Socket.Close();
                    }
                    catch { }

                    this._IsConnected = false;
                    this.OnDisconnect(this);
                }
                else throw new InvalidOperationException("Connection is already disconnected");
            }
        }

        /// <summary>
        /// Sends the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public void Send(byte[] packet)
        {
            lock (this.ConnectionLock)
            {
                if (this._IsConnected)
                {
                    try
                    {
                        byte[] bytes = new byte[packet.Length + 4];
                        Buffer.BlockCopy(BitConverter.GetBytes(packet.Length), 0, bytes, 0, 4);
                        Buffer.BlockCopy(packet, 0, bytes, 4, packet.Length);

                        SocketError e;
                        this.Socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, out e, this.EndSend, null);
                        if (e != SocketError.Success) this.Error(e);
                    }
                    catch (SocketException e)
                    {
                        this.Error(e.SocketErrorCode);
                    }
                }
                else throw new InvalidOperationException("Connection is not connected");
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
                                if (this.OnPacketReceived != null) this.OnPacketReceived(this, bytes);
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