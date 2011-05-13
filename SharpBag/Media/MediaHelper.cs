using System.Drawing;

#if DOTNET4

using System.Diagnostics.Contracts;
using System.IO;
using System.Windows.Media.Imaging;

#endif

namespace SharpBag.Media
{
	/// <summary>
	/// Media helpers.
	/// </summary>
	public static class MediaHelper
	{
		/// <summary>
		/// Creates a rectangle.
		/// </summary>
		/// <param name="x">The upper left x coordinate.</param>
		/// <param name="y">The upper left y coordinate.</param>
		/// <param name="nextX">The lower right x coordinate.</param>
		/// <param name="nextY">The lower right y coordinate.</param>
		/// <returns>A rectangle.</returns>
		/// <remarks>Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx</remarks>
		public static Rectangle CreateRectangle(int x, int y, int nextX, int nextY)
		{
#if DOTNET4
			Contract.Requires(nextX > x);
			Contract.Requires(nextY > y);
#endif
			return new Rectangle(x, y, nextX - x, nextY - y);
		}

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