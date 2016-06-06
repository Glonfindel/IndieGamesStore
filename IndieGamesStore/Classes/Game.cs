using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using C = System.Data.SqlClient;

namespace IndieGamesStore
{
    public abstract class Game
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Developer { get; private set; }
        public string Genre { get; private set; }
        public string Link { get; private set; }

        public Game(string title, string desc, string dev, string genre, string link)
        {
            this.Title = title;
            this.Description = desc;
            this.Developer = dev;
            this.Genre = genre;
            this.Link = link;
        }

        public Game()
        {

        }

        public Game(string title, string link)
        {
            this.Title = title;
            this.Link = link;
        }

        public object SelectGames()
        {
            try
            {
                using (var connection = new C.SqlConnection(
                    "Server=tcp:igs.database.windows.net,1433;Database=IGS_database;User ID=Glonfindel;Password=Karol1995!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    ))
                {
                    connection.Open();
                    C.SqlCommand cmd = new C.SqlCommand
                    {
                        CommandText = "SELECT * FROM Games ORDER BY title",
                        CommandType = CommandType.Text,
                        Connection = connection
                    };
                    var reader = cmd.ExecuteReader();
                    Store.GameList.Clear();
                    while (reader.Read())
                    {
                        var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(reader["image"]);
                        Store.GameList.Add(new GameToLoad(reader["title"].ToString(), reader["description"].ToString(), reader["developer"].ToString(), reader["genre"].ToString(), reader["link"].ToString(), (int)reader["Id"], bitmap));
                    }
                    reader.Close();
                }
                return null;
            }
            catch (C.SqlException)
            {
                return null;
            }
        }

        public object SelectUserGames()
        {
            try
            {
                using (var connection = new C.SqlConnection(
                    "Server=tcp:igs.database.windows.net,1433;Database=IGS_database;User ID=Glonfindel;Password=Karol1995!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    ))
                {
                    connection.Open();
                    C.SqlCommand cmd = new C.SqlCommand
                    {
                        CommandText = "SELECT Games.Id, Games.image, Games.title, Games.link, UserLibrary.idGame, UserLibrary.idUser, Users.Id FROM Games INNER JOIN UserLibrary ON Games.Id = UserLibrary.idGame INNER JOIN Users ON Users.Id = UserLibrary.idUser WHERE UserLibrary.idUser = " + Login.user.ID  + " ORDER BY Games.title",
                        CommandType = CommandType.Text,
                        Connection = connection
                    };

                    var reader = cmd.ExecuteReader();
                    Library.LibraryList.Clear();
                    while (reader.Read())
                    {
                        var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(reader["image"]);
                        Library.LibraryList.Add(new GameToLoad(reader["title"].ToString(), bitmap, reader["link"].ToString()));
                    }
                    reader.Close();
                }
                return null;
            }
            catch (C.SqlException e)
            {
                return MessageBox.Show(e.ToString());
            }
        }
    }
}
