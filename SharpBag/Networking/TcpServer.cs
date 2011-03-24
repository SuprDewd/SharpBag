using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SharpBag.Networking
{
    /// <summary>
    /// A class that manages a multithreaded Tcp server.
    /// </summary>
    [Obsolete("Use SharpBag.Network.Server instead.")]
    public class TcpServer : IDisposable
    {
        /// <summary>
        /// Creates a TcpServer.
        /// </summary>
        /// <param name="ports">A collection of ports. The first free port in the collection will be used.</param>
        /// <param name="checkInterval">The interval to check for new connections.</param>
        /// <returns>A new TcpServer instance.</returns>
        public static TcpServer Create(IEnumerable<int> ports = null, int checkInterval = 50)
        {
            if (ports == null) ports = 1.To(0xFFFF);
            int port = ports.First(Network.IsPortFree);
            IPAddress ip = Network.LocalIPAddresses.First(i => i.AddressFamily == AddressFamily.InterNetwork);

            return new TcpServer(new TcpListener(ip, port), checkInterval);
        }

        /// <summary>
        /// The thread to listen for incoming clients.
        /// </summary>
        public Thread Thread { get; private set; }

        /// <summary>
        /// The TcpListener.
        /// </summary>
        public TcpListener Listener { get; private set; }

        /// <summary>
        /// Whether the server is listening for incoming clients.
        /// </summary>
        public bool Listening { get; private set; }

        /// <summary>
        /// The interval, in milliseconds, to check for incoming clients.
        /// </summary>
        public int CheckInterval { get; set; }

        /// <summary>
        /// An event that is fired when a client is received.
        /// </summary>
        public event Action<TcpServer, TcpClientHandler> ClientReceived;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="listener">The TcpListener.</param>
        /// <param name="checkInterval">The interval to check for incoming clients.</param>
        /// <param name="receiveTimeout">The time, in milliseconds, before a timeout occurs when reading data from a client.</param>
        /// <param name="start">Whether to start the server.</param>
        public TcpServer(TcpListener listener, int checkInterval = 50, int receiveTimeout = 5000, bool start = false)
        {
            this.Listener = listener;
            this.Listener.Server.ReceiveTimeout = receiveTimeout;
            this.CheckInterval = checkInterval;

            if (start) this.Start();
        }

        /// <summary>
        /// Opens the TcpListener, starts the listening thread and starts listening for incoming clients.
        /// </summary>
        public bool Start()
        {
            if (this.Listening) return false;
            this.Listener.Start();
            this.Thread = new Thread(Listen);
            this.Thread.Start();
            return true;
        }

        /// <summary>
        /// Stops listening for incoming clients, stops the listening thread and closes the TcpListener.
        /// </summary>
        public void Stop()
        {
            this.Listening = false;
            this.Thread.Abort();
            this.Listener.Stop();
        }

        /// <summary>
        /// Listen for incoming clients.
        /// </summary>
        private void Listen()
        {
            this.Listening = true;

            try
            {
                bool first = true;

                while (true)
                {
                    if (this.Listening && Thread.CurrentThread.ThreadState == ThreadState.Running && !this.Listener.Pending()) { Thread.Sleep(this.CheckInterval); continue; }
                    if (!this.Listening || Thread.CurrentThread.ThreadState != ThreadState.Running) break;

                    try
                    {
                        if (this.ClientReceived != null)
                        {
                            this.ClientReceived(this, new TcpClientHandler(this.Listener.AcceptTcpClient()));
                        }
                        else if (first)
                        {
                            this.ClientReceived.IfNotNull(a => a(this, new TcpClientHandler(this.Listener.AcceptTcpClient())));
                        }
                    }
                    catch { }

                    first = false;
                }
            }
            catch { this.Listening = false; }
        }

        /// <see cref="IDisposable.Dispose()"/>
        public void Dispose()
        {
            this.Stop();
        }
    }
}