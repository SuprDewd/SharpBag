using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using SharpBag.Math;
using Point = SharpBag.Math.Geometry.Point;

namespace SharpBag.Media.Drawing
{
    using System;

    /// <summary>
    /// A color scheme.
    /// </summary>
    public class ColorScheme
    {
        /// <summary>
        /// A color stop.
        /// </summary>
        public class Stop
        {
            /// <summary>
            /// The position to stop at.
            /// </summary>
            public double At { get; private set; }

            /// <summary>
            /// Gets the color.
            /// </summary>
            public Color Color { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Stop"/> class.
            /// </summary>
            /// <param name="at">The position to stop at.</param>
            /// <param name="color">The color.</param>
            public Stop(double at, Color color)
            {
                Contract.Requires(at >= 0);
                Contract.Requires(at <= 1);

                this.At = at;
                this.Color = color;
            }
        }

        private Func<double, double> ALinearInterpolation;
        private Func<double, double> RLinearInterpolation;
        private Func<double, double> GLinearInterpolation;
        private Func<double, double> BLinearInterpolation;

        private Func<double, double> ACubicInterpolation;
        private Func<double, double> RCubicInterpolation;
        private Func<double, double> GCubicInterpolation;
        private Func<double, double> BCubicInterpolation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorScheme"/> class.
        /// </summary>
        /// <param name="colorStops">The color stops.</param>
        public ColorScheme(IEnumerable<Stop> colorStops)
        {
            Contract.Requires(colorStops.Any());

            this.ALinearInterpolation = Interpolation.Linear(colorStops.OrderBy(i => i.At).Select(i => new Point(i.At, i.Color.A)).ToArray());
            this.RLinearInterpolation = Interpolation.Linear(colorStops.OrderBy(i => i.At).Select(i => new Point(i.At, i.Color.R)).ToArray());
            this.GLinearInterpolation = Interpolation.Linear(colorStops.OrderBy(i => i.At).Select(i => new Point(i.At, i.Color.G)).ToArray());
            this.BLinearInterpolation = Interpolation.Linear(colorStops.OrderBy(i => i.At).Select(i => new Point(i.At, i.Color.B)).ToArray());

            this.ACubicInterpolation = Interpolation.Cubic(colorStops.OrderBy(i => i.At).Select(i => new Point(i.At, i.Color.A)).ToArray());
            this.RCubicInterpolation = Interpolation.Cubic(colorStops.OrderBy(i => i.At).Select(i => new Point(i.At, i.Color.R)).ToArray());
            this.GCubicInterpolation = Interpolation.Cubic(colorStops.OrderBy(i => i.At).Select(i => new Point(i.At, i.Color.G)).ToArray());
            this.BCubicInterpolation = Interpolation.Cubic(colorStops.OrderBy(i => i.At).Select(i => new Point(i.At, i.Color.B)).ToArray());
        }

        /// <summary>
        /// Gets the color for the specified value, using linear interpolation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="from">The starting value of the scheme.</param>
        /// <param name="to">The ending value of the scheme.</param>
        /// <returns>The color.</returns>
        public Color LinearColorFor(double value, double from, double to)
        {
            value = (value - from) / (to - from);
            if (value < 0) value = 0;
            else if (value > 1) value = 1;
            return this.LinearColorFor(value);
        }

        /// <summary>
        /// Gets the color for the specified value, using linear interpolation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The color.</returns>
        public Color LinearColorFor(double value)
        {
            return Color.FromArgb(
                Math.Min(Math.Max((int)this.ALinearInterpolation(value), 0), 255),
                Math.Min(Math.Max((int)this.RLinearInterpolation(value), 0), 255),
                Math.Min(Math.Max((int)this.GLinearInterpolation(value), 0), 255),
                Math.Min(Math.Max((int)this.BLinearInterpolation(value), 0), 255)
            );
        }

        /// <summary>
        /// Gets the color for the specified value, using cubic interpolation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="from">The starting value of the scheme.</param>
        /// <param name="to">The ending value of the scheme.</param>
        /// <returns>The color.</returns>
        public Color CubicColorFor(double value, double from, double to)
        {
            value = (value - from) / (to - from);
            if (value < 0) value = 0;
            else if (value > 1) value = 1;
            return this.CubicColorFor(value);
        }

        /// <summary>
        /// Gets the color for the specified value, using linear interpolation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The color.</returns>
        public Color CubicColorFor(double value)
        {
            return Color.FromArgb(
                Math.Min(Math.Max((int)this.ACubicInterpolation(value), 0), 255),
                Math.Min(Math.Max((int)this.RCubicInterpolation(value), 0), 255),
                Math.Min(Math.Max((int)this.GCubicInterpolation(value), 0), 255),
                Math.Min(Math.Max((int)this.BCubicInterpolation(value), 0), 255)
            );
        }
    }
}