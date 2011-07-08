#if DOTNET4

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;
using SharpBag.Strings;
using Fraction = SharpBag.Math.ForInt64.Fraction;

namespace SharpBag.Math.ForFraction.ForInt64
{
	/// <summary>
	/// A matrix.
	/// </summary>
	/// <remarks>http://www.codeproject.com/KB/recipes/matrix.aspx</remarks>
	public class Matrix : MatrixBase<Fraction, Matrix>
	{
		#region Accessors

		/// <summary>
		/// An accessor.
		/// </summary>
		public abstract class Accessor : IEnumerable<Vector>
		{
			/// <summary>
			/// Gets the internal matrix.
			/// </summary>
			public Matrix InternalMatrix { get; private set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="Accessor"/> class.
			/// </summary>
			/// <param name="internalMatrix">The internal matrix.</param>
			internal Accessor(Matrix internalMatrix) { this.InternalMatrix = internalMatrix; }

			/// <summary>
			/// Gets or sets the <see cref="SharpBag.Math.ForFraction.ForInt64.Vector"/> at the specified index.
			/// </summary>
			public abstract Vector this[int row] { get; set; }

			/// <summary>
			/// Gets the enumerator.
			/// </summary>
			/// <returns>The enumerator.</returns>
			public abstract IEnumerator<Vector> GetEnumerator();

			/// <summary>
			/// Returns an enumerator that iterates through a collection.
			/// </summary>
			/// <returns>
			/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
			/// </returns>
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
		}

		/// <summary>
		/// A row acccessor.
		/// </summary>
		public class RowAccessor : Accessor
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="RowAccessor"/> class.
			/// </summary>
			/// <param name="internalMatrix">The internal matrix.</param>
			internal RowAccessor(Matrix internalMatrix) : base(internalMatrix) { }

			/// <summary>
			/// Gets or sets the <see cref="SharpBag.Math.ForFraction.ForInt64.Vector"/> at the specified index.
			/// </summary>
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

			/// <summary>
			/// Gets the enumerator.
			/// </summary>
			/// <returns>
			/// The enumerator.
			/// </returns>
			public override IEnumerator<Vector> GetEnumerator()
			{
				for (int row = 0; row < this.InternalMatrix.RowCount; row++) yield return this[row];
			}
		}

		/// <summary>
		/// A column accessor.
		/// </summary>
		public class ColumnAccessor : Accessor
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="ColumnAccessor"/> class.
			/// </summary>
			/// <param name="internalMatrix">The internal matrix.</param>
			internal ColumnAccessor(Matrix internalMatrix) : base(internalMatrix) { }

			/// <summary>
			/// Gets or sets the <see cref="SharpBag.Math.ForFraction.ForInt64.Vector"/> at the specified index.
			/// </summary>
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

			/// <summary>
			/// Gets the enumerator.
			/// </summary>
			/// <returns>
			/// The enumerator.
			/// </returns>
			public override IEnumerator<Vector> GetEnumerator()
			{
				for (int col = 0; col < this.InternalMatrix.ColumnCount; col++) yield return this[col];
			}
		}

		#endregion Accessors

		#region Properties

		/// <summary>
		/// Gets the rows.
		/// </summary>
		public RowAccessor Rows { get; private set; }

		/// <summary>
		/// Gets the columns.
		/// </summary>
		public ColumnAccessor Columns { get; private set; }

		/// <summary>
		/// Gets the determinant.
		/// </summary>
		public override Fraction Determinant
		{
			get
			{
				if (!this.IsSquare) throw new Exception("Only square matrices have determinants");

				if (this.DeterminantCached) return this.DeterminantCache;
				if (this.RowCount == 0) return 0;
				if (this.RowCount == 1) return this[0, 0];
				if (this.RowCount == 2) return -this[0, 1] * this[1, 0] + this[0, 0] * this[1, 1];
				if (this.RowCount == 3) return -this[0, 2] * this[1, 1] * this[2, 0] + this[0, 1] * this[1, 2] * this[2, 0] + this[0, 2] * this[1, 0] * this[2, 1] - this[0, 0] * this[1, 2] * this[2, 1] - this[0, 1] * this[1, 0] * this[2, 2] + this[0, 0] * this[1, 1] * this[2, 2];

				Fraction det = 0;
				bool negate = false;
				for (int j = 0; j < this.ColumnCount; j++)
				{
					Fraction minorDet = Matrix.Minor(this, 0, j).Determinant;
					det += this[0, j] * (negate ? -minorDet : minorDet);
					negate = !negate;
				}

				this.DeterminantCached = true;
				return this.DeterminantCache = det;
			}
		}

