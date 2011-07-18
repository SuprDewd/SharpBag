using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Networking
{
	public abstract class NetworkServerServiceHandler
	{
		protected Dictionary<int, INetworkServerService> Services = new Dictionary<int, INetworkServerService>();

		public virtual void RegisterService(int id, INetworkServerService service)
		{
			Contract.Requires(id >= 0);
			this.Services.Add(id, service);
		}

		public virtual void UnregisterService(int id)
		{
			this.Services.Remove(id);
		}

		public virtual INetworkServerService GetService(int id)
		{
			INetworkServerService service;
			if (this.Services.TryGetValue(id, out service))
			{
				return service;
			}

			return null;
		}

		protected void OpenAllServices(NetworkServer server)
		{
			foreach (var service in this.Services)
			{
				service.Value.Open(service.Key, server);
			}
		}

		protected void CloseAllServices(NetworkServer server)
		{
			foreach (var service in this.Services)
			{
				service.Value.Close(service.Key, server);
			}
		}

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