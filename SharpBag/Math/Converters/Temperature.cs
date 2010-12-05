using System;

namespace SharpBag.Math.Converters
{
    /// <summary>
    /// An interface for temperatures.
    /// </summary>
    public interface Temperature
    {
        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        string ToString(bool unit);

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        string ToString(bool unit, Func<double, string> result);
    }

    /// <summary>
    /// The Celsius temperature unit.
    /// </summary>
    public class Celsius : Temperature
    {
        /// <summary>
        /// The Celsius unit.
        /// </summary>
        public const string Unit = "°C";

        private double Value { get; set; }

        /// <summary>
        /// The Celsius temperature unit.
        /// </summary>
        /// <param name="v">The value.</param>
        public Celsius(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// An implicit conversion from double to Celsius.
        /// </summary>
        /// <param name="v">The double value.</param>
        /// <returns>The double value as Celsius.</returns>
        public static implicit operator Celsius(double v)
        {
            return new Celsius(v);
        }

        /// <summary>
        /// An implicit conversion from Celsius to double.
        /// </summary>
        /// <param name="v">The Celsius value.</param>
        /// <returns>The Celsius value as a double.</returns>
        public static implicit operator double(Celsius v)
        {
            return v.Value;
        }

        /// <summary>
        /// An explicit conversion from Kelvin to Celsius.
        /// </summary>
        /// <param name="v">The Kelvin value.</param>
        /// <returns>The Kelvin value as Celsius.</returns>
        public static explicit operator Celsius(Kelvin v)
        {
            return new Celsius(v - 273.15D);
        }

        /// <summary>
        /// An explicit conversion from Fahrenheit to Celsius.
        /// </summary>
        /// <param name="v">The Fahrenheit value.</param>
        /// <returns>The Fahrenheit value as Celsius.</returns>
        public static explicit operator Celsius(Fahrenheit v)
        {
            return new Celsius((v - 32D) * (5D / 9D));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Celsius.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Celsius.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Celsius.Unit : "");
        }
    }

    /// <summary>
    /// The Fahrenheit temperature unit.
    /// </summary>
    public class Fahrenheit : Temperature
    {
        /// <summary>
        /// The Fahrenheit unit.
        /// </summary>
        public const string Unit = "°F";

        private double Value { get; set; }

        /// <summary>
        /// The Fahrenheit temperature unit.
        /// </summary>
        /// <param name="v">The value.</param>
        public Fahrenheit(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// An implicit conversion from double to Fahrenheit.
        /// </summary>
        /// <param name="v">The double value.</param>
        /// <returns>The double value as Fahrenheit.</returns>
        public static implicit operator Fahrenheit(double v)
        {
            return new Fahrenheit(v);
        }

        /// <summary>
        /// An implicit conversion from Fahrenheit to double.
        /// </summary>
        /// <param name="v">The Fahrenheit value.</param>
        /// <returns>The Fahrenheit value as a double.</returns>
        public static implicit operator double(Fahrenheit v)
        {
            return v.Value;
        }

        /// <summary>
        /// An explicit conversion from Kelvin to Fahrenheit.
        /// </summary>
        /// <param name="v">The Kelvin value.</param>
        /// <returns>The Kelvin value as Fahrenheit.</returns>
        public static explicit operator Fahrenheit(Kelvin v)
        {
            return new Fahrenheit(v * (9D / 5D) - 459.67D);
        }

        /// <summary>
        /// An explicit conversion from Celsius to Fahrenheit.
        /// </summary>
        /// <param name="v">The Celsius value.</param>
        /// <returns>The Celsius value as Fahrenheit.</returns>
        public static explicit operator Fahrenheit(Celsius v)
        {
            return new Fahrenheit(v * (9D / 5D) + 32D);
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Fahrenheit.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Fahrenheit.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Fahrenheit.Unit : "");
        }
    }

    /// <summary>
    /// The Kelvin temperature unit.
    /// </summary>
    public class Kelvin : Temperature
    {
        /// <summary>
        /// The Kelvin unit.
        /// </summary>
        public const string Unit = "K";

        private double Value { get; set; }

        /// <summary>
        /// The Kelvin temperature unit.
        /// </summary>
        /// <param name="v">The value.</param>
        public Kelvin(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// An implicit conversion from double to Kelvin.
        /// </summary>
        /// <param name="v">The double value.</param>
        /// <returns>The double value as Kelvin.</returns>
        public static implicit operator Kelvin(double v)
        {
            return new Kelvin(v);
        }

        /// <summary>
        /// An implicit conversion from Kelvin to double.
        /// </summary>
        /// <param name="v">The Kelvin value.</param>
        /// <returns>The Kelvin value as a double.</returns>
        public static implicit operator double(Kelvin v)
        {
            return v.Value;
        }

        /// <summary>
        /// An explicit conversion from Fahrenheit to Kelvin.
        /// </summary>
        /// <param name="v">The Fahrenheit value.</param>
        /// <returns>The Fahrenheit value as Kelvin.</returns>
        public static explicit operator Kelvin(Fahrenheit v)
        {
            return new Kelvin((v + 459.67D) * (5D / 9D));
        }

        /// <summary>
        /// An explicit conversion from Celsius to Kelvin.
        /// </summary>
        /// <param name="v">The Celsius value.</param>
        /// <returns>The Celsius value as Kelvin.</returns>
        public static explicit operator Kelvin(Celsius v)
        {
            return new Kelvin(v + 273.15D);
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Kelvin.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Kelvin.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return result(this.Value) + (unit ? " " + Kelvin.Unit : "");
        }
    }
}