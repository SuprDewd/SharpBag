using System;
using System.Net.NetworkInformation;
using System.Linq;

namespace SharpBag.BagNet
{
    /// <summary>
    /// A static class for working with internet communications and such.
    /// </summary>
    public static class BagNet
    {
        /// <summary>
        /// Checks whether the specified port number is valid and not in use.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns>Whether the specified port number is valid and not in use.</returns>
        public static bool IsPortFree(int port)
        {
            if (port < 0 || port > 0xFFFF) return false;

            return (from i in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections() where i.LocalEndPoint.Port == port select true).Count() == 0;
        }
    }
}