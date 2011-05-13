using System;

#if DOTNET4

using System.Diagnostics.Contracts;

#endif

namespace SharpBag.Games
{
	/// <summary>
	/// A dice.
	/// </summary>
	public class Dice
	{
		/// <summary>
		/// The numer of sides on the dice.
		/// </summary>
		public int Sides { get; protected set; }

		/// <summary>
		/// The random number generator.
		/// </summary>
		protected Random Rand { get; set; }

		/// <summary>
		/// Creates a new dice.
		/// </summary>
		/// <param name="sides">The number of sides on the dice.</param>
		public Dice(int sides = 6)
			: this(new Random(), sides)
		{
#if DOTNET4
			Contract.Requires(sides > 0);
#endif
		}

		/// <summary>
		/// Creates a new dice.
		/// </summary>
		/// <param name="rand">The random number generator.</param>
		/// <param name="sides">The number of sides on the dice.</param>
		public Dice(Random rand, int sides = 6)
		{
#if DOTNET4
			Contract.Requires(rand != null);
			Contract.Requires(sides > 0);
#endif
			this.Sides = sides;
			this.Rand = rand;
		}

		/// <summary>
		/// Throw the dice.
		/// </summary>
		/// <returns>The number on the top of the dice.</returns>
		public virtual int Throw()
		{
			return this.Rand.Next(1, this.Sides + 1);
		}
	}
}