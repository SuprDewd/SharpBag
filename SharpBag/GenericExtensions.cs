using System;
using System.Collections;
using System.Collections.Generic;

#if DOTNET4
using System.Diagnostics.Contracts;
#endif

using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharpBag
{
    /// <summary>
    /// A static class containing generic extension methods.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Invokes the specified action if the current object is not null.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>(this T obj, Action<T> action) where T : class
        {
#if DOTNET4
            Contract.Requires(action != null);
#endif
            if (obj != null) action(obj);
        }

        /// <summary>
        /// Invokes the specified action if the current object is not null.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>(this T obj, Action action) where T : class
        {
#if DOTNET4
            Contract.Requires(action != null);
#endif
            if (obj != null) action();
        }

        #region InvokeIfRequired overloads

        /// <summary>
        /// Simple helper extension method to marshall to correct thread if its required.
        /// </summary>
        /// <param name="control">The source control.</param>
        /// <param name="methodcall">The method to call.</param>
        public static void InvokeIfRequired(this DispatcherObject control, Action methodcall)
        {
#if DOTNET4
            Contract.Requires(control != null);
            Contract.Requires(methodcall != null);
#endif
            control.InvokeIfRequired(methodcall, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Simple helper extension method to marshall to correct thread if its required.
        /// </summary>
        /// <param name="control">The source control.</param>
        /// <param name="methodcall">The method to call.</param>
        /// <param name="priorityForCall">The thread priority.</param>
        public static void InvokeIfRequired(this DispatcherObject control, Action methodcall, DispatcherPriority priorityForCall)
        {
#if DOTNET4
            Contract.Requires(control != null);
            Contract.Requires(methodcall != null);
#endif
            if (control.Dispatcher.Thread != Thread.CurrentThread) control.Dispatcher.Invoke(priorityForCall, methodcall);
            else methodcall();
        }

        #endregion InvokeIfRequired overloads

        /// <summary>
        /// Executes the specified action on the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the current instance.</typeparam>
        /// <param name="obj">The current instance.</param>
        /// <param name="act">An action.</param>
        public static void With<T>(this T obj, Action<T> act)
        {
#if DOTNET4
            Contract.Requires(act != null);
#endif
            act(obj);
        }

        /// <summary>
        /// If the current instance is not null, returns the value returned from selector function, else returns the elseValue.
        /// </summary>
        /// <typeparam name="TIn">The type of the current instance.</typeparam>
        /// <typeparam name="TReturn">The type of the return value.</typeparam>
        /// <param name="obj">The current instance.</param>
        /// <param name="selector">A selector function.</param>
        /// <param name="elseValue">The default value to return.</param>
        /// <returns>If the current instance is not null, returns the value returned from selector function, else returns the elseValue.</returns>
        public static TReturn NullOr<TIn, TReturn>(this TIn obj, Func<TIn, TReturn> selector, TReturn elseValue = default(TReturn)) where TIn : class
        {
#if DOTNET4
            Contract.Requires(selector != null);
#endif
            return obj != null ? selector(obj) : elseValue;
        }

        /// <summary>
        /// Whether the current instance is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of items in the current instance.</typeparam>
        /// <param name="collection">The current instance.</param>
        /// <returns>Whether the current instance is null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// Whether the current instance is T.
        /// </summary>
        /// <typeparam name="T">The type to check against.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <returns>Whether the current instance is T.</returns>
        public static bool Is<T>(this object item) where T : class
        {
            return item is T;
        }

        /// <summary>
        /// Whether the current instance is not T.
        /// </summary>
        /// <typeparam name="T">The type to check against.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <returns>Whether the current instance is not T.</returns>
        public static bool IsNot<T>(this object item) where T : class
        {
            return !(item.Is<T>());
        }

        /// <summary>
        /// Returns the current instance as T.
        /// </summary>
        /// <typeparam name="T">The type to return the current instance as.</typeparam>
        /// <param name="item">The current instance.</param>
        /// <returns>The current instance as T.</returns>
        public static T CastAs<T>(this object item) where T : class
        {
            return item as T;
        }

        /// <summary>
        /// Converts an the current instance to a dictionary, with it's properties as the keys.
        /// </summary>
        /// <param name="o">The current instance.</param>
        /// <returns>The current instance as a dictionary.</returns>
        public static Dictionary<string, object> ToDictionary(this object o)
        {
#if DOTNET4
            Contract.Requires(o != null);
            Contract.Ensures(Contract.Result<Dictionary<string, object>>() != null);
#endif
            return o.GetType().GetProperties().Where(propertyInfo => propertyInfo.GetIndexParameters().Length == 0).ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.GetValue(o, null));
        }

        /// <summary>
        /// Converts the current instance to the specified type.
        /// </summary>
        /// <typeparam name="TOut">Type the current instance will be converted to.</typeparam>
        /// <param name="original">The current instance.</param>
        /// <param name="defaultValue">The default value to use in case the current instance can't be converted.</param>
        /// <returns>The converted value.</returns>
        public static TOut As<TOut>(this object original, TOut defaultValue = default(TOut))
        {
            return original.As(CultureInfo.CurrentCulture, defaultValue);
        }

        /// <summary>
        /// Converts the current instance to the specified type.
        /// </summary>
        /// <typeparam name="TOut">Type the current instance will be converted to.</typeparam>
        /// <param name="original">The current instance.</param>
        /// <param name="provider">An IFormatProvider.</param>
        /// <param name="defaultValue">The default value to use in case the current instance can't be converted.</param>
        /// <returns>The converted value.</returns>
        public static TOut As<TOut>(this object original, IFormatProvider provider, TOut defaultValue = default(TOut))
        {
#if DOTNET4
            Contract.Requires(provider != null);
#endif
            Type type = typeof(TOut);
            if (type.IsNullableType()) type = Nullable.GetUnderlyingType(type);

            try
            {
                return type.IsEnum && original.Is<string>() ? (TOut)Enum.Parse(type, original.As<string>(), true) : (TOut)Convert.ChangeType(original, type, provider);
            }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Returns whether or not the specified type is Nullable{T}
        /// </summary>
        /// <param name="type">A Type.</param>
        /// <returns>True if the specified type is Nullable{T}; otherwise, false.</returns>
        /// <remarks>Use <see cref="Nullable.GetUnderlyingType"/> to access the underlying type.</remarks>
        public static bool IsNullableType(this Type type)
        {
#if DOTNET4
            Contract.Requires(type != null);
#endif
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        /// <summary>
        /// If the current instance is true, the specified value is returned, else the specified default value is returned.
        /// </summary>
        /// <typeparam name="T">The type of the current instance.</typeparam>
        /// <param name="obj">The current instance.</param>
        /// <param name="expression">The value.</param>
        /// <param name="def">The default value.</param>
        /// <returns>The specified value is returned, else the specified default value is returned.</returns>
        public static T Then<T>(this bool expression, T obj, T def = default(T))
        {
            return expression ? obj : def;
        }

        /// <summary>
        /// Writes the current instance to the console.
        /// </summary>
        /// <param name="o">The specified object.</param>
        /// <returns>A console helper.</returns>
        public static ConsoleHelper Write<T>(this T o)
        {
            return ConsoleHelper.Create().Write<T>(o);
        }

        /// <summary>
        /// Writes the current instance to the console, and then puts the cursor on a new line.
        /// </summary>
        /// <param name="o">The specified object.</param>
        /// <returns>A console helper.</returns>
        public static ConsoleHelper WriteLine<T>(this T o)
        {
            return ConsoleHelper.Create().WriteLine<T>(o);
        }

        /// <summary>
        /// Gets the current instance's hash.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="hasher">The hash function.</param>
        /// <returns>The hash.</returns>
        public static byte[] GetHash(this Stream s, HashAlgorithm hasher = null)
        {
            if (hasher == null) hasher = SHA1.Create();
            return hasher.ComputeHash(s);
        }

        /// <summary>
        /// Gets the current instance's hash.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="hasher">The hash function.</param>
        /// <returns>The hash.</returns>
        public static string GetHashString(this Stream s, HashAlgorithm hasher = null)
        {
            return String.Join("", s.GetHash(hasher).Select(f =>
            {
                string st = Convert.ToString(f, 16);
                if (st.Length == 1) return "0" + st;
                else return st;
            }).ToArray());
        }

        /// <summary>
        /// Gets the current instance's hash.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="hasher">The hash function.</param>
        /// <returns>The hash.</returns>
        public static byte[] GetHash(this string s, HashAlgorithm hasher = null)
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s));
            return ms.GetHash(hasher);
        }

        /// <summary>
        /// Gets the current instance's hash.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="hasher">The hash function.</param>
        /// <returns>The hash.</returns>
        public static string GetHashString(this string s, HashAlgorithm hasher = null)
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s));
            return ms.GetHashString(hasher);
        }

        /// <summary>
        /// Assigns the current instance to the specified variable.
        /// </summary>
        /// <typeparam name="T">The type of the current isntance.</typeparam>
        /// <param name="value">The current instance.</param>
        /// <param name="to">The variable.</param>
        /// <returns>The current instance.</returns>
        public static T AssignTo<T>(this T value, out T to)
        {
            return to = value;
        }
    }
}