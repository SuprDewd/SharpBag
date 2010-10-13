using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.BagGames
{
    /// <summary>
    /// A class representing a stack og cards.
    /// </summary>
    public class CardStack : Stack<Card>
    {
        /// <summary>
        /// The main constructor.
        /// </summary>
        public CardStack() : this(true) { }
        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="shuffle">Whether or not to shuffle the stack.</param>
        public CardStack(bool shuffle)
        {
            this.Reset();

            if (shuffle) this.Shuffle();
        }

        /// <summary>
        /// Resets the card stack.
        /// </summary>
        public void Reset()
        {
            base.Clear();

            foreach (CardType t in Enum.GetValues(typeof(CardType)))
            {
                foreach (CardValue v in Enum.GetValues(typeof(CardValue)))
                {
                    base.Push(new Card(t, v));
                }
            }
        }

        /// <summary>
        /// Draws the top card from the stack.
        /// </summary>
        /// <returns>The next card in the stack.</returns>
        public Card Draw()
        {
            return this.Draw(false);
        }

        /// <summary>
        /// Shuffles the stack.
        /// </summary>
        public void Shuffle()
        {
            IEnumerable<Card> cards = this.Shuffle(new Random());
            base.Clear();

            foreach (Card c in cards)
            {
                base.Push(c);
            }
        }

        /// <summary>
        /// Draws the top card from the stack.
        /// </summary>
        /// <param name="isDown">Whether or not the card drawn should be face down or face up.</param>
        /// <returns>The next card in the stack.</returns>
        public Card Draw(bool isDown)
        {
            Card c = base.Pop();
            c.IsDown = isDown;

            return c;
        }
    }
}