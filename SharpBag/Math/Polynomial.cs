using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBag.Math.Calculators;

namespace SharpBag.Math
{
	public class Polynomial<T>
	{
		static Polynomial()
		{
			if (Calculator == null) Calculator = CalculatorFactory.GetInstanceFor<T>();
		}

		public static Calculator<T> Calculator { get; set; }

		private T[] Coefficients;

		public int Degree
		{
			get
			{
				return this.Coefficients.Length - 1;
			}
		}

		public Polynomial()
		{
			this.Coefficients = new T[1];
			this.Coefficients[0] = Calculator.Zero;
		}

		public Polynomial(params T[] coefficients)
		{
			int len = coefficients.Length;
			while (len > 0 && Calculator.Equal(coefficients[len - 1], Calculator.Zero)) len--;

			if (len == 0)
			{
				this.Coefficients = new T[1];
				this.Coefficients[0] = Calculator.Zero;
			}
			else
			{
				this.Coefficients = new T[len];
				for (int i = 0; i < len; i++) this.Coefficients[i] = coefficients[i];
			}
		}

		public T Evaluate(T x)
		{
			T sum = Calculator.Zero,
			  pow = Calculator.One;

			for (int i = 0; i <= this.Degree; i++)
			{
				if (i != 0) pow = Calculator.Multiply(pow, x);
				sum = Calculator.Add(sum, Calculator.Multiply(this[i], pow));
			}

			return sum;
		}

		public T this[int i]
		{
			get
			{
				if (i >= this.Coefficients.Length) return Calculator.Zero;
				return this.Coefficients[i];
			}
			set
			{
				if (i >= this.Coefficients.Length)
				{
					int old = this.Coefficients.Length;
					Array.Resize<T>(ref this.Coefficients, i + 1);
					for (int j = old; j < i; j++)
					{
						this.Coefficients[j] = Calculator.Zero;
					}
				}

				this.Coefficients[i] = value;
			}
		}

		public static Polynomial<T> operator +(Polynomial<T> left, Polynomial<T> right)
		{
			T[] added = new T[left.Coefficients.Length > right.Coefficients.Length ? left.Coefficients.Length : right.Coefficients.Length];
			for (int i = left.Coefficients.Length; i < added.Length; i++) added[i] = Calculator.Zero;
			for (int i = 0; i < left.Coefficients.Length; i++) added[i] = left[i];
			for (int i = 0; i < added.Length; i++) added[i] = Calculator.Add(added[i], right[i]);
			return new Polynomial<T>(added);
		}

		public static Polynomial<T> operator -(Polynomial<T> left, Polynomial<T> right)
		{
			T[] subtracted = new T[left.Coefficients.Length > right.Coefficients.Length ? left.Coefficients.Length : right.Coefficients.Length];
			for (int i = left.Coefficients.Length; i < subtracted.Length; i++) subtracted[i] = Calculator.Zero;
			for (int i = 0; i < left.Coefficients.Length; i++) subtracted[i] = left[i];
			for (int i = 0; i < subtracted.Length; i++) subtracted[i] = Calculator.Subtract(subtracted[i], right[i]);
			return new Polynomial<T>(subtracted);
		}

		public static Polynomial<T> operator *(Polynomial<T> left, Polynomial<T> right)
		{
			int newLength = left.Degree + right.Degree + 1;
			T[] multiplied = new T[newLength];
			for (int i = 0; i < newLength; i++) multiplied[i] = Calculator.Zero;

			for (int a = 0; a <= left.Degree; a++)
			{
				for (int b = 0; b <= right.Degree; b++)
				{
					multiplied[a + b] = Calculator.Add(multiplied[a + b], Calculator.Multiply(left[a], right[b]));
				}
			}

			return new Polynomial<T>(multiplied);
		}

		public static implicit operator Polynomial<T>(T n)
		{
			return new Polynomial<T>(n);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = this.Degree; i >= 0; i--)
			{
				if (Calculator.Equal(this[i], Calculator.Zero)) continue;
				sb.Append(" + ");
				sb.Append(this[i].ToString());
				if (i > 0) sb.Append('x');
				if (i > 1) sb.Append('^').Append(i);
			}

			return sb.ToString().TrimStart(' ', '+');
		}
	}
}