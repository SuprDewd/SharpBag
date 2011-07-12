using System;
using System.Linq;
using System.Text;
using SharpBag.Strings;

#if DOTNET4

using System.Diagnostics.Contracts;

#endif

namespace SharpBag.Media.Drawing.Ascii
{
	/// <summary>
	/// A class for drawing shapes in ASCII.
	/// </summary>
	public static class DrawAscii
	{
		/// <summary>
		/// Draw a square.
		/// </summary>
		/// <param name="rows">The number of rows.</param>
		/// <param name="cols">The number of columns.</param>
		/// <param name="border">The border.</param>
		/// <param name="fill">The fill.</param>
		/// <returns>The drawn square.</returns>
		public static string Square(int rows, int cols, char border = '#', char fill = ' ')
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < rows; i++)
			{
				if (i != 0) sb.AppendLine();

				for (int j = 0; j < cols; j++)
				{
					sb.Append((i == 0 || j == 0 || i == rows - 1 || j == cols - 1) ? border : fill);
				}
			}

			return sb.ToString();
		}

		private static string ReverseLines(string s)
		{
			return String.Join("\n", s.Split('\n').Select(i => i.Reverse()));
		}

		/// <summary>
		/// Draw a triangle.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <param name="border">The border.</param>
		/// <param name="fill">The fill.</param>
		/// <returns>The drawn triangle.</returns>
		public static string TriangleTL(int size, char border = '#', char fill = ' ')
		{
#if DOTNET4
			Contract.Requires(size >= 0);
#endif
			return DrawAscii.ReverseLines(DrawAscii.TriangleTR(size, border, fill));
		}

		/// <summary>
		/// Draw a triangle.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <param name="border">The border.</param>
		/// <param name="fill">The fill.</param>
		/// <returns>The drawn triangle.</returns>
		public static string TriangleTR(int size, char border = '#', char fill = ' ')
		{
#if DOTNET4
			Contract.Requires(size >= 0);
#endif
			if (size == 0) return "";
			return String.Join("\n", size.To(1).Select(i => i == size ? new String(border, i) : String.Join("", new string[] { new String(' ', System.Math.Max(size - i, 0)), border.ToString(), new String(fill, System.Math.Max(i - 2, 0)), i == 1 ? "" : border.ToString() })).ToArray());
		}

		/// <summary>
		/// Draw a triangle.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <param name="border">The border.</param>
		/// <param name="fill">The fill.</param>
		/// <returns>The drawn triangle.</returns>
		public static string TriangleBR(int size, char border = '#', char fill = ' ')
		{
#if DOTNET4
			Contract.Requires(size >= 0);
#endif
			if (size == 0) return "";
			return String.Join("\n", 1.To(size).Select(i => i == size ? new String(border, i) : String.Join("", new string[] { new String(' ', System.Math.Max(size - i, 0)), border.ToString(), new String(fill, System.Math.Max(i - 2, 0)), i == 1 ? "" : border.ToString() })).ToArray());
		}

		/// <summary>
		/// Draw a triangle.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <param name="border">The border.</param>
		/// <param name="fill">The fill.</param>
		/// <returns>The drawn triangle.</returns>
		public static string TriangleBL(int size, char border = '#', char fill = ' ')
		{
#if DOTNET4
			Contract.Requires(size >= 0);
#endif
			return DrawAscii.ReverseLines(DrawAscii.TriangleBR(size, border, fill));
		}
	}
}