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
		/// A natural cubic spline interpolation.
		/// </summary>
		/// <param name="points">The known data points.</param>
		/// <returns>An interpolation function.</returns>
		public static Func<double, double> CubicSpline(Point<double>[] points)
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
		public static Func<T, T> CubicSpline<T>(Point<T>[] points, Calculator<T> calc = null)
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
		public static Func<double, double> LinearSpline(Point<double>[] points)
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
		public static Func<T, T> LinearSpline<T>(Point<T>[] points, Calculator<T> calc = null)
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
	}
}