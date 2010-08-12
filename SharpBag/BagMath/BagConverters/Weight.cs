using System;

namespace SharpBag.BagMath.BagConverters
{
    /// <summary>
    /// An interface for weight.
    /// </summary>
    public interface Weight
    {
        string ToString(bool unit);
        string ToString(bool unit, Func<double, string> result);
    }

    public class Milligram : Weight
    {
        public const string Unit = "mg";
        public const double BaseValue = 1D;
        private double Value { get; set; }

        public Milligram(double v)
        {
            this.Value = v;
        }

        public static implicit operator Milligram(double v)
        {
            return new Milligram(v);
        }

        public static implicit operator double(Milligram v)
        {
            return v.Value;
        }

        public static explicit operator Milligram(Gram v)
        {
            return new Milligram(v * (Gram.BaseValue / Milligram.BaseValue));
        }

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

    public class Gram : Weight
    {
        public const string Unit = "g";
        public const double BaseValue = 1000D;
        private double Value { get; set; }

        public Gram(double v)
        {
            this.Value = v;
        }

        public static implicit operator Gram(double v)
        {
            return new Gram(v);
        }

        public static implicit operator double(Gram v)
        {
            return v.Value;
        }

        public static explicit operator Gram(Milligram v)
        {
            return new Gram(v * (Milligram.BaseValue / Gram.BaseValue));
        }

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

    public class Kilogram : Weight
    {
        public const string Unit = "kg";
        public const double BaseValue = 1000000D;
        private double Value { get; set; }

        public Kilogram(double v)
        {
            this.Value = v;
        }

        public static implicit operator Kilogram(double v)
        {
            return new Kilogram(v);
        }

        public static implicit operator double(Kilogram v)
        {
            return v.Value;
        }

        public static explicit operator Kilogram(Milligram v)
        {
            return new Kilogram(v * (Milligram.BaseValue / Kilogram.BaseValue));
        }

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