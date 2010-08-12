using System;

namespace SharpBag.BagMath.BagConverters
{
    /// <summary>
    /// An interface for bit and byte size.
    /// </summary>
    public interface BitSize
    {
        /// <see cref="Object.ToString()"/>
        string ToString(bool unit);
        /// <see cref="Object.ToString()"/>
        string ToString(bool unit, Func<double, string> result);
    }

    /// <summary>
    /// A class representing a bit.
    /// </summary>
    public class Bit : BitSize
    {
        public const string Unit = "b";
        public const double BaseValue = 1D;
        private double Value { get; set; }

        public Bit(double v)
        {
            this.Value = v;
        }

        public static implicit operator Bit(double v)
        {
            return new Bit(v);
        }

        public static implicit operator double(Bit v)
        {
            return v.Value;
        }

        public static explicit operator Bit(Kilobit v)
        {
            return new Bit(v * (Kilobit.BaseValue / Bit.BaseValue));
        }

        public static explicit operator Bit(Megabit v)
        {
            return new Bit(v * (Megabit.BaseValue / Bit.BaseValue));
        }

        public static explicit operator Bit(Gigabit v)
        {
            return new Bit(v * (Gigabit.BaseValue / Bit.BaseValue));
        }

        public static explicit operator Bit(Terabit v)
        {
            return new Bit(v * (Terabit.BaseValue / Bit.BaseValue));
        }

        public static explicit operator Bit(Byte v)
        {
            return new Bit(v * (Byte.BaseValue / Bit.BaseValue));
        }

        public static explicit operator Bit(Kilobyte v)
        {
            return new Bit(v * (Kilobyte.BaseValue / Bit.BaseValue));
        }

        public static explicit operator Bit(Megabyte v)
        {
            return new Bit(v * (Megabyte.BaseValue / Bit.BaseValue));
        }

        public static explicit operator Bit(Gigabyte v)
        {
            return new Bit(v * (Gigabyte.BaseValue / Bit.BaseValue));
        }

        public static explicit operator Bit(Terabyte v)
        {
            return new Bit(v * (Terabyte.BaseValue / Bit.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Bit.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Bit.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Bit.Unit : "");
        }
    }

    public class Kilobit : BitSize
    {
        public const string Unit = "Kb";
        public const double BaseValue = 1024D;
        private double Value { get; set; }

        public Kilobit(double v)
        {
            this.Value = v;
        }

        public static implicit operator Kilobit(double v)
        {
            return new Kilobit(v);
        }

        public static implicit operator double(Kilobit v)
        {
            return v.Value;
        }

        public static explicit operator Kilobit(Bit v)
        {
            return new Kilobit(v * (Bit.BaseValue / Kilobit.BaseValue));
        }

        public static explicit operator Kilobit(Megabit v)
        {
            return new Kilobit(v * (Megabit.BaseValue / Kilobit.BaseValue));
        }

        public static explicit operator Kilobit(Gigabit v)
        {
            return new Kilobit(v * (Gigabit.BaseValue / Kilobit.BaseValue));
        }

        public static explicit operator Kilobit(Terabit v)
        {
            return new Kilobit(v * (Terabit.BaseValue / Kilobit.BaseValue));
        }

        public static explicit operator Kilobit(Byte v)
        {
            return new Kilobit(v * (Byte.BaseValue / Kilobit.BaseValue));
        }

        public static explicit operator Kilobit(Kilobyte v)
        {
            return new Kilobit(v * (Kilobyte.BaseValue / Kilobit.BaseValue));
        }

        public static explicit operator Kilobit(Megabyte v)
        {
            return new Kilobit(v * (Megabyte.BaseValue / Kilobit.BaseValue));
        }

        public static explicit operator Kilobit(Gigabyte v)
        {
            return new Kilobit(v * (Gigabyte.BaseValue / Kilobit.BaseValue));
        }

