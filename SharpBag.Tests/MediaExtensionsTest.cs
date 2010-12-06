// <copyright file="MediaExtensionsTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Collections.Generic;

namespace SharpBag.Media
{
    [TestClass]
    [PexClass(typeof(MediaExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MediaExtensionsTest
    {
        [PexMethod]
        public BitmapSource ToBitmapSource(Image img)
        {
            BitmapSource result = MediaExtensions.ToBitmapSource(img);
            return result;
            // TODO: add assertions to method MediaExtensionsTest.ToBitmapSource(Image)
        }
        [PexMethod]
        public IEnumerable<Rectangle> GetSubRectangles(
            Rectangle rect,
            int width,
            int height
        )
        {
            IEnumerable<Rectangle> result = MediaExtensions.GetSubRectangles(rect, width, height);
            return result;
            // TODO: add assertions to method MediaExtensionsTest.GetSubRectangles(Rectangle, Int32, Int32)
        }
        [PexMethod]
        public Rectangle GetRectangle(Image image)
        {
            Rectangle result = MediaExtensions.GetRectangle(image);
            return result;
            // TODO: add assertions to method MediaExtensionsTest.GetRectangle(Image)
        }
        [PexMethod]
        public IEnumerable<Color> GetPixels(Bitmap image, Rectangle rect)
        {
            IEnumerable<Color> result = MediaExtensions.GetPixels(image, rect);
            return result;
            // TODO: add assertions to method MediaExtensionsTest.GetPixels(Bitmap, Rectangle)
        }
        [PexMethod]
        public double GetLuminosity(Bitmap image, Rectangle rect)
        {
            double result = MediaExtensions.GetLuminosity(image, rect);
            return result;
            // TODO: add assertions to method MediaExtensionsTest.GetLuminosity(Bitmap, Rectangle)
        }
        [PexMethod]
        public Rectangle CreateRectangle(
            int x,
            int y,
            int nextX,
            int nextY
        )
        {
            Rectangle result = MediaExtensions.CreateRectangle(x, y, nextX, nextY);
            return result;
            // TODO: add assertions to method MediaExtensionsTest.CreateRectangle(Int32, Int32, Int32, Int32)
        }
        [PexMethod]
        public double Contrast(double d, double contrast)
        {
            double result = MediaExtensions.Contrast(d, contrast);
            return result;
            // TODO: add assertions to method MediaExtensionsTest.Contrast(Double, Double)
        }
    }
}
