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

        public Queue<ICard> ReshuffleDeck(IPlayer player1, IPlayer player2)
        {
            var deck = new Queue<ICard>();
            var tempDeck = new List<ICard>();

            foreach (var card in player1.DiscardPile)
            {
                tempDeck.Add(card);
            }

            foreach (var card in player2.DiscardPile)
            {
                tempDeck.Add(card);
            }

            var rand = new Random();
            int splitAt = rand.Next() % tempDeck.Count;

            for (int i = splitAt; i >= 0; i--)
            {
                deck.Enqueue(tempDeck[i]);
            }

            for (int i = tempDeck.Count - 1; i > splitAt; i--)
            {
                deck.Enqueue(tempDeck[i]);
            }

            return deck;
        }
    }
}
