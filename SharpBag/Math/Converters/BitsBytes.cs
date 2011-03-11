using System;

namespace SharpBag.Math.Converters
{
    /// <summary>
    /// An abstract class defining a bit size.
    /// </summary>
    public abstract class BitSize
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
        public BitSize(double v)
        {
            this.Value = v;
        }

        /// <summary>
        /// Converts a bit size to another bit size.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>The bit size converted to another bit size.</returns>
        public TResult As<TValue, TResult>()
            where TResult : BitSize, new()
            where TValue : BitSize, new()
        {
            return new TResult() { Value = (new TValue().Value * (new TValue().BaseValue / this.BaseValue)) };
        }

        /*
         * TODO: Find a way for this to work.
         *
         * public static implicit operator BitSize(double v)
         * {
         *     return new BitSize(v);
         * }
         *
         * */

        /// <summary>
        /// An implicit convert from a bit size to a double.
        /// </summary>
        /// <param name="v">The bit size.</param>
        /// <returns>The bit size as a double.</returns>
        public static implicit operator double(BitSize v)
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
    /// A class representing a bit.
    /// </summary>
    public class Bit : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "b"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Bit(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a kilobit.
    /// </summary>
    public class Kilobit : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "Kb"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1024D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Kilobit(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a megabit.
    /// </summary>
    public class Megabit : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "Mb"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1048576D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Megabit(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a gigabit.
    /// </summary>
    public class Gigabit : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "Gb"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1073741824D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Gigabit(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a terabit.
    /// </summary>
    public class Terabit : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "Tb"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 1099511627776D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Terabit(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a byte.
    /// </summary>
    public class Byte : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "B"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 8D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Byte(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a kilobyte.
    /// </summary>
    public class Kilobyte : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "KB"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 8192D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Kilobyte(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a megabyte.
    /// </summary>
    public class Megabyte : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "MB"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 8388608D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Megabyte(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a gigabyte.
    /// </summary>
    public class Gigabyte : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "GB"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 8589934592D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Gigabyte(double v) : base(v) { }
    }

    /// <summary>
    /// A class reperesenting a terabyte.
    /// </summary>
    public class Terabyte : BitSize
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public override string Unit { get { return "TB"; } }

        /// <summary>
        /// The base value.
        /// </summary>
        public override double BaseValue { get { return 8796093020000D; } }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="v">The value.</param>
        public Terabyte(double v) : base(v) { }
    }
}