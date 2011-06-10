using System.Drawing;

#if DOTNET4

using System.Diagnostics.Contracts;
using System.IO;
using System.Windows.Media.Imaging;

#endif

namespace SharpBag.Media
{
	/// <summary>
	/// Media extensions.
	/// </summary>
	public static class MediaExtensions
	{
#if DOTNET4

		/// <summary>
		/// Turns the current instance into a BitmapSource.
		/// </summary>
		/// <param name="img">The current instance.</param>
		/// <returns>The BitmapSource.</returns>
		public static BitmapSource ToBitmapSource(this Image img)
		{
			Contract.Requires(img != null);
			MemoryStream memStream = new MemoryStream();
			img.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);
			PngBitmapDecoder decoder = new PngBitmapDecoder(memStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
			return decoder.Frames[0];
		}

#endif
	}
}