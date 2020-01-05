using SixtySix.Crontracts;
using System.Linq;

namespace SixtySix.Framework.Models.Players
{
    public class UserPlayer : Player, IUserPlayer
    {
        public ICard PlayCard(int index)
        {
            var card = CurrentHand[index];

            if (IsValid(card))
            {
                CurrentHand.Remove(card);
                return card;
            }

            return null;
        }

        private bool IsValid(ICard card)
        {
            bool result = true;

            if (GameInfo.Closed == true && IsFirst == false)
            {
                var cardSuit = card.Suit.ToLower();
                var calledSuit = GameInfo.cardPlayed.Suit.ToLower();
                var trumpSuit = GameInfo.trumpSuit.ToLower();

                if (cardSuit != calledSuit)
                {
                    if (HasInHand(calledSuit))
                    {
                        result = false;
                    }
                    else if (HasInHand(trumpSuit) &&
                        cardSuit != trumpSuit)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        private bool HasInHand(string suit)
        {
            return CurrentHand.Any(x => x.Suit.ToLower() == suit.ToLower());
        }
    }
}
