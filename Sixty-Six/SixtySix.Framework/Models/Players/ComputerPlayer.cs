using SixtySix.Crontracts;

namespace SixtySix.Framework.Models.Players
{
    public class ComputerPlayer : Player, IComputerPlayer
    {
        public ComputerPlayer(int handSize)
            : base(handSize)
        {

        }
    }
}
