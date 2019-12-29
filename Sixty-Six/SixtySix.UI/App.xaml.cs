using Autofac;
using SixtySix.Container;
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
            ServiceLocator.Build();

            var mainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