        public static explicit operator Kilobit(Terabyte v)
        {
            return new Kilobit(v * (Terabyte.BaseValue / Kilobit.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Kilobit.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Kilobit.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Kilobit.Unit : "");
        }
    }

    public class Megabit : BitSize
    {
        public const string Unit = "Mb";
        public const double BaseValue = 1048576D;
        private double Value { get; set; }

        public Megabit(double v)
        {
            this.Value = v;
        }

        public static implicit operator Megabit(double v)
        {
            return new Megabit(v);
        }

        public static implicit operator double(Megabit v)
        {
            return v.Value;
        }

        public static explicit operator Megabit(Bit v)
        {
            return new Megabit(v * (Bit.BaseValue / Megabit.BaseValue));
        }

        public static explicit operator Megabit(Kilobit v)
        {
            return new Megabit(v * (Kilobit.BaseValue / Megabit.BaseValue));
        }

        public static explicit operator Megabit(Gigabit v)
        {
            return new Megabit(v * (Gigabit.BaseValue / Megabit.BaseValue));
        }

        public static explicit operator Megabit(Terabit v)
        {
            return new Megabit(v * (Terabit.BaseValue / Megabit.BaseValue));
        }

        public static explicit operator Megabit(Byte v)
        {
            return new Megabit(v * (Byte.BaseValue / Megabit.BaseValue));
        }

        public static explicit operator Megabit(Kilobyte v)
        {
            return new Megabit(v * (Kilobyte.BaseValue / Megabit.BaseValue));
        }

        public static explicit operator Megabit(Megabyte v)
        {
            return new Megabit(v * (Megabyte.BaseValue / Megabit.BaseValue));
        }

        public static explicit operator Megabit(Gigabyte v)
        {
            return new Megabit(v * (Gigabyte.BaseValue / Megabit.BaseValue));
        }

        public static explicit operator Megabit(Terabyte v)
        {
            return new Megabit(v * (Terabyte.BaseValue / Megabit.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Megabit.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Megabit.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Megabit.Unit : "");
        }
    }

    public class Gigabit : BitSize
    {
        public const string Unit = "Gb";
        public const double BaseValue = 1073741824D;
        private double Value { get; set; }

        public Gigabit(double v)
        {
            this.Value = v;
        }

        public static implicit operator Gigabit(double v)
        {
            return new Gigabit(v);
        }

        public static implicit operator double(Gigabit v)
        {
            return v.Value;
        }

        public static explicit operator Gigabit(Bit v)
        {
            return new Gigabit(v * (Bit.BaseValue / Gigabit.BaseValue));
        }

        public static explicit operator Gigabit(Kilobit v)
        {
            return new Gigabit(v * (Kilobit.BaseValue / Gigabit.BaseValue));
        }

        public static explicit operator Gigabit(Megabit v)
        {
            return new Gigabit(v * (Megabit.BaseValue / Gigabit.BaseValue));
        }

        public static explicit operator Gigabit(Terabit v)
        {
            return new Gigabit(v * (Terabit.BaseValue / Gigabit.BaseValue));
        }

        public static explicit operator Gigabit(Byte v)
        {
            return new Gigabit(v * (Byte.BaseValue / Gigabit.BaseValue));
        }

        public static explicit operator Gigabit(Kilobyte v)
        {
            return new Gigabit(v * (Kilobyte.BaseValue / Gigabit.BaseValue));
        }

        public static explicit operator Gigabit(Megabyte v)
        {
            return new Gigabit(v * (Megabyte.BaseValue / Gigabit.BaseValue));
        }

        public static explicit operator Gigabit(Gigabyte v)
        {
            return new Gigabit(v * (Gigabyte.BaseValue / Gigabit.BaseValue));
        }

        public static explicit operator Gigabit(Terabyte v)
        {
            return new Gigabit(v * (Terabyte.BaseValue / Gigabit.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Gigabit.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Gigabit.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Gigabit.Unit : "");
        }
    }

