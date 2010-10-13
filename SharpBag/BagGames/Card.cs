using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.BagGames
{
    /// <summary>
    /// An enumeration containing types of cards.
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// A heart.
        /// </summary>
        Heart,
        /// <summary>
        /// A spade.
        /// </summary>
        Spade,
        /// <summary>
        /// A diamond.
        /// </summary>
        Diamond,
        /// <summary>
        /// A leaf.
        /// </summary>
        Leaf
    }

    /// <summary>
    /// An enumeration containing values of cards.
    /// </summary>
    public enum CardValue
    {
        /// <summary>
        /// An ace.
        /// </summary>
        Ace = 1,
        /// <summary>
        /// A two.
        /// </summary>
        Two,
        /// <summary>
        /// A three.
        /// </summary>
        Three,
        /// <summary>
        /// A four.
        /// </summary>
        Four,
        /// <summary>
        /// A five.
        /// </summary>
        Five,
        /// <summary>
        /// A six.
        /// </summary>
        Six,
        /// <summary>
        /// A seven.
        /// </summary>
        Seven,
        /// <summary>
        /// An eight.
        /// </summary>
        Eight,
        /// <summary>
        /// A nine.
        /// </summary>
        Nine,
        /// <summary>
        /// A ten.
        /// </summary>
        Ten,
        /// <summary>
        /// A jack.
        /// </summary>
        Jack,
        /// <summary>
        /// A queen.
        /// </summary>
        Queen,
        /// <summary>
        /// A king.
        /// </summary>
        King
    }

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

        public event Action<Card> CardTurned;

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="type">The type of the card.</param>
        /// <param name="value">The value of the card.</param>
        public Card(CardType type, CardValue value) : this(type, value, true) { }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="type">The type of the card.</param>
        /// <param name="value">The value of the card.</param>
        /// <param name="isDown">Whether the card is face down or face up.</param>
        public Card(CardType type, CardValue value, bool isDown)
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