using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace IndieGamesStore
{
    class RegistationValidator
    {
        public static bool ValidateLogin(string login)
        {
            if (login == null)
                return false;
            if (login.Length < 6)
                return false;
            if (!Regex.IsMatch(login, @"^(?=[a-zA-Z])[-\w.]{6,32}([a-zA-Z\d]|(?<![-.])_)$"))
                return false;

            return true;
        }

        public static bool ValidatePassword(string password, string passwordRepeat)
        {
            if (password == null)
                return false;
            if (password.Length < 8)
                return false;
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$"))
                return false;
            if (!password.Equals(passwordRepeat))
                return false;
            return true;
        }

        public static bool ValidateEmail(string email, string emailRepeat)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                if (!email.Equals(emailRepeat))
                    return false;
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static bool Check(UserRegister user)
        {
            if (!ValidateLogin(user.Login))
            {
                MessageBox.Show("Nieprawidłowy login!");
                return false;
            }
            if (!ValidatePassword(user.Password, user.PasswordRepeat))
            {
                MessageBox.Show("Nieprawidłowe hasło lub hasła nie są takie same!");
                return false;
            }
            if (!ValidateEmail(user.Email, user.EmailRepeat))
            {
                MessageBox.Show("Nieprawidłowy email lub adresy email nie są takie same!");
                return false;
            }
            return true;
        }

    }
}
