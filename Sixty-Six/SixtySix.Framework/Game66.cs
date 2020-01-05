using Bytes2you.Validation;
using SixtySix.Crontracts;
using SixtySix.Framework.Models;
using System;
using System.Collections.Generic;

namespace SixtySix.Framework
{
    public class Game66 : IGame
    {
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
            this.Deck = deckProvider.GenerateDeck();
            Closed = new Pair<IPlayer, bool>();
            CurrentTrick = new Dictionary<IPlayer, ICard>();
            ChangeFirstPlayer(RandomPlayer());
        }

        public IPair<IPlayer, bool> Closed { get; set; }
        public IDictionary<IPlayer, ICard> CurrentTrick { get; set; }
        public IUserPlayer UserPlayer { get; }
        public IComputerPlayer ComputerPlayer { get; }
        public ICard OpenedTrump { get; set; }
        public Queue<ICard> Deck { get; set; }
        public bool RoundOver => IsRoundOver();

        public void StartRound()
        {
            ResetRound();
            DealCards(3, 2);
            DrawTrump();
            ComputerPlaysIf(ComputerPlayer == firstPlayer);
        }

        public void ResolveCurrentTrick()
        {
            var trickWinner = ResolveTrickWinner();
            trickWinner.TakeTrick(CurrentTrick);
            CurrentTrick.Clear();
            ChangeFirstPlayer(trickWinner);

            if (RoundOver)
            {
                var roundWinner = ResolveRoundWinner();
                ScoreRound(roundWinner);
                ChangeFirstPlayer(roundWinner);
                return;
            }

            DealCards(1);
            ComputerPlaysIf(ComputerPlayer == firstPlayer);
        }
        public void ComputerPlaysIf(bool cond)
        {
            if (cond)
            {
                var card = ComputerPlayer.PlayCard();
                CurrentTrick[ComputerPlayer] = card;
                ComputerPlayer.GameInfo.cardPlayed = card;
                UserPlayer.GameInfo.cardPlayed = card;
            }
        }
        public void Close(IPlayer player)
        {
            Closed.First = player;
            Closed.Second = true;
            player.GameInfo.Closed = true;
            Not(player).GameInfo.Closed = true;
        }
        public IPlayer CheckForGameWinner()
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

        private void DealCards(int number)
        {
            if (Closed.Second)
            {
                return;
            }

            for (int i = 0; i < number; i++)
            {
                firstPlayer.CurrentHand.Add(Deck.Dequeue());
            }

            if (Deck.Count == 0)
            {
                secondPlayer.CurrentHand.Add(OpenedTrump);
                OpenedTrump = null;
                Closed.Second = true;
                return;
            }

            for (int i = 0; i < number; i++)
            {
                secondPlayer.CurrentHand.Add(Deck.Dequeue());
            }
        }
        private void DealCards(int number, int times)
        {
            for (int i = 0; i < times; i++)
            {
                DealCards(number);
            }
        }
        private void DrawTrump()
        {
            OpenedTrump = Deck.Dequeue();
            trumpSuit = OpenedTrump.Suit;
            UserPlayer.GameInfo.trumpSuit = OpenedTrump.Suit;
            ComputerPlayer.GameInfo.trumpSuit = OpenedTrump.Suit;
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
            return UserPlayer == player ? (IPlayer)ComputerPlayer : UserPlayer;
        }
        private IPlayer RandomPlayer()
        {
            var rand = new Random();
            return rand.Next() % 2 == 0
                ? (IPlayer)UserPlayer
                : ComputerPlayer;
        }
        private IPlayer ResolveTrickWinner()
        {
            if (CurrentTrick[firstPlayer].Suit != CurrentTrick[secondPlayer].Suit)
            {
                foreach (var playerCardPair in CurrentTrick)
                {
                    if (playerCardPair.Value.Suit == trumpSuit)
                    {
                        return playerCardPair.Key;
                    }
                }

                return firstPlayer;
            }

            return CurrentTrick[firstPlayer].CompareTo(CurrentTrick[secondPlayer]) > 0
                ? firstPlayer
                : secondPlayer;
        }
        private IPlayer ResolveRoundWinner()
        {
            firstPlayer.RoundPoints += 10;
            IPlayer roundWinner;

            if (Closed.First == null)
            {
                roundWinner = UserPlayer.RoundPoints > ComputerPlayer.RoundPoints ? (IPlayer)UserPlayer : ComputerPlayer;
            }
            else if (Closed.First != null &&
                Closed.First.RoundPoints >= 66)
            {
                roundWinner = Closed.First;
            }
            else
            {
                roundWinner = Not(Closed.First);
            }

            return roundWinner;
        }
        private bool IsRoundOver()
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
            if (Closed.First != null)
            {
                if (roundWinner == Closed.First &&
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
            if (UserPlayer.DiscardPile.Count > 0 || ComputerPlayer.DiscardPile.Count > 0)
            {
                var cards = CollectAllCards();
                Deck = deckProvider.ReshuffleDeck(cards);

                Closed.First = null;
                Closed.Second = false;
                trumpSuit = null;
                OpenedTrump = null;

                UserPlayer.RoundPoints = 0;
                UserPlayer.DiscardPile.Clear();
                UserPlayer.CurrentHand.Clear();
                UserPlayer.GameInfo = new GameInfo();
                ComputerPlayer.RoundPoints = 0;
                ComputerPlayer.DiscardPile.Clear();
                ComputerPlayer.CurrentHand.Clear();
                UserPlayer.GameInfo = new GameInfo();
            }
        }

        private IList<ICard> CollectAllCards()
        {
            var cards = new List<ICard>();

            foreach (var card in UserPlayer.DiscardPile)
            {
                cards.Add(card);
            }

            foreach (var card in ComputerPlayer.DiscardPile)
            {
                cards.Add(card);
            }

            foreach (var card in UserPlayer.CurrentHand)
            {
                cards.Add(card);
            }

            foreach (var card in ComputerPlayer.CurrentHand)
            {
                cards.Add(card);
            }

            if (OpenedTrump != null)
            {
                cards.Add(OpenedTrump);

                while (Deck.Count > 0)
                {
                    cards.Add(Deck.Dequeue());
                }
            }

            return cards;
        }
    }
}
