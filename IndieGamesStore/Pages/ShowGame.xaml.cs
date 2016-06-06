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

namespace IndieGamesStore.Pages
{
    /// <summary>
    /// Interaction logic for ShowGame.xaml
    /// </summary>
    public partial class ShowGame : Page
    {
        private GameToLoad gameToShow;
        public ShowGame()
        {
            InitializeComponent();
            gameToShow = Store.game;

            gameImage.Source = gameToShow.Image;
            gameTitle.Content = gameToShow.Title;
            gameDev.Content = gameToShow.Developer;
            gameDesc.Text = gameToShow.Description;

            btnAddToLibrary.Content = "Dodaj " + gameTitle.Content + " do swojej biblioteki.";
        }

        private void btnAddToLibrary_Click(object sender, RoutedEventArgs e)
        {
            gameToShow.AddToLibrary();
            Store.storeGames.SelectUserGames();
        }
    }
}
