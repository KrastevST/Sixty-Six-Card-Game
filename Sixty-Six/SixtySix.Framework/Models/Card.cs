using Bytes2you.Validation;
using System;

namespace SixtySix.Framework.Models
{
    public class Card : IComparable
    {
        public Card(string symbol, string suit, int value)
        {
            Guard.WhenArgument(symbol, "symbol").IsNullOrWhiteSpace().Throw();
            Guard.WhenArgument(suit, "suit").IsNullOrWhiteSpace().Throw();
            Guard.WhenArgument(value, "value").IsLessThan(0).IsGreaterThan(12).Throw();
            this.Symbol = symbol.ToLower();
            this.Suit = suit.ToLower();
            this.Value = value;
        }

        public string Symbol { get; }
        public string Suit { get; }
        public int Value { get; }

        public int CompareTo(object obj)
        {
            Card otherCard = obj as Card;
            Guard.WhenArgument(otherCard, "otherCard").IsNull().Throw();
            return this.Value - otherCard.Value;
        }
    }
}
