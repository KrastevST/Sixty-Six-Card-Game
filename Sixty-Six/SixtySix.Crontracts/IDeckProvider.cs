using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IDeckProvider
    {
        Queue<ICard> GenerateDeck();
        Queue<ICard> ReshuffleDeck(ICollection<ICard> discardPile1, ICollection<ICard> discardPile2);
    }
}
