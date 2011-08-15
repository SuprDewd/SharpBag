using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Text;

namespace SharpBag.Networking
{
    /// <summary>
    /// Extensions methods for the Network class.
    /// </summary>
    public static class NetworkExtensions
    {
        /// <summary>
        /// Downloads the source of the specified location and returns it as a string.
        /// </summary>
        /// <param name="page">The location.</param>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>The source</returns>
        public static string DownloadHttp(this Uri page, Encoding encoding = null)
        {
            Contract.Requires(page.Scheme == "http" || page.Scheme == "https");
            WebRequest request = HttpWebRequest.Create(page);
            StringBuilder results = new StringBuilder();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding ?? Encoding.UTF8))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        results.AppendLine(line);
                    }
                }
            }

            return results.ToString();
        }
    }
}