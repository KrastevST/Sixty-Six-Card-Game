using Autofac;
using SixtySix.Crontracts;
using SixtySix.Framework.Models;
using SixtySix.Framework.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtySix.Container.Modules
{
    public class BasicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Card>().As<ICard>();
            builder.RegisterType<DeckProvider66>().As<IDeckProvider>();
            builder.RegisterType<Game66>().As<IGame>();
        }
    }
}
