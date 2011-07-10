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
		/// <summary>
		/// Gets the elements.
		/// </summary>
		protected T[] Elements { get; private set; }

		/// <summary>
		/// Gets the dimension.
		/// </summary>
		public int Dimension { get; private set; }

		/// <summary>
		/// Gets the length.
		/// </summary>
		public abstract T Length { get; }

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		public T this[int i] { get { return this.Elements[i]; } set { this.Elements[i] = value; } }

		/// <summary>
		/// Gets the reverse.
		/// </summary>
		public abstract V Reverse { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VectorBase&lt;T, V&gt;"/> class.
		/// </summary>
		/// <param name="dimension">The dimension.</param>
		public VectorBase(int dimension)
		{
			this.Dimension = dimension;
			this.Elements = new T[dimension];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VectorBase&lt;T, V&gt;"/> class.
		/// </summary>
		/// <param name="dimension">The dimension.</param>
		/// <param name="def">The default value.</param>
		public VectorBase(int dimension, T def)
			: this(dimension)
		{
			for (int i = 0; i < this.Dimension; i++)
			{
				this.Elements[i] = def;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VectorBase&lt;T, V&gt;"/> class.
		/// </summary>
		/// <param name="elements">The elements.</param>
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

		/// <summary>
		/// Determines whether the specified vector is equal to this instance.
		/// </summary>
		/// <param name="other">The vector to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified vector is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool Equals(V other);

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) { return obj.GetType() == typeof(V) && this.Equals((V)obj); }

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < this.Dimension; i++)
			{
				yield return this[i];
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Copies this instance.
		/// </summary>
		/// <returns>The copy.</returns>
		public abstract V Copy();

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public object Clone() { return this.Copy(); }

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Elements);
		}
	}
}