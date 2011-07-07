using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharpBag.Math
{
	public class Matrix
	{
		#region Accessors

		public abstract class Accessor : IEnumerable<Vector>
		{
			public Matrix InternalMatrix { get; private set; }

			internal Accessor(Matrix internalMatrix) { this.InternalMatrix = internalMatrix; }

			public abstract Vector this[int row] { get; set; }

			public abstract IEnumerator<Vector> GetEnumerator();

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
		}

		public class RowAccessor : Accessor
		{
			internal RowAccessor(Matrix internalMatrix) : base(internalMatrix) { }

			public override Vector this[int row]
			{
				get
				{
					Vector v = new Vector(this.InternalMatrix.ColumnCount);
					for (int col = 0; col < v.Length; col++) v[col] = this.InternalMatrix[row, col];
					return v;
				}
				set
				{
					Contract.Requires(value.Length == this.InternalMatrix.ColumnCount);
					for (int col = 0; col < value.Length; col++) this.InternalMatrix[row, col] = value[col];
				}
			}

			public override IEnumerator<Vector> GetEnumerator()
			{
				for (int row = 0; row < this.InternalMatrix.RowCount; row++) yield return this[row];
			}
		}

		public class ColumnAccessor : Accessor
		{
			internal ColumnAccessor(Matrix internalMatrix) : base(internalMatrix) { }

			public override Vector this[int column]
			{
				get
				{
					Vector v = new Vector(this.InternalMatrix.RowCount);
					for (int row = 0; row < v.Length; row++) v[row] = this.InternalMatrix[row, column];
					return v;
				}
				set
				{
					Contract.Requires(value.Length == this.InternalMatrix.RowCount);
					for (int row = 0; row < value.Length; row++) this.InternalMatrix[row, column] = value[row];
				}
			}

			public override IEnumerator<Vector> GetEnumerator()
			{
				for (int col = 0; col < this.InternalMatrix.ColumnCount; col++) yield return this[col];
			}
		}

		#endregion Accessors

		#region Properties

		private Complex[,] Elements;

		public int RowCount { get; private set; }

		public int ColumnCount { get; private set; }

		public RowAccessor Rows { get; private set; }

		public ColumnAccessor Columns { get; private set; }

		public Matrix Transpose
		{
			get
			{
				Matrix result = new Matrix(this.ColumnCount, this.RowCount);
				for (int row = 0; row < this.RowCount; row++)
				{
					for (int col = 0; col < this.ColumnCount; col++)
					{
						result[col, row] = this[row, col];
					}
				}

				return result;
			}
		}

		public bool IsSquare { get { return this.RowCount == this.ColumnCount; } }

		public bool IsRowVector { get { return this.RowCount == 1; } }

		public bool IsColumnVector { get { return this.ColumnCount == 1; } }

		public bool IsVector { get { return this.IsRowVector || this.IsColumnVector; } }

		public bool IsDiagonal
		{
			get
			{
				if (!this.IsSquare) return false;

				for (int row = 0; row < this.RowCount; row++)
				{
					for (int col = 0; col < this.ColumnCount; col++)
					{
						if (this[row, col] != 0 && row != col) return false;
					}
				}

				return true;
			}
		}

		public bool IsIdentity
		{
			get
			{
				if (!this.IsDiagonal) return false;
				for (int i = 0; i < this.RowCount; i++) if (this[i, i] != 1) return false;
				return true;
			}
		}

		public bool IsSymmetric
		{
			get
			{
				if (!this.IsSquare) return false;

				for (int row = 0; row < this.RowCount; row++)
				{
					for (int col = 0; col < this.ColumnCount; col++)
					{
						if (this[row, col] != this[col, row]) return false;
					}
				}

				return true;
			}
		}

		public bool IsUpperTriangular
		{
			get
			{
				if (!this.IsSquare) return false;

				for (int row = 0; row < this.RowCount; row++)
				{
					for (int col = 0; col < this.ColumnCount; col++)
					{
						if (this[row, col] != 0 && row > col) return false;
					}
				}

				return true;
			}
		}

		public bool IsStrictlyUpperTriangular
		{
			get
			{
				if (!this.IsSquare) return false;

				for (int row = 0; row < this.RowCount; row++)
				{
					for (int col = 0; col < this.ColumnCount; col++)
					{
						if (this[row, col] != 0 && row >= col) return false;
					}
				}

				return true;
			}
		}

		public bool IsLowerTriangular
		{
			get
			{
				if (!this.IsSquare) return false;

				for (int row = 0; row < this.RowCount; row++)
				{
					for (int col = 0; col < this.ColumnCount; col++)
					{
						if (this[row, col] != 0 && row < col) return false;
					}
				}

				return true;
			}
		}

		public bool IsStrictlyLowerTriangular
		{
			get
			{
				if (!this.IsSquare) return false;

				for (int row = 0; row < this.RowCount; row++)
				{
					for (int col = 0; col < this.ColumnCount; col++)
					{
						if (this[row, col] != 0 && row <= col) return false;
					}
				}

				return true;
			}
		}

		public bool IsTriangular { get { return this.IsUpperTriangular || this.IsLowerTriangular; } }

		public bool IsStrictlyTriangular { get { return this.IsStrictlyLowerTriangular || this.IsStrictlyUpperTriangular; } }

		#endregion Properties

		public Matrix()
		{
			this.Rows = new RowAccessor(this);
			this.Columns = new ColumnAccessor(this);
		}

		public Matrix(int rows, int columns)
			: this()
		{
			this.RowCount = rows;
			this.ColumnCount = columns;
			this.Elements = new Complex[this.RowCount, this.ColumnCount];
		}

		public Matrix(Complex[,] elements)
			: this(elements.GetLength(0), elements.GetLength(1))
		{
			for (int row = 0; row < this.RowCount; row++)
			{
				for (int col = 0; col < this.ColumnCount; col++)
				{
					this.Elements[row, col] = elements[row, col];
				}
			}
		}

		public Matrix(Matrix other) : this(other.Elements) { }

		public static Matrix Identity(int size)
		{
			Matrix result = new Matrix(size, size);
			for (int i = 0; i < size; i++) result[i, i] = 1;
			return result;
		}

		public static Matrix Diagonal(params Complex[] diagonals)
		{
			Matrix result = new Matrix(diagonals.Length, diagonals.Length);
			for (int i = 0; i < diagonals.Length; i++) result[i, i] = diagonals[i];
			return result;
		}

		public static Matrix operator +(Matrix left, Matrix right) { return Matrix.Add(left, right); }

		public static Matrix operator -(Matrix left, Matrix right) { return Matrix.Subtract(left, right); }

		public static Matrix operator *(Matrix left, Matrix right) { return Matrix.Multiply(left, right); }

		public static Matrix operator *(Matrix left, Complex right) { return Matrix.Multiply(left, right); }

		public static Matrix operator *(Complex left, Matrix right) { return Matrix.Multiply(left, right); }

		public static Matrix Add(Matrix left, Matrix right)
		{
			Contract.Requires(left.RowCount == right.RowCount && left.ColumnCount == right.ColumnCount);
			Matrix result = new Matrix(left);

			for (int row = 0; row < left.RowCount; row++)
			{
				for (int col = 0; col < left.ColumnCount; col++)
				{
					result[row, col] += right[row, col];
				}
			}

			return result;
		}

		public static Matrix Subtract(Matrix left, Matrix right)
		{
			Contract.Requires(left.RowCount == right.RowCount && left.ColumnCount == right.ColumnCount);
			Matrix result = new Matrix(left);

			for (int row = 0; row < left.RowCount; row++)
			{
				for (int col = 0; col < left.ColumnCount; col++)
				{
					result[row, col] -= right[row, col];
				}
			}

			return result;
		}

		public static Matrix Multiply(Matrix left, Matrix right)
		{
			Contract.Requires(left.ColumnCount == right.RowCount);
			Matrix result = new Matrix(left.RowCount, right.ColumnCount);

			for (int row = 0; row < left.RowCount; row++)
			{
				for (int col = 0; col < right.ColumnCount; col++)
				{
					// result[row, col] = Vector.DotProduct(left.Rows[row], right.Columns[col]);

					for (int i = 0; i < left.ColumnCount; i++)
					{
						result[row, col] += left[row, i] * right[i, col];
					}
				}
			}

			return result;
		}

		public static Matrix Multiply(Matrix left, Complex right)
		{
			Matrix result = new Matrix(left);

			for (int row = 0; row < left.RowCount; row++)
			{
				for (int col = 0; col < left.ColumnCount; col++)
				{
					result[row, col] *= right;
				}
			}

			return result;
		}

		public static Matrix Multiply(Complex left, Matrix right) { return Matrix.Multiply(right, left); }

		public Complex this[int row, int column] { get { return this.Elements[row, column]; } set { this.Elements[row, column] = value; } }

		public static implicit operator Matrix(Complex[,] elements) { return new Matrix(elements); }

		public static implicit operator Complex[,](Matrix matrix) { return matrix.Elements; }

		public Matrix Copy() { return new Matrix(this); }

		internal static void ComplexToString(StringBuilder sb, Complex c)
		{
			if (c.Real == 0)
			{
				if (c.Imaginary == 1) sb.Append('i');
				else if (c.Imaginary == 0) sb.Append('0');
				else sb.Append(c.Imaginary).Append('i');
			}
			else
			{
				if (c.Imaginary == 1) sb.Append(c.Real).Append(" + i");
				else if (c.Imaginary > 0) sb.Append(c.Real).Append(" + ").Append(c.Imaginary).Append("i");
				else if (c.Imaginary < 0) sb.Append(c.Real).Append(" - ").Append(-c.Imaginary).Append("i");
				else sb.Append(c.Real);
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder().Append('[').AppendLine();

			for (int row = 0; row < this.RowCount; row++)
			{
				sb.Append("\t[ ");

				for (int col = 0; col < this.ColumnCount; col++)
				{
					if (col != 0) sb.Append('\t');
					ComplexToString(sb, this[row, col]);
				}

				sb.Append(" ]");
				if (row != this.RowCount - 1) sb.Append(',');
				sb.AppendLine();
			}

			return sb.Append(']').ToString();
		}
	}
}