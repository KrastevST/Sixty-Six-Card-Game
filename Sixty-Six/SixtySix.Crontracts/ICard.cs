using System;

namespace SixtySix.Crontracts
{
    public interface ICard : IComparable
    {
        string Rank { get; }
        string Suit { get; }
        int Value { get; }
    }
}
