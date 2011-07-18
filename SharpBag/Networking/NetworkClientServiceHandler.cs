using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SharpBag.Networking
{
	public abstract class NetworkClientServiceHandler
	{
		protected Dictionary<int, INetworkClientService> Services = new Dictionary<int, INetworkClientService>();

		public virtual void RegisterService(int id, INetworkClientService service)
		{
			Contract.Requires(id >= 0);
			this.Services.Add(id, service);
		}

		public virtual void UnregisterService(int id)
		{
			this.Services.Remove(id);
		}

		public virtual INetworkClientService GetService(int id)
		{
			INetworkClientService service;
			if (this.Services.TryGetValue(id, out service))
			{
				return service;
			}

			return null;
		}

		protected void DeliverPacket(NetworkPacket packet)
		{
			INetworkClientService service;
			if (this.Services.TryGetValue(packet.Service, out service))
			{
				service.Receive(packet);
			}
		}

		protected void OpenAllServices(NetworkClient client)
		{
			foreach (var service in this.Services)
			{
				service.Value.Open(service.Key, client);
			}
		}

		protected void CloseAllServices(NetworkClient client)
		{
			foreach (var service in this.Services)
			{
				service.Value.Close(service.Key, client);
			}
		}
	}
}