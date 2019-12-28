using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IDeckProvider
    {
        Queue<ICard> CreateDeck();
        Queue<ICard> RebuildDeck(IPlayer player1, IPlayer player2);
    }
}
