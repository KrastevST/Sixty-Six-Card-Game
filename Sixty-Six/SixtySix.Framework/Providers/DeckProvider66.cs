using Bytes2you.Validation;
using SixtySix.Crontracts;
using System;
using System.Collections.Generic;

namespace SixtySix.Framework.Providers
{
    public class DeckProvider66 : IDeckProvider
    {
        private readonly string[] ranks = { "9", "J", "Q", "K", "10", "A"};
        private readonly string[] suits = { "spades", "hearts", "diamonds", "clubs"};
        private readonly int[] values = { 0, 2, 3, 4, 10, 11};
        private readonly ICardFactory cardFactory;

        public DeckProvider66(ICardFactory cardFactory)
        {
            Guard.WhenArgument(cardFactory, "cardFactory").IsNull().Throw();
            this.cardFactory = cardFactory;
        }
        
        public Queue<ICard> GenerateDeck()
        {
            var rand = new Random();
            var randomizer = new SortedList<int, ICard>();
            var deck = new Queue<ICard>();

            for (int i = 0; i < ranks.Length; i++)
            {
                foreach (string suit in suits)
                {
                    var card = cardFactory.GenerateCard(ranks[i], suit, values[i]);
                    int key = rand.Next();

                    while (randomizer.ContainsKey(key))
                    {
                        key = rand.Next();
                    }

                    randomizer.Add(key, card);
                }
            }

            foreach (var kvp in randomizer)
            {
                deck.Enqueue(kvp.Value);
            }

            return deck;
        }

        public Queue<ICard> ReshuffleDeck(IList<ICard> cards)
        {
            var newDeck = new Queue<ICard>();

            var rand = new Random();
            int splitAt = rand.Next() % cards.Count;

            for (int i = splitAt; i >= 0; i--)
            {
                newDeck.Enqueue(cards[i]);
            }

            for (int i = cards.Count - 1; i > splitAt; i--)
            {
                newDeck.Enqueue(cards[i]);
            }

            return newDeck;
        }
    }
}
