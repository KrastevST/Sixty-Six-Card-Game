using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IPlayer
    {
        ICollection<ICard> CurrentHand { get; set; }
        bool IsFirst { get; set; }
        int RoundPoints { get; set; }
        int GamePoints { get; set; }

        ICard PlayCard();
        void TakeTrick(IDictionary<IPlayer, ICard> currentRound);
    }
}
