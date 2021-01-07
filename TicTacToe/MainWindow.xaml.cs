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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        private MarkType[][] values;
        private bool playerOneTurn;
        private bool gameEnded;

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            values = new MarkType[3][];
            for (int i = 0; i < 3; i++)
            {
                values[i] = new MarkType[3];
                for (int j = 0; j < 3; j++)
                {
                    values[i][j] = MarkType.Free;
                }

                
            }
            playerOneTurn = true;
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.Black;
                button.Foreground = Brushes.Green;
            });

            gameEnded = false;

        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button) sender;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            
            if (values[column][row] != MarkType.Free)
                return;

            values[column][row] = playerOneTurn ? MarkType.Cross : MarkType.Circle;
            
            button.Content = playerOneTurn ? "X" : "O";

            if (!playerOneTurn)
                button.Foreground = Brushes.Red;
            
            playerOneTurn = !playerOneTurn;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    var testing = values[i][j];
                    if (values[i][j-1] != values[i][j] || values[i][j] == MarkType.Free)
                        break;
                    if(j < 2)
                        continue;
                    gameEnded = true;
                    MarkRow(i);
                    return;
                }
                for (int j = 1; j < 3; j++)
                {
                    if(values[j-1][i] != values[j][i] || values[j][i] == MarkType.Free)
                        break;
                    if(j < 2)
                        continue;
                    gameEnded = true;
                    MarkCol(i);
                    return;
                }
            }

            for (int i = 1; i < 3; i++)
            {
                if (values[i-1][i-1] != values[i][i] || values[i][i] == MarkType.Free)
                    break;
                if (i < 2)
                    continue;
                gameEnded = true;
                MarkDiag();
                return;
            }
                
            for (int i = 1, j = 1; i < 3 & j >= 0; i++, j--)
            {
                if (values[i-1][j+1] != values[i][j] || values[i][j] == MarkType.Free)
                    break;
                if (i < 2)
                    continue;
                gameEnded = true;
                MarkRDiag();
            }
            
            if(FullBoard())
                NewGame();
        }

        private void MarkCol(int i)
        {
            for (int j = 0; j < 3; j++)
            {
                Container.Children.Cast<Button>().ToList()[j + i * 3].Background = Brushes.Gold;
            }
        }

        private void MarkRow(int i)
        {
            for (int j = 0+i; j < 7+i; j+=3)
            {
                Container.Children.Cast<Button>().ToList()[j].Background = Brushes.Gold;
            }
        }

        private void MarkDiag()
        {
            for (int j = 0; j < 9; j+=4)
            {
                Container.Children.Cast<Button>().ToList()[j].Background = Brushes.Gold;
            }
        }

        private void MarkRDiag()
        {
            for (int j = 2; j < 7; j+=2)
            {
                Container.Children.Cast<Button>().ToList()[j].Background = Brushes.Gold;
            }
        }

        private bool FullBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (values[i][j] == MarkType.Free)
                        return false;
                }
            }
            return true;
        }
    }
}