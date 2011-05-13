using System;
using System.Collections.Generic;

namespace SharpBag.Games
{
	/// <summary>
	/// A class representing a Card in a Card Game.
	/// </summary>
	public class Card
	{
		/// <summary>
		/// The type of the card.
		/// </summary>
		public CardType Type
		{
			get;
			private set;
		}

		/// <summary>
		/// The value of the card.
		/// </summary>
		public CardValue Value
		{
			get;
			private set;
		}

		private bool _IsDown;

		/// <summary>
		/// Whether the card is face down or face up.
		/// </summary>
		public bool IsDown { get { return this._IsDown; } set { this._IsDown = value; this.CardTurned.IfNotNull(() => this.CardTurned(this)); } }

		/// <summary>
		/// An event that is fired when a card is turned.
		/// </summary>
		public event Action<Card> CardTurned;

		/// <summary>
		/// The main constructor.
		/// </summary>
		/// <param name="type">The type of the card.</param>
		/// <param name="value">The value of the card.</param>
		/// <param name="isDown">Whether the card is face down or face up.</param>
		public Card(CardType type, CardValue value, bool isDown = true)
		{
			this.Type = type;
			this.Value = value;
			this.IsDown = isDown;
		}

		/// <summary>
		/// Gets the value of the card.
		/// </summary>
		/// <returns>The value of the card.</returns>
		public int GetValue()
		{
			return (int)this.Value;
		}

		/// <summary>
		/// Gets the value of the card, using the specified rules.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns>The value of the card.</returns>
		public int GetValue(Dictionary<CardValue, int> values)
		{
			return values[this.Value];
		}
	}
}