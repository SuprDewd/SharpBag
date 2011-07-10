#if !DOTNET4

namespace System
{
	/// <summary>
	/// A 1-tuple.
	/// </summary>
	public class Tuple<T1> : IEquatable<Tuple<T1>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1)
		{
			this.Item1 = item1;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1> other)
		{
			return this.Item1.Equals(other.Item1);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1> && this.Equals(obj as Tuple<T1>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" + this.Item1.ToString() + ")";
		}
	}

	/// <summary>
	/// A 2-tuple.
	/// </summary>
	public class Tuple<T1, T2> : IEquatable<Tuple<T1, T2>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T2 Item2 { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1, T2> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2> && this.Equals(obj as Tuple<T1, T2>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1, this.Item2);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() +
				   ")";
		}
	}

	/// <summary>
	/// A 3-tuple.
	/// </summary>
	public class Tuple<T1, T2, T3> : IEquatable<Tuple<T1, T2, T3>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T2 Item2 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T3 Item3 { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1, T2, T3> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3> && this.Equals(obj as Tuple<T1, T2, T3>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1, this.Item2, this.Item3);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() + ", " +
					this.Item3.ToString() +
				   ")";
		}
	}

	/// <summary>
	/// A 4-tuple.
	/// </summary>
	public class Tuple<T1, T2, T3, T4> : IEquatable<Tuple<T1, T2, T3, T4>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T2 Item2 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T3 Item3 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T4 Item4 { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1, T2, T3, T4> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4> && this.Equals(obj as Tuple<T1, T2, T3, T4>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1, this.Item2, this.Item3, this.Item4);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() + ", " +
					this.Item3.ToString() + ", " +
					this.Item4.ToString() +
				   ")";
		}
	}

	/// <summary>
	/// A 5-tuple.
	/// </summary>
	public class Tuple<T1, T2, T3, T4, T5> : IEquatable<Tuple<T1, T2, T3, T4, T5>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T2 Item2 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T3 Item3 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T4 Item4 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T5 Item5 { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1, T2, T3, T4, T5> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4) &&
				   this.Item5.Equals(other.Item5);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1, this.Item2, this.Item3, this.Item4, this.Item5);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() + ", " +
					this.Item3.ToString() + ", " +
					this.Item4.ToString() + ", " +
					this.Item5.ToString() +
				   ")";
		}
	}

	/// <summary>
	/// A 6-tuple.
	/// </summary>
	public class Tuple<T1, T2, T3, T4, T5, T6> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T2 Item2 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T3 Item3 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T4 Item4 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T5 Item5 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T6 Item6 { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1, T2, T3, T4, T5, T6> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4) &&
				   this.Item5.Equals(other.Item5) &&
				   this.Item6.Equals(other.Item6);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5, T6> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5, T6>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1, this.Item2, this.Item3, this.Item4, this.Item5, this.Item6);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() + ", " +
					this.Item3.ToString() + ", " +
					this.Item4.ToString() + ", " +
					this.Item5.ToString() + ", " +
					this.Item6.ToString() +
				   ")";
		}
	}

	/// <summary>
	/// A 7-tuple.
	/// </summary>
	public class Tuple<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T2 Item2 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T3 Item3 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T4 Item4 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T5 Item5 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T6 Item6 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T7 Item7 { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
			this.Item7 = item7;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1, T2, T3, T4, T5, T6, T7> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4) &&
				   this.Item5.Equals(other.Item5) &&
				   this.Item6.Equals(other.Item6) &&
				   this.Item7.Equals(other.Item7);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5, T6, T7> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5, T6, T7>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1, this.Item2, this.Item3, this.Item4, this.Item5, this.Item6, this.Item7);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() + ", " +
					this.Item3.ToString() + ", " +
					this.Item4.ToString() + ", " +
					this.Item5.ToString() + ", " +
					this.Item6.ToString() + ", " +
					this.Item7.ToString() +
				   ")";
		}
	}

	/// <summary>
	/// An 8-tuple.
	/// </summary>
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7, T8>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T2 Item2 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T3 Item3 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T4 Item4 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T5 Item5 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T6 Item6 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T7 Item7 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T8 Item8 { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
			this.Item7 = item7;
			this.Item8 = item8;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1, T2, T3, T4, T5, T6, T7, T8> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4) &&
				   this.Item5.Equals(other.Item5) &&
				   this.Item6.Equals(other.Item6) &&
				   this.Item7.Equals(other.Item7) &&
				   this.Item8.Equals(other.Item8);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5, T6, T7, T8> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5, T6, T7, T8>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1, this.Item2, this.Item3, this.Item4, this.Item5, this.Item6, this.Item7, this.Item8);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() + ", " +
					this.Item3.ToString() + ", " +
					this.Item4.ToString() + ", " +
					this.Item5.ToString() + ", " +
					this.Item6.ToString() + ", " +
					this.Item7.ToString() + ", " +
					this.Item8.ToString() +
				   ")";
		}
	}

	/// <summary>
	/// An 8-tuple with room for another tuple.
	/// </summary>
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest>>
	{
		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T1 Item1 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T2 Item2 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T3 Item3 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T4 Item4 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T5 Item5 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T6 Item6 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T7 Item7 { get; private set; }

		/// <summary>
		/// An item in the tuple.
		/// </summary>
		public T8 Item8 { get; private set; }

		/// <summary>
		/// The rest of the tuple (possibly another tuple).
		/// </summary>
		public TRest Rest { get; private set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, TRest rest)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
			this.Item7 = item7;
			this.Item8 = item8;
			this.Rest = rest;
		}

		/// <summary>
		/// IEquatable.Equals()
		/// </summary>
		/// <param name="other">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public bool Equals(Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4) &&
				   this.Item5.Equals(other.Item5) &&
				   this.Item6.Equals(other.Item6) &&
				   this.Item7.Equals(other.Item7) &&
				   this.Item8.Equals(other.Item8) &&
				   this.Rest.Equals(other.Rest);
		}

		/// <summary>
		/// Object.Equals()
		/// </summary>
		/// <param name="obj">The other tuple.</param>
		/// <returns>Whether the tuples are equal.</returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest>);
		}

		/// <summary>
		/// Object.GetHashCode()
		/// </summary>
		/// <returns>The hash code of the tuple.</returns>
		public override int GetHashCode()
		{
			return Utils.Hash(this.Item1, this.Item2, this.Item3, this.Item4, this.Item5, this.Item6, this.Item7, this.Item8, this.Rest);
		}

		/// <summary>
		/// Object.ToString()
		/// </summary>
		/// <returns>The string representation of the tuple.</returns>
		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() + ", " +
					this.Item3.ToString() + ", " +
					this.Item4.ToString() + ", " +
					this.Item5.ToString() + ", " +
					this.Item6.ToString() + ", " +
					this.Item7.ToString() + ", " +
					this.Item8.ToString() + ", " +
					this.Rest.ToString() +
				   ")";
		}
	}

	/// <summary>
	/// A tuple.
	/// </summary>
	public static class Tuple
	{
		/// <summary>
		/// Creates a 1-tuple.
		/// </summary>
		public static Tuple<T1> Create<T1>(T1 item1) { return new Tuple<T1>(item1); }

		/// <summary>
		/// Creates a 2-tuple.
		/// </summary>
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) { return new Tuple<T1, T2>(item1, item2); }

		/// <summary>
		/// Creates a 3-tuple.
		/// </summary>
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) { return new Tuple<T1, T2, T3>(item1, item2, item3); }

		/// <summary>
		/// Creates a 4-tuple.
		/// </summary>
		public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) { return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4); }

		/// <summary>
		/// Creates a 5-tuple.
		/// </summary>
		public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) { return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5); }

		/// <summary>
		/// Creates a 6-tuple.
		/// </summary>
		public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) { return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6); }

		/// <summary>
		/// Creates a 7-tuple.
		/// </summary>
		public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) { return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7); }

		/// <summary>
		/// Creates an 8-tuple.
		/// </summary>
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8) { return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(item1, item2, item3, item4, item5, item6, item7, item8); }

		/// <summary>
		/// Creates an 8-tuple with room for another tuple.
		/// </summary>
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest> Create<T1, T2, T3, T4, T5, T6, T7, T8, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, TRest rest) { return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest>(item1, item2, item3, item4, item5, item6, item7, item8, rest); }
	}
}

#endif