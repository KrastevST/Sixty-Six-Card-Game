using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IGame
    {
        IDictionary<IPlayer, ICard> CurrentTrick { get; set; }
        IUserPlayer UserPlayer { get; }
        IComputerPlayer ComputerPlayer { get; }
        ICard OpenedTrump { get; set; }
        Queue<ICard> Deck { get; set; }

        void Start();
        bool CheckCurrentTrick();
        void computerPlaysIf(bool cond);
    }
}
