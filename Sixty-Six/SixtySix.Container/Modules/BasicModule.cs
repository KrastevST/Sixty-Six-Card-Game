using Autofac;
using SixtySix.Crontracts;
using SixtySix.Framework.Models;
using SixtySix.Framework.Factories;
using SixtySix.Framework.Models.Players;
using SixtySix.Framework.Providers;
using SixtySix.Framework;

namespace SixtySix.Container.Modules
{
    public class BasicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Card>().As<ICard>();
            builder.RegisterType<CardFactory>().As<ICardFactory>();
            builder.RegisterType<DeckProvider66>().As<IDeckProvider>().SingleInstance();
            builder.RegisterType<UserPlayer>().As<IUserPlayer>();
            builder.RegisterType<ComputerPlayer>().As<IComputerPlayer>();
            builder.RegisterType<Game66>().As<IGame>().SingleInstance();
        }
    }
}
