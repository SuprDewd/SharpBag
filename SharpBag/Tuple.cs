#if !DOTNET4

namespace System
{
	public class Tuple<T1> : IEquatable<Tuple<T1>>
	{
		public T1 Item1 { get; private set; }

		public Tuple(T1 item1)
		{
			this.Item1 = item1;
		}

		public bool Equals(Tuple<T1> other)
		{
			return this.Item1.Equals(other.Item1);
		}

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1> && this.Equals(obj as Tuple<T1>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() * 2;
		}

		public override string ToString()
		{
			return "(" + this.Item1.ToString() + ")";
		}
	}

	public class Tuple<T1, T2> : IEquatable<Tuple<T1, T2>>
	{
		public T1 Item1 { get; private set; }

		public T2 Item2 { get; private set; }

		public Tuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		public bool Equals(Tuple<T1, T2> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2);
		}

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2> && this.Equals(obj as Tuple<T1, T2>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() ^
				   this.Item2.GetHashCode();
		}

		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() +
				   ")";
		}
	}

	public class Tuple<T1, T2, T3> : IEquatable<Tuple<T1, T2, T3>>
	{
		public T1 Item1 { get; private set; }

		public T2 Item2 { get; private set; }

		public T3 Item3 { get; private set; }

		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		public bool Equals(Tuple<T1, T2, T3> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3);
		}

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3> && this.Equals(obj as Tuple<T1, T2, T3>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() ^
				   this.Item2.GetHashCode() ^
				   this.Item3.GetHashCode();
		}

		public override string ToString()
		{
			return "(" +
					this.Item1.ToString() + ", " +
					this.Item2.ToString() + ", " +
					this.Item3.ToString() +
				   ")";
		}
	}

	public class Tuple<T1, T2, T3, T4> : IEquatable<Tuple<T1, T2, T3, T4>>
	{
		public T1 Item1 { get; private set; }

		public T2 Item2 { get; private set; }

		public T3 Item3 { get; private set; }

		public T4 Item4 { get; private set; }

		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
		}

		public bool Equals(Tuple<T1, T2, T3, T4> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4);
		}

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4> && this.Equals(obj as Tuple<T1, T2, T3, T4>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() ^
				   this.Item2.GetHashCode() ^
				   this.Item3.GetHashCode() ^
				   this.Item4.GetHashCode();
		}

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

	public class Tuple<T1, T2, T3, T4, T5> : IEquatable<Tuple<T1, T2, T3, T4, T5>>
	{
		public T1 Item1 { get; private set; }

		public T2 Item2 { get; private set; }

		public T3 Item3 { get; private set; }

		public T4 Item4 { get; private set; }

		public T5 Item5 { get; private set; }

		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
		}

		public bool Equals(Tuple<T1, T2, T3, T4, T5> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4) &&
				   this.Item5.Equals(other.Item5);
		}

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() ^
				   this.Item2.GetHashCode() ^
				   this.Item3.GetHashCode() ^
				   this.Item4.GetHashCode() ^
				   this.Item5.GetHashCode();
		}

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

	public class Tuple<T1, T2, T3, T4, T5, T6> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6>>
	{
		public T1 Item1 { get; private set; }

		public T2 Item2 { get; private set; }

		public T3 Item3 { get; private set; }

		public T4 Item4 { get; private set; }

		public T5 Item5 { get; private set; }

		public T6 Item6 { get; private set; }

		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
		}

		public bool Equals(Tuple<T1, T2, T3, T4, T5, T6> other)
		{
			return this.Item1.Equals(other.Item1) &&
				   this.Item2.Equals(other.Item2) &&
				   this.Item3.Equals(other.Item3) &&
				   this.Item4.Equals(other.Item4) &&
				   this.Item5.Equals(other.Item5) &&
				   this.Item6.Equals(other.Item6);
		}

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5, T6> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5, T6>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() ^
				   this.Item2.GetHashCode() ^
				   this.Item3.GetHashCode() ^
				   this.Item4.GetHashCode() ^
				   this.Item5.GetHashCode() ^
				   this.Item6.GetHashCode();
		}

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

	public class Tuple<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7>>
	{
		public T1 Item1 { get; private set; }

		public T2 Item2 { get; private set; }

		public T3 Item3 { get; private set; }

		public T4 Item4 { get; private set; }

		public T5 Item5 { get; private set; }

		public T6 Item6 { get; private set; }

		public T7 Item7 { get; private set; }

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

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5, T6, T7> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5, T6, T7>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() ^
				   this.Item2.GetHashCode() ^
				   this.Item3.GetHashCode() ^
				   this.Item4.GetHashCode() ^
				   this.Item5.GetHashCode() ^
				   this.Item6.GetHashCode() ^
				   this.Item7.GetHashCode();
		}

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

	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7, T8>>
	{
		public T1 Item1 { get; private set; }

		public T2 Item2 { get; private set; }

		public T3 Item3 { get; private set; }

		public T4 Item4 { get; private set; }

		public T5 Item5 { get; private set; }

		public T6 Item6 { get; private set; }

		public T7 Item7 { get; private set; }

		public T8 Item8 { get; private set; }

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

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5, T6, T7, T8> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5, T6, T7, T8>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() ^
				   this.Item2.GetHashCode() ^
				   this.Item3.GetHashCode() ^
				   this.Item4.GetHashCode() ^
				   this.Item5.GetHashCode() ^
				   this.Item6.GetHashCode() ^
				   this.Item7.GetHashCode() ^
				   this.Item8.GetHashCode();
		}

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

	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest>>
	{
		public T1 Item1 { get; private set; }

		public T2 Item2 { get; private set; }

		public T3 Item3 { get; private set; }

		public T4 Item4 { get; private set; }

		public T5 Item5 { get; private set; }

		public T6 Item6 { get; private set; }

		public T7 Item7 { get; private set; }

		public T8 Item8 { get; private set; }

		public TRest Rest { get; private set; }

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

		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest> && this.Equals(obj as Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest>);
		}

		public override int GetHashCode()
		{
			return this.Item1.GetHashCode() ^
				   this.Item2.GetHashCode() ^
				   this.Item3.GetHashCode() ^
				   this.Item4.GetHashCode() ^
				   this.Item5.GetHashCode() ^
				   this.Item6.GetHashCode() ^
				   this.Item7.GetHashCode() ^
				   this.Item8.GetHashCode() ^
				   this.Rest.GetHashCode();
		}

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

	public static class Tuple
	{
		public static Tuple<T1> Create<T1>(T1 item1) { return new Tuple<T1>(item1); }

		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) { return new Tuple<T1, T2>(item1, item2); }

		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) { return new Tuple<T1, T2, T3>(item1, item2, item3); }

		public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) { return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4); }

		public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) { return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5); }

		public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) { return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6); }

		public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) { return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7); }

		public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8) { return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(item1, item2, item3, item4, item5, item6, item7, item8); }

		public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest> Create<T1, T2, T3, T4, T5, T6, T7, T8, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, TRest rest) { return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, TRest>(item1, item2, item3, item4, item5, item6, item7, item8, rest); }
	}
}

#endif