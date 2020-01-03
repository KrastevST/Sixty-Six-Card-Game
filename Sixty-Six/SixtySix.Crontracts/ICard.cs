using System;

namespace SixtySix.Crontracts
{
    public interface ICard : IComparable, IEquatable<ICard>
    {
        string Rank { get; }
        string Suit { get; }
        int Value { get; }
    }
}
