namespace SixtySix.Crontracts
{
    public interface IGameInfo
    {
        bool Closed { get; set; }
        ICard cardPlayed { get; set; }
        string trumpSuit { get; set; }
    }
}
