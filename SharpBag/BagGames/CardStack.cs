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
        public CardStack()
        {
            this.Reset();
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