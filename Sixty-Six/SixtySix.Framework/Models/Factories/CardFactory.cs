using SixtySix.Crontracts;

namespace SixtySix.Framework.Models.Factories
{
    public class CardFactory : ICardFactory
    {
        public ICard GenerateCard(string rank, string suit, int value)
        {
            return new Card(rank, suit, value);
        }
    }
}
