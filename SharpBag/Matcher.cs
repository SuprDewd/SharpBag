using System;
using System.Collections.Generic;

namespace SharpBag
{
    /// <summary>
    /// A class for object matching.
    /// </summary>
    /// <typeparam name="TIn">The type to match.</typeparam>
    /// <typeparam name="TOut">The type of the result.</typeparam>
    public class Matcher<TIn, TOut>
    {
        /// <summary>
        /// The value to match.
        /// </summary>
        public TIn Value { get; private set; }

        /// <summary>
        /// The default value.
        /// </summary>
        private Func<TIn, TOut> DefaultValue { get; set; }

        /// <summary>
        /// The matcher functions.
        /// </summary>
        private Dictionary<Func<TIn, bool>, Func<TIn, TOut>> Matchers = new Dictionary<Func<TIn, bool>, Func<TIn, TOut>>();

        private Matcher() { }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="value">The value to match.</param>
        internal Matcher(TIn value)
        {
            this.Value = value;
            this.DefaultValue = i => default(TOut);
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="other">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(TIn other, TOut result)
        {
            this.Matchers.Add(i => i.Equals(other), i => result);
            return this;
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="other">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(TIn other, Func<TOut> result)
        {
            this.Matchers.Add(i => i.Equals(other), i => result());
            return this;
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="other">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(TIn other, Func<TIn, TOut> result)
        {
            this.Matchers.Add(i => i.Equals(other), result);
            return this;
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="func">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(Func<bool> func, TOut result)
        {
            this.Matchers.Add(i => func(), i => result);
            return this;
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="func">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(Func<bool> func, Func<TOut> result)
        {
            this.Matchers.Add(i => func(), i => result());
            return this;
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="func">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(Func<bool> func, Func<TIn, TOut> result)
        {
            this.Matchers.Add(i => func(), result);
            return this;
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="func">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(Func<TIn, bool> func, TOut result)
        {
            this.Matchers.Add(func, i => result);
            return this;
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="func">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(Func<TIn, bool> func, Func<TOut> result)
        {
            this.Matchers.Add(func, i => result());
            return this;
        }

        /// <summary>
        /// Adds a matcher and a result to the match evaluator.
        /// </summary>
        /// <param name="func">The matcher.</param>
        /// <param name="result">The result.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> When(Func<TIn, bool> func, Func<TIn, TOut> result)
        {
            this.Matchers.Add(func, result);
            return this;
        }

        /// <summary>
        /// Specifies what the default value of the match will be.
        /// </summary>
        /// <param name="value">The default value.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> Default(TOut value)
        {
            this.DefaultValue = i => value;
            return this;
        }

        /// <summary>
        /// Specifies what the default value of the match will be.
        /// </summary>
        /// <param name="value">The default value.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> Default(Func<TOut> value)
        {
            this.DefaultValue = i => value();
            return this;
        }

        /// <summary>
        /// Specifies what the default value of the match will be.
        /// </summary>
        /// <param name="value">The default value.</param>
        /// <returns>The current instance (for chaining).</returns>
        public Matcher<TIn, TOut> Default(Func<TIn, TOut> value)
        {
            this.DefaultValue = value;
            return this;
        }

        /// <summary>
        /// Evaluates the match.
        /// </summary>
        /// <returns>The result.</returns>
        public TOut EndMatch()
        {
            foreach (var matcher in this.Matchers) if (matcher.Key(this.Value)) return matcher.Value(this.Value);
            return this.DefaultValue(this.Value);
        }
    }
}