using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBag.Math.ForDouble;

namespace SharpBag.Math
{
	/// <summary>
	/// Methods for interpolation.
	/// </summary>
	public static class Interpolation
	{
		/// <summary>
		/// Convert an array of values to an array of points.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <param name="x0">The first x.</param>
		/// <param name="xDelta">The x delta.</param>
		/// <returns>The array of points.</returns>
		public static Point[] ConvertToPoints(double[] points, double x0, double xDelta)
		{
			Point[] pointArray = new Point[points.Length];

			for (int i = 0; i < points.Length; i++)
			{
				pointArray[i] = new Point(x0, points[i]);
				x0 += xDelta;
			}

			return pointArray;
		}

		/// <summary>
		/// A cubic spline interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<double, double> Cubic(Point[] points)
		{
			if (points.Length < 2) return null;

			int n = points.Length - 1;
			double[] b = new double[n],
					 d = new double[n],
					 h = new double[n],
					 alpha = new double[n],
					 c = new double[n + 1],
					 l = new double[n + 1],
					 mu = new double[n + 1],
					 z = new double[n + 1];

			for (int i = 0; i < n; i++)
			{
				h[i] = points[i + 1].X - points[i].X;
			}

			for (int i = 1; i < n; i++)
			{
				alpha[i] = (3 / h[i]) * (points[i + 1].Y - points[i].Y) - (3 / h[i - 1]) * (points[i].Y - points[i - 1].Y);
			}

			l[0] = mu[0] = z[0] = 0;

			for (int i = 1; i < n; i++)
			{
				l[i] = 2 * (points[i + 1].X - points[i - 1].X) - h[i - 1] * mu[i - 1];
				mu[i] = h[i] / l[i];
				z[i] = (alpha[i] - h[i - 1] * z[i - 1]) / l[i];
			}

			l[n] = 1;
			z[n] = c[n] = 0;

			for (int j = n - 1; j >= 0; j--)
			{
				c[j] = z[j] - mu[j] * c[j + 1];
				b[j] = (points[j + 1].Y - points[j].Y) / h[j] - (h[j] * (c[j + 1] + 2 * c[j])) / 3;
				d[j] = (c[j + 1] - c[j]) / (3 * h[j]);
			}

			Polynomial[] splines = new Polynomial[n];

			for (int i = 0; i < n; i++)
			{
				splines[i] = new Polynomial(points[i].Y, b[i], c[i], d[i]);
			}

			return x =>
			{
				Polynomial last = splines[0];
				int i;
				for (i = 1; i < splines.Length; i++)
				{
					if (points[i].X > x) break;
					last = splines[i];
				}

				return last.Evaluate(x - points[i - 1].X);
			};
		}

		/// <summary>
		/// A linear spline interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<double, double> Linear(Point[] points)
		{
			if (points.Length < 2) return null;
			Polynomial[] splines = new Polynomial[points.Length - 1];

			for (int i = 1; i < points.Length; i++)
			{
				double x0 = points[i - 1].X,
					   y0 = points[i - 1].Y,
					   x1 = points[i].X,
					   y1 = points[i].Y;

				splines[i - 1] = new Polynomial(y0, (y1 - y0) / (x1 - x0));
			}

			return x =>
			{
				Polynomial last = splines[0];
				int i;

				for (i = 1; i < splines.Length; i++)
				{
					if (points[i].X > x) break;
					last = splines[i];
				}

				return last.Evaluate(x - points[i - 1].X);
			};
		}

		/// <summary>
		/// A bilinear spline interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <param name="x0">The first X.</param>
		/// <param name="y0">The first Y.</param>
		/// <param name="xDelta">The delta X.</param>
		/// <param name="yDelta">The delta Y.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<double, double, double> Bilinear(double[,] points, double x0, double y0, double xDelta, double yDelta)
		{
			if (points.GetLength(0) < 2 || points.GetLength(1) < 2) return null;

			return (x, y) =>
			{
				double x1 = x0,
					   y1 = y0;

				int i = 0, j = 0;

				for (i = 0; (x1 + xDelta) < x && i < points.GetLength(0) - 1; i++)
				{
					x1 += xDelta;
				}

				for (j = 0; (y1 + yDelta) < y && j < points.GetLength(1) - 1; j++)
				{
					y1 += yDelta;
				}

				double d = xDelta * yDelta,
					   x2 = x1 + xDelta,
					   y2 = y1 + yDelta,
					   n1 = x2 - x,
					   n2 = y2 - y,
					   n3 = x - x1,
					   n4 = y - y1;

				return points[i, j] / d * n1 * n2 +
					   points[i + 1, j] / d * n3 * n2 +
					   points[i, j + 1] / d * n1 * n4 +
					   points[i + 1, j + 1] / d * n3 * n4;
			};
		}

		/// <summary>
		/// A bicubic spline interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <param name="x0">The first X.</param>
		/// <param name="y0">The first Y.</param>
		/// <param name="xDelta">The delta X.</param>
		/// <param name="yDelta">The delta Y.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<double, Func<double, double>> Bicubic(double[,] points, double x0, double y0, double xDelta, double yDelta)
		{
			if (points.GetLength(0) < 2 || points.GetLength(1) < 2) return null;

			double[][] pointsAlt = new double[points.GetLength(1)][];
			for (int x = 0; x < points.GetLength(1); x++)
			{
				pointsAlt[x] = new double[points.GetLength(0)];
				for (int y = 0; y < points.GetLength(0); y++)
				{
					pointsAlt[x][y] = points[y, x];
				}
			}

			Func<double, double>[] splines = new Func<double, double>[points.GetLength(1)];
			for (int x = 0; x < pointsAlt.Length; x++)
			{
				splines[x] = Interpolation.Cubic(Interpolation.ConvertToPoints(pointsAlt[x], x0, xDelta));
			}

			return x =>
			{
				double[] interpolated = new double[splines.Length];
				for (int i = 0; i < splines.Length; i++)
				{
					interpolated[i] = splines[i](x);
				}

				return Interpolation.Cubic(Interpolation.ConvertToPoints(interpolated, y0, yDelta));
			};
		}
	}
}