using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using SharpBag.Math;

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

        /// <summary>
        /// Gets an enumerable containing all pixels that are in the specified rectangle on the current instance.
        /// </summary>
        /// <param name="image">The current instance.</param>
        /// <param name="rect">The rectangle in the current instance.</param>
        /// <returns>An enumerable containing all pixels that are in the specified rectangle on the current instance.</returns>
        public static IEnumerable<Color> GetPixels(this Bitmap image, Rectangle rect)
        {
            return from y in rect.Top.To(rect.Bottom)
                   from x in rect.Left.To(rect.Right)
                   select image.GetPixel(x, y);
        }

        /// <summary>
        /// Gets the luminosity of the specified rectangle in the current instance.
        /// </summary>
        /// <param name="image">The current instance.</param>
        /// <param name="rect">The rectangle in the current instance.</param>
        /// <returns>The luminosity of the specified rectangle in the current instance.</returns>
        public static double GetLuminosity(this Bitmap image, Rectangle rect)
        {
            return image.GetPixels(rect).Average(c => .3 * c.R + .59 * c.G + .11 * c.B) / 255;
        }

        /// <summary>
        /// Gets a new rectangle that has the same with and height as the current instance.
        /// </summary>
        /// <param name="image">The current instance.</param>
        /// <returns>A new rectangle that has the same with and height as the current instance.</returns>
        public static Rectangle GetRectangle(this Image image)
        {
            return new Rectangle(0, 0, image.Width, image.Height);
        }

        /// <summary>
        /// Gets the contrast of the current instance.
        /// </summary>
        /// <param name="d">The current instance.</param>
        /// <param name="contrast">The contrast.</param>
        /// <returns>The contrast.</returns>
        public static double Contrast(this double d, double contrast)
        {
            return (((d - .5) * contrast) + .5).Bound(0, 1);
        }

        /// <summary>
        /// Gets a subset of rectangles all with the specified width and height from the current instance.
        /// </summary>
        /// <param name="rect">The current instance.</param>
        /// <param name="width">The width of all sub-rectangles.</param>
        /// <param name="height">The height of all sub-rectangles.</param>
        /// <returns>An enumerable with the sub-rectangles.</returns>
        public static IEnumerable<Rectangle> GetSubRectangles(this Rectangle rect, int width, int height)
        {
            var xSize = rect.Width / (double)width;
            var ySize = rect.Height / (double)height;
            return from y in 0.To(height)
                   from x in 0.To(width)
                   let r = CreateRectangle((int)(x * xSize), (int)(y * ySize), (int)((x + 1) * xSize), (int)((y + 1) * ySize))
                   where r.Height > 0 && r.Width > 0
                   select r;
        }

        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="x">The upper left x coordinate.</param>
        /// <param name="y">The upper left y coordinate.</param>
        /// <param name="nextX">The lower right x coordinate.</param>
        /// <param name="nextY">The lower right y coordinate.</param>
        /// <returns>A rectangle.</returns>
        public static Rectangle CreateRectangle(int x, int y, int nextX, int nextY)
        {
            return new Rectangle(x, y, nextX - x, nextY - y);
        }
    }
}