		/// <summary>
		/// Gets the transpose.
		/// </summary>
		public override Matrix Transpose
		{
			get
			{
				Matrix result = new Matrix(this.ColumnCount, this.RowCount);
				MatrixBase<Fraction, Matrix>.InternalTranspose(result, this);
				return result;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is diagonal.
		/// </summary>
		/// <value>
		/// Whether this instance is diagonal.
		/// </value>
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

		/// <summary>
		/// Gets a value indicating whether this instance is an identity.
		/// </summary>
		/// <value>
		/// Whether this instance is an identity.
		/// </value>
		public override bool IsIdentity
		{
			get
			{
				if (!this.IsDiagonal) return false;
				for (int i = 0; i < this.RowCount; i++) if (this[i, i] != 1) return false;
				return true;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is upper triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is upper triangular.
		/// </value>
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

		/// <summary>
		/// Gets a value indicating whether this instance is strictly upper triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is strictly upper triangular.
		/// </value>
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

		/// <summary>
		/// Gets a value indicating whether this instance is lower triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is lower triangular.
		/// </value>
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

		/// <summary>
		/// Gets a value indicating whether this instance is strictly lower triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is strictly lower triangular.
		/// </value>
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

		/// <summary>
		/// Gets a value indicating whether this instance is singular.
		/// </summary>
		/// <value>
		/// Whether this instance is singular.
		/// </value>
		public override bool IsSingular { get { return this.Determinant == 0; } }

		/// <summary>
		/// Gets a value indicating whether this instance is invertible.
		/// </summary>
		/// <value>
		/// Whether this instance is invertible.
		/// </value>
		public override bool IsInvertible { get { return this.Determinant != 0; } }

		#endregion Properties

		#region Constructors / Factories

		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix"/> class.
		/// </summary>
		/// <param name="rows">The number of rows.</param>
		/// <param name="columns">The number of columns.</param>
		public Matrix(int rows, int columns) : base(rows, columns) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix"/> class.
		/// </summary>
		/// <param name="elements">The elements.</param>
		public Matrix(Fraction[,] elements) : base(elements) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix"/> class.
		/// </summary>
		/// <param name="other">Another instance to copy.</param>
		public Matrix(Matrix other) : base(other.Elements) { }

		/// <summary>
		/// Creates an identity matrix of the specified size.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns>The identity matrix.</returns>
		public static Matrix Identity(int size)
		{
			Matrix result = new Matrix(size, size);
			MatrixBase<Fraction, Matrix>.InternalIdentity(result, size, 1);
			return result;
		}

		/// <summary>
		/// Creates a diagonal matrix with the specified diagonals.
		/// </summary>
		/// <param name="diagonals">The diagonals.</param>
		/// <returns>The diagonal matrix.</returns>
		public static Matrix Diagonal(params Fraction[] diagonals)
		{
			Matrix result = new Matrix(diagonals.Length, diagonals.Length);
			MatrixBase<Fraction, Matrix>.InternalDiagonal(result, diagonals);
			return result;
		}

		#endregion Constructors / Factories

		#region Operators

		/// <summary>
		/// Implements the operator +.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Matrix operator +(Matrix left, Matrix right) { return Matrix.Add(left, right); }

		/// <summary>
		/// Implements the operator -.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Matrix operator -(Matrix left, Matrix right) { return Matrix.Subtract(left, right); }

		/// <summary>
		/// Implements the operator -.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Matrix operator -(Matrix matrix) { return Matrix.Negate(matrix); }

		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Matrix operator *(Matrix left, Matrix right) { return Matrix.Multiply(left, right); }

		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right number.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Matrix operator *(Matrix left, Fraction right) { return Matrix.Multiply(left, right); }

		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="left">The left number.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Matrix operator *(Fraction left, Matrix right) { return Matrix.Multiply(left, right); }

		/// <summary>
		/// Implements the operator |.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Matrix operator |(Matrix left, Matrix right) { return Matrix.Augment(left, right); }

		#endregion Operators

		#region Methods

		/// <summary>
		/// Adds the specified matrices.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>The result.</returns>
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

		/// <summary>
		/// Subtracts the specified matrices.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>The result.</returns>
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

		/// <summary>
		/// Multiplies the specified matrices.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>The result.</returns>
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

		/// <summary>
		/// Multiplies the specified elements.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right number.</param>
		/// <returns>The result.</returns>
		public static Matrix Multiply(Matrix left, Fraction right)
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

		/// <summary>
		/// Multiplies the specified elements.
		/// </summary>
		/// <param name="left">The left number.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns></returns>
		public static Matrix Multiply(Fraction left, Matrix right) { return Matrix.Multiply(right, left); }

		/// <summary>
		/// Returns the specified minor of the matrix.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		/// <returns>The specified minor of the matrix.</returns>
		public static Matrix Minor(Matrix matrix, int row, int column)
		{
			Contract.Requires(matrix.RowCount > 0 && matrix.ColumnCount > 0);
			Contract.Requires(row >= 0 && row < matrix.RowCount);
			Contract.Requires(column >= 0 && column < matrix.ColumnCount);

			Matrix result = new Matrix(matrix.RowCount - 1, matrix.ColumnCount - 1);
			MatrixBase<Fraction, Matrix>.InternalMinor(result, matrix, row, column);
			return result;
		}

		/// <summary>
		/// Calculates the echelon form of the matrix.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The echelon form of the matrix.</returns>
		public static Matrix EchelonForm(Matrix matrix)
		{
			Matrix result = matrix.Copy();
			for (int i = 0; i < matrix.RowCount; i++)
			{
				if (result[i, i] == 0)
				{
					for (int j = i + 1; j < result.RowCount; j++)
					{
						if (result[j, i] != 0) MatrixBase<Fraction, Matrix>.SwapRows(result, i, j);
					}
				}

				if (result[i, i] == 0) continue;
				if (result[i, i] != 1)
				{
					for (int j = i + 1; j < result.RowCount; j++)
					{
						if (result[j, i] == 1) MatrixBase<Fraction, Matrix>.SwapRows(result, i, j);
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

		/// <summary>
		/// Calculates the reduced echelon form of the matrix.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The reduced echelon form of the matrix.</returns>
		public static Matrix ReducedEchelonForm(Matrix matrix)
		{
			Matrix result = matrix.Copy();
			for (int i = 0; i < matrix.RowCount; i++)
			{
				if (result[i, i] == 0)
				{
					for (int j = i + 1; j < result.RowCount; j++)
					{
						if (result[j, i] != 0) MatrixBase<Fraction, Matrix>.SwapRows(result, i, j);
					}
				}

				if (result[i, i] == 0) continue;
				if (result[i, i] != 1)
				{
					for (int j = i + 1; j < result.RowCount; j++)
					{
						if (result[j, i] == 1) MatrixBase<Fraction, Matrix>.SwapRows(result, i, j);
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

		/// <summary>
		/// Calculates the adjugate of the matrix.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The adjugate.</returns>
		public static Matrix Adjugate(Matrix matrix)
		{
			Contract.Requires(matrix.IsSquare);
			Matrix result = new Matrix(matrix.RowCount, matrix.ColumnCount);

			for (int i = 0; i < matrix.RowCount; i++)
			{
				for (int j = 0; j < matrix.ColumnCount; j++)
				{
					result[i, j] = Fraction.Pow(-1, i + j) * Matrix.Minor(matrix, i, j).Determinant;
				}
			}

			return result.Transpose;
		}

		/// <summary>
		/// Calculates the inverse of the matrix.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The inverse.</returns>
		public static Matrix Inverse(Matrix matrix)
		{
			Contract.Requires(matrix.IsSquare);
			Contract.Requires(matrix.Determinant != 0);
			return (1 / matrix.Determinant) * Matrix.Adjugate(matrix);
		}

		/// <summary>
		/// Negates the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix Negate(Matrix matrix) { return -1 * matrix; }

		/// <summary>
		/// Augments the specified matrices.
		/// </summary>
		/// <param name="left">The left matrix.</param>
		/// <param name="right">The right matrix.</param>
		/// <returns>The augmented matrix.</returns>
		public static Matrix Augment(Matrix left, Matrix right)
		{
			Contract.Requires(left.RowCount == right.RowCount);
			Matrix result = new Matrix(left.RowCount, left.ColumnCount + right.ColumnCount);
			MatrixBase<Fraction, Matrix>.InternalAugment(result, left, right);
			return result;
		}

		/// <summary>
		/// Creates the accessors.
		/// </summary>
		protected override void CreateAccessors()
		{
			this.Rows = new RowAccessor(this);
			this.Columns = new ColumnAccessor(this);
		}

		#endregion Methods

		#region Casting

		/// <summary>
		/// Performs an implicit conversion from an array of numbers to <see cref="SharpBag.Math.ForFraction.ForInt64.Matrix"/>.
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator Matrix(Fraction[,] elements) { return new Matrix(elements); }

		/// <summary>
		/// Performs an implicit conversion from <see cref="SharpBag.Math.ForFraction.ForInt64.Matrix"/> to an array of numbers.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator Fraction[,](Matrix matrix) { return matrix.Elements; }

		/// <summary>
		/// Performs an explicit conversion from <see cref="SharpBag.Math.ForFraction.ForInt64.Matrix"/> to <see cref="SharpBag.Math.ForFraction.ForInt64.Vector"/>.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator Vector(Matrix matrix)
		{
			Contract.Requires(matrix.IsVector);
			if (matrix.IsColumnVector) return matrix.Columns[0];
			else return matrix.Rows[0];
		}

		#endregion Casting

		#region Comparing / Ordering

		/// <summary>
		/// Whether the current instance is equal to the specified matrix.
		/// </summary>
		/// <param name="other">The specified matrix.</param>
		/// <returns>Whether the current instance is equal to the specified matrix.</returns>
		public override bool Equals(Matrix other) { return MatrixBase<Fraction, Matrix>.InternalEquals(this, other); }

		#endregion Comparing / Ordering

		#region Other

		/// <summary>
		/// Copies this instance.
		/// </summary>
		/// <returns>The copy.</returns>
		public override Matrix Copy() { return new Matrix(this); }

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder().Append('[').AppendLine();

			for (int row = 0; row < this.RowCount; row++)
			{
				sb.Append("\t[ ");

				for (int col = 0; col < this.ColumnCount; col++)
				{
					if (col != 0) sb.Append('\t');
					sb.Append(this[row, col].ToString());
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