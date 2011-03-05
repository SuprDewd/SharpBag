using System;
using System.IO;
using System.Linq;
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
        /// <returns></returns>
        public static string Download(this Uri page, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            WebRequest request = HttpWebRequest.Create(page);
            StringBuilder results = new StringBuilder();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        results.AppendLine(line);
                    }

                    sr.Close();
                }

                response.Close();
            }

            return results.ToString();
        }
    }
}