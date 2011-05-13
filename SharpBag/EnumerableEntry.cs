namespace SharpBag
{
	/// <summary>
	/// A wrapper class for IEnumerable entries.
	/// </summary>
	public static class EnumerableEntry
	{
		/// <summary>
		/// An IEnumerable entry with extended info.
		/// </summary>
		/// <typeparam name="T">The type.</typeparam>
		public class WithInfo<T>
		{
			/// <summary>
			/// The previous entry.
			/// </summary>
			public T Previous { get; private set; }

			/// <summary>
			/// The current entry.
			/// </summary>
			public T Value { get; private set; }

			/// <summary>
			/// The next entry.
			/// </summary>
			public T Next { get; private set; }

			/// <summary>
			/// The index of the current entry.
			/// </summary>
			public int Index { get; private set; }

			/// <summary>
			/// Whether the current entry is the first entry.
			/// </summary>
			public bool IsFirst { get; private set; }

			/// <summary>
			/// Whether the current entry is the last entry.
			/// </summary>
			public bool IsLast { get; private set; }

			internal WithInfo(T value, int index, bool isFirst, bool isLast, T previous = default(T), T next = default(T))
			{
				this.Value = value;
				this.Index = index;
				this.IsFirst = isFirst;
				this.IsLast = isLast;
				this.Previous = previous;
				this.Next = next;
			}
		}

		/// <summary>
		/// An IEnumerable entry with index.
		/// </summary>
		/// <typeparam name="T">The type.</typeparam>
		public class WithIndex<T>
		{
			/// <summary>
			/// The value.
			/// </summary>
			public T Value { get; private set; }

			/// <summary>
			/// The index.
			/// </summary>
			public int Index { get; private set; }

			internal WithIndex(T value, int index)
			{
				this.Value = value;
				this.Index = index;
			}
		}
	}
}