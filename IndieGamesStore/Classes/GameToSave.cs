using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C = System.Data.SqlClient;
using IndieGamesStore.Interfaces;

namespace IndieGamesStore
{
    class GameToSave : Game, IInsert
    {
        public Byte[] Image { get; private set; }

        public GameToSave()
        {
            
        }

        public GameToSave(string title, string desc, string dev, string genre, string link, Byte[] image) : base(title, desc, dev, genre, link)
        {
            this.Image = image;
        }

        public void Insert(string table, object[] args)
        {
            try
            {
                using (var connection = new C.SqlConnection(
                    "Server=tcp:igs.database.windows.net,1433;Database=IGS_database;User ID=Glonfindel;Password=Karol1995!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=3600;"
                    ))
                {
                    connection.Open();
                    using (C.SqlCommand cmd = new C.SqlCommand("INSERT INTO " + table + "(title, description, genre, image, developer, link) VALUES (@title, @description, @genre, @image, @developer, @link)"))
                    {
                        cmd.Connection = connection;
                        cmd.Parameters.Add("@title", SqlDbType.VarChar, 64).Value = args[0];
                        cmd.Parameters.Add("@description", SqlDbType.Text).Value = args[1];
                        cmd.Parameters.Add("@genre", SqlDbType.VarChar, 32).Value = args[2];
                        cmd.Parameters.Add("@image", SqlDbType.Image).Value = args[3];
                        cmd.Parameters.Add("@developer", SqlDbType.VarChar, 64).Value = args[4];
                        cmd.Parameters.Add("@link", SqlDbType.Text).Value = args[5];
                        cmd.ExecuteNonQuery();
                    };
                }
            }
            catch
                (C.SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