    public class Terabit : BitSize
    {
        public const string Unit = "Tb";
        public const double BaseValue = 1099511627776D;
        private double Value { get; set; }

        public Terabit(double v)
        {
            this.Value = v;
        }

        public static implicit operator Terabit(double v)
        {
            return new Terabit(v);
        }

        public static implicit operator double(Terabit v)
        {
            return v.Value;
        }

        public static explicit operator Terabit(Bit v)
        {
            return new Terabit(v * (Bit.BaseValue / Terabit.BaseValue));
        }

        public static explicit operator Terabit(Kilobit v)
        {
            return new Terabit(v * (Kilobit.BaseValue / Terabit.BaseValue));
        }

        public static explicit operator Terabit(Megabit v)
        {
            return new Terabit(v * (Megabit.BaseValue / Terabit.BaseValue));
        }

        public static explicit operator Terabit(Gigabit v)
        {
            return new Terabit(v * (Gigabit.BaseValue / Terabit.BaseValue));
        }

        public static explicit operator Terabit(Byte v)
        {
            return new Terabit(v * (Byte.BaseValue / Terabit.BaseValue));
        }

        public static explicit operator Terabit(Kilobyte v)
        {
            return new Terabit(v * (Kilobyte.BaseValue / Terabit.BaseValue));
        }

        public static explicit operator Terabit(Megabyte v)
        {
            return new Terabit(v * (Megabyte.BaseValue / Terabit.BaseValue));
        }

        public static explicit operator Terabit(Gigabyte v)
        {
            return new Terabit(v * (Gigabyte.BaseValue / Terabit.BaseValue));
        }

        public static explicit operator Terabit(Terabyte v)
        {
            return new Terabit(v * (Terabyte.BaseValue / Terabit.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Terabit.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Terabit.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Terabit.Unit : "");
        }
    }

    public class Byte : BitSize
    {
        public const string Unit = "B";
        public const double BaseValue = 8D;
        private double Value { get; set; }

        public Byte(double v)
        {
            this.Value = v;
        }

        public static implicit operator Byte(double v)
        {
            return new Byte(v);
        }

        public static implicit operator double(Byte v)
        {
            return v.Value;
        }

        public static explicit operator Byte(Bit v)
        {
            return new Byte(v * (Bit.BaseValue / Byte.BaseValue));
        }

        public static explicit operator Byte(Kilobit v)
        {
            return new Byte(v * (Kilobit.BaseValue / Byte.BaseValue));
        }

        public static explicit operator Byte(Megabit v)
        {
            return new Byte(v * (Megabit.BaseValue / Byte.BaseValue));
        }

        public static explicit operator Byte(Gigabit v)
        {
            return new Byte(v * (Gigabit.BaseValue / Byte.BaseValue));
        }

        public static explicit operator Byte(Terabit v)
        {
            return new Byte(v * (Terabit.BaseValue / Byte.BaseValue));
        }

        public static explicit operator Byte(Kilobyte v)
        {
            return new Byte(v * (Kilobyte.BaseValue / Byte.BaseValue));
        }

        public static explicit operator Byte(Megabyte v)
        {
            return new Byte(v * (Megabyte.BaseValue / Byte.BaseValue));
        }

        public static explicit operator Byte(Gigabyte v)
        {
            return new Byte(v * (Gigabyte.BaseValue / Byte.BaseValue));
        }

        public static explicit operator Byte(Terabyte v)
        {
            return new Byte(v * (Terabyte.BaseValue / Byte.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Byte.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Byte.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Byte.Unit : "");
        }
    }

    public class Kilobyte : BitSize
    {
        public const string Unit = "KB";
        public const double BaseValue = 8192D;
        private double Value { get; set; }

        public Kilobyte(double v)
        {
            this.Value = v;
        }

        public static implicit operator Kilobyte(double v)
        {
            return new Kilobyte(v);
        }

        public static implicit operator double(Kilobyte v)
        {
            return v.Value;
        }

