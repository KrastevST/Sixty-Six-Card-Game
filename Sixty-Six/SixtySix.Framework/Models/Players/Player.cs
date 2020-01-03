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
        }

        public bool IsFirst { get; set; }
        public IList<ICard> CurrentHand { get; set; }
        public ICollection<ICard> DiscardPile { get; set; }
        public int RoundPoints { get; set; }
        public int GamePoints { get; set; }
        public IGameInfo GameInfo { get; set; }

        public ICard PlayCard(int index)
        {
            var card = CurrentHand[index];
            CurrentHand.Remove(card);

            return card;
        }

        public void TakeTrick(IDictionary<IPlayer, ICard> currentTrick)
        {
            foreach (var kvp in currentTrick)
            {
                DiscardPile.Add(kvp.Value);
                RoundPoints += kvp.Value.Value;
            }
        }
    }
}
