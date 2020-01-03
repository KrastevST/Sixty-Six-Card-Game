using SixtySix.Crontracts;
using System.Collections.Generic;
using System.Linq;

namespace SixtySix.Framework.Models.Players
{
    public class ComputerPlayer : Player, IComputerPlayer
    {
        public ICard PlayCard()
        {
            if (GameInfo.Closed == true && this.IsFirst == false)
            {
                var sameSuitCards = CurrentHand.Where(x => x.Suit.ToLower() == GameInfo.cardPlayed.Suit.ToLower());
                if (sameSuitCards.Count() > 0)
                {
                    return StrongestCard(sameSuitCards);
                }

                var trumps = CurrentHand.Where(x => x.Suit.ToLower() == GameInfo.trumpSuit.ToLower());
                if (trumps.Count() > 0)
                {
                    return WeakestCard(trumps);
                }

                return WeakestCard(CurrentHand);
            }
            else if (GameInfo.Closed == false && this.IsFirst == false)
            {
                bool playedNotATrump = GameInfo.cardPlayed.Suit.ToLower() != GameInfo.trumpSuit.ToLower();
                if (playedNotATrump)
                {
                    var strongerCards = CurrentHand.Where(x =>
                        x.Suit.ToLower() == GameInfo.cardPlayed.Suit.ToLower() &&
                        x.Value > GameInfo.cardPlayed.Value);
                    if (strongerCards.Count() > 0)
                    {
                        return WeakestCard(strongerCards);
                    }
                }
            }

            var nonTrumps = CurrentHand.Where(x => x.Suit.ToLower() != GameInfo.trumpSuit.ToLower());
            if (nonTrumps.Count() > 0)
            {
                return WeakestCard(nonTrumps);
            }

            return WeakestCard(CurrentHand);
        }

        private ICard WeakestCard(IEnumerable<ICard> set)
        {
            return set.OrderBy(x => x.Value).ToArray()[0];
        }
        private ICard StrongestCard(IEnumerable<ICard> set)
        {
            return set.OrderByDescending(x => x.Value).ToArray()[0];
        }
    }
}
