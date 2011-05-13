using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#if DOTNET4

using System.Diagnostics.Contracts;

#endif

namespace SharpBag.Strings
{
	/// <summary>
	/// Extension methods for strings.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Reverses the current string instance.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <returns>The current string instance reversed.</returns>
		public static string Reverse(this string s)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			char[] array = s.ToCharArray();
			Array.Reverse(array);
			return new String(array);
		}

		/// <summary>
		/// Reverses the current string instance using XORing.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <returns>The current string instance reversed.</returns>
		public static string ReverseXor(this string s)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			char[] charArray = s.ToCharArray();
			int len = s.Length - 1;

			for (int i = 0; i < len; i++, len--)
			{
				charArray[i] ^= charArray[len];
				charArray[len] ^= charArray[i];
				charArray[i] ^= charArray[len];
			}

			return new string(charArray);
		}

		/// <summary>
		/// Outputs the enumerable as a pretty string.
		/// </summary>
		/// <typeparam name="T">The type of elements.</typeparam>
		/// <param name="source">The current instance.</param>
		/// <param name="before">A string to prepend to the output.</param>
		/// <param name="delimiter">A string to insert in between the elements.</param>
		/// <param name="after">A string to append to the output.</param>
		/// <returns>The current instance as a pretty string.</returns>
		public static string ToStringPretty<T>(this IEnumerable<T> source, string before = "", string delimiter = ", ", string after = "")
		{
#if DOTNET4
			Contract.Requires(source != null);
			Contract.Requires(before != null);
			Contract.Requires(delimiter != null);
			Contract.Requires(after != null);
#endif
			StringBuilder result = new StringBuilder();
			result.Append(before);

			bool firstElement = true;
			foreach (T elem in source)
			{
				if (firstElement) firstElement = false;
				else result.Append(delimiter);

				result.Append(elem.ToString());
			}

			result.Append(after);
			return result.ToString();
		}

		/// <summary>
		/// Converts the current instance to title case.
		/// </summary>
		/// <param name="text">The current instance.</param>
		/// <returns>The current instance with title case.</returns>
		public static string ToTitleCase(this string text)
		{
#if DOTNET4
			Contract.Requires(text != null);
#endif
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			return textInfo.ToTitleCase(text.ToLower());
		}

		/// <summary>
		/// Takes a camelCase or a PascalCase string and splits it up into words.
		/// </summary>
		/// <param name="s">A camelCase or a PascalCase string.</param>
		/// <returns>The words.</returns>
		public static string Wordify(this string s)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			return !Regex.IsMatch(s, "[a-z]") ? s : string.Join(" ", Regex.Split(s, @"(?<!^)(?=[A-Z])"));
		}

		/// <summary>
		/// Capitalizes the current instance.
		/// </summary>
		/// <param name="word">The current instance.</param>
		/// <returns>The capitalized string.</returns>
		public static string Capitalize(this string word)
		{
#if DOTNET4
			Contract.Requires(word != null);
#endif
			return word[0].ToString().ToUpper() + word.Substring(1);
		}

		/// <summary>
		/// String.Format(string, object[])
		/// </summary>
		public static string Format(this string s, params object[] args)
		{
#if DOTNET4
			Contract.Requires(s != null);
			Contract.Requires(args != null);
#endif
			return String.Format(s, args);
		}

		/// <summary>
		/// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string repeatedly until the new string no longer contains the specified string.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <param name="oldValue">The string to be replaced.</param>
		/// <param name="newValue">The string to replace all occurrences of oldValue.</param>
		/// <returns>A string that is equivalent to the current string except that all instances of oldValue are repeatedly replaced with newValue until the new string no longer contains oldValue.</returns>
		public static string ReplaceAll(this string s, string oldValue, string newValue)
		{
#if DOTNET4
			Contract.Requires(s != null);
			Contract.Requires(oldValue != null);
			Contract.Requires(newValue != null);
			Contract.Requires(!newValue.Contains(oldValue));
#endif
			string tS = s;
			while (tS.Contains(oldValue)) tS = tS.Replace(oldValue, newValue);
			return tS;
		}

		/// <summary>
		/// Returns all the words in the string.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <returns>All the words in the string.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string[] Words(this string s)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			return s.Split(new char[] { ' ', '.', ',', '?' }, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// Returns all the lines in the string.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <returns>All the lines in the string.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string[] Lines(this string s)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			return s.NoCarriageReturns().Split('\n');
		}

		/// <summary>
		/// Takes the string and removes all carriage returns ('\r').
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <returns>The string without carriage returns ('\r').</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string NoCarriageReturns(this string s)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			return s.Replace("\r", "");
		}

		/// <summary>
		/// Takes the string, replaces all line breaks with a space, then replaces all double spaces with a space and finally trims the string.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <returns>The string in one line, with no double spaces, trimmed.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string OneLineNoDoubleSpaceTrimmed(this string s)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			return s.Trim().NoCarriageReturns().Replace("\n", " ").ReplaceAll("  ", " ");
		}

		/// <summary>
		/// Returns a copy of this System.Char converted to uppercase, using the casing rules of the current culture.
		/// </summary>
		/// <param name="c">The current instance.</param>
		/// <returns>A copy of this System.Char converted to uppercase.</returns>
		public static char ToUpper(this char c)
		{
			return c.ToString().ToUpper()[0];
		}

		/// <summary>
		/// Returns a copy of this System.Char converted to lowercase, using the casing rules of the current culture.
		/// </summary>
		/// <param name="c">The current instance.</param>
		/// <returns>A copy of this System.Char converted to lowercase.</returns>
		public static char ToLower(this char c)
		{
			return c.ToString().ToLower()[0];
		}

		#region Split overloads.

		/// <summary>
		/// Returns a string array that contains the substrings in this string that are delimited by the specified string. A parameter specifies whether to return empty array elements.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <param name="separator">A string that delimits the substrings in this string.</param>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by the separator.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string[] Split(this string s, string separator)
		{
#if DOTNET4
			Contract.Requires(s != null);
			Contract.Requires(separator != null);
#endif
			return s.Split(separator, StringSplitOptions.None);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this string that are delimited by the specified string. A parameter specifies whether to return empty array elements.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <param name="separator">A string that delimits the substrings in this string.</param>
		/// <param name="options">RemoveEmptyEntries to omit empty array elements from the array returned; or None to include empty array elements in the array returned.</param>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by the separator.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string[] Split(this string s, string separator, StringSplitOptions options)
		{
#if DOTNET4
			Contract.Requires(s != null);
			Contract.Requires(separator != null);
#endif
			return s.Split(new string[] { separator }, options);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this string that are delimited by the specified char. A parameter specifies whether to return empty array elements.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <param name="separator">A char that delimits the substrings in this string.</param>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by the separator.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string[] Split(this string s, char separator)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			return s.Split(separator, StringSplitOptions.None);
		}

		/// <summary>
		/// Returns a string array that contains the substrings in this string that are delimited by the specified char. A parameter specifies whether to return empty array elements.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <param name="separator">A char that delimits the substrings in this string.</param>
		/// <param name="options">RemoveEmptyEntries to omit empty array elements from the array returned; or None to include empty array elements in the array returned.</param>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by the separator.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static string[] Split(this string s, char separator, StringSplitOptions options)
		{
#if DOTNET4
			Contract.Requires(s != null);
#endif
			return s.Split(new char[] { separator }, options);
		}

		#endregion Split overloads.

		/// <summary>
		/// Compares the current instance to another string using the specified char array to determine the results.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <param name="b">The string to compare to.</param>
		/// <param name="c">The char array.</param>
		/// <param name="caseSensitive">Whether or not the comparison is case-sensitive.</param>
		/// <returns>Whether the current instance is less than, equal to or greater than the specified string.</returns>
		public static int CompareTo(this string s, string b, char[] c, bool caseSensitive = false)
		{
#if DOTNET4
			Contract.Requires(s != null);
			Contract.Requires(b != null);
			Contract.Requires(c != null);
#endif
			string a = s;
			if (!caseSensitive)
			{
				a = a.ToLower();
				b = b.ToLower();

				for (int i = 0; i < c.Length; i++)
				{
					c[i] = Convert.ToChar(c[i].ToString().ToLower());
				}
			}

			if (a == b) return 0;

			for (int i = 0; i < (a.Length > b.Length ? a.Length : b.Length); i++)
			{
				int r = a[i].CompareTo(b[i]);
				if (r == 0) continue;
				return r;
			}

			return 0;
		}

		/// <summary>
		/// Compares the current instance to another char using the specified char array to determine the results.
		/// </summary>
		/// <param name="a">The current instance.</param>
		/// <param name="b">The char to compare to.</param>
		/// <param name="c">The char array.</param>
		/// <returns>Whether the current instance is less than, equal to or greater than the specified char.</returns>
		public static int CompareTo(this char a, char b, char[] c)
		{
#if DOTNET4
			Contract.Requires(c != null);
#endif
			if (a == b) return 0;

			if (!c.Contains(a) || !c.Contains(b)) return ((int)a).CompareTo(b);

			for (int i = 0; i < c.Length; i++)
			{
				if (c[i] == a) return -1;
				if (c[i] == b) return 1;
			}

			return ((int)a).CompareTo(b);
		}

		/// <summary>
		/// Calculates the edit distance between the current instance and the specified string.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <param name="t">The string to compare to.</param>
		/// <param name="caseSensitive">Whether or not to perform a case sensitive comparison.</param>
		/// <returns>The edit distance between the current instance and the specified string.</returns>
		public static int DistanceTo(this string s, string t, bool caseSensitive = true)
		{
			if (!caseSensitive)
			{
				s = s.ToLower();
				t = t.ToLower();
			}

			int n = s.Length;
			int m = t.Length;
			int[,] d = new int[n + 1, m + 1];

			if (n == 0) return m;
			if (m == 0) return n;

			for (int i = 0; i <= n; d[i, 0] = i++) ;
			for (int j = 0; j <= m; d[0, j] = j++) ;

			for (int i = 1; i <= n; i++)
			{
				for (int j = 1; j <= m; j++)
				{
					int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

					d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
				}
			}

			return d[n, m];
		}

		/// <summary>
		/// Whether the current instance equals the specified string, if case is ignored.
		/// </summary>
		/// <param name="a">The current instance.</param>
		/// <param name="b">A string.</param>
		/// <returns>Whether the current instance equals the specified string, if case is ignored.</returns>
		public static bool EqualsIgnoreCase(this string a, string b)
		{
#if DOTNET4
			Contract.Requires(a != null);
			Contract.Requires(b != null);
#endif
			return String.Equals(a, b, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Whether the current instance starts with the specified string, if case is ignored.
		/// </summary>
		/// <param name="a">The current instance.</param>
		/// <param name="b">A string.</param>
		/// <returns>Whether the current instance starts with the specified string, if case is ignored.</returns>
		public static bool StartsWithIgnoreCase(this string a, string b)
		{
#if DOTNET4
			Contract.Requires(a != null);
			Contract.Requires(b != null);
#endif
			return a.StartsWith(b, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Whether the current instance ends with the specified string, if case is ignored.
		/// </summary>
		/// <param name="a">The current instance.</param>
		/// <param name="b">A string.</param>
		/// <returns>Whether the current instance ends with the specified string, if case is ignored.</returns>
		public static bool EndsWithIgnoreCase(this string a, string b)
		{
#if DOTNET4
			Contract.Requires(a != null);
			Contract.Requires(b != null);
#endif
			return a.EndsWith(b, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Whether the current instance contains the specified string, if case is ignored.
		/// </summary>
		/// <param name="a">The current instance.</param>
		/// <param name="b">A string.</param>
		/// <returns>Whether the current instance contains the specified string, if case is ignored.</returns>
		public static bool ContainsIgnoreCase(this string a, string b)
		{
#if DOTNET4
			Contract.Requires(a != null);
			Contract.Requires(b != null);
#endif
			return a.ToLower().Contains(b.ToLower());
		}

		/// <summary>
		/// Whether the current instance is a match to the specified regular expression.
		/// </summary>
		/// <param name="s">The current instance.</param>
		/// <param name="regex">A regular expression.</param>
		/// <returns></returns>
		public static bool IsLike(this string s, string regex)
		{
#if DOTNET4
			Contract.Requires(s != null);
			Contract.Requires(!String.IsNullOrEmpty(regex));
#endif
			return Regex.IsMatch(s, String.Format("^{0}$", regex));
		}

		/// <summary>
		/// Returns the current instance in an upper-lower name variant.
		/// </summary>
		/// <param name="value">The current instance.</param>
		/// <returns>The current instance in an upper-lower name variant.</returns>
		public static string ToUpperLowerNameVariant(this string value)
		{
#if DOTNET4
			Contract.Requires(value != null);
#endif
			char[] valuearray = value.ToLower().ToCharArray();
			bool nextupper = true;

			for (int i = 0; i < (valuearray.Count() - 1); i++)
			{
				if (nextupper)
				{
					valuearray[i] = char.Parse(valuearray[i].ToString().ToUpper());
					nextupper = false;
				}
				else
				{
					switch (valuearray[i])
					{
						case ' ':
						case '-':
						case '.':
						case ':':
						case '\n': nextupper = true; break;
						default: nextupper = false; break;
					}
				}
			}

			return new String(valuearray);
		}

		/// <summary>
		/// Encrypts a string using the supplied key. Encoding is done using RSA encryption.
		/// </summary>
		/// <param name="stringToEncrypt">String that must be encrypted.</param>
		/// <param name="key">An encryption key.</param>
		/// <returns>A string representing a byte array separated by a minus sign.</returns>
		/// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
		public static string Encrypt(this string stringToEncrypt, string key)
		{
#if DOTNET4
			Contract.Requires(!String.IsNullOrEmpty(stringToEncrypt));
			Contract.Requires(!String.IsNullOrEmpty(key));
#endif
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(new CspParameters { KeyContainerName = key }) { PersistKeyInCsp = true })
			{
				return BitConverter.ToString(rsa.Encrypt(Encoding.UTF8.GetBytes(stringToEncrypt), true));
			}
		}

		/// <summary>
		/// Decrypts a string using the supplied key. Decoding is done using RSA encryption.
		/// </summary>
		/// <param name="stringToDecrypt">String that must be decrypted.</param>
		/// <param name="key">Decryption key.</param>
		/// <returns>The decrypted string or null if decryption failed.</returns>
		/// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
		public static string Decrypt(this string stringToDecrypt, string key)
		{
#if DOTNET4
			Contract.Requires(!String.IsNullOrEmpty(stringToDecrypt));
			Contract.Requires(!String.IsNullOrEmpty(key));
#endif
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(new CspParameters { KeyContainerName = key }) { PersistKeyInCsp = true })
			{
				return Encoding.UTF8.GetString(rsa.Decrypt(Array.ConvertAll(stringToDecrypt.Split('-'), (s => Convert.ToByte(Byte.Parse(s, NumberStyles.HexNumber)))), true));
			}
		}

		/// <summary>
		/// Prepends a zero to the current instance, if it's less than 10.
		/// </summary>
		/// <param name="i">The current instance.</param>
		/// <returns>The current instance as a string.</returns>
		public static string AddZeroIfLessThan10(this int i)
		{
			return i > 9 ? i.ToString() : "0" + i;
		}

		/// <summary>
		/// Repeats the specified string N times.
		/// </summary>
		/// <param name="n">The current instance.</param>
		/// <param name="s">The specified string.</param>
		/// <returns>The repeated string.</returns>
		public static string Times(this int n, string s)
		{
#if DOTNET4
			Contract.Requires(n >= 0);
			Contract.Requires(s != null);
#endif
			StringBuilder sb = new StringBuilder(s.Length * n);
			for (int i = 0; i < n; i++) sb.Append(s);
			return sb.ToString();
		}

		/// <summary>
		/// Repeats the specified string N times.
		/// </summary>
		/// <param name="n">The current instance.</param>
		/// <param name="s">The specified string.</param>
		/// <param name="separator">A separator to put between strings.</param>
		/// <returns>The repeated string.</returns>
		public static string Times(this int n, string s, string separator)
		{
#if DOTNET4
			Contract.Requires(n >= 0);
			Contract.Requires(s != null);
			Contract.Requires(separator != null);
#endif
			StringBuilder sb = new StringBuilder((s.Length * n) + ((n - 1) * separator.Length));
			for (int i = 0; i < n; i++)
			{
				if (i != 0) sb.Append(separator);
				sb.Append(s);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Replaces the character at the specified index in the specified string, with the specified character.
		/// </summary>
		/// <param name="s">The specified string.</param>
		/// <param name="i">The index to replace.</param>
		/// <param name="c">The character to replace with.</param>
		/// <returns>The new string.</returns>
		public static string SetCharAt(this string s, int i, char c)
		{
#if DOTNET4
			Contract.Requires(s != null);
			Contract.Requires(i >= 0 && i < s.Length);
#endif
			char[] charArray = s.ToCharArray();
			charArray[i] = c;
			return new String(charArray);
		}
	}
}