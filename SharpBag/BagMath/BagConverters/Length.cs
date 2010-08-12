using System;

namespace SharpBag.BagMath.BagConverters
{
    /// <summary>
    /// An interface for length.
    /// </summary>
    public interface Length
    {
        /// <see cref="Object.ToString()"/>
        string ToString(bool unit);
        /// <see cref="Object.ToString()"/>
        string ToString(bool unit, Func<double, string> result);
    }

    /// <summary>
    /// A class representing a millimeter.
    /// </summary>
    public class Millimeter : Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public const string Unit = "mm";
        /// <summary>
        /// The base value.
        /// </summary>
        public const double BaseValue = 1D;
        /// <summary>
        /// The value.
        /// </summary>
        private double Value { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Millimeter(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator Millimeter(double v)
        {
            return new Millimeter(v);
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator double(Millimeter v)
        {
            return v.Value;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Millimeter(Centimeter v)
        {
            return new Millimeter(v * (Centimeter.BaseValue / Millimeter.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Millimeter(Meter v)
        {
            return new Millimeter(v * (Meter.BaseValue / Millimeter.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
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

    /// <summary>
    /// A class representing a centimeter.
    /// </summary>
    public class Centimeter : Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public const string Unit = "cm";
        /// <summary>
        /// The base value.
        /// </summary>
        public const double BaseValue = 10D;
        /// <summary>
        /// The value.
        /// </summary>
        private double Value { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Centimeter(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator Centimeter(double v)
        {
            return new Centimeter(v);
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator double(Centimeter v)
        {
            return v.Value;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Centimeter(Millimeter v)
        {
            return new Centimeter(v * (Millimeter.BaseValue / Centimeter.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Centimeter(Meter v)
        {
            return new Centimeter(v * (Meter.BaseValue / Centimeter.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
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

    /// <summary>
    /// A class representing a meter.
    /// </summary>
    public class Meter : Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public const string Unit = "m";
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
        public Meter(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator Meter(double v)
        {
            return new Meter(v);
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator double(Meter v)
        {
            return v.Value;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Meter(Millimeter v)
        {
            return new Meter(v * (Millimeter.BaseValue / Meter.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Meter(Centimeter v)
        {
            return new Meter(v * (Centimeter.BaseValue / Meter.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
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

    /// <summary>
    /// A class representing a kilometer.
    /// </summary>
    public class Kilometer : Length
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public const string Unit = "km";
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
        public Kilometer(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator Kilometer(double v)
        {
            return new Kilometer(v);
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static implicit operator double(Kilometer v)
        {
            return v.Value;
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Kilometer(Millimeter v)
        {
            return new Kilometer(v * (Millimeter.BaseValue / Kilometer.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
        public static explicit operator Kilometer(Centimeter v)
        {
            return new Kilometer(v * (Centimeter.BaseValue / Kilometer.BaseValue));
        }

        /// <summary>
        /// A conversion.
        /// </summary>
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