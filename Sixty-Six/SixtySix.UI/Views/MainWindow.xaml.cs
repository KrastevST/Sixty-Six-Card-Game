using SixtySix.Crontracts;
using SixtySix.Injection;
using System;
using System.Windows;

namespace SixtySix.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Resolve<IGame>().Start();
            var gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
