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
	/// A matrix.
	/// </summary>
	/// <remarks>http://www.codeproject.com/KB/recipes/matrix.aspx</remarks>
	public class Matrix : MatrixBase<Complex, Matrix>
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
					for (int col = 0; col < v.Dimension; col++) v[col] = this.InternalMatrix[row, col];
					return v;
				}
				set
				{
					if (value.Dimension != this.InternalMatrix.ColumnCount) throw new ArgumentException("Incorrect Vector length");
					for (int col = 0; col < value.Dimension; col++) this.InternalMatrix[row, col] = value[col];
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
					for (int row = 0; row < v.Dimension; row++) v[row] = this.InternalMatrix[row, column];
					return v;
				}
				set
				{
					if (value.Dimension != this.InternalMatrix.RowCount) throw new ArgumentException("Incorrect Vector length");
					for (int row = 0; row < value.Dimension; row++) this.InternalMatrix[row, column] = value[row];
				}
			}

			public override IEnumerator<Vector> GetEnumerator()
			{
				for (int col = 0; col < this.InternalMatrix.ColumnCount; col++) yield return this[col];
			}
		}

		#endregion Accessors

		#region Properties

		public RowAccessor Rows { get; private set; }

		public ColumnAccessor Columns { get; private set; }

		public override Complex Determinant
		{
			get
			{
				if (!this.IsSquare) throw new Exception("Only square matrices have determinants");

				if (this.DeterminantCached) return this.DeterminantCache;
				if (this.RowCount == 0) return 0;
				if (this.RowCount == 1) return this[0, 0];
				if (this.RowCount == 2) return -this[0, 1] * this[1, 0] + this[0, 0] * this[1, 1];
				if (this.RowCount == 3) return -this[0, 2] * this[1, 1] * this[2, 0] + this[0, 1] * this[1, 2] * this[2, 0] + this[0, 2] * this[1, 0] * this[2, 1] - this[0, 0] * this[1, 2] * this[2, 1] - this[0, 1] * this[1, 0] * this[2, 2] + this[0, 0] * this[1, 1] * this[2, 2];

				Complex det = 0;
				bool negate = false;
				for (int j = 0; j < this.ColumnCount; j++)
				{
					Complex minorDet = Matrix.Minor(this, 0, j).Determinant;
					det += this[0, j] * (negate ? -minorDet : minorDet);
					negate = !negate;
				}

				this.DeterminantCached = true;
				return this.DeterminantCache = det;
			}
		}

		public override Matrix Transpose
		{
			get
			{
				Matrix result = new Matrix(this.ColumnCount, this.RowCount);
				MatrixBase<Complex, Matrix>.InternalTranspose(result, this);
				return result;
			}
		}

		public override bool IsDiagonal
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

		public override bool IsIdentity
		{
			get
			{
				if (!this.IsDiagonal) return false;
				for (int i = 0; i < this.RowCount; i++) if (this[i, i] != 1) return false;
				return true;
			}
		}

		public override bool IsUpperTriangular
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

		public override bool IsStrictlyUpperTriangular
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

		public override bool IsLowerTriangular
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

		public override bool IsStrictlyLowerTriangular
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

		public override bool IsSingular { get { return this.Determinant == 0; } }

		public override bool IsInvertible { get { return this.Determinant != 0; } }

		#endregion Properties

		#region Constructors / Factories

		public Matrix(int rows, int columns) : base(rows, columns) { }

		public Matrix(Complex[,] elements) : base(elements) { }

		public Matrix(Matrix other) : base(other.Elements) { }

		public static Matrix Identity(int size)
		{
			Matrix result = new Matrix(size, size);
			MatrixBase<Complex, Matrix>.InternalIdentity(result, size, 1);
			return result;
		}

		public static Matrix Diagonal(params Complex[] diagonals)
		{
			Matrix result = new Matrix(diagonals.Length, diagonals.Length);
			MatrixBase<Complex, Matrix>.InternalDiagonal(result, diagonals);
			return result;
		}

		#endregion Constructors / Factories

		#region Operators

		public static Matrix operator +(Matrix left, Matrix right) { return Matrix.Add(left, right); }

		public static Matrix operator -(Matrix left, Matrix right) { return Matrix.Subtract(left, right); }

		public static Matrix operator -(Matrix matrix) { return Matrix.Negate(matrix); }

		public static Matrix operator *(Matrix left, Matrix right) { return Matrix.Multiply(left, right); }

		public static Matrix operator *(Matrix left, Complex right) { return Matrix.Multiply(left, right); }

		public static Matrix operator *(Complex left, Matrix right) { return Matrix.Multiply(left, right); }

		public static Matrix operator |(Matrix left, Matrix right) { return Matrix.Augment(left, right); }

		#endregion Operators

		#region Methods

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

		public static Matrix Minor(Matrix matrix, int row, int column)
		{
			Contract.Requires(matrix.RowCount > 0 && matrix.ColumnCount > 0);
			Contract.Requires(row >= 0 && row < matrix.RowCount);
			Contract.Requires(column >= 0 && column < matrix.ColumnCount);

			Matrix result = new Matrix(matrix.RowCount - 1, matrix.ColumnCount - 1);
			MatrixBase<Complex, Matrix>.InternalMinor(result, matrix, row, column);
			return result;
		}

		public static Matrix EchelonForm(Matrix matrix)
		{
			Matrix result = matrix.Copy();
			for (int i = 0; i < matrix.RowCount; i++)
			{
				if (result[i, i] == 0)
				{
					for (int j = i + 1; j < result.RowCount; j++)
					{
						if (result[j, i] != 0) MatrixBase<Complex, Matrix>.SwapRows(result, i, j);
					}
				}

				if (result[i, i] == 0) continue;
				if (result[i, i] != 1)
				{
					for (int j = i + 1; j < result.RowCount; j++)
					{
						if (result[j, i] == 1) MatrixBase<Complex, Matrix>.SwapRows(result, i, j);
					}
				}

				result.Rows[i] *= 1 / result[i, i];
				for (int j = i + 1; j < result.RowCount; j++)
				{
					result.Rows[j] += result.Rows[i] * -result[j, i];
				}
			}

			return result;
		}

		public static Matrix ReducedEchelonForm(Matrix matrix)
		{
			Matrix result = matrix.Copy();
			for (int i = 0; i < matrix.RowCount; i++)
			{
				if (result[i, i] == 0)
				{
					for (int j = i + 1; j < result.RowCount; j++)
					{
						if (result[j, i] != 0) MatrixBase<Complex, Matrix>.SwapRows(result, i, j);
					}
				}

				if (result[i, i] == 0) continue;
				if (result[i, i] != 1)
				{
					for (int j = i + 1; j < result.RowCount; j++)
					{
						if (result[j, i] == 1) MatrixBase<Complex, Matrix>.SwapRows(result, i, j);
					}
				}

				result.Rows[i] *= 1 / result[i, i];
				for (int j = i + 1; j < result.RowCount; j++)
				{
					result.Rows[j] += result.Rows[i] * -result[j, i];
				}

				for (int j = i - 1; j >= 0; j--)
				{
					result.Rows[j] += result.Rows[i] * -result[j, i];
				}
			}

			return result;
		}

		public static Matrix Adjoint(Matrix matrix)
		{
			Contract.Requires(matrix.IsSquare);
			Matrix result = new Matrix(matrix.RowCount, matrix.ColumnCount);

			for (int i = 0; i < matrix.RowCount; i++)
			{
				for (int j = 0; j < matrix.ColumnCount; j++)
				{
					result[i, j] = Complex.Pow(-1, i + j) * Matrix.Minor(matrix, i, j).Determinant;
				}
			}

			return result.Transpose;
		}

		public static Matrix Inverse(Matrix matrix)
		{
			Contract.Requires(matrix.IsSquare);
			Contract.Requires(matrix.Determinant != 0);
			return (1 / matrix.Determinant) * Matrix.Adjoint(matrix);
		}

		public static Matrix Negate(Matrix matrix) { return -1 * matrix; }

		public static Matrix Augment(Matrix left, Matrix right)
		{
			Contract.Requires(left.RowCount == right.RowCount);
			Matrix result = new Matrix(left.RowCount, left.ColumnCount + right.ColumnCount);
			MatrixBase<Complex, Matrix>.InternalAugment(result, left, right);
			return result;
		}

		protected override void CreateAccessors()
		{
			this.Rows = new RowAccessor(this);
			this.Columns = new ColumnAccessor(this);
		}

		#endregion Methods

		#region Casting

		public static implicit operator Matrix(Complex[,] elements) { return new Matrix(elements); }

		public static implicit operator Complex[,](Matrix matrix) { return matrix.Elements; }

		public static explicit operator Vector(Matrix matrix)
		{
			Contract.Requires(matrix.IsVector);
			if (matrix.IsColumnVector) return matrix.Columns[0];
			else return matrix.Rows[0];
		}

		#endregion Casting

		#region Comparing / Ordering

		public override bool Equals(Matrix other) { return MatrixBase<Complex, Matrix>.InternalEquals(this, other); }

		#endregion Comparing / Ordering

		#region Other

		public override Matrix Copy() { return new Matrix(this); }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder().Append('[').AppendLine();

			for (int row = 0; row < this.RowCount; row++)
			{
				sb.Append("\t[ ");

				for (int col = 0; col < this.ColumnCount; col++)
				{
					if (col != 0) sb.Append('\t');
					sb.Append(this[row, col].ToComplexString());
				}

				sb.Append(" ]");
				if (row != this.RowCount - 1) sb.Append(',');
				sb.AppendLine();
			}

			return sb.Append(']').ToString();
		}

		#endregion Other
	}
}

#endif