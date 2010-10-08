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
        /// <returns></returns>
        public Card Draw()
        {
            return base.Pop();
        }
    }
}