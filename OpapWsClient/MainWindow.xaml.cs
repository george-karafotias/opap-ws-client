using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace OpapWsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private OpapResultsClient opapResultsClient;
        private OpapResults opapResults;
        private OpapDateResults opapDateResults;
        private Game selectedGame;

        public Game SelectedGame
        {
            get
            {
                return selectedGame;
            }
            set
            {
                selectedGame = value;
                RaisePropertyChanged("SelectedGame");
            }
        }

        public OpapResults OpapResults
        {
            get
            {
                return opapResults;
            }
            set
            {
                opapResults = value;
                RaisePropertyChanged("OpapResults");
            }
        }

        public OpapDateResults OpapDateResults
        {
            get
            {
                return opapDateResults;
            }
            set
            {
                opapDateResults = value;
                RaisePropertyChanged("OpapDateResults");
            }
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            opapResultsClient = new OpapResultsClient();
        }

        private void ShowGameSelectionPanel()
        {
            GameSelectionPanel.Visibility = Visibility.Visible;
            DrawSelectionPanel.Visibility = Visibility.Collapsed;
            lastDrawPanel.Visibility = Visibility.Collapsed;
            specificDrawPanel.Visibility = Visibility.Collapsed;
            drawByDatePanel.Visibility = Visibility.Collapsed;
            menuPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowDrawSelectionPanel()
        {
            GameSelectionPanel.Visibility = Visibility.Collapsed;
            DrawSelectionPanel.Visibility = Visibility.Visible;
            lastDrawPanel.Visibility = Visibility.Collapsed;
            specificDrawPanel.Visibility = Visibility.Collapsed;
            drawByDatePanel.Visibility = Visibility.Collapsed;
            menuPanel.Visibility = Visibility.Visible;
        }

        private void ShowLastDrawPanel()
        {
            GameSelectionPanel.Visibility = Visibility.Collapsed;
            DrawSelectionPanel.Visibility = Visibility.Collapsed;
            lastDrawPanel.Visibility = Visibility.Visible;
            specificDrawPanel.Visibility = Visibility.Collapsed;
            drawByDatePanel.Visibility = Visibility.Collapsed;
            menuPanel.Visibility = Visibility.Visible;
        }

        private void ShowSpecificDrawPanel()
        {
            GameSelectionPanel.Visibility = Visibility.Collapsed;
            DrawSelectionPanel.Visibility = Visibility.Collapsed;
            lastDrawPanel.Visibility = Visibility.Collapsed;
            specificDrawPanel.Visibility = Visibility.Visible;
            drawByDatePanel.Visibility = Visibility.Collapsed;
            menuPanel.Visibility = Visibility.Visible;
            OpapResults = null;
            DrawNumber.Text = "";
            DrawNumber.Focus();
            specificDrawResultsPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowDrawByDatePanel()
        {
            GameSelectionPanel.Visibility = Visibility.Collapsed;
            DrawSelectionPanel.Visibility = Visibility.Collapsed;
            lastDrawPanel.Visibility = Visibility.Collapsed;
            specificDrawPanel.Visibility = Visibility.Collapsed;
            drawByDatePanel.Visibility = Visibility.Visible;
            menuPanel.Visibility = Visibility.Visible;
            OpapResults = null;
            DrawDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            DrawDate.Focus();
            drawByDateResultsPanel.Visibility = Visibility.Collapsed;
        }

        private void gameButton_Click(object sender, RoutedEventArgs e)
        {
            Button gameButton = sender as Button;
            SelectedGame = new Game(){ Name = gameButton.Name, Title = gameButton.Tag.ToString() };
            ShowDrawSelectionPanel();
        }

        private void lastDrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGame != null)
            {
                OpapResults = opapResultsClient.GetLastResults(SelectedGame.Name);
                ShowLastDrawPanel();
            }
        }

        private void showSpecificDrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGame != null)
            {
                ShowSpecificDrawPanel();
            }
        }

        private void specificDrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGame != null)
            {
                if (!string.IsNullOrEmpty(DrawNumber.Text))
                {
                    OpapResults = opapResultsClient.GetSpecificDrawResults(SelectedGame.Name, DrawNumber.Text.Trim());
                    if (OpapResults != null && OpapResults.draw != null)
                        specificDrawResultsPanel.Visibility = Visibility.Visible;
                    else
                        specificDrawResultsPanel.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void showDrawByDateButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGame != null)
            {
                ShowDrawByDatePanel();
            }
        }

        private void drawByDateButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGame != null)
            {
                if (!string.IsNullOrEmpty(DrawDate.Text))
                {
                    OpapDateResults = opapResultsClient.GetDrawByDateResults(SelectedGame.Name, DrawDate.Text.Trim());
                    if (OpapDateResults != null && OpapDateResults.draws != null && OpapDateResults.draws.draw != null)
                    {
                        drawByDateResultsPanel.Visibility = Visibility.Visible;
                    }   
                    else
                    {
                        drawByDateResultsPanel.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void goHomeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowGameSelectionPanel();
        }

        private void selectDrawButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDrawSelectionPanel();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propertyName)
        {
            var handlers = PropertyChanged;

            handlers(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Game
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
