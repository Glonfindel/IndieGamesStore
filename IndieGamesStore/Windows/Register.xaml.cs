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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        UserRegister dbConnect = new UserRegister();

        public Register()
        {
            InitializeComponent();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            UserRegister userRegister = new UserRegister(this.txtBoxLogin.Text, this.txtBoxPassword.Password, this.txtBoxPasswordRepeat.Password, this.txtBoxEmail.Text, this.txtBoxEmailRepeat.Text);
            if (RegistationValidator.Check(userRegister))
            {
                var sha1 = new SHA1CryptoServiceProvider();
                var passwordData = Encoding.ASCII.GetBytes(userRegister.Password);
                var passwordByte = sha1.ComputeHash(passwordData);

                StringBuilder passwordHash = new StringBuilder();

                for (int i = 0; i < passwordByte.Length; i++)
                {
                    passwordHash.Append(passwordByte[i].ToString("x2"));
                }

                string[] args = { userRegister.Login, passwordHash.ToString(), userRegister.Email };

                dbConnect.Insert("Users", args);
                var login = new Login();
                login.Show();
                Close();
            }
        }
    }
}
