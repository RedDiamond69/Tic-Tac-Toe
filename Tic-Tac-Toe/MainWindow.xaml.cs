using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it's player 1's turn (X) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of the free cells.
            mResults = new MarkType[9];
            for(int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }
            // Make sure Player 1 starts the game.
            mPlayer1Turn = true;
            // Iterate every button on the grid.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Changed background, foreground and content to default values.
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.DarkViolet;
            });
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            int index = column + (row * 3);
            if (mResults[index] != MarkType.Free)
                return;
            mResults[index] = (mPlayer1Turn) ? MarkType.Cross : MarkType.Nought;
            button.Content = (mPlayer1Turn) ? "X" : "O";
            if (!mPlayer1Turn)
                button.Foreground = Brushes.DarkRed;
            mPlayer1Turn ^= true;
            CheckWinner();
        }

        /// <summary>
        /// Check the winner
        /// </summary>
        private void CheckWinner()
        {
            #region Horizontal Wins

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGreen;
                MessageBox.Show($"Player {((!mPlayer1Turn) ? 1 : 2)} Win!!!", 
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
                MessageBox.Show($"Player {((!mPlayer1Turn) ? 1 : 2)} Win!!!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGreen;
                MessageBox.Show($"Player {((!mPlayer1Turn) ? 1 : 2)} Win!!!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            #endregion

            #region Vertical Wins

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.LightGreen;
                MessageBox.Show($"Player {((!mPlayer1Turn) ? 1 : 2)} Win!!!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.LightGreen;
                MessageBox.Show($"Player {((!mPlayer1Turn) ? 1 : 2)} Win!!!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.LightGreen;
                MessageBox.Show($"Player {((!mPlayer1Turn) ? 1 : 2)} Win!!!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            #endregion

            #region Diagonal Wins

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightGreen;
                MessageBox.Show($"Player {((!mPlayer1Turn) ? 1 : 2)} Win!!!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.LightGreen;
                MessageBox.Show($"Player {((!mPlayer1Turn) ? 1 : 2)} Win!!!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            #endregion

            #region No Wins

            if (!mResults.Any(f => f == MarkType.Free))
            {
                mGameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    // Changed background, foreground and content to default values.
                    button.Background = Brushes.Orange;
                });
                MessageBox.Show("Draw!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            #endregion
        }
    }
}
