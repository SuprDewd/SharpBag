#if DOTNET4

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;
using SharpBag.Strings;

namespace SharpBag.Math.ForComplex
{
	/// <summary>
	/// A vector.
	/// </summary>
	public class Vector : IEnumerable<Complex>, ICloneable, IEquatable<Vector>
	{
		#region Properties

		private Complex[] Elements;

		public int Dimension { get; private set; }

		public Complex Length
		{
			get
			{
				Complex sum = 0;
				for (int i = 0; i < this.Dimension; i++) sum += this.Elements[i] * this.Elements[i];
				return Complex.Sqrt(sum);
			}
		}

		public Complex this[int i] { get { return this.Elements[i]; } set { this.Elements[i] = value; } }

		#endregion Properties

		#region Constructors

		public Vector(int dimension)
		{
			this.Dimension = dimension;
			this.Elements = new Complex[dimension];
		}

		public Vector(Complex[] elements)
			: this(elements.Length)
		{
			for (int i = 0; i < elements.Length; i++) this[i] = elements[i];
		}

		public Vector(Vector other) : this(other.Elements) { }

		#endregion Constructors

		#region Casting

		public static implicit operator Vector(Complex[] elements) { return new Vector(elements); }

		public static implicit operator Complex[](Vector vector) { return vector.Elements; }

		#endregion Casting

		#region Operators

		public static Vector operator +(Vector left, Vector right) { return Vector.Add(left, right); }

		public static Vector operator -(Vector left, Vector right) { return Vector.Subtract(left, right); }

		public static Vector operator -(Vector vector) { return Vector.Negate(vector); }

		public static Vector operator *(Vector left, Complex right) { return Vector.Multiply(left, right); }

		public static Vector operator *(Complex left, Vector right) { return Vector.Multiply(left, right); }

		#endregion Operators

		#region Methods

		public static Vector Add(Vector left, Vector right)
		{
			Contract.Requires(left.Dimension == right.Dimension);
			Vector result = new Vector(left);
			for (int i = 0; i < result.Dimension; i++) result[i] += right[i];
			return result;
		}

		public static Vector Subtract(Vector left, Vector right)
		{
			Contract.Requires(left.Dimension == right.Dimension);
			Vector result = new Vector(left);
			for (int i = 0; i < result.Dimension; i++) result[i] -= right[i];
			return result;
		}

		public static Vector Multiply(Vector left, Complex right)
		{
			Vector result = new Vector(left);
			for (int i = 0; i < result.Dimension; i++) result[i] *= right;
			return result;
		}

		public static Vector Multiply(Complex left, Vector right) { return Vector.Multiply(right, left); }

		public static Vector Negate(Vector vector) { return -1 * vector; }

		public static Complex DotProduct(Vector left, Vector right)
		{
			Contract.Requires(left.Dimension == right.Dimension);
			Complex sum = 0;
			for (int i = 0; i < left.Dimension; i++) sum += left[i] * right[i];
			return sum;
		}

		public static Vector CrossProduct(Vector left, Vector right)
		{
			Contract.Requires(left.Dimension == 3 && right.Dimension == 3);
			return new Complex[] { -left[2] * right[1] + left[1] * right[2], left[2] * right[0] - left[0] * right[2], -left[1] * right[0] + left[0] * right[1] };
		}

		public Matrix ToColumnMatrix()
		{
			Matrix result = new Matrix(this.Dimension, 1);
			for (int i = 0; i < this.Dimension; i++) result[i, 0] = this[i];
			return result;
		}

		public Matrix ToRowMatrix()
		{
			Matrix result = new Matrix(1, this.Dimension);
			for (int i = 0; i < this.Dimension; i++) result[0, i] = this[i];
			return result;
		}

		#endregion Methods

		#region Comparing / Ordering

		public bool Equals(Vector other)
		{
			if (this.Dimension != other.Dimension) return false;
			for (int i = 0; i < this.Dimension; i++) if (!this[i].Equals(other[i])) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			return obj is Vector && this.Equals(obj as Vector);
		}

		#endregion Comparing / Ordering

		#region Other

		public IEnumerator<Complex> GetEnumerator()
		{
			for (int i = 0; i < this.Dimension; i++)
			{
				yield return this[i];
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public Vector Copy() { return new Vector(this); }

		public object Clone() { return this.Copy(); }

		public override int GetHashCode()
		{
			int hash = 0;
			for (int i = 0; i < this.Dimension; i++) hash ^= this[i].GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder().Append("[ ");
			for (int i = 0; i < this.Dimension; i++)
			{
				if (i != 0) sb.Append('\t');
				sb.Append(this[i].ToComplexString());
			}

			return sb.Append(" ]").ToString();
		}

		#endregion Other
	}
}

#endif