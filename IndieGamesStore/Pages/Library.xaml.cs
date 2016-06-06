using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace IndieGamesStore
{
    /// <summary>
    /// Interaction logic for Library.xaml
    /// </summary>
    public partial class Library : Page
    {
        public static ObservableCollection<GameToLoad> LibraryList { get; set; }
        public Library()
        {
            InitializeComponent();
            LibraryList = new ObservableCollection<GameToLoad>();
            this.DataContext = this;
            Store.storeGames.SelectUserGames();
        }

        private void btnDownloadGame_Click(object sender, RoutedEventArgs e)
        {
            var url = ((Button)sender).Tag;
            System.Diagnostics.Process.Start(url.ToString());
        }
    }
}
