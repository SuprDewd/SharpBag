using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SharpBag.Networking.Services;

namespace SharpBag.Networking
{
    /// <summary>
    /// A network server service handler.
    /// </summary>
    public abstract class NetworkServerServiceHandler
    {
        /// <summary>
        /// The services.
        /// </summary>
        protected Dictionary<int, INetworkServerService> Services = new Dictionary<int, INetworkServerService>();

        /// <summary>
        /// Registers the service.
        /// </summary>
        /// <param name="id">The id of the service.</param>
        /// <param name="service">The service.</param>
        public virtual void RegisterService(int id, INetworkServerService service)
        {
            Contract.Requires(id >= 0);
            this.Services.Add(id, service);
        }

        /// <summary>
        /// Unregisters the service with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public virtual void UnregisterService(int id)
        {
            this.Services.Remove(id);
        }

        /// <summary>
        /// Gets the service with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The service with the specified id.</returns>
        public virtual INetworkServerService GetService(int id)
        {
            INetworkServerService service;
            if (this.Services.TryGetValue(id, out service))
            {
                return service;
            }

            return null;
        }

        /// <summary>
        /// Starts all services.
        /// </summary>
        /// <param name="server">The server.</param>
        protected void StartAllServices(NetworkServer server)
        {
            foreach (var service in this.Services)
            {
                service.Value.ID = service.Key;
                service.Value.ServerSetter = server;
                service.Value.Start();
            }
        }

        /// <summary>
        /// Stops all services.
        /// </summary>
        /// <param name="server">The server.</param>
        protected void StopAllServices(NetworkServer server)
        {
            foreach (var service in this.Services)
            {
                service.Value.Stop();
                service.Value.ID = 0;
                service.Value.ServerSetter = null;
            }
        }

        /// <summary>
        /// Delivers the packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        protected void DeliverPacket(NetworkPacket packet)
        {
            INetworkServerService service;
            if (this.Services.TryGetValue(packet.Service, out service))
            {
                service.Receive(packet);
            }
        }
    }
}