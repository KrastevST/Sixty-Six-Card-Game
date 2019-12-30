using System.Collections.Generic;

namespace SixtySix.Crontracts
{
    public interface IGame
    {
        IDictionary<IPlayer, ICard> CurrentTrick { get; set; }
        IUserPlayer UserPlayer { get; }
        IComputerPlayer ComputerPlayer { get; }

        void Start();
        void CheckCurrentTrick();
    }
}
