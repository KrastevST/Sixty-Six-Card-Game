using Bytes2you.Validation;
using SixtySix.Crontracts;
using System;
using System.Collections.Generic;

namespace SixtySix.Framework.Models
{
    public class Game66 : IGame
    {
        private Queue<ICard> deck;
        private ICard openedTrump;
        private string trumpSuit;
        private IPlayer firstPlayer;
        private IPlayer secondPlayer;
        IDeckProvider deckProvider;

        public Game66(IDeckProvider deckProvider, IUserPlayer userPlayer, IComputerPlayer computerPlayer)
        {
            Guard.WhenArgument(userPlayer, "userPlayer").IsNull().Throw();
            Guard.WhenArgument(computerPlayer, "computerPlayer").IsNull().Throw();
            Guard.WhenArgument(deckProvider, "deckProvider").IsNull().Throw();

            this.UserPlayer = userPlayer;
            this.ComputerPlayer = computerPlayer;
            this.deckProvider = deckProvider;
            this.deck = deckProvider.GenerateDeck();
            Closed.Status = false;
            ChangeFirstPlayer(RandomPlayer());
        }

        public Pair Closed { get; set; }
        public IDictionary<IPlayer, ICard> CurrentTrick { get; set; }
        public IUserPlayer UserPlayer { get; }
        public IComputerPlayer ComputerPlayer { get; }

        public void Start()
        {
            DealCards(3, 2);
            openedTrump = deck.Dequeue();
            trumpSuit = openedTrump.Suit;
            computerPlayIf(ComputerPlayer == firstPlayer);
        }

        public void CheckCurrentTrick()
        {
            if (CurrentTrick.Count < 2)
            {
                ComputerPlayer.PlayCard();
            }

            var trickWinner = ResolveTrick();
            trickWinner.TakeTrick(CurrentTrick);
            CurrentTrick = null;
            ChangeFirstPlayer(trickWinner);

            if (RoundOver())
            {
                var roundWinner = ResolveRound();
                ScoreRound(roundWinner);

                if (GameWinner() != null)
                {
                    EndGame();
                }

                ResetRound();
                ChangeFirstPlayer(roundWinner);
            }

            DealCards(1);
            computerPlayIf(ComputerPlayer == firstPlayer);
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
                secondPlayer.CurrentHand.Add(openedTrump);
                openedTrump = null;
                Closed.Status = true;
                return;
            }

            for (int i = 0; i < number; i++)
            {
                ComputerPlayer.CurrentHand.Add(deck.Dequeue());
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
        private void computerPlayIf(bool cond)
        {
            if (cond)
            {
                var card = ComputerPlayer.PlayCard();
                CurrentTrick[ComputerPlayer] = card;
            }
        }
        private IPlayer Not(IPlayer player)
        {
            Guard.WhenArgument(player, "player").IsNull().Throw();
            return UserPlayer == player ? (IPlayer)ComputerPlayer : UserPlayer;
        }
        private IPlayer RandomPlayer()
        {
            var rand = new Random();
            return rand.Next() % 2 == 0 
                ? (IPlayer)UserPlayer 
                : ComputerPlayer;
        }
        private IPlayer ResolveTrick()
        {
            if (CurrentTrick[firstPlayer].Suit != CurrentTrick[secondPlayer].Suit)
            {
                foreach (var kvp in CurrentTrick)
                {
                    if (kvp.Value.Suit == trumpSuit)
                    {
                        return kvp.Key;
                    }
                }

                return firstPlayer;
            }

            return CurrentTrick[firstPlayer].CompareTo(CurrentTrick[secondPlayer]) > 0
                ? firstPlayer
                : secondPlayer;
        }
        private IPlayer ResolveRound()
        {
            firstPlayer.RoundPoints += 10;
            IPlayer roundWinner;

            if (Closed.Initiator == null)
            {
                roundWinner = UserPlayer.RoundPoints > ComputerPlayer.RoundPoints ? (IPlayer)UserPlayer : ComputerPlayer;
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
        private IPlayer GameWinner()
        {
            if (ComputerPlayer.GamePoints >= 11)
            {
                return ComputerPlayer;
            }

            if (UserPlayer.GamePoints >= 11)
            {
                return UserPlayer;
            }

            return null;
        } 
        private bool RoundOver()
        {
            bool result = false;

            if (UserPlayer.RoundPoints >= 66 || ComputerPlayer.RoundPoints >= 66)
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
            UserPlayer.RoundPoints = 0;
            ComputerPlayer.RoundPoints = 0;
            Closed = null;
            trumpSuit = null;
            deck = deckProvider.ReshuffleDeck(UserPlayer, ComputerPlayer);
        }
        private void EndGame()
        {
            //TODO: Implement
        }
    }
}
