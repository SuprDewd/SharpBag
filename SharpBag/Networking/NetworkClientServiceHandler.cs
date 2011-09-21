using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SharpBag.Networking.Services;

namespace SharpBag.Networking
{
    /// <summary>
    /// A network client service handler.
    /// </summary>
    public abstract class NetworkClientServiceHandler
    {
        /// <summary>
        /// The services.
        /// </summary>
        protected Dictionary<int, INetworkClientService> Services = new Dictionary<int, INetworkClientService>();

        /// <summary>
        /// Registers the service.
        /// </summary>
        /// <param name="id">The id of the service.</param>
        /// <param name="service">The service.</param>
        public virtual void RegisterService(int id, INetworkClientService service)
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
        public virtual INetworkClientService GetService(int id)
        {
            INetworkClientService service;
            if (this.Services.TryGetValue(id, out service))
            {
                return service;
            }

            return null;
        }

        /// <summary>
        /// Delivers the packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        protected void DeliverPacket(NetworkPacket packet)
        {
            INetworkClientService service;
            if (this.Services.TryGetValue(packet.Service, out service))
            {
                service.Receive(packet);
            }
        }

        /// <summary>
        /// Starts all services.
        /// </summary>
        /// <param name="client">The client.</param>
        protected void StartAllServices(NetworkClient client)
        {
            foreach (var service in this.Services)
            {
                service.Value.ID = service.Key;
                service.Value.ClientSetter = client;
                service.Value.Start();
            }
        }

        /// <summary>
        /// Stops all services.
        /// </summary>
        /// <param name="client">The client.</param>
        protected void StopAllServices(NetworkClient client)
        {
            foreach (var service in this.Services)
            {
                service.Value.Stop();
                service.Value.ID = 0;
                service.Value.ClientSetter = null;
            }
        }
    }
}