using Bytes2you.Validation;
using SixtySix.Crontracts;
using System;

namespace SixtySix.Framework.Models
{
    public class Card : ICard, IComparable, IEquatable<ICard>
    {
        public Card(string rank, string suit, int value)
        {
            Guard.WhenArgument(rank, "symbol").IsNullOrWhiteSpace().Throw();
            Guard.WhenArgument(suit, "suit").IsNullOrWhiteSpace().Throw();
            Guard.WhenArgument(value, "value").IsLessThan(0).IsGreaterThan(12).Throw();
            this.Rank = rank.ToLower();
            this.Suit = suit.ToLower();
            this.Value = value;
        }

        public string Rank { get; }
        public string Suit { get; }
        public int Value { get; }

        public int CompareTo(object obj)
        {
            Card otherCard = obj as Card;
            Guard.WhenArgument(obj, "obj").IsNull().Throw();

            if (otherCard == null)
            {
                throw new ArgumentException("obj is not a card", "obj");
            }

            return this.Value - otherCard.Value;
        }

        public bool Equals(ICard other)
        {
            return Rank.Equals(other.Rank) && Suit.Equals(other.Suit);
        }
    }
}
