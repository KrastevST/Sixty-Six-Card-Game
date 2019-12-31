namespace SixtySix.Crontracts
{
    public interface ICardFactory
    {
        ICard GenerateCard(string rank, string suit, int value);
    }
}
