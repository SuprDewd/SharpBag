using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace SharpBag.Net
{
    /// <summary>
    /// A class that handles TcpClients.
    /// </summary>
    public class TcpClientHandler
    {
        /// <summary>
        /// The listening thread.
        /// </summary>
        public Thread Thread { get; private set; }
        /// <summary>
        /// The stream.
        /// </summary>
        public NetworkStream Stream { get; private set; }
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
        /// The buffer size.
        /// </summary>
        /// <remarks>Used when receiving messages.</remarks>
        public int BufferSize { get; set; }
        /// <summary>
        /// The encoding to use when receiving messages.
        /// </summary>
        public Encoding Encoding { get; set; }

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
        /// <param name="bufferSize">The size of the buffer.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="checkInterval">The interval to check for messages.</param>
        public TcpClientHandler(TcpClient client, int bufferSize = 2048, Encoding encoding = null, int checkInterval = 50)
        {
            this.BufferSize = bufferSize;
            this.Encoding = encoding == null ? Encoding.Default : encoding;
            this.CheckInterval = checkInterval;
            this.Client = client;
            this.Stream = this.Client.GetStream();
            this.Thread = new Thread(new ThreadStart(Listen));
            this.Thread.Start();
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="s">The message to send.</param>
        public void SendMessage(string s)
        {
            lock (this.Stream)
            {
                byte[] bs = this.Encoding.GetBytes(s);
                this.Stream.Write(bs, 0, bs.Length);
                this.Stream.Flush();
            }
        }

        /// <summary>
        /// Stops listening for messages, stops the listening thread and closes the TcpClient.
        /// </summary>
        public void Stop()
        {
            this.Listening = false;
            this.Thread.Abort();
            this.Stream.Close();
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
                    if (this.Listening && Thread.CurrentThread.ThreadState == ThreadState.Running && this.Client.Connected && this.Client.Client.Connected && !this.Stream.DataAvailable) { Thread.Sleep(this.CheckInterval); continue; }
                    if (!this.Listening || Thread.CurrentThread.ThreadState != ThreadState.Running) break;
                    if (!this.Client.Connected || !this.Client.Client.Connected) { this.Disconnected.IfNotNull(a => a(this)); break; }

                    int bSize = this.BufferSize;
                    Encoding enc = this.Encoding;
                    StringBuilder sb = new StringBuilder();

                    byte[] bs = new byte[bSize];
                    int read;

                    lock (this.Stream)
                    {
                        while((read = this.Stream.Read(bs, 0, bSize)) > 0)
                        {
                            sb.Append(enc.GetString(bs, 0, read));

                            if (!this.Stream.DataAvailable) break;
                        }
                    }

                    this.MessageReceived.IfNotNull(a => a(this, sb.ToString()));
                }
            }
            catch { this.Listening = false; }
        }
    }
}