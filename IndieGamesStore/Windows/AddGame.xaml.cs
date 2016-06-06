using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using Microsoft.Win32;

namespace IndieGamesStore
{
    /// <summary>
    /// Interaction logic for AddGame.xaml
    /// </summary>
    public partial class AddGame : Window
    {

        public AddGame()
        {
            InitializeComponent();
            this.cbGenre.ItemsSource = Enum.GetValues(typeof(Genre.eGenre));
            this.cbGenre.SelectedIndex = 0;
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Wybierz obrazek.";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imagePreview.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void btnAddGame_Click(object sender, RoutedEventArgs e)
        {
            var imageBytes = getJPGFromImageControl(imagePreview.Source as BitmapImage);

            if (imageBytes != null)
            {
                GameToSave addGame = new GameToSave(txtGameTitle.Text,
                    new TextRange(rtbDescription.Document.ContentStart, rtbDescription.Document.ContentEnd).Text,
                    txtDeveloper.Text, cbGenre.Text, txtLink.Text, imageBytes);

                if (GameValidator.Check(addGame))
                {
                    object[] args = { addGame.Title, addGame.Description, addGame.Genre, addGame.Image, addGame.Developer, addGame.Link };

                    addGame.Insert("Games", args);
                    addGame.SelectGames();   
                    Close();
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageC));
                encoder.Save(memStream);
                return memStream.ToArray();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Wybierz obrazek!");
                return null;
            }
        }
    }
}
