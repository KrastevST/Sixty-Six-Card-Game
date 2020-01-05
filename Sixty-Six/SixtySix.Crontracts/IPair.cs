namespace SixtySix.Crontracts
{
    public interface IPair<T, U>
    {
        T First { get; set; }
        U Second { get; set; }
    }
}
