using System.Diagnostics.Contracts;

namespace SharpBag
{
    /// <summary>
    /// An option.
    /// </summary>
    /// <typeparam name="T">The type of the option.</typeparam>
    public class Option<T>
    {
        /// <summary>
        /// Initializes a None option.
        /// </summary>
        internal Option()
        {
            this._HasValue = false;
        }

        /// <summary>
        /// Initializes a Some option with the specified value.
        /// </summary>
        internal Option(T value)
        {
            this._Value = value;
            this._HasValue = true;
        }

        private T _Value { get; set; }

        private bool _HasValue { get; set; }

        /// <summary>
        /// The value of the Option.
        /// </summary>
        public T Value
        {
            get
            {
#if DOTNET4
                Contract.Requires(this._HasValue);
#endif
                return this._Value;
            }
        }

        /// <summary>
        /// Whether the Option is Some.
        /// </summary>
        public bool IsSome { get { return this._HasValue; } }

        /// <summary>
        /// Whether the Options is None.
        /// </summary>
        public bool IsNone { get { return !this._HasValue; } }
    }

    /// <summary>
    /// A factory class for Options.
    /// </summary>
    public static class Option
    {
        /// <summary>
        /// Create a new None Option.
        /// </summary>
        /// <typeparam name="T">The type of the Option.</typeparam>
        /// <returns>The new None Option.</returns>
        public static Option<T> None<T>()
        {
            return new Option<T>();
        }

        /// <summary>
        /// Create a new Some Option with the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the Option.</typeparam>
        /// <returns>The new Some Option.</returns>
        public static Option<T> Some<T>(T value)
        {
            return new Option<T>(value);
        }
    }
}