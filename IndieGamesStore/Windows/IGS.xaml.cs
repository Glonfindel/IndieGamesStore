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
using IndieGamesStore.Pages;

namespace IndieGamesStore
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class IGS : Window
    {
        Store storePage = new Store();
        Library libraryPage = new Library();

        public IGS()
        {
            InitializeComponent();
            pageController.NavigationService.Navigate(storePage);
            
        }

        private void btnShop_Click(object sender, RoutedEventArgs e)
        {
            pageController.NavigationService.Navigate(storePage);
        }

        private void btnLibrary_Click(object sender, RoutedEventArgs e)
        {

            pageController.NavigationService.Navigate(libraryPage);
        }

        private void btnAddGame_Click(object sender, RoutedEventArgs e)
        {
            var addGame = new AddGame();
            addGame.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;

            storePage.Sort(mi.Name.ToString());

        }

        private void MenuItemAll_Click(object sender, RoutedEventArgs e)
        {
            Store.storeGames.SelectGames();
        }
    }
}
