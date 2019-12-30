using Autofac;
using SixtySix.Crontracts;
using SixtySix.Framework.Models;
using SixtySix.Framework.Models.Players;
using SixtySix.Framework.Providers;
using System.Collections.Generic;

namespace SixtySix.Container.Modules
{
    public class BasicModule : Module
    {
        private const int handSize = 6;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Card>().As<ICard>();
            builder.RegisterType<DeckProvider66>().As<IDeckProvider>().SingleInstance();
            builder.Register(c => new UserPlayer(handSize)).As<IUserPlayer>();
            builder.Register(c => new ComputerPlayer(handSize)).As<IComputerPlayer>();
            builder.RegisterType<Game66>().As<IGame>().SingleInstance();
        }
    }
}
