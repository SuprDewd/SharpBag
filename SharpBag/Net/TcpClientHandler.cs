using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;

namespace SharpBag.Net
{
    /// <summary>
    /// A class that handles TcpClients.
    /// </summary>
    public class TcpClientHandler : IDisposable
    {
        /// <summary>
        /// The listening thread.
        /// </summary>
        public Thread Thread { get; private set; }
        /// <summary>
        /// The stream.
        /// </summary>
        public NetworkStream BaseStream { get; private set; }
        /// <summary>
        /// The stream reader.
        /// </summary>
        public BinaryReader Reader { get; private set; }
        /// <summary>
        /// The stream writer.
        /// </summary>
        public BinaryWriter Writer { get; private set; }
        /// <summary>
        /// The client.
        /// </summary>
        public TcpClient Client { get; private set; }
        /// <summary>
        /// Whether the handler is listening for messages.
        /// </summary>
        public bool Listening { get; private set; }
        /// <summary>
        /// The interval, in milliseconds, to check for messages.
        /// </summary>
        public int CheckInterval { get; set; }
        /// <summary>
        /// The ping thread.
        /// </summary>
        public Thread PingThread { get; set; }
        private int _PingInterval = -1;
        /// <summary>
        /// The ping interval.
        /// </summary>
        public int PingInterval
        {
            get { return this._PingInterval; }
            set
            {
                this._PingInterval = value;

                if (this._PingInterval > 0 && this.PingThread == null)
                {
                    this.PingThread = new Thread(new ThreadStart(() =>
                    {
                        while (this.Listening && this._PingInterval > 0)
                        {
                            Thread.Sleep(this._PingInterval);

                            this.SendMessage("P");
                        }
                    }));

                    this.PingThread.Start();
                }
                else if (this.PingThread != null)
                {
                    try
                    {
                        this.PingThread.Abort();
                        this.PingThread = null;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// An event that is fired when a message is received.
        /// </summary>
        public event Action<TcpClientHandler, string> MessageReceived;
        /// <summary>
        /// An event that is fired when the TcpClient disconnects.
        /// </summary>
        public event Action<TcpClientHandler> Disconnected;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="client">The TcpClient.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="checkInterval">The interval to check for messages.</param>
        /// <param name="ping">The interval, in milliseconds, to ping the client. If it's a negative integer, no pings are sent.</param>
        public TcpClientHandler(TcpClient client, Encoding encoding = null, int checkInterval = 50, int ping = -1)
        {
            this.CheckInterval = checkInterval;
            this.Client = client;
            this.BaseStream = this.Client.GetStream();
            this.BaseStream.ReadTimeout = 1000;
            this.Reader = new BinaryReader(this.BaseStream, (encoding == null ? Encoding.Default : encoding));
            this.Writer = new BinaryWriter(this.BaseStream, (encoding == null ? Encoding.Default : encoding));
            this.Thread = new Thread(new ThreadStart(Listen));
            this.Thread.Start();

            this.Disconnected += c =>
            {
                try
                {
                    this.PingThread.Abort();
                    this.PingThread = null;
                }
                catch { }
            };

            this.PingInterval = ping;
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="s">The message to send.</param>
        /// <returns>Whether the message was sent.</returns>
        public bool SendMessage(string s)
        {
            lock (this.BaseStream)
            {
                try
                {
                    this.Writer.Write(s);
                    this.Writer.Flush();
                    return true;
                }
                catch (IOException)
                {
                    this.Disconnected.IfNotNull(a => a(this));
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Stops listening for messages, stops the listening thread and closes the TcpClient.
        /// </summary>
        public void Stop()
        {
            this.BaseStream.Close();
            this.Listening = false;
            this.Thread.Abort();
        }

        /// <summary>
        /// Listen to messages.
        /// </summary>
        private void Listen()
        {
            this.Listening = true;

            try
            {
                while (true)
                {
                    if (this.Listening && Thread.CurrentThread.ThreadState == ThreadState.Running && this.Client.Connected && this.Client.Client.Connected && !this.BaseStream.DataAvailable) { Thread.Sleep(this.CheckInterval); continue; }
                    if (!this.Listening || Thread.CurrentThread.ThreadState != ThreadState.Running) break;
                    if (!this.Client.Connected || !this.Client.Client.Connected) { this.Disconnected.IfNotNull(a => a(this)); break; }

                    string msg;

                    lock (this.BaseStream)
                    {
                        msg = this.Reader.ReadString();
                    }

                    if (msg == "P") continue;

                    try
                    {
                        this.MessageReceived.IfNotNull(a => a(this, msg));
                    }
                    catch { }
                }
            }
            catch { this.Listening = false; }
        }

        /// <summary>
        /// Checks whether a is equal to b.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">B.</param>
        /// <returns>Whether a is equal to b.</returns>
        public static bool operator ==(TcpClientHandler a, TcpClientHandler b)
        {
            try
            {
                return a.Client == b.Client;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks whether a is not equal to b.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">B.</param>
        /// <returns>Whether a is not equal to b.</returns>
        public static bool operator !=(TcpClientHandler a, TcpClientHandler b)
        {
            try
            {
                return !(a.Client == b.Client);
            }
            catch
            {
                return false;
            }
        }

        /// <see cref="Object.Equals(object)"/>
        public override bool Equals(object obj)
        {
            try
            {
                if (!(obj is TcpClientHandler)) return false;

                return this.Client.Equals((obj as TcpClientHandler).Client);
            }
            catch
            {
                return false;
            }
        }

        /// <see cref="Object.GetHashCode()"/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <see cref="IDisposable.Dispose()"/>
        public void Dispose()
        {
            this.Stop();
        }
    }
}