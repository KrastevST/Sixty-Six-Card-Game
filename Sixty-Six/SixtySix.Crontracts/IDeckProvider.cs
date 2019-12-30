using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IDeckProvider
    {
        Queue<ICard> GenerateDeck();
        Queue<ICard> ReshuffleDeck(IPlayer player1, IPlayer player2);
    }
}
