using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IGame
    {
        IPair<IPlayer, bool> Closed { get; set; }
        IDictionary<IPlayer, ICard> CurrentTrick { get; set; }
        IUserPlayer UserPlayer { get; }
        IComputerPlayer ComputerPlayer { get; }
        ICard OpenedTrump { get; set; }
        Queue<ICard> Deck { get; set; }
        bool RoundOver { get; }

        void StartRound();
        void ResolveCurrentTrick();
        void ComputerPlaysIf(bool cond);
        IPlayer CheckForGameWinner();
        void Close(IPlayer player);
    }
}
