using NUnit.Framework;
using SixtySix.Framework.Factories;
using SixtySix.Framework.Providers;
using System.Linq;

namespace SixtySix.Framework.UnitTests.ModelsTests.ProvidersTests.DeckProvider66Tests
{
    [TestFixture]
    public class GenerateDeckShould
    {
        [Test]
        public void GenerateADeckof24UniqueCards_WhenCalled()
        {
            var factory = new CardFactory();
            var provider = new DeckProvider66(factory);

            var deck = provider.GenerateDeck();
            var uniqueCardsCount = deck.ToList().Distinct().Count();

            Assert.AreEqual(deck.Count, uniqueCardsCount);
            Assert.AreEqual(deck.Count, 24);
        }
    }
}
