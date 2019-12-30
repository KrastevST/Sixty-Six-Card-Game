using SixtySix.Container;
using SixtySix.Crontracts;
using SixtySix.Injection;
using System.Windows;

namespace SixtySix.UI.Views
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        private void card1_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(1);
        }

        private void card2_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(2);
        }

        private void card3_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(3);
        }

        private void card4_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(4);
        }

        private void card5_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(5);
        }

        private void card6_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(6);
        }

        private void openTrump_Click(object sender, RoutedEventArgs e)
        {

        }

        private void trickCard1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void trickCard2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void talon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayCard(int index)
        {
            var game = ServiceLocator.Resolve<IGame>();
            var player = game.UserPlayer;
            var card = player.PlayCard(index);
            game.CurrentTrick[player] = card;
            game.CheckCurrentTrick();
        }
    }
}
