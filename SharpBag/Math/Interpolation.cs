using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBag.Math.Calculators;

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
		public static Point<double>[] ConvertToPoints(double[] points, double x0, double xDelta)
		{
			Point<double>[] pointArray = new Point<double>[points.Length];

			for (int i = 0; i < points.Length; i++)
			{
				pointArray[i] = new Point<double>(x0, points[i]);
				x0 += xDelta;
			}

			return pointArray;
		}

		/// <summary>
		/// Convert an array of values to an array of points.
		/// </summary>
		/// <typeparam name="T">The type of points.</typeparam>
		/// <param name="points">The known data points.</param>
		/// <param name="x0">The first x.</param>
		/// <param name="xDelta">The x delta.</param>
		/// <param name="calc">The calculator.</param>
		/// <returns>The array of points.</returns>
		public static Point<T>[] ConvertToPoints<T>(T[] points, T x0, T xDelta, Calculator<T> calc = null)
		{
			if (calc == null) calc = CalculatorFactory.GetInstanceFor<T>();
			Point<T>[] pointArray = new Point<T>[points.Length];

			for (int i = 0; i < points.Length; i++)
			{
				pointArray[i] = new Point<T>(x0, points[i]);
				x0 = calc.Add(x0, xDelta);
			}

			return pointArray;
		}

		/// <summary>
		/// A natural cubic spline interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<double, double> Cubic(Point<double>[] points)
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

			Polynomial<double>[] splines = new Polynomial<double>[n];

			for (int i = 0; i < n; i++)
			{
				splines[i] = new Polynomial<double>(points[i].Y, b[i], c[i], d[i]);
			}

			return x =>
			{
				Polynomial<double> last = splines[0];
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
		/// A natural cubic spline interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <param name="calc">The calculator.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<T, T> Cubic<T>(Point<T>[] points, Calculator<T> calc = null)
		{
			if (points.Length < 2) return null;
			if (calc == null) calc = CalculatorFactory.GetInstanceFor<T>();

			int n = points.Length - 1;
			T[] b = new T[n],
					 d = new T[n],
					 h = new T[n],
					 alpha = new T[n],
					 c = new T[n + 1],
					 l = new T[n + 1],
					 mu = new T[n + 1],
					 z = new T[n + 1];

			for (int i = 0; i < n; i++)
			{
				h[i] = calc.Subtract(points[i + 1].X, points[i].X);
			}

			for (int i = 1; i < n; i++)
			{
				alpha[i] = calc.Subtract(calc.Multiply(calc.Divide(calc.Convert(3), h[i]), calc.Subtract(points[i + 1].Y, points[i].Y)), calc.Multiply(calc.Divide(calc.Convert(3), h[i - 1]), calc.Subtract(points[i].Y, points[i - 1].Y)));
			}

			l[0] = mu[0] = z[0] = calc.Zero;

			for (int i = 1; i < n; i++)
			{
				l[i] = calc.Subtract(calc.Multiply(calc.Convert(2), calc.Subtract(points[i + 1].X, points[i - 1].X)), calc.Multiply(h[i - 1], mu[i - 1]));
				mu[i] = calc.Divide(h[i], l[i]);
				z[i] = calc.Divide(calc.Subtract(alpha[i], calc.Multiply(h[i - 1], z[i - 1])), l[i]);
			}

			l[n] = calc.One;
			z[n] = c[n] = calc.Zero;

			for (int j = n - 1; j >= 0; j--)
			{
				c[j] = calc.Subtract(z[j], calc.Multiply(mu[j], c[j + 1]));
				b[j] = calc.Subtract(calc.Divide(calc.Subtract(points[j + 1].Y, points[j].Y), h[j]), calc.Divide(calc.Multiply(h[j], calc.Add(c[j + 1], calc.Multiply(calc.Convert(2), c[j]))), calc.Convert(3)));
				d[j] = calc.Divide(calc.Subtract(c[j + 1], c[j]), calc.Multiply(calc.Convert(3), h[j]));
			}

			Polynomial<T>[] splines = new Polynomial<T>[n];

			for (int i = 0; i < n; i++)
			{
				splines[i] = new Polynomial<T>(points[i].Y, b[i], c[i], d[i]);
			}

			return x =>
			{
				Polynomial<T> last = splines[0];
				int i;
				for (i = 1; i < splines.Length; i++)
				{
					if (calc.GreaterThan(points[i].X, x)) break;
					last = splines[i];
				}

				return last.Evaluate(calc.Subtract(x, points[i - 1].X));
			};
		}

		/// <summary>
		/// A linear spline interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<double, double> Linear(Point<double>[] points)
		{
			if (points.Length < 2) return null;
			Polynomial<double>[] splines = new Polynomial<double>[points.Length - 1];

			for (int i = 1; i < points.Length; i++)
			{
				double x0 = points[i - 1].X,
					   y0 = points[i - 1].Y,
					   x1 = points[i].X,
					   y1 = points[i].Y;

				splines[i - 1] = new Polynomial<double>(y0, (y1 - y0) / (x1 - x0));
			}

			return x =>
			{
				Polynomial<double> last = splines[0];
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
		/// <param name="calc">The calculator.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<T, T> Linear<T>(Point<T>[] points, Calculator<T> calc = null)
		{
			if (points.Length < 2) return null;
			if (calc == null) calc = CalculatorFactory.GetInstanceFor<T>();
			Polynomial<T>[] splines = new Polynomial<T>[points.Length - 1];

			for (int i = 1; i < points.Length; i++)
			{
				T x0 = points[i - 1].X,
					   y0 = points[i - 1].Y,
					   x1 = points[i].X,
					   y1 = points[i].Y;

				splines[i - 1] = new Polynomial<T>(y0, calc.Divide(calc.Subtract(y1, y0), calc.Subtract(x1, x0)));
			}

			return x =>
			{
				Polynomial<T> last = splines[0];
				int i;

				for (i = 1; i < splines.Length; i++)
				{
					if (calc.GreaterThan(points[i].X, x)) break;
					last = splines[i];
				}

				return last.Evaluate(calc.Subtract(x, points[i - 1].X));
			};
		}

		/// <summary>
		/// A bilinear interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <param name="x0">First X.</param>
		/// <param name="y0">First Y.</param>
		/// <param name="xDelta">X delta.</param>
		/// <param name="yDelta">Y delta.</param>
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
		/// A bilinear interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <param name="x0">First X.</param>
		/// <param name="y0">First Y.</param>
		/// <param name="xDelta">X delta.</param>
		/// <param name="yDelta">Y delta.</param>
		/// <param name="calc">A calculator.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<T, T, T> Bilinear<T>(T[,] points, T x0, T y0, T xDelta, T yDelta, Calculator<T> calc = null)
		{
			if (points.GetLength(0) < 2 || points.GetLength(1) < 2) return null;
			if (calc == null) calc = CalculatorFactory.GetInstanceFor<T>();

			return (x, y) =>
			{
				T x1 = x0,
				  y1 = y0;

				int i = 0, j = 0;

				for (i = 0; calc.LessThan(calc.Add(x1, xDelta), x) && i < points.GetLength(0) - 1; i++)
				{
					x1 = calc.Add(x1, xDelta);
				}

				for (j = 0; calc.LessThan(calc.Add(y1, yDelta), y) && j < points.GetLength(1) - 1; j++)
				{
					y1 = calc.Add(y1, yDelta);
				}

				T d = calc.Multiply(xDelta, yDelta),
					   x2 = calc.Add(x1, xDelta),
					   y2 = calc.Add(y1, yDelta),
					   n1 = calc.Subtract(x2, x),
					   n2 = calc.Subtract(y2, y),
					   n3 = calc.Subtract(x, x1),
					   n4 = calc.Subtract(y, y1);

				return calc.Add(calc.Multiply(calc.Multiply(calc.Divide(points[i, j], d), n1), n2),
					   calc.Add(calc.Multiply(calc.Multiply(calc.Divide(points[i + 1, j], d), n3), n2),
					   calc.Add(calc.Multiply(calc.Multiply(calc.Divide(points[i, j + 1], d), n1), n4),
					   calc.Multiply(calc.Multiply(calc.Divide(points[i + 1, j + 1], d), n3), n4))));
			};
		}

		public static Func<double, double, double> Bicubic(double[,] points, double x0, double y0, double xDelta, double yDelta)
		{
			if (points.GetLength(0) < 2 || points.GetLength(1) < 2) return null;

			double[][] pointsAlt = new double[points.GetLength(0)][];
			for (int x = 0; x < points.GetLength(0); x++)
			{
				pointsAlt[x] = new double[points.GetLength(1)];
				for (int y = 0; y < points.GetLength(1); y++)
				{
					pointsAlt[x][y] = points[x, y];
				}
			}

			Func<double, double>[] splines = new Func<double, double>[points.GetLength(0)];
			for (int x = 0; x < pointsAlt.Length; x++)
			{
				splines[x] = Interpolation.Cubic(Interpolation.ConvertToPoints(pointsAlt[x], x0, xDelta));
			}

			return (x, y) =>
			{
				double[] interpolated = new double[splines.Length];
				for (int i = 0; i < splines.Length; i++)
				{
					interpolated[i] = splines[i](y);
				}

				return Interpolation.Cubic(Interpolation.ConvertToPoints(interpolated, x0, xDelta))(x);
			};
		}
	}
}