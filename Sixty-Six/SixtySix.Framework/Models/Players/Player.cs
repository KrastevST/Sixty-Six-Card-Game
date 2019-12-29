using SixtySix.Crontracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtySix.Framework.Models.Players
{
    public class Player : IPlayer
    {
        public bool IsFirst { get; set; }
        public ICollection<ICard> CurrentHand { get; set; }
        public ICollection<ICard> DiscardPile { get; set; }
        public int RoundPoints { get; set; }
        public int GamePoints { get; set; }

        public ICard PlayCard()
        {
            return null;
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
