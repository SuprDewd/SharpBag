using System;
using System.Collections.Generic;



using System.Diagnostics.Contracts;



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

			Contract.Requires(action != null);

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

			Contract.Requires(action != null);

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

			Contract.Requires(control != null);
			Contract.Requires(methodcall != null);

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

			Contract.Requires(control != null);
			Contract.Requires(methodcall != null);

			if (control.Dispatcher.Thread != Thread.CurrentThread) control.Dispatcher.Invoke(priorityForCall, methodcall);
			else methodcall();
		}

		#endregion InvokeIfRequired overloads

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

			Contract.Requires(provider != null);

			Type type = typeof(TOut);
			if (type.IsNullableType()) type = Nullable.GetUnderlyingType(type);

			try
			{
				return type.IsEnum && (original is string) ? (TOut)Enum.Parse(type, original as string, true) : (TOut)Convert.ChangeType(original, type, provider);
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

			Contract.Requires(type != null);

			return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
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
		/// Executes the specified action and returns the current instance.
		/// </summary>
		/// <typeparam name="T">The type of the current instance.</typeparam>
		/// <param name="variable">The current instance.</param>
		/// <param name="action">The action to execute.</param>
		/// <returns>The current instance.</returns>
		public static T Do<T>(this T variable, Action<T> action)
		{
			action(variable);
			return variable;
		}

		/// <summary>
		/// Executes the specified function and returns the result.
		/// </summary>
		/// <typeparam name="TIn">The type of the current instance.</typeparam>
		/// <typeparam name="TOut">The type of the result.</typeparam>
		/// <param name="variable">The current instance.</param>
		/// <param name="func">The function to execute.</param>
		/// <returns>The function result.</returns>
		public static TOut Do<TIn, TOut>(this TIn variable, Func<TIn, TOut> func)
		{
			return func(variable);
		}
	}
}