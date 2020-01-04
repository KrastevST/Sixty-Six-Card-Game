using Bytes2you.Validation;
using SixtySix.Crontracts;
using System;
using System.Collections.Generic;

namespace SixtySix.Framework.Models.Players
{
    public class Player : IPlayer
    {
        public Player()
        {
            CurrentHand = new List<ICard>();
            DiscardPile = new List<ICard>();
            GameInfo = new GameInfo();
        }

        public bool IsFirst { get; set; }
        public IList<ICard> CurrentHand { get; set; }
        public ICollection<ICard> DiscardPile { get; set; }
        public int RoundPoints { get; set; }
        public int GamePoints { get; set; }
        public IGameInfo GameInfo { get; set; }

        public void TakeTrick(IDictionary<IPlayer, ICard> currentTrick)
        {
            foreach (var playerCardPair in currentTrick)
            {
                DiscardPile.Add(playerCardPair.Value);
                RoundPoints += playerCardPair.Value.Value;
            }
        }
    }
}
