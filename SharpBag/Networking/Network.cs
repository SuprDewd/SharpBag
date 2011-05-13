using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace SharpBag.Networking
{
	/// <summary>
	/// A static class with utilities for networking.
	/// </summary>
	public static class Network
	{
		/// <summary>
		/// Checks whether the specified port number is valid and not in use.
		/// </summary>
		/// <param name="port">The port.</param>
		/// <returns>Whether the specified port number is valid and not in use.</returns>
		public static bool IsPortFree(int port)
		{
			if (port < 0 || port > 0xFFFF) return false;

			return !IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().Where(i => i.LocalEndPoint.Port == port).Any();
		}

		/// <summary>
		/// Returns the local machines IP addresses.
		/// </summary>
		public static IEnumerable<IPAddress> LocalIPAddresses
		{
			get
			{
				return Dns.GetHostAddresses(Dns.GetHostName());
			}
		}
	}
}