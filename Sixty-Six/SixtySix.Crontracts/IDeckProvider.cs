using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IDeckProvider
    {
        Queue<ICard> CreateDeck();
    }
}
