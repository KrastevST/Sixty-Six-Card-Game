using SixtySix.Crontracts;

namespace SixtySix.Framework.Models
{
    public class Pair<T, U> : IPair<T, U>
    {
        public T First { get; set; }
        public U Second { get; set; }
    }
}
