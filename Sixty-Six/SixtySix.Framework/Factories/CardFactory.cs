using SixtySix.Crontracts;
using SixtySix.Framework.Models;

namespace SixtySix.Framework.Factories
{
    public class CardFactory : ICardFactory
    {
        public ICard GenerateCard(string rank, string suit, int value)
        {
            return new Card(rank, suit, value);
        }
    }
}
