using System;

namespace SharpBag.BagMath.BagConverters
{
    /// <summary>
    /// An interface for weight.
    /// </summary>
    public interface Weight
    {
        /// <see cref="Object.ToString()"/>
        string ToString(bool unit);
        /// <see cref="Object.ToString()"/>
        string ToString(bool unit, Func<double, string> result);
    }

    /// <summary>
    /// A class representing a milligram.
    /// </summary>
    public class Milligram : Weight
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public const string Unit = "mg";
        /// <summary>
        /// The base value.
        /// </summary>
        public const double BaseValue = 1D;
        /// <summary>
        /// The value.
        /// </summary>
        private double Value { get; set; }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Milligram(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator Milligram(double v)
        {
            return new Milligram(v);
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator double(Milligram v)
        {
            return v.Value;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Milligram(Gram v)
        {
            return new Milligram(v * (Gram.BaseValue / Milligram.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Milligram(Kilogram v)
        {
            return new Milligram(v * (Kilogram.BaseValue / Milligram.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Milligram.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Milligram.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Milligram.Unit : "");
        }
    }

    /// <summary>
    /// A class representing a gram.
    /// </summary>
    public class Gram : Weight
    {
        /// <summary>
        /// The unit,
        /// </summary>
        public const string Unit = "g";
        /// <summary>
        /// The base value.
        /// </summary>
        public const double BaseValue = 1000D;
        /// <summary>
        /// The value.
        /// </summary>
        private double Value { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Gram(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator Gram(double v)
        {
            return new Gram(v);
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator double(Gram v)
        {
            return v.Value;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Gram(Milligram v)
        {
            return new Gram(v * (Milligram.BaseValue / Gram.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Gram(Kilogram v)
        {
            return new Gram(v * (Kilogram.BaseValue / Gram.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Gram.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Gram.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Gram.Unit : "");
        }
    }

    /// <summary>
    /// A class representing a kilogram.
    /// </summary>
    public class Kilogram : Weight
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public const string Unit = "kg";
        /// <summary>
        /// The base value.
        /// </summary>
        public const double BaseValue = 1000000D;
        /// <summary>
        /// The value.
        /// </summary>
        private double Value { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Kilogram(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator Kilogram(double v)
        {
            return new Kilogram(v);
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator double(Kilogram v)
        {
            return v.Value;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Kilogram(Milligram v)
        {
            return new Kilogram(v * (Milligram.BaseValue / Kilogram.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Kilogram(Gram v)
        {
            return new Kilogram(v * (Gram.BaseValue / Kilogram.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Kilogram.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Kilogram.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Kilogram.Unit : "");
        }
    }
}