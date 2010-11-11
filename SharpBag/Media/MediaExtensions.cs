using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace SharpBag.Media
{
    /// <summary>
    /// Media extensions.
    /// </summary>
    public static class MediaExtensions
    {
        /// <summary>
        /// Turns the current instance into a BitmapSource.
        /// </summary>
        /// <param name="img">The current instance.</param>
        /// <returns>The BitmapSource.</returns>
        public static BitmapSource ToBitmapSource(this Image img)
        {
            MemoryStream memStream = new MemoryStream();
            img.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);
            PngBitmapDecoder decoder = new PngBitmapDecoder(memStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }
    }
}