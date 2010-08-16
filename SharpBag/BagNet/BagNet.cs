using System;

namespace SharpBag.BagNet
{
    /// <summary>
    /// A static class for working with internet communications and such.
    /// </summary>
    public static class BagNet
    {
        /// <summary>
        /// Gets a response object from the specified page. The object can the be used to read data from the page.
        /// </summary>
        /// <param name="page">The page to get response from.</param>
        /// <returns>The response object of from the page.</returns>
        public static string DownloadPage(Uri page)
        {
            return page.Download();
        }
    }
}
