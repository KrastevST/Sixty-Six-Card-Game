using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IDeckProvider
    {
        Queue<ICard> GenerateDeck();
        Queue<ICard> ReshuffleDeck(IList<ICard> cards);
    }
}
