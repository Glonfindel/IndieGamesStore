using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using IndieGamesStore.Properties;

namespace IndieGamesStore
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : Page
    {
        public static GameToLoad game;
        
        public static NavigationService navService;
        public static GameToLoad storeGames = new GameToLoad();
        public static ObservableCollection<GameToLoad> GameList { get; set; }
        public Store()
        {
            InitializeComponent();
            GameList = new ObservableCollection<GameToLoad>();
            this.DataContext = this;
            storeGames.SelectGames();
        }

        public void Sort(string genre)
        {
            storeGames.SortGames(genre);
            lvGames.ItemsSource = Store.GameList;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                game = (GameToLoad) item.Content;
                ShowGame gamePage = new ShowGame();
                this.NavigationService.Navigate(gamePage);
            }
        }
    }
}
