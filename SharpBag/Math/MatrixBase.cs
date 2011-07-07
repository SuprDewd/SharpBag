using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math
{
	/// <summary>
	/// A matrix base class.
	/// </summary>
	/// <typeparam name="T">The type of elements.</typeparam>
	/// <typeparam name="M">The child class.</typeparam>
	public abstract class MatrixBase<T, M> : IEquatable<M>, ICloneable
	{
		/// <summary>
		/// Gets the elements.
		/// </summary>
		protected T[,] Elements { get; private set; }

		/// <summary>
		/// Whether the determinant is cached.
		/// </summary>
		protected bool DeterminantCached = false;

		/// <summary>
		/// The cached determinant.
		/// </summary>
		protected T DeterminantCache;

		/// <summary>
		/// Gets the row count.
		/// </summary>
		public int RowCount { get; private set; }

		/// <summary>
		/// Gets the column count.
		/// </summary>
		public int ColumnCount { get; private set; }

		/// <summary>
		/// Gets or sets the element at the specified row and column.
		/// </summary>
		public T this[int row, int column] { get { return this.Elements[row, column]; } set { this.Elements[row, column] = value; } }

		/// <summary>
		/// Gets the determinant.
		/// </summary>
		public abstract T Determinant { get; }

		/// <summary>
		/// Gets the transpose.
		/// </summary>
		public abstract M Transpose { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is square.
		/// </summary>
		/// <value>
		/// Whether this instance is square.
		/// </value>
		public bool IsSquare { get { return this.RowCount == this.ColumnCount; } }

		/// <summary>
		/// Gets a value indicating whether this instance is a row vector.
		/// </summary>
		/// <value>
		/// Whether this instance is a row vector.
		/// </value>
		public bool IsRowVector { get { return this.RowCount == 1; } }

		/// <summary>
		/// Gets a value indicating whether this instance is a column vector.
		/// </summary>
		/// <value>
		/// Whether this instance is a column vector.
		/// </value>
		public bool IsColumnVector { get { return this.ColumnCount == 1; } }

		/// <summary>
		/// Gets a value indicating whether this instance is a vector.
		/// </summary>
		/// <value>
		/// Whether this instance is a vector.
		/// </value>
		public bool IsVector { get { return this.IsRowVector || this.IsColumnVector; } }

		/// <summary>
		/// Gets a value indicating whether this instance is diagonal.
		/// </summary>
		/// <value>
		/// Whether this instance is diagonal.
		/// </value>
		public abstract bool IsDiagonal { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is an identity.
		/// </summary>
		/// <value>
		/// Whether this instance is an identity.
		/// </value>
		public abstract bool IsIdentity { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is upper triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is upper triangular.
		/// </value>
		public abstract bool IsUpperTriangular { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is strictly upper triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is strictly upper triangular.
		/// </value>
		public abstract bool IsStrictlyUpperTriangular { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is lower triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is lower triangular.
		/// </value>
		public abstract bool IsLowerTriangular { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is strictly lower triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is strictly lower triangular.
		/// </value>
		public abstract bool IsStrictlyLowerTriangular { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is singular.
		/// </summary>
		/// <value>
		/// Whether this instance is singular.
		/// </value>
		public abstract bool IsSingular { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is invertible.
		/// </summary>
		/// <value>
		/// Whether this instance is invertible.
		/// </value>
		public abstract bool IsInvertible { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is symmetric.
		/// </summary>
		/// <value>
		/// Whether this instance is symmetric.
		/// </value>
		public virtual bool IsSymmetric
		{
			get
			{
				if (!this.IsSquare) return false;

				for (int row = 0; row < this.RowCount; row++)
				{
					for (int col = 0; col < this.ColumnCount; col++)
					{
						if (!this[row, col].Equals(this[col, row])) return false;
					}
				}

				return true;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is triangular.
		/// </value>
		public bool IsTriangular { get { return this.IsUpperTriangular || this.IsLowerTriangular; } }

		/// <summary>
		/// Gets a value indicating whether this instance is strictly triangular.
		/// </summary>
		/// <value>
		/// Whether this instance is strictly triangular.
		/// </value>
		public bool IsStrictlyTriangular { get { return this.IsStrictlyLowerTriangular || this.IsStrictlyUpperTriangular; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="MatrixBase&lt;T, M&gt;"/> class.
		/// </summary>
		/// <param name="rows">The number of rows.</param>
		/// <param name="columns">The number of columns.</param>
		public MatrixBase(int rows, int columns)
		{
			this.RowCount = rows;
			this.ColumnCount = columns;
			this.Elements = new T[this.RowCount, this.ColumnCount];
			this.CreateAccessors();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MatrixBase&lt;T, M&gt;"/> class.
		/// </summary>
		/// <param name="elements">The elements.</param>
		public MatrixBase(T[,] elements)
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

		internal static void InternalTranspose(MatrixBase<T, M> result, MatrixBase<T, M> matrix)
		{
			for (int row = 0; row < matrix.RowCount; row++)
			{
				for (int col = 0; col < matrix.ColumnCount; col++)
				{
					result[col, row] = matrix[row, col];
				}
			}
		}

		internal static void InternalMinor(MatrixBase<T, M> result, MatrixBase<T, M> matrix, int row, int column)
		{
			int newRow = 0;
			for (int i = 0; i < matrix.RowCount; i++)
			{
				if (i == row) continue;
				int newColumn = 0;
				for (int j = 0; j < matrix.ColumnCount; j++)
				{
					if (j == column) continue;
					result[newRow, newColumn++] = matrix[i, j];
				}

				newRow++;
			}
		}

		internal static void InternalDiagonal(MatrixBase<T, M> result, T[] diagonals)
		{
			for (int i = 0; i < diagonals.Length; i++) result[i, i] = diagonals[i];
		}

		internal static void InternalIdentity(MatrixBase<T, M> result, int size, T unit)
		{
			for (int i = 0; i < size; i++) result[i, i] = unit;
		}

		internal static void InternalAugment(MatrixBase<T, M> result, MatrixBase<T, M> left, MatrixBase<T, M> right)
		{
			int col = 0;

			for (int cCol = 0; cCol < left.ColumnCount; cCol++)
			{
				for (int row = 0; row < left.RowCount; row++)
				{
					result[row, col] = left[row, cCol];
				}

				col++;
			}

			for (int cCol = 0; cCol < right.ColumnCount; cCol++)
			{
				for (int row = 0; row < right.RowCount; row++)
				{
					result[row, col] = right[row, cCol];
				}

				col++;
			}
		}

		internal static bool InternalEquals(MatrixBase<T, M> left, MatrixBase<T, M> right)
		{
			if (left.RowCount != right.RowCount || left.ColumnCount != right.ColumnCount) return false;
			for (int row = 0; row < left.RowCount; row++)
			{
				for (int col = 0; col < left.ColumnCount; col++)
				{
					if (!left[row, col].Equals(right[row, col])) return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			int hash = 0;
			for (int row = 0; row < this.RowCount; row++)
			{
				for (int col = 0; col < this.ColumnCount; col++)
				{
					hash ^= this[row, col].GetHashCode();
				}
			}

			return hash;
		}

		/// <summary>
		/// Swaps the rows.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="i">The i.</param>
		/// <param name="j">The j.</param>
		internal static void SwapRows(MatrixBase<T, M> result, int i, int j)
		{
			for (int k = 0; k < result.ColumnCount; k++)
			{
				T temp = result[i, k];
				result[i, k] = result[j, k];
				result[j, k] = temp;
			}
		}

		/// <summary>
		/// Creates the accessors.
		/// </summary>
		protected abstract void CreateAccessors();

		/// <summary>
		/// Copies this instance.
		/// </summary>
		/// <returns>The copy.</returns>
		public abstract M Copy();

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public object Clone() { return this.Copy(); }

		/// <summary>
		/// Determines whether the specified matrix is equal to this instance.
		/// </summary>
		/// <param name="other">The matrix to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified matrix is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool Equals(M other);

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) { return obj.GetType() == typeof(M) && this.Equals((M)obj); }
	}
}