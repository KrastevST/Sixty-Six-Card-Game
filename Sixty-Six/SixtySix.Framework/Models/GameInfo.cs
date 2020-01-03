using SixtySix.Crontracts;

namespace SixtySix.Framework.Models
{
    public class GameInfo : IGameInfo
    {
        public bool Closed { get; set; }
        public ICard cardPlayed { get; set; }
        public string trumpSuit { get; set; }
    }
}
