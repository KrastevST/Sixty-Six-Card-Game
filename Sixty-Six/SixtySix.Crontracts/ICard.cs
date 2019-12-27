using System;

namespace SixtySix.Crontracts
{
    public interface ICard : IComparable
    {
        string Symbol { get; }
        string Suit { get; }
        int Value { get; }
    }
}
