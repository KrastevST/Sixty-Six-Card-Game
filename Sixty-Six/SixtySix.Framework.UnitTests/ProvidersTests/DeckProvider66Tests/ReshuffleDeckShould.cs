using Moq;
using NUnit.Framework;
using SixtySix.Crontracts;
using SixtySix.Framework.Models;
using SixtySix.Framework.Providers;
using System.Collections.Generic;
using System.Linq;

namespace SixtySix.Framework.UnitTests.ProvidersTests.DeckProvider66Tests
{
    [TestFixture]
    public class ReshuffleDeckShould
    {
        [Test]
        public void ReturnAllCardsItRecieved_WhenValidCardCollectionPassed()
        {
            var cards = new List<ICard>();
            cards.Add(new Card("A", "hearts", 11));
            cards.Add(new Card("A", "spades", 11));
            cards.Add(new Card("A", "clubs", 11));
            cards.Add(new Card("A", "diamonds", 11));
            cards.Add(new Card("K", "diamonds", 4));
            cards.Add(new Card("Q", "diamonds", 3));

            var factory = new Mock<ICardFactory>();
            var provider = new DeckProvider66(factory.Object);

            var newDeck = provider.ReshuffleDeck(cards);

            bool allCardsPresent = true;

            foreach (var card in cards)
            {
                if (!newDeck.Contains(card))
                {
                    allCardsPresent = false;
                    break;
                }
            }

            Assert.IsTrue(allCardsPresent);
        }

        [Test]
        public void ReturnAsMuchCardsAsReceived_WhenValicCardCollectionPassed()
        {
            var cards = new List<ICard>();
            cards.Add(new Card("A", "hearts", 11));
            cards.Add(new Card("A", "spades", 11));
            cards.Add(new Card("9", "clubs", 0));
            cards.Add(new Card("A", "diamonds", 11));
            cards.Add(new Card("2", "diamonds", 0));

            var factory = new Mock<ICardFactory>();
            var provider = new DeckProvider66(factory.Object);

            var newDeck = provider.ReshuffleDeck(cards);

            Assert.AreEqual(cards.Count, newDeck.Count);
        }

        [Test]
        public void ReturnADeckOfUniqueCards_WhenValidCardCollectionPassed()
        {
            var cards = new List<ICard>();
            cards.Add(new Card("A", "hearts", 11));
            cards.Add(new Card("A", "spades", 11));
            cards.Add(new Card("9", "clubs", 0));
            cards.Add(new Card("A", "diamonds", 11));
            cards.Add(new Card("2", "diamonds", 0));

            var factory = new Mock<ICardFactory>();
            var provider = new DeckProvider66(factory.Object);

            var newDeck = provider.ReshuffleDeck(cards);
            int uniqueCardsCount = newDeck.Distinct().Count();

            Assert.AreEqual(newDeck.Count, uniqueCardsCount);
        }
    }
}
