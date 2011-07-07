using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math
{
	public abstract class MatrixBase<T, M> : IEquatable<M>, ICloneable
	{
		protected T[,] Elements { get; private set; }

		protected bool DeterminantCached = false;
		protected T DeterminantCache;

		public int RowCount { get; private set; }

		public int ColumnCount { get; private set; }

		public T this[int row, int column] { get { return this.Elements[row, column]; } set { this.Elements[row, column] = value; } }

		public abstract T Determinant { get; }

		public abstract M Transpose { get; }

		public bool IsSquare { get { return this.RowCount == this.ColumnCount; } }

		public bool IsRowVector { get { return this.RowCount == 1; } }

		public bool IsColumnVector { get { return this.ColumnCount == 1; } }

		public bool IsVector { get { return this.IsRowVector || this.IsColumnVector; } }

		public abstract bool IsDiagonal { get; }

		public abstract bool IsIdentity { get; }

		public abstract bool IsUpperTriangular { get; }

		public abstract bool IsStrictlyUpperTriangular { get; }

		public abstract bool IsLowerTriangular { get; }

		public abstract bool IsStrictlyLowerTriangular { get; }

		public abstract bool IsSingular { get; }

		public abstract bool IsInvertible { get; }

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

		public bool IsTriangular { get { return this.IsUpperTriangular || this.IsLowerTriangular; } }

		public bool IsStrictlyTriangular { get { return this.IsStrictlyLowerTriangular || this.IsStrictlyUpperTriangular; } }

		public MatrixBase(int rows, int columns)
		{
			this.RowCount = rows;
			this.ColumnCount = columns;
			this.Elements = new T[this.RowCount, this.ColumnCount];
			this.CreateAccessors();
		}

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

		public static void SwapRows(MatrixBase<T, M> result, int i, int j)
		{
			for (int k = 0; k < result.ColumnCount; k++)
			{
				T temp = result[i, k];
				result[i, k] = result[j, k];
				result[j, k] = temp;
			}
		}

		protected abstract void CreateAccessors();

		public abstract M Copy();

		public object Clone() { return this.Copy(); }

		public abstract bool Equals(M other);

		public override bool Equals(object obj) { return obj.GetType() == typeof(M) && this.Equals((M)obj); }
	}
}