        public static explicit operator Kilobyte(Bit v)
        {
            return new Kilobyte(v * (Bit.BaseValue / Kilobyte.BaseValue));
        }

        public static explicit operator Kilobyte(Kilobit v)
        {
            return new Kilobyte(v * (Kilobit.BaseValue / Kilobyte.BaseValue));
        }

        public static explicit operator Kilobyte(Megabit v)
        {
            return new Kilobyte(v * (Megabit.BaseValue / Kilobyte.BaseValue));
        }

        public static explicit operator Kilobyte(Gigabit v)
        {
            return new Kilobyte(v * (Gigabit.BaseValue / Kilobyte.BaseValue));
        }

        public static explicit operator Kilobyte(Terabit v)
        {
            return new Kilobyte(v * (Terabit.BaseValue / Kilobyte.BaseValue));
        }

        public static explicit operator Kilobyte(Byte v)
        {
            return new Kilobyte(v * (Byte.BaseValue / Kilobyte.BaseValue));
        }

        public static explicit operator Kilobyte(Megabyte v)
        {
            return new Kilobyte(v * (Megabyte.BaseValue / Kilobyte.BaseValue));
        }

        public static explicit operator Kilobyte(Gigabyte v)
        {
            return new Kilobyte(v * (Gigabyte.BaseValue / Kilobyte.BaseValue));
        }

        public static explicit operator Kilobyte(Terabyte v)
        {
            return new Kilobyte(v * (Terabyte.BaseValue / Kilobyte.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Kilobyte.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Kilobyte.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Kilobyte.Unit : "");
        }
    }

    public class Megabyte : BitSize
    {
        public const string Unit = "MB";
        public const double BaseValue = 8388608D;
        private double Value { get; set; }

        public Megabyte(double v)
        {
            this.Value = v;
        }

        public static implicit operator Megabyte(double v)
        {
            return new Megabyte(v);
        }

        public static implicit operator double(Megabyte v)
        {
            return v.Value;
        }

        public static explicit operator Megabyte(Bit v)
        {
            return new Megabyte(v * (Bit.BaseValue / Megabyte.BaseValue));
        }

        public static explicit operator Megabyte(Kilobit v)
        {
            return new Megabyte(v * (Kilobit.BaseValue / Megabyte.BaseValue));
        }

        public static explicit operator Megabyte(Megabit v)
        {
            return new Megabyte(v * (Megabit.BaseValue / Megabyte.BaseValue));
        }

        public static explicit operator Megabyte(Gigabit v)
        {
            return new Megabyte(v * (Gigabit.BaseValue / Megabyte.BaseValue));
        }

        public static explicit operator Megabyte(Terabit v)
        {
            return new Megabyte(v * (Terabit.BaseValue / Megabyte.BaseValue));
        }

        public static explicit operator Megabyte(Byte v)
        {
            return new Megabyte(v * (Byte.BaseValue / Megabyte.BaseValue));
        }

        public static explicit operator Megabyte(Kilobyte v)
        {
            return new Megabyte(v * (Kilobyte.BaseValue / Megabyte.BaseValue));
        }

        public static explicit operator Megabyte(Gigabyte v)
        {
            return new Megabyte(v * (Gigabyte.BaseValue / Megabyte.BaseValue));
        }

        public static explicit operator Megabyte(Terabyte v)
        {
            return new Megabyte(v * (Terabyte.BaseValue / Megabyte.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Megabyte.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Megabyte.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Megabyte.Unit : "");
        }
    }

    public class Gigabyte : BitSize
    {
        public const string Unit = "GB";
        public const double BaseValue = 8589934592D;
        private double Value { get; set; }

        public Gigabyte(double v)
        {
            this.Value = v;
        }

        public static implicit operator Gigabyte(double v)
        {
            return new Gigabyte(v);
        }

        public static implicit operator double(Gigabyte v)
        {
            return v.Value;
        }

        public static explicit operator Gigabyte(Bit v)
        {
            return new Gigabyte(v * (Bit.BaseValue / Gigabyte.BaseValue));
        }

