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
        public void GenerateADeckofUniqueCards_WhenCalled()
        {
            var factory = new CardFactory();
            var provider = new DeckProvider66(factory);

            var deck = provider.GenerateDeck();
            var uniqueCardsCount = deck.Distinct().Count();

            Assert.AreEqual(deck.Count, uniqueCardsCount);
        }

        [Test]
        public void GenerateADeckOf24Cards_WhenCalled()
        {
            var factory = new CardFactory();
            var provider = new DeckProvider66(factory);

            var deck = provider.GenerateDeck();

            Assert.AreEqual(deck.Count, 24);
        }
    }
}
