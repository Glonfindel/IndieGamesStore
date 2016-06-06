using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace IndieGamesStore
{
    class GameValidator
    {
        public static bool ValidateTitle(string title)
        {
            if (!Regex.IsMatch(title, @"^[a-zA-Z0-9 ]+$"))
                return false;

            return true;
        }
        public static bool ValidateDesc(string desc)
        {
            if (desc.Length < 3 || desc.Length > 255)
                return false;

            return true;
        }
        public static bool ValidateDev(string dev)
        {
            if (!Regex.IsMatch(dev, @"^[a-zA-Z0-9 ]+$"))
                return false;

            return true;
        }
        public static bool Check(GameToSave game)
        {
            if (!ValidateTitle(game.Title))
            {
                MessageBox.Show("Podaj nazwę gry!");
                return false;
            }
            if (!ValidateDesc(game.Description))
            {
                MessageBox.Show("Podaj opis gry!");
                return false;
            }
            if (!ValidateDev(game.Developer))
            {
                MessageBox.Show("Podaj twórcę gry!");
                return false;
            }
            return true;
        }
    }
}
