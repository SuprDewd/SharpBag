using System;

namespace SharpBag.BagMath.BagConverters
{
    /// <summary>
    /// An interface for length.
    /// </summary>
    public interface Length
    {
        string ToString(bool unit);
        string ToString(bool unit, Func<double, string> result);
    }

    public class Millimeter : Length
    {
        public const string Unit = "mm";
        public const double BaseValue = 1D;
        private double Value { get; set; }

        public Millimeter(double v)
        {
            this.Value = v;
        }

        public static implicit operator Millimeter(double v)
        {
            return new Millimeter(v);
        }

        public static implicit operator double(Millimeter v)
        {
            return v.Value;
        }

        public static explicit operator Millimeter(Centimeter v)
        {
            return new Millimeter(v * (Centimeter.BaseValue / Millimeter.BaseValue));
        }

        public static explicit operator Millimeter(Meter v)
        {
            return new Millimeter(v * (Meter.BaseValue / Millimeter.BaseValue));
        }

        public static explicit operator Millimeter(Kilometer v)
        {
            return new Millimeter(v * (Kilometer.BaseValue / Millimeter.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Millimeter.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Millimeter.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Millimeter.Unit : "");
        }
    }

    public class Centimeter : Length
    {
        public const string Unit = "cm";
        public const double BaseValue = 10D;
        private double Value { get; set; }

        public Centimeter(double v)
        {
            this.Value = v;
        }

        public static implicit operator Centimeter(double v)
        {
            return new Centimeter(v);
        }

        public static implicit operator double(Centimeter v)
        {
            return v.Value;
        }

        public static explicit operator Centimeter(Millimeter v)
        {
            return new Centimeter(v * (Millimeter.BaseValue / Centimeter.BaseValue));
        }

        public static explicit operator Centimeter(Meter v)
        {
            return new Centimeter(v * (Meter.BaseValue / Centimeter.BaseValue));
        }

        public static explicit operator Centimeter(Kilometer v)
        {
            return new Centimeter(v * (Kilometer.BaseValue / Centimeter.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Centimeter.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Centimeter.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Centimeter.Unit : "");
        }
    }

    public class Meter : Length
    {
        public const string Unit = "m";
        public const double BaseValue = 1000D;
        private double Value { get; set; }

        public Meter(double v)
        {
            this.Value = v;
        }

        public static implicit operator Meter(double v)
        {
            return new Meter(v);
        }

        public static implicit operator double(Meter v)
        {
            return v.Value;
        }

        public static explicit operator Meter(Millimeter v)
        {
            return new Meter(v * (Millimeter.BaseValue / Meter.BaseValue));
        }

        public static explicit operator Meter(Centimeter v)
        {
            return new Meter(v * (Centimeter.BaseValue / Meter.BaseValue));
        }

        public static explicit operator Meter(Kilometer v)
        {
            return new Meter(v * (Kilometer.BaseValue / Meter.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Meter.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Meter.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Meter.Unit : "");
        }
    }

    public class Kilometer : Length
    {
        public const string Unit = "km";
        public const double BaseValue = 1000000D;
        private double Value { get; set; }

        public Kilometer(double v)
        {
            this.Value = v;
        }

        public static implicit operator Kilometer(double v)
        {
            return new Kilometer(v);
        }

        public static implicit operator double(Kilometer v)
        {
            return v.Value;
        }

        public static explicit operator Kilometer(Millimeter v)
        {
            return new Kilometer(v * (Millimeter.BaseValue / Kilometer.BaseValue));
        }

        public static explicit operator Kilometer(Centimeter v)
        {
            return new Kilometer(v * (Centimeter.BaseValue / Kilometer.BaseValue));
        }

        public static explicit operator Kilometer(Meter v)
        {
            return new Kilometer(v * (Meter.BaseValue / Kilometer.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Kilometer.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Kilometer.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Kilometer.Unit : "");
        }
    }
}