        public static explicit operator Gigabyte(Kilobit v)
        {
            return new Gigabyte(v * (Kilobit.BaseValue / Gigabyte.BaseValue));
        }

        public static explicit operator Gigabyte(Megabit v)
        {
            return new Gigabyte(v * (Megabit.BaseValue / Gigabyte.BaseValue));
        }

        public static explicit operator Gigabyte(Gigabit v)
        {
            return new Gigabyte(v * (Gigabit.BaseValue / Gigabyte.BaseValue));
        }

        public static explicit operator Gigabyte(Terabit v)
        {
            return new Gigabyte(v * (Terabit.BaseValue / Gigabyte.BaseValue));
        }

        public static explicit operator Gigabyte(Byte v)
        {
            return new Gigabyte(v * (Byte.BaseValue / Gigabyte.BaseValue));
        }

        public static explicit operator Gigabyte(Kilobyte v)
        {
            return new Gigabyte(v * (Kilobyte.BaseValue / Gigabyte.BaseValue));
        }

        public static explicit operator Gigabyte(Megabyte v)
        {
            return new Gigabyte(v * (Megabyte.BaseValue / Gigabyte.BaseValue));
        }

        public static explicit operator Gigabyte(Terabyte v)
        {
            return new Gigabyte(v * (Terabyte.BaseValue / Gigabyte.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Gigabyte.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Gigabyte.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Gigabyte.Unit : "");
        }
    }

    public class Terabyte : BitSize
    {
        public const string Unit = "TB";
        public const double BaseValue = 8796093020000D;
        private double Value { get; set; }

        public Terabyte(double v)
        {
            this.Value = v;
        }

        public static implicit operator Terabyte(double v)
        {
            return new Terabyte(v);
        }

        public static implicit operator double(Terabyte v)
        {
            return v.Value;
        }

        public static explicit operator Terabyte(Bit v)
        {
            return new Terabyte(v * (Bit.BaseValue / Terabyte.BaseValue));
        }

        public static explicit operator Terabyte(Kilobit v)
        {
            return new Terabyte(v * (Kilobit.BaseValue / Terabyte.BaseValue));
        }

        public static explicit operator Terabyte(Megabit v)
        {
            return new Terabyte(v * (Megabit.BaseValue / Terabyte.BaseValue));
        }

        public static explicit operator Terabyte(Gigabit v)
        {
            return new Terabyte(v * (Gigabit.BaseValue / Terabyte.BaseValue));
        }

        public static explicit operator Terabyte(Terabit v)
        {
            return new Terabyte(v * (Terabit.BaseValue / Terabyte.BaseValue));
        }

        public static explicit operator Terabyte(Byte v)
        {
            return new Terabyte(v * (Byte.BaseValue / Terabyte.BaseValue));
        }

        public static explicit operator Terabyte(Kilobyte v)
        {
            return new Terabyte(v * (Kilobyte.BaseValue / Terabyte.BaseValue));
        }

        public static explicit operator Terabyte(Megabyte v)
        {
            return new Terabyte(v * (Megabyte.BaseValue / Terabyte.BaseValue));
        }

        public static explicit operator Terabyte(Gigabyte v)
        {
            return new Terabyte(v * (Gigabyte.BaseValue / Terabyte.BaseValue));
        }

        /// <see cref="Object.ToString()"/>
        public override string ToString()
        {
            return this.Value.ToString() + " " + Terabyte.Unit;
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        public string ToString(bool unit)
        {
            return this.Value.ToString() + (unit ? " " + Terabyte.Unit : "");
        }

        /// <see cref="Object.ToString()"/>
        /// <param name="unit">Whether or not to append the measurement unit.</param>
        /// <param name="result">The result.</param>
        public string ToString(bool unit, Func<double, string> result)
        {
            return this.Value.ToString() + (unit ? " " + Terabyte.Unit : "");
        }
    }
}