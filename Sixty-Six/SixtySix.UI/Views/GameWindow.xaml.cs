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
            UpdateUI();
        }

        private void card1_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(0);
        }

        private void card2_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(1);
        }

        private void card3_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(2);
        }

        private void card4_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(3);
        }

        private void card5_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(4);
        }

        private void card6_Click(object sender, RoutedEventArgs e)
        {
            PlayCard(5);
        }

        private void openTrump_Click(object sender, RoutedEventArgs e)
        {

        }

        private void trickCard1_Click(object sender, RoutedEventArgs e)
        {
            TakeTrick();
        }

        private void trickCard2_Click(object sender, RoutedEventArgs e)
        {
            TakeTrick();
        }

        private void talon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayCard(int index)
        {
            var game = ServiceLocator.Resolve<IGame>();

            if (index < game.UserPlayer.CurrentHand.Count)
            {
                var card = game.UserPlayer.PlayCard(index);
                game.CurrentTrick[game.UserPlayer] = card;
                game.UserPlayer.GameInfo.cardPlayed = card;
                game.ComputerPlayer.GameInfo.cardPlayed = card;

                game.computerPlaysIf(!game.CurrentTrick.ContainsKey(game.ComputerPlayer));
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            var game = ServiceLocator.Resolve<IGame>();
            var playerHand = game.UserPlayer.CurrentHand;

            if (playerHand.Count >= 1)
            {
                card1Rank.Text = playerHand[0].Rank.ToUpper();
                card1Suit.Text = playerHand[0].Suit.ToLower();
            }
            else
            {
                card1Rank.Text = null;
                card1Suit.Text = null;
            }

            if (playerHand.Count >= 2)
            {
                card2Rank.Text = playerHand[1].Rank.ToUpper();
                card2Suit.Text = playerHand[1].Suit.ToLower();
            }
            else
            {
                card2Rank.Text = null;
                card2Suit.Text = null;
            }

            if (playerHand.Count >= 3)
            {
                card3Rank.Text = playerHand[2].Rank.ToUpper();
                card3Suit.Text = playerHand[2].Suit.ToLower();
            }
            else
            {
                card3Rank.Text = null;
                card3Suit.Text = null;
            }

            if (playerHand.Count >= 4)
            {
                card4Rank.Text = playerHand[3].Rank.ToUpper();
                card4Suit.Text = playerHand[3].Suit.ToLower();
            }
            else
            {
                card4Rank.Text = null;
                card4Suit.Text = null;
            }

            if (playerHand.Count >= 5)
            {
                card5Rank.Text = playerHand[4].Rank.ToUpper();
                card5Suit.Text = playerHand[4].Suit.ToLower();
            }
            else
            {
                card5Rank.Text = null;
                card5Suit.Text = null;
            }

            if (playerHand.Count >= 6)
            {
                card6Rank.Text = playerHand[5].Rank.ToUpper();
                card6Suit.Text = playerHand[5].Suit.ToLower();
            }
            else
            {
                card6Rank.Text = null;
                card6Suit.Text = null;
            }

            if (game.OpenedTrump == null)
            {
                openedTrumpRank.Text = null;
                openedTrumpSuit.Text = null;
            }
            else
            {
                openedTrumpRank.Text = game.OpenedTrump.Rank.ToUpper();
                openedTrumpSuit.Text = game.OpenedTrump.Suit.ToLower();
            }

            talonLabel.Text = game.Deck.Count.ToString();

            if (game.CurrentTrick.ContainsKey(game.ComputerPlayer))
            {
                trickCard1Rank.Text = game.CurrentTrick[game.ComputerPlayer].Rank.ToUpper();
                trickCard1Suit.Text = game.CurrentTrick[game.ComputerPlayer].Suit.ToLower();
            }
            else
            {
                trickCard1Rank.Text = null;
                trickCard1Suit.Text = null;
            }

            if (game.CurrentTrick.ContainsKey(game.UserPlayer))
            {
                trickCard2Rank.Text = game.CurrentTrick[game.UserPlayer].Rank.ToUpper();
                trickCard2Suit.Text = game.CurrentTrick[game.UserPlayer].Suit.ToLower();
            }
            else
            {
                trickCard2Rank.Text = null;
                trickCard2Suit.Text = null;
            }

            discardPile1Label.Text = game.ComputerPlayer.RoundPoints.ToString();
            discardPile2Label.Text = game.UserPlayer.RoundPoints.ToString();
        }

        private void TakeTrick()
        {
            var game = ServiceLocator.Resolve<IGame>();

            if (game.CurrentTrick.Count == 2)
            {
                game.ResolveCurrentTrick();

                if (game.RoundOver)
                {
                    var resultsWindow = new ResultsWindow();
                    resultsWindow.Show();
                    this.Close();
                }

                UpdateUI();
            }
        }
    }
}
