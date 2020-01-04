using SixtySix.Crontracts;

namespace SixtySix.Framework.Models.Players
{
    public class UserPlayer : Player, IUserPlayer
    {
        public ICard PlayCard(int index)
        {
            var card = CurrentHand[index];
            CurrentHand.Remove(card);
            return card;
        }
    }
}
