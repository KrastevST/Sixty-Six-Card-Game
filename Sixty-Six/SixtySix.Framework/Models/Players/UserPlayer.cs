using SixtySix.Crontracts;

namespace SixtySix.Framework.Models.Players
{
    public class UserPlayer : Player, IUserPlayer
    {
        public UserPlayer(int handSize)
            : base(handSize)
        {

        }
    }
}
