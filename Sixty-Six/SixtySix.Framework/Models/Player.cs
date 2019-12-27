using SixtySix.Crontracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtySix.Framework.Models
{
    public class Player : IPlayer
    {
        private ICollection<Card> takenCards;
        private Dictionary<string, int> cardPoints;

        public Player()
        {

        }

        public Dictionary<string, int> CardPoints => this.cardPoints;
        public bool IsFirst { get; set; }
        public ICollection<ICard> CurrentHand { get; set; }
        public int RoundPoints { get; set; }
        public int GamePoints { get; set; }

        public ICard PlayCard()
        {
            throw new NotImplementedException();
        }

        public void TakeTrick(IDictionary<IPlayer, ICard> currentRound)
        {
            throw new NotImplementedException();
        }
    }
}
