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
	public class Vector : VectorBase<Complex, Vector>
	{
		#region Properties

		public override Complex Length
		{
			get
			{
				Complex sum = 0;
				for (int i = 0; i < this.Dimension; i++) sum += this.Elements[i] * this.Elements[i];
				return Complex.Sqrt(sum);
			}
		}

		public override Vector Reverse
		{
			get
			{
				Vector result = new Vector(this.Dimension);
				VectorBase<Complex, Vector>.InternalReverse(result, this);
				return result;
			}
		}

		#endregion Properties

		#region Constructors

		public Vector(int dimension) : base(dimension) { }

		public Vector(Complex[] elements) : base(elements) { }

		public Vector(Vector other) : base(other.Elements) { }

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
			VectorBase<Complex, Vector>.InternalToColumnMatrix(result, this);
			return result;
		}

		public Matrix ToRowMatrix()
		{
			Matrix result = new Matrix(1, this.Dimension);
			VectorBase<Complex, Vector>.InternalToRowMatrix(result, this);
			return result;
		}

		#endregion Methods

		#region Comparing / Ordering

		public override bool Equals(Vector other) { return VectorBase<Complex, Vector>.InternalEquals(this, other); }

		#endregion Comparing / Ordering

		#region Other

		public override Vector Copy() { return new Vector(this); }

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