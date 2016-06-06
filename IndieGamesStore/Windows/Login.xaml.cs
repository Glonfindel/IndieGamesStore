using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace IndieGamesStore
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static User user;

        public Login()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            user = new User(this.txtBoxLogin.Text, this.txtBoxPassword.Password);

            var sha1 = new SHA1CryptoServiceProvider();
            var passwordData = Encoding.ASCII.GetBytes(user.Password);
            var passwordByte = sha1.ComputeHash(passwordData);

            StringBuilder passwordHash = new StringBuilder();

            for (int i = 0; i < passwordByte.Length; i++)
            {
                passwordHash.Append(passwordByte[i].ToString("x2"));
            }

            string[] args = { user.Login, passwordHash.ToString()};

            try
            {
                var passwordFromDb = user.Select("Users", "*", user.Login);

                if (passwordFromDb.Equals(passwordHash.ToString()))
                {
                    var igs = new IGS();
                    igs.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Nieprawidłowy login lub hasło!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Podaj login lub hasło!");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnLogin_Click(sender, null);
            }
        }
    }
}
