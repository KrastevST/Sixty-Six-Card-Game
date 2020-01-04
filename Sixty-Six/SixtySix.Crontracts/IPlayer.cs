using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IPlayer
    {
        bool IsFirst { get; set; }
        IList<ICard> CurrentHand { get; set; }
        ICollection<ICard> DiscardPile { get; set; }
        int RoundPoints { get; set; }
        int GamePoints { get; set; }
        IGameInfo GameInfo { get; set; }

        void TakeTrick(IDictionary<IPlayer, ICard> currentRound);
    }
}
