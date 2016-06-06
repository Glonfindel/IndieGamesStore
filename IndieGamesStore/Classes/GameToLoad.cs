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
    public class GameToLoad : Game
    {
        public BitmapSource Image { get; private set; }
        public int ID { get; private set; }

        public GameToLoad() 
        {
        }

        public GameToLoad(string title, BitmapSource image, string link) : base(title, link)
        {
            this.Image = image;
        }

        public GameToLoad(string title, string desc, string dev, string genre, string link, int id, BitmapSource image) : base(title, desc, dev, genre, link)
        {
            this.Image = image;
            this.ID = id;
        }

        public object SortGames(string genre)
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
                        CommandText = "SELECT * FROM Games WHERE genre = '" + genre + "' ORDER BY title",
                        CommandType = CommandType.Text,
                        Connection = connection
                    };
                    var reader = cmd.ExecuteReader();
                    Store.GameList.Clear();
                    while (reader.Read())
                    {
                        var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(reader["image"]);
                        Store.GameList.Add(new GameToLoad(reader["title"].ToString(), reader["description"].ToString(), reader["developer"].ToString(), reader["genre"].ToString(), reader["link"].ToString(), (int) reader["Id"], bitmap));
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
        public void AddToLibrary()
        {
            try
            {
                using (var connection = new C.SqlConnection(
                    "Server=tcp:igs.database.windows.net,1433;Database=IGS_database;User ID=Glonfindel;Password=Karol1995!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    ))
                {
                    connection.Open();
                    C.SqlTransaction transaction = connection.BeginTransaction("AddToLib");
                    using (C.SqlCommand cmd = new C.SqlCommand("IF NOT EXISTS (SELECT * FROM UserLibrary WHERE idUser = @userID AND idGame = @gameID) INSERT INTO UserLibrary VALUES (@userID, @gameID)"))
                    {
                        try
                        {

                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@userID", SqlDbType.Int).Value = Login.user.ID;
                            cmd.Parameters.Add("@gameID", SqlDbType.Int).Value = this.ID;
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.GetType().ToString());
                            MessageBox.Show(ex.Message);
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                // This catch block will handle any errors that may have occurred
                                // on the server that would cause the rollback to fail, such as
                                // a closed connection.
                                MessageBox.Show(ex2.GetType().ToString());
                                MessageBox.Show(ex2.Message);
                            }
                        }
                    };
                }
            }
            catch
                (C.SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
