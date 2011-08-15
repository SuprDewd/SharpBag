using System;

using System.Diagnostics.Contracts;

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
            Contract.Requires(sides > 0);
        }

        /// <summary>
        /// Creates a new dice.
        /// </summary>
        /// <param name="rand">The random number generator.</param>
        /// <param name="sides">The number of sides on the dice.</param>
        public Dice(Random rand, int sides = 6)
        {
            Contract.Requires(rand != null);
            Contract.Requires(sides > 0);

            this.Sides = sides;
            this.Rand = rand;
        }

        /// <summary>
        /// Roll the dice.
        /// </summary>
        /// <returns>The number on the top of the dice.</returns>
        public virtual int Roll()
        {
            lock (this.Rand)
            {
                return this.Rand.Next(1, this.Sides + 1);
            }
        }
    }
}