using System;

namespace SharpBag.Math.Converters
{
    /// <summary>
    /// An abstract class for weight.
    /// </summary>
    public abstract class Weight
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
        /// THe value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v"></param>
        protected Weight(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// Converts one weight to another weight.
        /// </summary>
        /// <typeparam name="TValue">The type of input weight.</typeparam>
        /// <typeparam name="TResult">The type of output weight.</typeparam>
        /// <returns>The input weight as output weight.</returns>
        public TResult As<TValue, TResult>()
            where TResult : Weight, new()
            where TValue : Weight, new()
        {
            return new TResult { Value = (new TValue().Value * (new TValue().BaseValue / this.BaseValue)) };
        }

        /*
         * TODO: Find a way for this to work.
         *
         * public static implicit operator Weight(double v)
         * {
         *     return new Weight(v);
         * }
         *
         * */

        /// <summary>
        /// An implicit convert from a weight to a double.
        /// </summary>
        /// <param name="v">The weight.</param>
        /// <returns>The weight as a double.</returns>
        public static implicit operator double(Weight v)
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
            return this.Value.ToString() + (unit ? " " + this.Unit : "");
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
    /// A class representing a milligram.
    /// </summary>
    public class Milligram : Weight
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "mg"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1D; } }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Milligram(double v) : base(v) { }
    }

    /// <summary>
    /// A class representing a gram.
    /// </summary>
    public class Gram : Weight
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "g"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1000D; } }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Gram(double v) : base(v) { }
    }

    /// <summary>
    /// A class representing a kilogram.
    /// </summary>
    public class Kilogram : Weight
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "kg"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1000000D; } }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Kilogram(double v) : base(v) { }
    }
}