using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math
{
	public class Vector
	{
		public Complex[] Elements;

		public int Length { get; private set; }

		public Vector(int length)
		{
			this.Length = length;
			this.Elements = new Complex[length];
		}

		public Vector(Complex[] elements)
			: this(elements.Length)
		{
			for (int i = 0; i < elements.Length; i++) this[i] = elements[i];
		}

		public Vector(Vector other) : this(other.Elements) { }

		public Complex this[int i] { get { return this.Elements[i]; } set { this.Elements[i] = value; } }

		public static implicit operator Vector(Complex[] elements) { return new Vector(elements); }

		public static implicit operator Complex[](Vector vector) { return vector.Elements; }

		public static Vector operator +(Vector left, Vector right) { return Vector.Add(left, right); }

		public static Vector operator -(Vector left, Vector right) { return Vector.Subtract(left, right); }

		public static Vector operator *(Vector left, Complex right) { return Vector.Multiply(left, right); }

		public static Vector operator *(Complex left, Vector right) { return Vector.Multiply(left, right); }

		public static Vector Add(Vector left, Vector right)
		{
			Contract.Requires(left.Length == right.Length);
			Vector result = new Vector(left);
			for (int i = 0; i < result.Length; i++) result[i] += right[i];
			return result;
		}

		public static Vector Subtract(Vector left, Vector right)
		{
			Contract.Requires(left.Length == right.Length);
			Vector result = new Vector(left);
			for (int i = 0; i < result.Length; i++) result[i] -= right[i];
			return result;
		}

		public static Vector Multiply(Vector left, Complex right)
		{
			Vector result = new Vector(left);
			for (int i = 0; i < result.Length; i++) result[i] *= right;
			return result;
		}

		public static Vector Multiply(Complex left, Vector right) { return Vector.Multiply(right, left); }

		public static Complex DotProduct(Vector left, Vector right)
		{
			Contract.Requires(left.Length == right.Length);
			Complex sum = 0;
			for (int i = 0; i < left.Length; i++) sum += left[i] * right[i];
			return sum;
		}

		public static Vector CrossProduct(Vector left, Vector right)
		{
			Contract.Requires(left.Length == 3 && right.Length == 3);
			return new Complex[] { -left[2] * right[1] + left[1] * right[2], left[2] * right[0] - left[0] * right[2], -left[1] * right[0] + left[0] * right[1] };
		}

		public Vector Copy() { return new Vector(this); }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder().Append("[ ");
			for (int i = 0; i < this.Length; i++)
			{
				if (i != 0) sb.Append('\t');
				Matrix.ComplexToString(sb, this[i]);
			}

			return sb.Append(" ]").ToString();
		}
	}
}