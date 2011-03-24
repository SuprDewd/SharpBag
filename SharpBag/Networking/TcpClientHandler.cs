using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SharpBag.Networking
{
    /// <summary>
    /// A class that handles TcpClients.
    /// </summary>
    [Obsolete("Use SharpBag.Network.Client instead.")]
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
        /// The encoding to use when reading from the client.
        /// </summary>
        public Encoding Encoding { get; private set; }

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

                if (this._PingInterval >= 0 && this.PingThread == null)
                {
                    this.PingThread = new Thread(() =>
                                                     {
                                                         while (this.Listening && this._PingInterval >= 0)
                                                         {
                                                             Thread.Sleep(this._PingInterval);

                                                             this.SendMessage("P");
                                                         }
                                                     });

                    this.PingThread.Start();
                }
                else if (this._PingInterval < 0 && this.PingThread != null)
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
        /// <param name="receiveTimeout">The time, in milliseconds, before a timeout occurs when reading data from the server.</param>
        /// <param name="start">Whether to start the handler.</param>
        public TcpClientHandler(TcpClient client, Encoding encoding = null, int checkInterval = 50, int receiveTimeout = 5000, bool start = false)
        {
            this.CheckInterval = checkInterval;
            this.Client = client;
            this.Client.ReceiveTimeout = receiveTimeout;
            this.Encoding = encoding ?? Encoding.Default;

            this.Disconnected += c =>
            {
                try
                {
                    this.PingThread.Abort();
                    this.PingThread = null;
                }
                catch { }
            };

            if (start) this.Start();
        }

        /// <summary>
        /// Opens the TcpListener, starts the listening thread and starts listening for messages.
        /// </summary>
        /// <param name="ping">The interval, in milliseconds, to ping the client. If it's a negative integer, no pings are sent.</param>
        public bool Start(int ping = -1)
        {
            if (this.Listening) return false;
            this.BaseStream = this.Client.GetStream();
            this.BaseStream.ReadTimeout = 1000;
            this.Reader = new BinaryReader(this.BaseStream, this.Encoding);
            this.Writer = new BinaryWriter(this.BaseStream, this.Encoding);
            this.Thread = new Thread(Listen);
            this.Thread.Start();
            this.PingInterval = ping;
            return true;
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
            try
            {
                this.BaseStream.Close();
                this.Listening = false;
                this.Thread.Abort();
            }
            catch { }
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
                        this.MessageReceived(this, msg);
                    }
                    catch { }
                }
            }
            catch { }

            this.Listening = false;
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
                bool aNull = a == null;
                bool bNull = b == null;

                if (aNull && bNull) return true;
                if (aNull || bNull) return false;

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
            return !(a == b);
        }

        /// <summary>
        /// Object.Equals(object)
        /// </summary>
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