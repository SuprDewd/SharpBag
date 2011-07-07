using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Math
{
	/// <summary>
	/// A vector base class.
	/// </summary>
	/// <typeparam name="T">The type of elements in the vector.</typeparam>
	/// <typeparam name="V">The child class.</typeparam>
	public abstract class VectorBase<T, V> : IEnumerable<T>, ICloneable, IEquatable<V>
	{
		protected T[] Elements { get; private set; }

		public int Dimension { get; private set; }

		public abstract T Length { get; }

		public T this[int i] { get { return this.Elements[i]; } set { this.Elements[i] = value; } }

		public abstract V Reverse { get; }

		public VectorBase(int dimension)
		{
			this.Dimension = dimension;
			this.Elements = new T[dimension];
		}

		public VectorBase(T[] elements)
			: this(elements.Length)
		{
			for (int i = 0; i < elements.Length; i++) this[i] = elements[i];
		}

		internal static void InternalReverse(VectorBase<T, V> result, VectorBase<T, V> vector)
		{
			int j = 0;
			for (int i = vector.Dimension - 1; i >= 0; i--) result[j++] = vector[i];
		}

		internal static void InternalToColumnMatrix<M>(MatrixBase<T, M> result, VectorBase<T, V> vector)
		{
			for (int i = 0; i < vector.Dimension; i++) result[i, 0] = vector[i];
		}

		internal static void InternalToRowMatrix<M>(MatrixBase<T, M> result, VectorBase<T, V> vector)
		{
			for (int i = 0; i < vector.Dimension; i++) result[0, i] = vector[i];
		}

		internal static bool InternalEquals(VectorBase<T, V> left, VectorBase<T, V> right)
		{
			if (left.Dimension != right.Dimension) return false;
			for (int i = 0; i < left.Dimension; i++) if (!left[i].Equals(right[i])) return false;
			return true;
		}

		public abstract bool Equals(V other);

		public override bool Equals(object obj) { return obj.GetType() == typeof(V) && this.Equals((V)obj); }

		public IEnumerator<T> GetEnumerator()
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

		public abstract V Copy();

		public object Clone() { return this.Copy(); }

		public override int GetHashCode()
		{
			int hash = 0;
			for (int i = 0; i < this.Dimension; i++) hash ^= this[i].GetHashCode();
			return hash;
		}
	}
}