using Bytes2you.Validation;
using SixtySix.Crontracts;
using System;
using System.Collections.Generic;

namespace SixtySix.Framework.Models
{
    public class Game66 : IGame
    {
        private Queue<ICard> deck;
        private IPlayer player1;
        private IPlayer player2;
        private ICard openTrump;
        private string trumpSuit;
        private bool gameOver;
        private IPlayer firstPlayer;
        private IPlayer secondPlayer;
        private IDictionary<IPlayer, ICard> currentTrick;

        public Game66(IDeckProvider deckProvider, IPlayer player1, IPlayer player2)
        {
            Guard.WhenArgument(player1, "player1").IsNull().Throw();
            Guard.WhenArgument(player2, "player2").IsNull().Throw();
            Guard.WhenArgument(deckProvider, "deckProvider").IsNull().Throw();

            this.player1 = player1;
            this.player2 = player2;
            this.deck = deckProvider.CreateDeck();
            Closed.Status = false;
            gameOver = false;
            ChangeFirstPlayer(player1);
        }

        public Pair Closed { get; set; }

        public void Start()
        {
            while (player1.GamePoints < 11 && player2.GamePoints < 11)
            {
                StartRound();
            }
        }

        private void StartRound()
        {
            DealCards(3, 2);
            openTrump = deck.Dequeue();
            trumpSuit = openTrump.Suit;

            while (gameOver == false)
            {
                currentTrick[firstPlayer] = firstPlayer.PlayCard();
                currentTrick[secondPlayer] = secondPlayer.PlayCard();
                var trickWinner = ResolveTrick();
                trickWinner.TakeTrick(currentTrick);
                ChangeFirstPlayer(trickWinner);
                gameOver = IsGameOver();
            }

            var roundWinner = ResolveRound();
            ScoreRound(roundWinner);
            ResetRound();
            ChangeFirstPlayer(roundWinner);
        }
        private void DealCards(int number)
        {
            if (deck.Count == 0)
            {
                return;
            }

            for (int i = 0; i < number; i++)
            {
                firstPlayer.CurrentHand.Add(deck.Dequeue());
            }

            if (deck.Count == 0)
            {
                secondPlayer.CurrentHand.Add(openTrump);
                openTrump = null;
                Closed.Status = true;
                return;
            }

            for (int i = 0; i < number; i++)
            {
                player2.CurrentHand.Add(deck.Dequeue());
            }
        }
        private void DealCards(int number, int times)
        {
            for (int i = 0; i < times; i++)
            {
                DealCards(number);
            }
        }
        private void ChangeFirstPlayer(IPlayer player)
        {
            Guard.WhenArgument(player, "player").IsNull().Throw();
            player.IsFirst = true;
            Not(player).IsFirst = false;
            firstPlayer = player;
            secondPlayer = Not(player);
        }
        private IPlayer Not(IPlayer player)
        {
            Guard.WhenArgument(player, "player").IsNull().Throw();
            return player1 == player ? player2 : player1;
        }
        private IPlayer ResolveTrick()
        {
            if (currentTrick[firstPlayer].Suit != currentTrick[secondPlayer].Suit)
            {
                foreach (var kvp in currentTrick)
                {
                    if (kvp.Value.Suit == trumpSuit)
                    {
                        return kvp.Key;
                    }
                }

                return firstPlayer;
            }

            return currentTrick[firstPlayer].CompareTo(currentTrick[secondPlayer]) > 0
                ? firstPlayer
                : secondPlayer;
        }
        private IPlayer ResolveRound()
        {
            firstPlayer.RoundPoints += 10;
            IPlayer roundWinner;

            if (Closed.Initiator == null)
            {
                roundWinner = player1.RoundPoints > player2.RoundPoints ? player1 : player2;
            }
            else if (Closed.Initiator != null &&
                Closed.Initiator.RoundPoints >= 66)
            {
                roundWinner = Closed.Initiator;
            }
            else
            {
                roundWinner = Not(Closed.Initiator);
            }

            return roundWinner;
        }
        private bool IsGameOver()
        {
            bool result = false;

            if (player1.RoundPoints >= 66 || player2.RoundPoints >= 66)
            {
                result = true;
            }
            else if (secondPlayer.CurrentHand.Count == 0)
            {
                result = true;
            }

            return result;
        }
        private void ScoreRound(IPlayer roundWinner)
        {
            if (Closed.Initiator != null)
            {
                if (roundWinner == Closed.Initiator &&
                    Not(roundWinner).RoundPoints >= 33)
                {
                    roundWinner.GamePoints += 2;
                }
                else
                {
                    roundWinner.GamePoints += 3;
                }
            }
            else
            {
                roundWinner.GamePoints++;

                if (Not(roundWinner).RoundPoints < 33)
                {
                    roundWinner.GamePoints++;
                }

                if (Not(roundWinner).RoundPoints == 0)
                {
                    roundWinner.GamePoints++;
                }
            }
        }
        private void ResetRound()
        {
            Closed = null;
            gameOver = false;
        }
    }
}
