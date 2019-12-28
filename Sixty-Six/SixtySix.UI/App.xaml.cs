using Autofac;
using SixtySix.Container.Modules;
using SixtySix.UI.Views;
using System.Windows;

namespace SixtySix.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void App_Startup(object sender, StartupEventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<BasicModule>();

            var container = builder.Build();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
