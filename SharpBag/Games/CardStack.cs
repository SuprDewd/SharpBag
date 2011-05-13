using System;
using System.Collections.Generic;

namespace SharpBag.Games
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

			if (shuffle)
				this.Shuffle();
		}

		/// <summary>
		/// Resets the card stack.
		/// </summary>
		public void Reset()
		{
			base.Clear();

			foreach (CardType t in Enum.GetValues(typeof(CardType)))
				foreach (CardValue v in Enum.GetValues(typeof(CardValue)))
					base.Push(new Card(t, v));
		}

		/// <summary>
		/// Shuffles the stack.
		/// </summary>
		public void Shuffle()
		{
			IEnumerable<Card> cards = CollectionExtensions.Shuffle(this);
			base.Clear();

			foreach (Card c in cards)
				base.Push(c);
		}

		/// <summary>
		/// Draws the top card from the stack.
		/// </summary>
		/// <param name="isDown">Whether or not the card drawn should be face down or face up.</param>
		/// <returns>The next card in the stack.</returns>
		public Card Draw(bool isDown = false)
		{
			Card c = base.Pop();
			c.IsDown = isDown;

			return c;
		}
	}
}