using System;

namespace SharpBag.Math.Converters
{
    /// <summary>
    /// An abstract class representing a length.
    /// </summary>
    public abstract class Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public abstract string Unit { get; }

        /// <summary>
        /// The base value.
        /// </summary>
        public abstract double BaseValue { get; }

        /// <summary>
        /// The value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v"></param>
        protected Length(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// Convert on length to another length.
        /// </summary>
        /// <typeparam name="TValue">The type of input length.</typeparam>
        /// <typeparam name="TResult">The type of output length.</typeparam>
        /// <returns>The input length as an output length.</returns>
        public TResult As<TValue, TResult>()
            where TResult : Length, new()
            where TValue : Length, new()
        {
            return new TResult { Value = (new TValue().Value * (new TValue().BaseValue / this.BaseValue)) };
        }

        /*
         * TODO: Find a way for this to work.
         *
         * public static implicit operator Length(double v)
         * {
         *     return new Length(v);
         * }
         *
         * */

        /// <summary>
        /// Implicitly converts a length to a double.
        /// </summary>
        /// <param name="v">The length.</param>
        /// <returns>The length as a double.</returns>
        public static implicit operator double(Length v)
        {
            return v.Value;
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value + (unit ? " " + this.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + this.Unit : "");
        }
    }

    /// <summary>
    /// A class representing a millimeter.
    /// </summary>
    public class Millimeter : Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "mm"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Millimeter(double v) : base(v) { }
    }

    /// <summary>
    /// A class representing a centimeter.
    /// </summary>
    public class Centimeter : Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "cm"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 10D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Centimeter(double v) : base(v) { }
    }

    /// <summary>
    /// A class representing a meter.
    /// </summary>
    public class Meter : Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "m"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1000D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Meter(double v) : base(v) { }
    }

    /// <summary>
    /// A class representing a kilometer.
    /// </summary>
    public class Kilometer : Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "km"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1000000D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Kilometer(double v) : base(v) { }
    }
}