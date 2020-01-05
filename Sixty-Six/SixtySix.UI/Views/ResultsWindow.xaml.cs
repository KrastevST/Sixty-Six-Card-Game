using SixtySix.Crontracts;
using SixtySix.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SixtySix.UI.Views
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public ResultsWindow()
        {
            InitializeComponent();
            DisplayStats();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            var game = ServiceLocator.Resolve<IGame>();

            if (game.CheckForGameWinner() == null)
            {
                game.StartRound();
                var gameWindow = new GameWindow();
                gameWindow.Show();
                this.Close();
            }
            else
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0]);
            }
        }

        private void DisplayStats()
        {
            var game = ServiceLocator.Resolve<IGame>();
            userRoundPointsLabel.Text = game.UserPlayer.RoundPoints.ToString();
            userGamePointsLabel.Text = game.UserPlayer.GamePoints.ToString();
            computerRoundPointsLabel.Text = game.ComputerPlayer.RoundPoints.ToString();
            computerGamePointsLabel.Text = game.ComputerPlayer.GamePoints.ToString();
        }
    }
}
