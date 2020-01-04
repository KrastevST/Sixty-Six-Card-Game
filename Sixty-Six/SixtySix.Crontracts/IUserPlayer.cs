namespace SixtySix.Crontracts
{
    public interface IUserPlayer : IPlayer
    {
        ICard PlayCard(int index);
    }
}
